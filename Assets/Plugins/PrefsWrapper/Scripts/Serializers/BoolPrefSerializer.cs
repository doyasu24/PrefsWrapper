using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public class BoolPrefSerializer : IPrefSerializer<bool>
    {
        // true: 1
        // false: 0
        public bool Deserialize(string key)
        {
            var value = PlayerPrefs.GetInt(key);
            if (value == 1)
                return true;
            if (value == 0)
                return false;
            throw new SerializerException("invalid bool format: " + value);
        }

        public void Serialize(string key, bool value)
        {
            var intValue = 0;
            if (value)
                intValue = 1;
            PlayerPrefs.SetInt(key, intValue);
        }
    }
}