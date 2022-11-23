﻿using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Server.Data;

public interface IRepository<T> where T : IPersistentObject, new()
{
    Task<string> Create();
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
        var id = await _firestoreProvider.AddOrUpdate(new T());
        _memoryCache.Set(Key(id), new T());
        return id;
    }

    public async Task<Unit> UpdateAsync(T item)
    {
        await _firestoreProvider.AddOrUpdate(item);
        _memoryCache.Set(Key(item.Id), item);
        return unit;
    }

    public async Task<Option<T>> GetAsync(string id)
    {
        var cacheItem = await GetCacheItem(Key(id))
            .IfNoneAsync(async () =>
            {
                // If we don't have a value check the database
                var val = await _firestoreProvider.Get<T>(id);

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
    private Option<T> GetCacheItem(string key) => _memoryCache.Get<T>(key);
}