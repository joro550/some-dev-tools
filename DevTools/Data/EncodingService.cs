using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Data;

public class EncodingService
{
    private readonly IMemoryCache _memoryCache;
    private readonly Session _session;

    public EncodingService(IMemoryCache memoryCache, Session session)
    {
        _memoryCache = memoryCache;
        _session = session;
    }

    public async Task Add(InputOutput encoding)
    {
        var sessionId = _session.GetSessionId();
        var item = await _memoryCache.GetOrCreateAsync($"encoding/{sessionId}", 
            _ => Task.FromResult(new List<InputOutput>()));

        item!.Add(encoding);
        _memoryCache.Set("encoding", item);
    }

    public async Task<List<InputOutput>> GetAsync()
    {
        var sessionId = _session.GetSessionId();
        var orCreateAsync = await _memoryCache.GetOrCreateAsync($"encoding/{sessionId}",
            _ => Task.FromResult(new List<InputOutput>()));
        return orCreateAsync!;
    }
}