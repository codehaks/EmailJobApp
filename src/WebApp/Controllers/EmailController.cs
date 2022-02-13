using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class Message
{
    public string Body { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ITaskJob _taskJob;

    public EmailController(ITaskJob taskJob)
    {
        _taskJob = taskJob;
    }

    public IActionResult Index() => Ok("Welcome!");

    [HttpPost]
    public IActionResult Send([FromBody]Message message)
    {
        _taskJob.Queue.Enqueue(message.Body);
        return Ok("done!");
    }
}
