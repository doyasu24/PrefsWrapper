using PrefsWrapper.Encoders;

namespace PrefsWrapper.Serializers
{
    public class EncodeSerializer<T> : IPrefSerializer<T>
    {
        readonly IPrefSerializer<byte[]> serializer;
        readonly IEncoder<T> encoder;

        public EncodeSerializer(IPrefSerializer<byte[]> serializer, IEncoder<T> encoder)
        {
            this.serializer = serializer;
            this.encoder = encoder;
        }

        public T Deserialize(string key)
        {
            var bytes = serializer.Deserialize(key);
            return encoder.Decode(bytes);
        }

        public void Serialize(string key, T value)
        {
            var bytes = encoder.Encode(value);
            serializer.Serialize(key, bytes);
        }
    }
}