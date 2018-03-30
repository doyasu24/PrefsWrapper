using UnityEngine;
using System;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class MemCachedPreference<T> : IPreference<T>
    {
        readonly string key;
        readonly IPrefSerializer<T> serializer;
        T cache;
        bool hasValue;

        public MemCachedPreference(string key, IPrefSerializer<T> serializer, T initialDefaultValue = default(T))
        {
            this.key = key;
            this.serializer = serializer;
            hasValue = PlayerPrefs.HasKey(key);
            if (HasValue)
                cache = serializer.Deserialize(key);
            else
                cache = initialDefaultValue;
        }

        public bool HasValue { get { return hasValue; } }

        public T Value
        {
            get
            {
                if (HasValue)
                    return cache;
                else
                    throw new InvalidOperationException();
            }
            set
            {
                serializer.Serialize(key, value);
                hasValue = true;
                cache = value;
            }
        }

        public T GetValueOrDefault()
        {
            return cache;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (HasValue)
                return cache;
            else
                return defaultValue;
        }

        public void DeleteValue()
        {
            PlayerPrefs.DeleteKey(key);
            cache = default(T);
            hasValue = false;
        }

        public override string ToString()
        {
            return string.Format("[MemCachedPreference: key-{0}, HasValue-{1}, ValueOrDefault-{2}]", key, HasValue, GetValueOrDefault());
        }
    }
}