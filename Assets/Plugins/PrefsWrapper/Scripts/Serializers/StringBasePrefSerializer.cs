using System;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public abstract class StringBasePrefSerializer<T> : IPrefSerializer<T>
    {
        protected abstract T ConvertToValue(string value);
        protected abstract string ConvertToString(T value);

        public T Deserialize(string key)
        {
            var strValue = PlayerPrefs.GetString(key);
            return ConvertToValue(strValue);
        }

        public void Serialize(string key, T value)
        {
            var strValue = ConvertToString(value);
            PlayerPrefs.SetString(key, strValue);
        }
    }

    /**
     * Serialize byte[] as string using Convert.ToBase64String and Convert.FromBase64String
     */
    public class BinaryPrefSerializer : StringBasePrefSerializer<byte[]>
    {
        protected override byte[] ConvertToValue(string value)
        {
            return Convert.FromBase64String(value);
        }

        protected override string ConvertToString(byte[] value)
        {
            return Convert.ToBase64String(value);
        }
    }

    /**
     * Serialize DateTime as string using DateTime.ToBinary and DateTime.FromBinary
     */
    public class DateTimePrefSerializer : StringBasePrefSerializer<DateTime>
    {
        protected override DateTime ConvertToValue(string value)
        {
            long date;
            if (long.TryParse(value, out date))
                return DateTime.FromBinary(date);
            else
                throw new SerializerException("invalid DateTime format: " + value);
        }

        protected override string ConvertToString(DateTime value)
        {
            return value.ToBinary().ToString();
        }
    }

    /**
     * Serialize TimeSpan as string using TimeSpan.Ticks
     */
    public class TimeSpanPrefSerializer : StringBasePrefSerializer<TimeSpan>
    {
        protected override TimeSpan ConvertToValue(string value)
        {
            long ticks;
            if (long.TryParse(value, out ticks))
                return TimeSpan.FromTicks(ticks);
            else
                throw new SerializerException("invalid TimeSpan format: " + value);
        }

        protected override string ConvertToString(TimeSpan value)
        {
            return value.Ticks.ToString();
        }
    }

    /**
     * Serialize Enum as string
     */
    public class EnumPrefSerializer<T> : StringBasePrefSerializer<T>
        where T : struct
    {
        protected override T ConvertToValue(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        protected override string ConvertToString(T value)
        {
            return value.ToString();
        }
    }

    /**
     * Serialize Json as string using JsonUtility
     */
    public class JsonPrefSerializer<T> : StringBasePrefSerializer<T>
    {
        protected override T ConvertToValue(string value)
        {
            return JsonUtility.FromJson<T>(value);
        }

        protected override string ConvertToString(T value)
        {
            return JsonUtility.ToJson(value);
        }
    }
}
