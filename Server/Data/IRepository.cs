using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Server.Data;

public interface IRepository<T> where T : IPersistentObject, new()
{
    Task<string> Create();
    Task<string> Create(T model);
    Task<Option<T>> GetAsync(string id);
    Task<Unit> UpdateAsync(T item);
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

    public async Task<string> Create()
    {
        var item = await _firestoreProvider.AddOrUpdate(new T());
        return item
            .Map(x => _memoryCache.Set(Key(x.Id), x))
            .Some(x => x.Id)
            .None(string.Empty);
    }

    public async Task<string> Create(T model)
    {
        var item = await _firestoreProvider.AddOrUpdate(model);
        return item
            .Map(x => _memoryCache.Set(Key(x.Id), x))
            .Some(x => x.Id)
            .None(string.Empty);
    }

    public async Task<Unit> UpdateAsync(T item)
    {
        var dbItem = await _firestoreProvider.AddOrUpdate(item);
        return dbItem
            .Map(x => _memoryCache.Set(Key(x.Id), x))
            .Some(_ => unit)
            .None(unit);
    }

    public async Task<Option<T>> GetAsync(string id)
    {
        var cacheItem = await GetCacheItem(Key(id))
            .IfNoneAsync(async () =>
            {
                // If we don't have a value in cache check the database
                var val = await _firestoreProvider.GetAsync<T>(id);

                // If there is a value update the cache if not return a default value
                return val
                    .Some(v => _memoryCache.Set(Key(id), v))
                    .None(() => new T());
            });

        
        return !string.Equals(cacheItem.Id, id, StringComparison.InvariantCulture) 
            ? None 
            : cacheItem;
    }

    private static string Key(string id) => $"{typeof(T).Name}/{id}";
    
    /// <summary>
    /// The in memory cache can return null so we need to isolate this so it returns a None value
    /// </summary>
    /// <param name="key">The key we're expecting it to be in</param>
    /// <returns>Option of item</returns>
    private Option<T> GetCacheItem(string key)
    {
        var cacheItem = _memoryCache.Get<T>(key);
        return cacheItem == null ? None : cacheItem;
    }
}