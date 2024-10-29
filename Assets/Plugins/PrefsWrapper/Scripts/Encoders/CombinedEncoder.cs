namespace PrefsWrapper.Encoders
{
    public class CombinedEncoder<T> : IEncoder<T>
    {
        private readonly IEncoder<T> _encoder;
        private readonly IEncoder<byte[]> _bytesEncoder;

        public CombinedEncoder(IEncoder<T> encoder, IEncoder<byte[]> bytesEncoder)
        {
            _encoder = encoder;
            _bytesEncoder = bytesEncoder;
        }

        public T Decode(byte[] bytes)
        {
            var decodedBytes = _bytesEncoder.Decode(bytes);
            return _encoder.Decode(decodedBytes);
        }

        public byte[] Encode(T t)
        {
            var bytes = _encoder.Encode(t);
            return _bytesEncoder.Encode(bytes);
        }
    }
}