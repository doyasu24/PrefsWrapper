using PrefsWrapper.Encoders;

namespace PrefsWrapper.Serializers
{
    public class EncodeSerializer<T> : IPrefSerializer<T>
    {
        private readonly IPrefSerializer<byte[]> _serializer;
        private readonly IEncoder<T> _encoder;

        public EncodeSerializer(IPrefSerializer<byte[]> serializer, IEncoder<T> encoder)
        {
            _serializer = serializer;
            _encoder = encoder;
        }

        public T Deserialize(string key)
        {
            var bytes = _serializer.Deserialize(key);
            return _encoder.Decode(bytes);
        }

        public void Serialize(string key, T value)
        {
            var bytes = _encoder.Encode(value);
            _serializer.Serialize(key, bytes);
        }
    }
}
