using AutoMapper;
using DevTools.Shared;
using DevTools.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/bucket")]
public class BucketController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<Bucket> _repo;

    public BucketController(IRepository<Bucket> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBucketRequest request) 
        => Ok(new CreateBucketResponse(await _repo.Create()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequests([FromRoute] string id)
    {
        var buckets = await _repo.GetAsync(id);
        var requests = buckets
            .Some(b => b.Requests)
            .None(() => new List<CustomHttpRequestEntity>());
        return Ok(_mapper.Map<List<CustomHttpRequestEntity>, List<CustomHttpRequest>>(requests));
    }
}