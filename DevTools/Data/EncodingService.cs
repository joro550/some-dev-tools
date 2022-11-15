using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Data;

public class EncodingService
{
    private readonly IMemoryCache _memoryCache;

    public EncodingService(IMemoryCache memoryCache) 
        => _memoryCache = memoryCache;

    public async Task Add(InputOutput encoding)
    {
        var item = await _memoryCache.GetOrCreateAsync("encoding", 
            _ => Task.FromResult(new List<InputOutput>()));

        item!.Add(encoding);
        _memoryCache.Set("encoding", item);
    }

    public async Task<List<InputOutput>> GetAsync()
    {
        var orCreateAsync = await _memoryCache.GetOrCreateAsync("encoding",
            _ => Task.FromResult(new List<InputOutput>()));
        return orCreateAsync!;
    }
}