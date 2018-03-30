using System;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    // save as DateTime.ToBinary string
    public class DateTimePrefSerializer : IPrefSerializer<DateTime>
    {
        public DateTime Deserialize(string key)
        {
            var str = PlayerPrefs.GetString(key);
            long date;
            if (long.TryParse(str, out date))
                return DateTime.FromBinary(date);
            else
                throw new SerializerException("invalid DateTime format: " + str);
        }

        public void Serialize(string key, DateTime value)
        {
            var date = value.ToBinary();
            PlayerPrefs.SetString(key, date.ToString());
        }
    }
}