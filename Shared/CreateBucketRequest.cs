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
}