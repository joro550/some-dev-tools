namespace DevTools.Shared;

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