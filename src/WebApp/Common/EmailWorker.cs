namespace WebApp
{
    public class EmailWorker : BackgroundService
    {
        private readonly ITaskJob _taskJob;
        private readonly ILogger<EmailWorker> _logger;
        public EmailWorker(ITaskJob taskJob, ILogger<EmailWorker> logger)
        {
            _taskJob = taskJob;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_taskJob.Queue.Any())
                {
                    var msg = _taskJob.Queue.Dequeue();
                    Console.WriteLine(msg);
                }
                _logger.LogInformation("EmailWorker executed.");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
