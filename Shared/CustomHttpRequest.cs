namespace DevTools.Shared;

public class CustomHttpRequest
{
    public string Method { get; set; } 
        = string.Empty;

    public string Body { get; set; }
        = string.Empty;
    
    public string? ContentType { get; set; }

    public IEnumerable<KeyValuePair<string, string>> Cookies { get; set; }
        = Array.Empty<KeyValuePair<string, string>>();

    public Dictionary<string, List<string?>> Query { get; set; } 
        = new();

    public Dictionary<string, List<string?>> FormCollection { get; set; }
        = new();
    
    public Dictionary<string, object?> RouteValues { get; set; }
        = new();

    public Dictionary<string, List<string?>> Headers { get; set; }
        = new();
}