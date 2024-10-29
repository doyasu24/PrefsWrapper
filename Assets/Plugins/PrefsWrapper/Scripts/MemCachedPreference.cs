using UnityEngine;
using System;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class MemCachedPreference<T> : IPreference<T>
    {
        private readonly string _key;
        private readonly IPrefSerializer<T> _serializer;

        private bool _loaded; // _cache, _hasValueがロードされたかどうか
        private T _cache;
        private bool _hasValue;

        public MemCachedPreference(string key, IPrefSerializer<T> serializer)
        {
            _key = key;
            _serializer = serializer;
        }

        public bool HasValue
        {
            get
            {
                TryLoadOnce();
                return _hasValue;
            }
        }

        public T Value
        {
            get
            {
                TryLoadOnce();
                if (_hasValue) return _cache;
                throw new InvalidOperationException();
            }
            set
            {
                _serializer.Serialize(_key, value);
                _hasValue = true;
                _cache = value;
                _loaded = true;
            }
        }

        public T GetValueOrDefault()
        {
            TryLoadOnce();
            return _hasValue ? _cache : default;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            TryLoadOnce();
            return _hasValue ? _cache : defaultValue;
        }

        public void DeleteValue()
        {
            PlayerPrefs.DeleteKey(_key);
            _cache = default;
            _hasValue = false;
            _loaded = true;
        }

        private void TryLoadOnce()
        {
            if (_loaded) return;
            _hasValue = PlayerPrefs.HasKey(_key);
            if (_hasValue)
            {
                _cache = _serializer.Deserialize(_key);
            }

            _loaded = true;
        }

        public override string ToString()
        {
            return $"[MemCachedPreference: key-{_key}, HasValue-{HasValue}, ValueOrDefault-{GetValueOrDefault()}]";
        }
    }
}
