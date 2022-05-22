using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;

namespace WebApp.Pages;

public class Email
{
    public string Message { get; set; }
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public string Message { get; set; } = default!;

    public IActionResult OnPost()
    {
        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("email-box",ExchangeType.Direct);
        channel.QueueDeclare("email-send",false,false,false);
        channel.QueueBind("email-send", "email-box", "email-add");

        var email=new Email { Message=Message};
        var json = System.Text.Json.JsonSerializer.Serialize(email);
        var body=System.Text.Encoding.UTF8.GetBytes(json);

        channel.BasicPublish("email-box", "email-add",null,body);


        
        TempData["status"] = "Email sent.";
        _logger.LogDebug("Email sent by user.");
        return Page();
    }

}
