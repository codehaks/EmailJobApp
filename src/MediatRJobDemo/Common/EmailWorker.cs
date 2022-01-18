namespace MediatRJobDemo.Common;

public class EmailWorker : BackgroundService
{

    private readonly ILogger<EmailWorker> _logger;
    public EmailWorker(ILogger<EmailWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            _logger.LogInformation("EmailWorker executed.");
            await Task.Delay(1000, stoppingToken);
        }
    }
}

