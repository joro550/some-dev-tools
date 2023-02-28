using DevTools.Server.Data;
using DevTools.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/contact")]
public class ContactController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(ContactRequest request, 
        IRepository<Feedback> feedbackRepo)
    {
        await feedbackRepo.Create(new Feedback { Message = request.Feedback});
        return Ok();
    }
}