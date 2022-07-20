namespace SomeService
{
    public interface ICacheService
    {
        Task<T> GetValueAsync<T>(string key);
        Task SetValueAsync<T>(string key, T value);
        Task DeleteValueAsync<T>(string key);
        Task UpdateValueAsync<T>(string key, T value);
    }
}
