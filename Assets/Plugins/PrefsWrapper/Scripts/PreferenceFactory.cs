using PrefsWrapper.Serializers;
using PrefsWrapper.Encoders;
using System;

namespace PrefsWrapper
{
    public static class PreferenceFactory
    {
        #region Pref

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

        static IPreference<T> CreatePref<T>(IPrefSerializer<T> serializer, string key, bool enableMemCachePref)
        {
            if (enableMemCachePref)
                return new MemCachedPreference<T>(key, serializer);
            else
                return new Preference<T>(key, serializer);
        }

        #endregion

        #region CryptoPref

        public static IPreference<string> CreateStringCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new StringEncoder());
        }

        public static IPreference<int> CreateIntCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new IntEncoder());
        }

        public static IPreference<float> CreateFloatCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new FloatEncoder());
        }

        public static IPreference<bool> CreateBoolCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new BoolEncoder());
        }

        public static IPreference<byte> CreateByteCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new ByteEncoder());
        }

        public static IPreference<sbyte> CreateSByteCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new SByteEncoder());
        }

        public static IPreference<char> CreateCharCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new CharEncoder());
        }

        public static IPreference<short> CreateShortCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new ShortEncoder());
        }

        public static IPreference<ushort> CreateUShortCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new UShortEncoder());
        }

        public static IPreference<byte[]> CreateBinaryCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new NopEncoder());
        }

        public static IPreference<DateTime> CreateDateTimeCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new DateTimeEncoder());
        }

        public static IPreference<TimeSpan> CreateTimeSpanCryptoPref(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new TimeSpanEncoder());
        }

        /// <summary>
        /// Create a preference to serialize Enum as string
        /// </summary>
        public static IPreference<T> CreateEnumCryptoPref<T>(string key, string password, string salt) where T : struct
        {
            return CreateCryptoPref(key, password, salt, new EnumEncoder<T>());
        }

        /// <summary>
        /// Create a AES encoding preference to serialize using UnityEngine.JsonUtility
        /// </summary>
        public static IPreference<T> CreateJsonCryptoPref<T>(string key, string password, string salt)
        {
            return CreateCryptoPref(key, password, salt, new JsonEncoder<T>());
        }

        static IPreference<T> CreateCryptoPref<T>(string key, string password, string salt, IEncoder<T> encoder)
        {
            var aesEncoder = new AesEncoder(password, salt);
            var combinedEncoder = new CombinedEncoder<T>(encoder, aesEncoder);
            var encodeSerializer = new EncodeSerializer<T>(new BinaryPrefSerializer(), combinedEncoder);
            return new KeyEncodePreference<T>(key, encodeSerializer, aesEncoder);
        }

        #endregion
    }
}
