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

You can add https://github.com/doyasu24/PrefsWrapper.git?path=Assets/Plugins/PrefsWrapper#0.2.0 to Package Manager

or import unitypackage from release page.

see `Assets/Examples/Sample.unity` scene.

# Sample

```Sample.cs
using UnityEngine;

namespace PrefsWrapper
{
    public class Sample : MonoBehaviour
    {
        void Start()
        {
            // preference
            IPreference<Vector3> pref = PreferenceFactory.CreateJsonPref<Vector3>(
                key: "vector3-test"
            );

            // set value
            pref.Value = Vector3.up;

            // Value: (0.0, 1.0, 0.0)
            Debug.Log("Value: " + pref.Value);

            // delete
            pref.DeleteValue();

            // GetValueOrDefault: (0.0, -1.0, 0.0)
            Debug.Log("GetValueOrDefault: " + pref.GetValueOrDefault(Vector3.down));

            // AES encoding preference
            IPreference<Vector3> cryptoPref = PreferenceFactory.CreateJsonCryptoPref<Vector3>(
                key: "vector3-crypto-test",
                password: "password",
                salt: "salt1234567890"
            );

            cryptoPref.Value = Vector3.up;

            // return true
            Debug.Log("CryptoHasValue: " + cryptoPref.HasValue);

            // key and value are encrypted
            // return false
            Debug.Log("PlayerPrefs.HasValue: " + PlayerPrefs.HasKey("vector3-crypto-test"));
        }
    }
}
```

# How to customize

see `PrefsWrapper.PreferenceFactory`

implement `IPrefsSerializer<T>` and `IEncoder<T>`

# License

MIT License
