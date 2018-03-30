using System;
using System.Collections.Generic;
using System.IO;
using PlistCS;
using UnityEditor;
using UnityEngine;

namespace PrefsWrapper
{
    public static class PlayerPrefsReader
    {
        public static IDictionary<string, object> Read()
        {
            if (Application.platform == RuntimePlatform.OSXEditor)
                return RetrieveFromMac();
            if (Application.platform == RuntimePlatform.WindowsEditor)
                return RetrieveFromWin();
            else throw new NotSupportedException("PlayerPrefsEditor doesn't support this Unity Editor platform");
        }

        public static DateTime GetLastWriteTime()
        {
            return File.GetLastWriteTime(PlayerPrefsPathForMac());
        }

        static string PlayerPrefsPathForMac()
        {
            // From Unity docs: On Mac OS X PlayerPrefs are stored in ~/Library/Preferences folder,
            // in a file named unity.[company name].[product name].plist,
            // where company and product names are the names set up in Project Settings.
            // The same .plist file is used for both Projects run in the Editor and standalone players.
            // Construct the plist filename from the project's settings
            string plistFilename = string.Format("unity.{0}.{1}.plist", PlayerSettings.companyName, PlayerSettings.productName);
            return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library/Preferences"), plistFilename);
        }

        static IDictionary<string, object> RetrieveFromMac()
        {
            var playerPrefsPath = PlayerPrefsPathForMac();
            if (!File.Exists(playerPrefsPath))
                return new Dictionary<string, object>();

            // Parse the plist then cast it to a Dictionary
            object plist = Plist.readPlist(playerPrefsPath);
            Dictionary<string, object> parsed = plist as Dictionary<string, object>;

            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> pair in parsed)
            {
                // Some float values may come back as double, so convert them back to floats
                if (pair.Value is double)
                    result.Add(pair.Key, PlayerPrefs.GetFloat(pair.Key));
                else
                    result.Add(pair.Key, pair.Value);
            }
            return result;
        }

        static IDictionary<string, object> RetrieveFromWin()
        {
            // From Unity docs: On Windows, PlayerPrefs are stored in the registry under HKCU\Software\[company name]\[product name] key, where company and product names are the names set up in Project Settings.
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\" + PlayerSettings.companyName + "\\" + PlayerSettings.productName);

            // Parse the registry if the specified registryKey exists
            if (registryKey == null)
                return new Dictionary<string, object>();

            // Get an array of what keys (registry value names) are stored
            string[] valueNames = registryKey.GetValueNames();

            var result = new Dictionary<string, object>();
            foreach (string valueName in valueNames)
            {
                string key = valueName;

                // Remove the _h193410979 style suffix used on player pref keys in Windows registry
                int index = key.LastIndexOf("_", StringComparison.Ordinal);
                key = key.Remove(index, key.Length - index);

                object ambiguousValue = registryKey.GetValue(valueName);

                // Unfortunately floats will come back as an int (at least on 64 bit) because the float is stored as
                // 64 bit but marked as 32 bit - which confuses the GetValue() method greatly! 
                if (ambiguousValue is int)
                {
                    // If the player pref is not actually an int then it must be a float, this will evaluate to true
                    // (impossible for it to be 0 and -1 at the same time)
                    if (PlayerPrefs.GetInt(key, -1) == -1 && PlayerPrefs.GetInt(key, 0) == 0) ambiguousValue = PlayerPrefs.GetFloat(key);// Fetch the float value from PlayerPrefs in memory
                }
                else if (ambiguousValue.GetType() == typeof(byte[]))
                {
                    // On Unity 5 a string may be stored as binary, so convert it back to a string
                    // Assign the key and value into the respective record in our output array
                    ambiguousValue = System.Text.Encoding.Default.GetString((byte[])ambiguousValue);
                }
                result.Add(key, ambiguousValue);
            }
            return result;
        }
    }
}