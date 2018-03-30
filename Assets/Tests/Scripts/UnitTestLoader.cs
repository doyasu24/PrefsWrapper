using PrefsWrapper.Encoders;
using PrefsWrapper.Serializers;
using RuntimeUnitTestToolkit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsWrapper
{
    public static class UnitTestLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if (SceneManager.GetActiveScene().name != "UnitTest")
                return;

            UnitTest.RegisterAllMethods<EncoderTest>();
            UnitTest.RegisterAllMethods<SerializerTest>();
            UnitTest.RegisterAllMethods<PreferenceTest>();
            UnitTest.RegisterAllMethods<MemCachedPreferenceTest>();
            UnitTest.RegisterAllMethods<KeyEncodePreferenceTest>();
        }
    }
}