
namespace PrefsWrapper
{
    public interface IPreference<T>
    {
        // like a Nullable API
        bool HasValue { get; }
        T Value { get; set; }
        T GetValueOrDefault();
        T GetValueOrDefault(T defaultValue);
        void DeleteValue();
    }
}