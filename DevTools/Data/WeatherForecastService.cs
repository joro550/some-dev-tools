using Microsoft.Extensions.Caching.Memory;

namespace DevTools.Data;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }
}


public class EncodingService
{
    private readonly IMemoryCache _memoryCache;

    public EncodingService(IMemoryCache memoryCache) 
        => _memoryCache = memoryCache;

    public async Task Add(InputOutput encoding)
    {
        var item = await _memoryCache.GetOrCreateAsync("encoding", 
            _ => Task.FromResult(new List<InputOutput>()));

        item.Add(encoding);
        _memoryCache.Set("encoding", item);
    }

    public async Task<List<InputOutput>> GetAsync() =>
        await _memoryCache.GetOrCreateAsync("encoding", 
            _ => Task.FromResult(new List<InputOutput>()));
}

public record Base64Encoding(string Input, string Output) : InputOutput(Input, Output);
public record Base64Decoding(string Input, string Output): InputOutput(Input, Output);

public record InputOutput(string Input, string Output);
