using UnityEngine;
using System;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class Preference<T> : IPreference<T>
    {
        protected virtual string Key { get { return OrginalKey; } }
        protected readonly string OrginalKey;
        readonly IPrefSerializer<T> serializer;

        public Preference(string key, IPrefSerializer<T> serializer)
        {
            this.OrginalKey = key;
            this.serializer = serializer;
        }

        public bool HasValue { get { return PlayerPrefs.HasKey(Key); } }

        public T Value
        {
            get
            {
                if (HasValue)
                    return serializer.Deserialize(Key);
                else
                    throw new InvalidOperationException();
            }
            set { serializer.Serialize(Key, value); }
        }

        public T GetValueOrDefault()
        {
            if (HasValue)
                return serializer.Deserialize(Key);
            else
                return default(T);
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (HasValue)
                return serializer.Deserialize(Key);
            else
                return defaultValue;
        }

        public void DeleteValue()
        {
            PlayerPrefs.DeleteKey(Key);
        }

        public override string ToString()
        {
            return string.Format("[Preference: Key-{0}, HasValue-{1}, ValueOrDefault-{2}]", Key, HasValue, GetValueOrDefault());
        }
    }
}