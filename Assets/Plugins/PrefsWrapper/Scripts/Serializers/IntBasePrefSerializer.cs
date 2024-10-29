using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public abstract class IntBasePrefSerializer<T> : IPrefSerializer<T>
    {
        protected abstract T ConvertToValue(int value);
        protected abstract int ConvertToInt(T value);

        public T Deserialize(string key)
        {
            var intValue = PlayerPrefs.GetInt(key);
            return ConvertToValue(intValue);
        }

        public void Serialize(string key, T value)
        {
            var intValue = ConvertToInt(value);
            PlayerPrefs.SetInt(key, intValue);
        }
    }

    /**
     * Serialize bool as int
     * true: 1
     * false: 0
     */
    public class BoolPrefSerializer : IntBasePrefSerializer<bool>
    {
        protected override bool ConvertToValue(int value)
        {
            if (value < 0 || value > 1)
                throw new SerializerException("invalid bool format: " + value);
            return value == 1;
        }

        protected override int ConvertToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }

    /**
     * Serialize byte as int
     */
    public class BytePrefSerializer : IntBasePrefSerializer<byte>
    {
        protected override byte ConvertToValue(int value)
        {
            if (value < byte.MinValue || value > byte.MaxValue)
                throw new SerializerException("invalid byte format: " + value);
            return (byte)value;
        }

        protected override int ConvertToInt(byte value)
        {
            return value;
        }
    }

    /**
     * Serialize sbyte as int
     */
    public class SBytePrefSerializer : IntBasePrefSerializer<sbyte>
    {
        protected override sbyte ConvertToValue(int value)
        {
            if (value < sbyte.MinValue || value > sbyte.MaxValue)
                throw new SerializerException("invalid sbyte format: " + value);
            return (sbyte)value;
        }

        protected override int ConvertToInt(sbyte value)
        {
            return value;
        }
    }

    /**
     * Serialize char as int
     */
    public class CharPrefSerializer : IntBasePrefSerializer<char>
    {
        protected override char ConvertToValue(int value)
        {
            if (value < char.MinValue || value > char.MaxValue)
                throw new SerializerException("invalid char format: " + value);
            return (char)value;
        }

        protected override int ConvertToInt(char value)
        {
            return value;
        }
    }

    /**
     * Serialize short as int
     */
    public class ShortPrefSerializer : IntBasePrefSerializer<short>
    {
        protected override short ConvertToValue(int value)
        {
            if (value < short.MinValue || value > short.MaxValue)
                throw new SerializerException("invalid short format: " + value);
            return (short)value;
        }

        protected override int ConvertToInt(short value)
        {
            return value;
        }
    }

    /**
     * Serialize ushort as int
     */
    public class UShortPrefSerializer : IntBasePrefSerializer<ushort>
    {
        protected override ushort ConvertToValue(int value)
        {
            if (value < ushort.MinValue || value > ushort.MaxValue)
                throw new SerializerException("invalid ushort format: " + value);
            return (ushort)value;
        }

        protected override int ConvertToInt(ushort value)
        {
            return value;
        }
    }
}
