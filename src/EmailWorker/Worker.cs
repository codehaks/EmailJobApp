using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailWorker;

class Email
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

        //channel.QueueDeclare("email-send-list", false, false, false, null);

        var consumer=new EventingBasicConsumer(channel);

        consumer.Received += Consumer_Received;

        channel.BasicConsume("email-send-list", true, consumer);

        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //    await Task.Delay(1000, stoppingToken);
        //}
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var msg = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
        var email = System.Text.Json.JsonSerializer.Deserialize<Email>(msg);

        _logger.LogInformation("Email send : " + email.Message);
    }
}
