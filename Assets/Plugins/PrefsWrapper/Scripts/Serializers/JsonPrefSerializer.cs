using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public class JsonPrefSerializer<T> : IPrefSerializer<T>
    {
        public T Deserialize(string key)
        {
            var str = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(str);
        }

        public void Serialize(string key, T value)
        {
            var str = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key, str);
        }
    }
}