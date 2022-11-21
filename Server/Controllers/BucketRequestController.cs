using DevTools.Shared;
using DevTools.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/{bucketId}/request")]
public class BucketRequestController : ControllerBase
{
    private readonly IRepository<CustomHttpRequest> _repo;

    public BucketRequestController(IRepository<CustomHttpRequest> repo) 
        => _repo = repo;
    
    [HttpGet]
    public async Task<IActionResult> AcceptGetRequest(string bucketId)
    {
        await _repo.AddAsync(bucketId, await Request.ToCustomHttpRequest());
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> AcceptPostRequest(string bucketId)
    {
        await _repo.AddAsync(bucketId, await Request.ToCustomHttpRequest());
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> AcceptPutRequest(string bucketId)
    {
        await _repo.AddAsync(bucketId, await Request.ToCustomHttpRequest());
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> AcceptDeleteRequest(string bucketId)
    {
        await _repo.AddAsync(bucketId, await Request.ToCustomHttpRequest());
        return Ok();
    }
}