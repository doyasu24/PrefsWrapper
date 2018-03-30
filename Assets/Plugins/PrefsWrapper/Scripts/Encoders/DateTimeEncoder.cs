using System;

namespace PrefsWrapper.Encoders
{
    public class DateTimeEncoder : IEncoder<DateTime>
    {
        public DateTime Decode(byte[] bytes)
        {
            var date = BitConverter.ToInt64(bytes, 0);
            return DateTime.FromBinary(date);
        }

        public byte[] Encode(DateTime t)
        {
            var date = t.ToBinary();
            return BitConverter.GetBytes(date);
        }
    }
}