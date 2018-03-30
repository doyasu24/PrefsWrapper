using System;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    // save as string
    public class EnumPrefSerializer<T> : IPrefSerializer<T>
        where T : struct
    {
        public T Deserialize(string key)
        {
            var str = PlayerPrefs.GetString(key);
            return (T)Enum.Parse(typeof(T), str);
        }

        public void Serialize(string key, T value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
    }
}