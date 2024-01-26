using NUnit.Framework;
using PrefsWrapper.Serializers;
using Assert = UnityEngine.Assertions.Assert;

namespace PrefsWrapper
{
    public class MemCachedPreferenceTest
    {
        readonly IPreference<int> preference;

        public MemCachedPreferenceTest()
        {
            preference = new Preference<int>("mem-cached-preference", new IntPrefSerializer());
            preference.DeleteValue();
        }

        [Test]
        public void DeletedPrefHasNoValue()
        {
            preference.DeleteValue();
            Assert.IsFalse(preference.HasValue);
        }

        [Test]
        public void DeletedPrefReturnDefault()
        {
            preference.DeleteValue();

            Assert.AreEqual(preference.GetValueOrDefault(), default);
            Assert.AreEqual(preference.GetValueOrDefault(1), 1);
        }

        [Test]
        public void PrefHasValue()
        {
            preference.DeleteValue();

            preference.Value = 1;
            Assert.IsTrue(preference.HasValue);
        }

        [Test]
        public void PrefHasValueReturnValue()
        {
            preference.DeleteValue();

            preference.Value = 1;
            Assert.AreEqual(preference.Value, 1);
            Assert.AreEqual(preference.GetValueOrDefault(), 1);
            Assert.AreEqual(preference.GetValueOrDefault(2), 1);
        }
    }
}