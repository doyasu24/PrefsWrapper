using UnityEngine;
using System;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class MemCachedPreference<T> : IPreference<T>
    {
        private readonly string _key;
        private readonly IPrefSerializer<T> _serializer;
        private T _cache;
        private bool _hasValue;

        public MemCachedPreference(string key, IPrefSerializer<T> serializer, T initialDefaultValue = default)
        {
            _key = key;
            _serializer = serializer;
            _hasValue = PlayerPrefs.HasKey(key);
            _cache = HasValue ? serializer.Deserialize(key) : initialDefaultValue;
        }

        public bool HasValue => _hasValue;

        public T Value
        {
            get
            {
                if (HasValue)
                    return _cache;
                throw new InvalidOperationException();
            }
            set
            {
                _serializer.Serialize(_key, value);
                _hasValue = true;
                _cache = value;
            }
        }

        public T GetValueOrDefault()
        {
            return _cache;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            return HasValue ? _cache : defaultValue;
        }

        public void DeleteValue()
        {
            PlayerPrefs.DeleteKey(_key);
            _cache = default;
            _hasValue = false;
        }

        public override string ToString()
        {
            return $"[MemCachedPreference: key-{_key}, HasValue-{HasValue}, ValueOrDefault-{GetValueOrDefault()}]";
        }
    }
}