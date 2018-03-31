# PrefsWrapper

Extensible PlayerPrefs wrapper.

AES encryption support 

PlayerPrefs Editor (EditorWindow) can list up and delete PlayerPrefs.

## Support types
- int
- float
- string
- bool
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

# How to Use

import PrefsWrapper unitypackage from release page.

see `Assets/Plugins/PrefsWrapper/Examples/Sample.unity` scene.

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


# Contribute

## Test

Open `RuntimeUnitTestToolkit/UnitTest.unity` scene and `Run All Tests`

[RuntimeUnitTestToolkit](https://github.com/neuecc/RuntimeUnitTestToolkit)

# License

MIT License

EditorWindow is using [sabresaurus/PlayerPrefsEditor](https://github.com/sabresaurus/PlayerPrefsEditor) code with some modified.

plist deserializer is using [animetrics/PlistCS](https://github.com/animetrics/PlistCS) code.
