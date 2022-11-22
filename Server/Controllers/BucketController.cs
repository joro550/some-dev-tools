using DevTools.Shared;
using DevTools.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/bucket")]
public class BucketController : ControllerBase
{
    private readonly IRepository<Bucket> _repo;

    public BucketController(IRepository<Bucket> repo) 
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