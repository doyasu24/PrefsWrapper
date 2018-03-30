using System;

namespace PrefsWrapper.Encoders
{
    public class TimeSpanEncoder : IEncoder<TimeSpan>
    {
        public TimeSpan Decode(byte[] bytes)
        {
            var ticks = BitConverter.ToInt64(bytes, 0);
            return TimeSpan.FromTicks(ticks);
        }

        public byte[] Encode(TimeSpan t)
        {
            return BitConverter.GetBytes(t.Ticks);
        }
    }
}