using System;
using System.Text;
using UnityEngine;

namespace PrefsWrapper.Encoders
{
    public class IntEncoder : IEncoder<int>
    {
        public int Decode(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        public byte[] Encode(int t)
        {
            return BitConverter.GetBytes(t);
        }
    }

    public class FloatEncoder : IEncoder<float>
    {
        public float Decode(byte[] bytes)
        {
            return BitConverter.ToSingle(bytes, 0);
        }

        public byte[] Encode(float t)
        {
            return BitConverter.GetBytes(t);
        }
    }

    public class StringEncoder : IEncoder<string>
    {
        public string Decode(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] Encode(string t)
        {
            return Encoding.UTF8.GetBytes(t);
        }
    }

    public class BoolEncoder : IEncoder<bool>
    {
        public bool Decode(byte[] bytes)
        {
            return BitConverter.ToBoolean(bytes, 0);
        }

        public byte[] Encode(bool t)
        {
            return BitConverter.GetBytes(t);
        }
    }

    public class ByteEncoder : IEncoder<byte>
    {
        public byte Decode(byte[] bytes)
        {
            return bytes[0];
        }

        public byte[] Encode(byte t)
        {
            return new[] { t };
        }
    }

    public class SByteEncoder : IEncoder<sbyte>
    {
        // sbyte [-128, 127] <--- ±128 ---> byte [0, 255]
        public sbyte Decode(byte[] bytes)
        {
            var byteValue = bytes[0];
            return (sbyte)(byteValue - 128);
        }

        public byte[] Encode(sbyte t)
        {
            var byteValue = (byte)(t + 128);
            return new[] { byteValue };
        }
    }

    public class CharEncoder : IEncoder<char>
    {
        public char Decode(byte[] bytes)
        {
            return BitConverter.ToChar(bytes, 0);
        }

        public byte[] Encode(char t)
        {
            return BitConverter.GetBytes(t);
        }
    }

    public class ShortEncoder : IEncoder<short>
    {
        public short Decode(byte[] bytes)
        {
            return BitConverter.ToInt16(bytes, 0);
        }

        public byte[] Encode(short t)
        {
            return BitConverter.GetBytes(t);
        }
    }

    public class UShortEncoder : IEncoder<ushort>
    {
        public ushort Decode(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0);
        }

        public byte[] Encode(ushort t)
        {
            return BitConverter.GetBytes(t);
        }
    }

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

    // encode as string
    public class EnumEncoder<T> : IEncoder<T>
        where T : struct
    {
        public T Decode(byte[] bytes)
        {
            var str = Encoding.UTF8.GetString(bytes);
            return (T)Enum.Parse(typeof(T), str);
        }

        public byte[] Encode(T t)
        {
            return Encoding.UTF8.GetBytes(t.ToString());
        }
    }

    public class JsonEncoder<T> : IEncoder<T>
    {
        public T Decode(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            return JsonUtility.FromJson<T>(json);
        }

        public byte[] Encode(T t)
        {
            var json = JsonUtility.ToJson(t);
            return Encoding.UTF8.GetBytes(json);
        }
    }

    public class NopEncoder : IEncoder<byte[]>
    {
        public byte[] Decode(byte[] bytes)
        {
            return bytes;
        }

        public byte[] Encode(byte[] t)
        {
            return t;
        }
    }
}
