namespace DevTools.Shared;

public class CreateBucketRequest
{
    public string? Prefix { get; set; }
}

public class CreateBucketResponse
{
    public CreateBucketResponse()
    {
        
    }
    
    public CreateBucketResponse(string id)
    {
        Id = id;
    }

    public string Id { get; set; } 
        = string.Empty;
}

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