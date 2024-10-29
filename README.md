# PrefsWrapper

Extensible PlayerPrefs wrapper.

AES encryption support

## Support types
- int
- bool
- byte
- sbyte
- char
- short
- ushort
- float
- string
- byte[]
- DateTime
- TimeSpan
- Enum
- Json (UnityEngine.JsonUtility)
  - Vector2, 3, 4
  - Quaternion
  - Color
  - ...
  - struct in UnityEngine

# Install

You can add https://github.com/doyasu24/PrefsWrapper.git?path=Assets/Plugins/PrefsWrapper#1.0.0 to Package Manager.

see `Assets/Examples/Sample.unity` scene.

# Sample

```Sample.cs
using UnityEngine;

namespace PrefsWrapper.Examples
{
    public class Sample : MonoBehaviour
    {
        // memory cache preference
        private readonly IPreference<string> _stringTestPref = PreferenceFactory.CreateStringPref("string-test-key");

        // non-memory cache preference
        private readonly IPreference<int> _intTestPref = PreferenceFactory.CreateIntPref("int-test-key", enableMemCachePref: false);

        // AES encoding preference
        private readonly IPreference<Vector3> _cryptoPref = PreferenceFactory.CreateJsonCryptoPref<Vector3>(
            key: "vector3-crypto-test",
            password: "password",
            salt: "salt1234567890"
        );

        private void Start()
        {
            // call PlayerPrefs.HasKey internally
            Debug.Log($"HasValue: {_stringTestPref.HasValue}");

            // call PlayerPrefs.GetString internally
            Debug.Log($"GetValueOrDefault: {_stringTestPref.GetValueOrDefault("default value")}");
            
            // call PlayerPrefs.DeleteKey internally
            _stringTestPref.DeleteValue();

            // call PlayerPrefs.SetString internally and set value to memory cache
            _stringTestPref.Value = "test";
            
            // get value from memory cache
            Debug.Log($"Value: {_stringTestPref.Value}");
            

            _cryptoPref.Value = Vector3.up;
            Debug.Log($"CryptoPref Decode: {_cryptoPref.Value}");

            // key and value are encrypted
            // return false
            Debug.Log($"PlayerPrefs.HasKey(\"vector3-crypto-test\"): {PlayerPrefs.HasKey("vector3-crypto-test")}");
        }
    }
}
```

# How to customize

see `PrefsWrapper.PreferenceFactory`

implement `IPrefsSerializer<T>` and `IEncoder<T>`

# License

MIT License
