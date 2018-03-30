using PrefsWrapper.Serializers;
using RuntimeUnitTestToolkit;

namespace PrefsWrapper
{
    public class PreferenceTest
    {
        readonly IPreference<int> preference;

        public PreferenceTest()
        {
            preference = new Preference<int>("preference-test", new IntPrefSerializer());
            preference.DeleteValue();
        }

        public void DeletedPrefHasNoValue()
        {
            preference.DeleteValue();
            preference.HasValue.IsFalse();
        }

        public void DeletedPrefReturnDefault()
        {
            preference.DeleteValue();

            preference.GetValueOrDefault().Is(default(int));
            preference.GetValueOrDefault(1).Is(1);
        }

        public void PrefHasValue()
        {
            preference.DeleteValue();

            preference.Value = 1;
            preference.HasValue.IsTrue();
        }

        public void PrefHasValueReturnValue()
        {
            preference.DeleteValue();

            preference.Value = 1;
            preference.Value.Is(1);
            preference.GetValueOrDefault().Is(1);
            preference.GetValueOrDefault(2).Is(1);
        }
    }
}