using System;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    // save as base64 string
    public class BinaryPrefSerializer : IPrefSerializer<byte[]>
    {
        public byte[] Deserialize(string key)
        {
            var str = PlayerPrefs.GetString(key);
            return Convert.FromBase64String(str);
        }

        public void Serialize(string key, byte[] value)
        {
            var str = Convert.ToBase64String(value);
            PlayerPrefs.SetString(key, str);
        }
    }
}