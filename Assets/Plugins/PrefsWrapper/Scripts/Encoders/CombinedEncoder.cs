namespace PrefsWrapper.Encoders
{
    public class CombinedEncoder<T> : IEncoder<T>
    {
        readonly IEncoder<T> encoder;
        readonly IEncoder<byte[]> bytesEncoder;

        public CombinedEncoder(IEncoder<T> encoder, IEncoder<byte[]> bytesEncoder)
        {
            this.encoder = encoder;
            this.bytesEncoder = bytesEncoder;
        }

        public T Decode(byte[] bytes)
        {
            var decodedBytes = bytesEncoder.Decode(bytes);
            return encoder.Decode(decodedBytes);
        }

        public byte[] Encode(T t)
        {
            var bytes = encoder.Encode(t);
            return bytesEncoder.Encode(bytes);
        }
    }
}