using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public interface IPrefSerializer<T>
    {
        void Serialize(string key, T value);
        T Deserialize(string key);
    }

    public class StringPrefSerializer : IPrefSerializer<string>
    {
        public string Deserialize(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void Serialize(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }

    public class IntPrefSerializer : IPrefSerializer<int>
    {
        public int Deserialize(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public void Serialize(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }

    public class FloatPrefSerializer : IPrefSerializer<float>
    {
        public float Deserialize(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void Serialize(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
}