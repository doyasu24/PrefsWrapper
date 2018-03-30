using System;
using System.Text;
using PrefsWrapper.Encoders;
using PrefsWrapper.Serializers;

namespace PrefsWrapper
{
    public class KeyEncodePreference<T> : Preference<T>
    {
        readonly IEncoder<byte[]> keyEncoder;

        public KeyEncodePreference(string key, IPrefSerializer<T> serializer, IEncoder<byte[]> keyEncoder) : base(key, serializer)
        {
            this.keyEncoder = keyEncoder;
        }

        protected override string Key { get { return EncodedKey(OrginalKey); } }

        string EncodedKey(string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var encodedBytes = keyEncoder.Encode(keyBytes);
            return Convert.ToBase64String(encodedBytes);
        }
    }
}