using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsWrapper
{
    public static class PlayerPrefsCache
    {
        static IDictionary<string, object> cachedPlayerPrefs = new SortedDictionary<string, object>();

        public static IDictionary<string, object> Cache { get { return cachedPlayerPrefs; } }

        public static readonly DateTime MISSING_DATETIME = new DateTime(1601, 1, 1);

        // Track last successful deserialisation to prevent doing this too often. On OSX this uses the player prefs file
        // last modified time, on Windows we just poll repeatedly and use this to prevent polling again too soon.
        static DateTime? lastDeserialization = null;

        public static DateTime LastUpdated { get { return lastDeserialization ?? MISSING_DATETIME; } }

        public static void CheckUpdate()
        {
            if (Application.platform == RuntimePlatform.OSXEditor)
                CheckUpdateForMac();
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                CheckUpdateForWin();
        }

        static void CheckUpdateForMac()
        {
            var lastWriteTime = PlayerPrefsReader.GetLastWriteTime();
            if (!lastDeserialization.HasValue || lastDeserialization.Value != lastWriteTime)
            {
                cachedPlayerPrefs = new SortedDictionary<string, object>(PlayerPrefsReader.Read());
                lastDeserialization = lastWriteTime;
            }
        }

        static void CheckUpdateForWin()
        {
            if (!lastDeserialization.HasValue || DateTime.Now - lastDeserialization.Value > TimeSpan.FromMilliseconds(500))
            {
                cachedPlayerPrefs = new SortedDictionary<string, object>(PlayerPrefsReader.Read());
                lastDeserialization = DateTime.Now;
            }
        }
    }
}