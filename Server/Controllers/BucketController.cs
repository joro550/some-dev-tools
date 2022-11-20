using DevTools.Shared;
using DevTools.Server.Data;
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
    
    [HttpGet]
    public IActionResult GetAsync()
    {
        return Ok(new CreateBucketResponse(Guid.NewGuid().ToString()));
    }
    
    [HttpGet("{id}/requests")]
    public async Task<IActionResult> GetRequests([FromRoute] string id)
    {
        var requests = await _repo.GetAsync(id);
        return Ok(requests);
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> AcceptGetRequest([FromRoute]string id)
    // {
    //     using var streamReader = new StreamReader(Request.Body);
    //     var request = new CustomHttpRequest
    //     {
    //         Method = Request.Method,
    //         Body = await streamReader.ReadToEndAsync()
    //     };
    //     
    //     
    //     await _repo.AddAsync(id, request);
    //     return Ok();
    // }
    //
    // [HttpPost("{id}")]
    // public async Task<IActionResult> AcceptPostRequest([FromRoute] string id)
    // {
    //     using var streamReader = new StreamReader(Request.Body);
    //     var request = new CustomHttpRequest
    //     {
    //         Method = Request.Method,
    //         Body = await streamReader.ReadToEndAsync()
    //     };
    //     
    //     await _repo.AddAsync(id, request);
    //     return Ok();
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<IActionResult> AcceptPutRequest([FromRoute] string id)
    // {
    //     using var streamReader = new StreamReader(Request.Body);
    //     var request = new CustomHttpRequest
    //     {
    //         Method = Request.Method,
    //         Body = await streamReader.ReadToEndAsync()
    //     };
    //     
    //     await _repo.AddAsync(id, request);
    //     return Ok();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> AcceptDeleteRequest([FromRoute] string id)
    // {
    //     using var streamReader = new StreamReader(Request.Body);
    //     var request = new CustomHttpRequest
    //     {
    //         Method = Request.Method,
    //         Body = await streamReader.ReadToEndAsync()
    //     };
    //     
    //     await _repo.AddAsync(id, request);
    //     return Ok();
    // }
}