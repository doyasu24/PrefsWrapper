using System;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public class TimeSpanPrefSerializer : IPrefSerializer<TimeSpan>
    {
        public TimeSpan Deserialize(string key)
        {
            var str = PlayerPrefs.GetString(key);
            long ticks;
            if (long.TryParse(str, out ticks))
                return TimeSpan.FromTicks(ticks);
            else
                throw new SerializerException("invalid TimeSpan format: " + str);
        }

        public void Serialize(string key, TimeSpan value)
        {
            PlayerPrefs.SetString(key, value.Ticks.ToString());
        }
    }
}