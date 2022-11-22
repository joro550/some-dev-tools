using AutoMapper;
using LanguageExt;
using DevTools.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/{bucketId}/request")]
public class BucketRequestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<Bucket> _repo;

    public BucketRequestController(IRepository<Bucket> repo, IMapper mapper) => (_repo, _mapper) = (repo, mapper);

    [HttpGet]
    public async Task<IActionResult> AcceptGetRequest(string bucketId) => await HandleRequest(bucketId);

    [HttpPost]
    public async Task<IActionResult> AcceptPostRequest(string bucketId)=> await HandleRequest(bucketId);

    [HttpPut]
    public async Task<IActionResult> AcceptPutRequest(string bucketId) => await HandleRequest(bucketId);

    [HttpDelete]
    public async Task<IActionResult> AcceptDeleteRequest(string bucketId) => await HandleRequest(bucketId);

    private async Task<IActionResult> HandleRequest(string bucketId)
    {
        var customHttpRequest = await Request.ToCustomHttpRequest();
        var entity = _mapper.Map<CustomHttpRequestEntity>(customHttpRequest);
        var bucket = await _repo.GetAsync(bucketId);

        if (bucket.IsNone)
            return NotFound();

        await bucket
            .IfSomeAsync(async b => await AddRequestToBucket(b, entity));
        return Ok();
    }

    private async Task<Unit> AddRequestToBucket(Bucket bucket, CustomHttpRequestEntity customHttpRequestEntity)
    {
        bucket.Requests.Add(customHttpRequestEntity);
        await _repo.UpdateAsync(bucket);
        return Unit.Default;
    }
}