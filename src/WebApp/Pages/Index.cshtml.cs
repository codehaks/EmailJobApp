using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;

namespace WebApp.Pages;

class Email
{
    public string Message { get; set; }
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITaskJob _taskJob;

    public IndexModel(ILogger<IndexModel> logger, ITaskJob taskJob)
    {
        _logger = logger;
        _taskJob = taskJob;
    }

    [BindProperty]
    public string Message { get; set; } = default!;

    public IActionResult OnPost()
    {
        //_taskJob.Queue.Enqueue(Message);

        var factory = new ConnectionFactory();
        var connection=factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("email-box", ExchangeType.Direct);
        channel.QueueDeclare("email-send-list", false, false, false, null);
        channel.QueueBind("email-send-list", "email-box", "email-add");        

        var email=new Email { Message=Message};
        var json = System.Text.Json.JsonSerializer.Serialize(email);
        var bytes=System.Text.Encoding.UTF8.GetBytes(json);
        channel.BasicPublish("email-box", "email-add",null,bytes);

        TempData["status"] = "Email sent.";
        _logger.LogDebug("Email sent by user.");
        return Page();
    }

}
