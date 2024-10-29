using System;
using PrefsWrapper.Serializers;
using UnityEngine;

namespace PrefsWrapper
{
    public class Preference<T> : IPreference<T>
    {
        protected virtual string Key => OrginalKey;
        protected readonly string OrginalKey;
        private readonly IPrefSerializer<T> _serializer;

        public Preference(string key, IPrefSerializer<T> serializer)
        {
            OrginalKey = key;
            _serializer = serializer;
        }

        public bool HasValue => PlayerPrefs.HasKey(Key);

        public T Value
        {
            get
            {
                if (HasValue)
                    return _serializer.Deserialize(Key);
                throw new InvalidOperationException();
            }
            set => _serializer.Serialize(Key, value);
        }

        public T GetValueOrDefault()
        {
            return HasValue ? _serializer.Deserialize(Key) : default;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            return HasValue ? _serializer.Deserialize(Key) : defaultValue;
        }

        public void DeleteValue()
        {
            PlayerPrefs.DeleteKey(Key);
        }

        public override string ToString()
        {
            return $"[Preference: Key-{Key}, HasValue-{HasValue}, ValueOrDefault-{GetValueOrDefault()}]";
        }
    }
}
