using System;
using System.Text;
using UnityEngine;

namespace PrefsWrapper.Encoders
{
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
}