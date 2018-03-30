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