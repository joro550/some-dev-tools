using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Server.Data;

public interface IRepository<T> where T : IPersistentObject, new()
{
    Task<Unit> Create(string id);
    Task<bool> AddAsync(string id, T item);
    Task<Option<T>> GetAsync(string id);
    Task<Unit> UpdateAsync(T bucket);
}

public class Repository<T> : IRepository<T> where T : IPersistentObject, new()
{
    private readonly IMemoryCache _memoryCache;
    private readonly IFirestoreProvider _firestoreProvider;

    public Repository(IMemoryCache memoryCache, IFirestoreProvider firestoreProvider)
    {
        _memoryCache = memoryCache;
        _firestoreProvider = firestoreProvider;
    }

    public Task<Unit> UpdateAsync(T bucket)
    {
        _memoryCache.Set(Key(bucket.Id), bucket);
        return Task.FromResult(unit);
    }

    public Task<Option<T>> GetAsync(string id)
    {
        var c = GetCacheItem2(Key(id));
        var cacheItem = GetCacheItem(Key(id));
        var persistentObject = cacheItem
            .Some(Some)
            .None(() => None);
        
        return Task.FromResult(persistentObject);
    }

    public Task<Unit> Create(string id)
    {
        _memoryCache.Set(Key(id), new T());
        return Task.FromResult(unit);
    }

    public Task<bool> AddAsync(string id, T item)
    {
        var cacheItem = GetCacheItem(Key(id));
        return Task.FromResult(cacheItem
            .Some(i =>
            {
                _memoryCache.Set(Key(id), i);
                return true;
            })
            .None(() => false));
    }

    private static string Key(string id) => $"{typeof(T).Name}/{id}";
    private Option<T> GetCacheItem(string key) => _memoryCache.Get<T>(key);
    
    private OptionAsync<T> GetCacheItem2(string key) => _memoryCache.Get<T>(key);
}