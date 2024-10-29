using System;
using System.Text;
using PrefsWrapper.Encoders;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class KeyEncodePreference<T> : Preference<T>
    {
        readonly IEncoder<byte[]> _keyEncoder;

        public KeyEncodePreference(string key, IPrefSerializer<T> serializer, IEncoder<byte[]> keyEncoder) : base(key,
            serializer)
        {
            _keyEncoder = keyEncoder;
        }

        protected override string Key => EncodedKey(OrginalKey);

        private string EncodedKey(string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var encodedBytes = _keyEncoder.Encode(keyBytes);
            return Convert.ToBase64String(encodedBytes);
        }
    }
}
