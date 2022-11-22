using DevTools.Shared;
using DevTools.Server.Data;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/bucket")]
public class BucketController : ControllerBase
{
    private readonly IRepository<CustomHttpRequest> _repo;

    public BucketController(IRepository<CustomHttpRequest> repo) 
        => _repo = repo;

    [HttpPost]
    public IActionResult CreateAsync(CreateBucketRequest request)
    {
        var id = $"{request.Prefix}-{Guid.NewGuid():N}";
        _repo.Create(id);

        return Ok(new CreateBucketResponse(id));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequests([FromRoute] string id) 
        => Ok(await _repo.GetAsync(id));
}




[FirestoreData]
public class Bucket
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    
    [FirestoreProperty]
    public string Prefix { get; set; }
    
    [FirestoreProperty]
    public Timestamp TimeStamp { get; set; }
    
    [FirestoreProperty]
    public List<CustomHttpRequest> Requests { get; set; }

    public Bucket()
    {
    }
}