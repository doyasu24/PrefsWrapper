using System;
using PrefsWrapper.Encoders;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public static class CryptoPreferenceFactory
    {
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

        private static IPreference<T> CreateCryptoPref<T>(string key, string password, string salt, IEncoder<T> encoder)
        {
            var aesEncoder = new AesEncoder(password, salt);
            var combinedEncoder = new CombinedEncoder<T>(encoder, aesEncoder);
            var encodeSerializer = new EncodeSerializer<T>(new BinaryPrefSerializer(), combinedEncoder);
            return new KeyEncodePreference<T>(key, encodeSerializer, aesEncoder);
        }
    }
}
