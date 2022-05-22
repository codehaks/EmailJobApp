using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailWorker;

public class Email
{
    public string Message { get; set; }
}

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, e) =>
        {
            var body=e.Body;
            var json = System.Text.Encoding.UTF8.GetString(body.ToArray());
            var email=System.Text.Json.JsonSerializer.Deserialize<Email>(json);

            _logger.LogInformation("Email sending : " + email.Message);

        };

        channel.BasicConsume("email-send",true,consumer);

    }
}
