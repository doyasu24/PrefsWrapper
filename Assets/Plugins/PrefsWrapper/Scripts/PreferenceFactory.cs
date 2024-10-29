using System;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public static class PreferenceFactory
    {
        public static IPreference<string> CreateStringPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new StringPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<int> CreateIntPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new IntPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<float> CreateFloatPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new FloatPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<bool> CreateBoolPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new BoolPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<byte> CreateBytePref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new BytePrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<sbyte> CreateSBytePref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new SBytePrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<char> CreateCharPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new CharPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<short> CreateShortPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new ShortPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<ushort> CreateUShortPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new UShortPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<byte[]> CreateBinaryPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new BinaryPrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<DateTime> CreateDateTimePref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new DateTimePrefSerializer(), key, enableMemCachePref);
        }

        public static IPreference<TimeSpan> CreateTimeSpanPref(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new TimeSpanPrefSerializer(), key, enableMemCachePref);
        }

        /// <summary>
        /// Create a preference to serialize Enum as string
        /// </summary>
        public static IPreference<T> CreateEnumPref<T>(string key, bool enableMemCachePref = true) where T : struct
        {
            return CreatePref(new EnumPrefSerializer<T>(), key, enableMemCachePref);
        }

        /// <summary>
        /// Create a preference to serialize using UnityEngine.JsonUtility
        /// </summary>
        public static IPreference<T> CreateJsonPref<T>(string key, bool enableMemCachePref = true)
        {
            return CreatePref(new JsonPrefSerializer<T>(), key, enableMemCachePref);
        }

        private static IPreference<T> CreatePref<T>(IPrefSerializer<T> serializer, string key, bool enableMemCachePref)
        {
            if (enableMemCachePref)
                return new MemCachedPreference<T>(key, serializer);
            return new Preference<T>(key, serializer);
        }
    }
}
