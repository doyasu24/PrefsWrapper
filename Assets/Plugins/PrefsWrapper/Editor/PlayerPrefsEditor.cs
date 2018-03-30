using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace PrefsWrapper
{
    public class PlayerPrefsEditor : EditorWindow
    {
        // When a search is in effect the search results are cached in this list
        readonly IDictionary<string, object> searchedPlayerPrefs = new SortedDictionary<string, object>();

        // The view position of the player prefs scroll view
        Vector2 scrollPosition;

        // The scroll position from last frame (used with scrollPosition to detect user scrolling)
        Vector2 lastScrollPosition;

        // Prevent OnInspector() forcing a repaint every time it's called
        int inspectorUpdateFrame = 0;

        // Filter the keys by search
        string searchFilter = string.Empty;

        // Because of some issues with deleting from OnGUI, we defer it to OnInspectorUpdate() instead
        string keyQueuedForDeletion = null;

        [MenuItem("Window/PlayerPrefs Editor")]
        private static void Init()
        {
            var editor = EditorWindow.GetWindow<PlayerPrefsEditor>();

            Vector2 minSize = editor.minSize;
            minSize.x = Mathf.Max(400, minSize.x);
            editor.minSize = minSize;
        }

        private void UpdateSearch()
        {
            searchedPlayerPrefs.Clear();
            if (string.IsNullOrEmpty(searchFilter)) return;

            foreach (var p in PlayerPrefsCache.Cache)
            {
                if (p.Key.ToLower().Contains(searchFilter.ToLower()))
                    searchedPlayerPrefs.Add(p.Key, p.Value);
            }
        }

        private void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Search", GUILayout.MaxWidth(50));
            string newSearchFilter = EditorGUILayout.TextField(searchFilter);
            if (newSearchFilter != searchFilter)
            {
                searchFilter = newSearchFilter;
                UpdateSearch();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawMainList()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Key", EditorStyles.boldLabel);
            GUILayout.Label("Value", EditorStyles.boldLabel);
            GUILayout.Label("Type", EditorStyles.boldLabel, GUILayout.Width(37));
            GUILayout.Label("Del", EditorStyles.boldLabel, GUILayout.Width(25));
            EditorGUILayout.EndHorizontal();

            var activePlayerPrefs = PlayerPrefsCache.Cache;
            if (!string.IsNullOrEmpty(searchFilter)) activePlayerPrefs = searchedPlayerPrefs;

            int entryCount = activePlayerPrefs.Count;

            lastScrollPosition = scrollPosition;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            if (scrollPosition.y < 0) scrollPosition.y = 0;

            // The following code has been optimised so that rather than attempting to draw UI for every single PlayerPref
            // it instead only draws the UI for those currently visible in the scroll view and pads above and below those
            // results to maintain the right size using GUILayout.Space(). This enables us to work with thousands of 
            // PlayerPrefs without slowing the interface to a halt.

            // Fixed height of one of the rows in the table
            float rowHeight = 18;

            // Determine how many rows are visible on screen. For simplicity, use Screen.height (the overhead is negligible)
            int visibleCount = Mathf.CeilToInt(Screen.height / rowHeight);

            // Determine the index of the first player pref that should be drawn as visible in the scrollable area
            int firstShownIndex = Mathf.FloorToInt(scrollPosition.y / rowHeight);

            // Determine the bottom limit of the visible player prefs (last shown index + 1)
            int shownIndexLimit = firstShownIndex + visibleCount;

            // If the actual number of player prefs is smaller than the caculated limit, reduce the limit to match
            if (entryCount < shownIndexLimit)
                shownIndexLimit = entryCount;

            // If the number of displayed player prefs is smaller than the number we can display (like we're at the end
            // of the list) then move the starting index back to adjust
            if (shownIndexLimit - firstShownIndex < visibleCount)
                firstShownIndex -= visibleCount - (shownIndexLimit - firstShownIndex);

            // Can't have a negative index of a first shown player pref, so clamp to 0
            if (firstShownIndex < 0) firstShownIndex = 0;

            // Pad above the on screen results so that we're not wasting draw calls on invisible UI and the drawn player
            // prefs end up in the same place in the list
            GUILayout.Space(firstShownIndex * rowHeight);

            foreach (var p in activePlayerPrefs.Skip(firstShownIndex).Take(visibleCount))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(p.Key);
                EditorGUILayout.LabelField(p.Value.ToString());

                Type valueType = p.Value.GetType();
                if (valueType == typeof(float))
                    GUILayout.Label("float", GUILayout.Width(37));
                else if (valueType == typeof(int))
                    GUILayout.Label("int", GUILayout.Width(37));
                else if (valueType == typeof(string))
                    GUILayout.Label("string", GUILayout.Width(37));

                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    PlayerPrefs.DeleteKey(p.Key);
                    PlayerPrefs.Save();
                    DeleteCachedRecord(p.Key);
                }
                EditorGUILayout.EndHorizontal();
            }

            // Calculate the padding at the bottom of the scroll view (because only visible player pref rows are drawn)
            float bottomPadding = (entryCount - shownIndexLimit) * rowHeight;

            // If the padding is positive, pad the bottom so that the layout and scroll view size is correct still
            if (bottomPadding > 0) GUILayout.Space(bottomPadding);

            EditorGUILayout.EndScrollView();

            GUILayout.Label("Entry Count: " + entryCount);
        }

        private void DrawBottomMenu()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete All Preferences"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                PlayerPrefsCache.Cache.Clear();
                searchedPlayerPrefs.Clear();
            }
            GUILayout.Space(15);

            if (GUILayout.Button("Force Save"))
                PlayerPrefs.Save();
            EditorGUILayout.EndHorizontal();
        }

        private void OnGUI()
        {
            PlayerPrefsCache.CheckUpdate();
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                if (PlayerPrefsCache.LastUpdated != PlayerPrefsCache.MISSING_DATETIME)
                    GUILayout.Label("PList Last Written: " + PlayerPrefsCache.LastUpdated);
                else
                    GUILayout.Label("PList Does Not Exist");
            }

            EditorGUILayout.Space();
            DrawSearchBar();
            DrawMainList();
            DrawBottomMenu();

            // If the user has scrolled, deselect - this is because control IDs within carousel will change when scrolled
            // so we'd end up with the wrong box selected.
            if (scrollPosition != lastScrollPosition) GUI.FocusControl("");// Deselect
        }

        private void DeleteCachedRecord(string key)
        {
            keyQueuedForDeletion = key;
        }

        // called by Unity at 10 times a second
        private void OnInspectorUpdate()
        {
            // If a player pref has been specified for deletion
            if (!string.IsNullOrEmpty(keyQueuedForDeletion))
            {
                PlayerPrefsCache.Cache.Remove(keyQueuedForDeletion);
                searchedPlayerPrefs.Remove(keyQueuedForDeletion);

                // Remove the queued key since we've just deleted it
                keyQueuedForDeletion = null;

                // Update the search results and repaint the window
                UpdateSearch();
                Repaint();
            }

            else if (inspectorUpdateFrame % 10 == 0)
                Repaint();// Force the window to repaint
            inspectorUpdateFrame++;
        }
    }
}