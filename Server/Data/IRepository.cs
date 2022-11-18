using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Server.Data;

public interface IRepository<T>
{
    void Create(string id);
    Task AddAsync(string id, T item);
    Task<List<T>> GetAsync(string id);
}

public class Repository<T> : IRepository<T>
{
    private readonly IMemoryCache _memoryCache;

    public Repository(IMemoryCache memoryCache) 
        => _memoryCache = memoryCache;

    public async Task<List<T>> GetAsync(string id) 
        => await GetCacheItem(Key(id));

    public void Create(string id) 
        => _memoryCache.Set(Key(id), new List<T>());

    public async Task AddAsync(string id, T item)
    {
        var key = Key(id);
        var cacheItem = await GetCacheItem(key);

        cacheItem.Add(item);
        _memoryCache.Set(key, cacheItem);
    }

    private static string Key(string id) => $"{typeof(T).Name}/{id}";

    private async Task<List<T>> GetCacheItem(string key) =>
        await _memoryCache.GetOrCreateAsync(key,
            _ => Task.FromResult(new List<T>())) ?? new List<T>();
}