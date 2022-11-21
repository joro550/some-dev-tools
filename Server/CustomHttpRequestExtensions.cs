using DevTools.Shared;

namespace DevTools.Server;

public static class CustomHttpRequestExtensions
{
    private const int EightMb = 8_000 * 1_000;
    public static async Task<CustomHttpRequest> ToCustomHttpRequest(this HttpRequest request)
    {
        using var streamReader = new StreamReader(request.Body);
        var readToEndAsync = await streamReader.ReadToEndAsync();

        return new CustomHttpRequest
        {
            Method = request.Method,
            ContentType = request.ContentType,
            Body = readToEndAsync.Length >= EightMb ? "Body was too long to save" : readToEndAsync,
            Cookies = request.Cookies.ToDictionary(query => query.Key, query => query.Value),
            Query = request.Query.ToDictionary(query => query.Key, query => query.Value.ToList()),
            Headers = request.Headers.ToDictionary(x => x.Key, pair => pair.Value.ToList()),
            // FormCollection = request.Form.ToDictionary(query => query.Key, query => query.Value.ToList()),
            RouteValues = request.RouteValues.ToDictionary(x => x.Key, pair => pair.Value)
        };
    }
}