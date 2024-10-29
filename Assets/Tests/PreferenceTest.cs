using NUnit.Framework;
using PrefsWrapper.Serializers;
using Assert = UnityEngine.Assertions.Assert;

namespace PrefsWrapper
{
    public class PreferenceTest
    {
        private readonly IPreference<int> _preference;

        public PreferenceTest()
        {
            _preference = new Preference<int>("preference-test", new IntPrefSerializer());
            _preference.DeleteValue();
        }

        [Test]
        public void DeletedPrefHasNoValue()
        {
            _preference.DeleteValue();
            Assert.IsFalse(_preference.HasValue);
        }

        [Test]
        public void DeletedPrefReturnDefault()
        {
            _preference.DeleteValue();

            Assert.AreEqual(_preference.GetValueOrDefault(), default);
            Assert.AreEqual(_preference.GetValueOrDefault(1), 1);
        }

        [Test]
        public void PrefHasValue()
        {
            _preference.DeleteValue();

            _preference.Value = 1;
            Assert.IsTrue(_preference.HasValue);
        }

        [Test]
        public void PrefHasValueReturnValue()
        {
            _preference.DeleteValue();

            _preference.Value = 1;
            Assert.AreEqual(_preference.Value, 1);
            Assert.AreEqual(_preference.GetValueOrDefault(), 1);
            Assert.AreEqual(_preference.GetValueOrDefault(2), 1);
        }
    }
}
