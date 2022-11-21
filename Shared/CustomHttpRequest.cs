namespace DevTools.Shared;

public class CustomHttpRequest
{
    public string Method { get; set; }
    public string Body { get; set; }
    public string? ContentType { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Cookies { get; set; }
    public Dictionary<string, List<string?>> Query { get; set; } 
    public Dictionary<string, List<string?>> FormCollection { get; set; } 
    public IDictionary<string, object?> RouteValues { get; set; }
    public Dictionary<string,List<string?>> Headers { get; set; }
}