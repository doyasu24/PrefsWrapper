namespace PrefsWrapper.Encoders
{
    public interface IEncoder<T>
    {
        byte[] Encode(T t);
        T Decode(byte[] bytes);
    }
}
