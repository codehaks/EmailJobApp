﻿namespace WebApp
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
                    var taskList=new List<Task>();
                    var queueCount = _taskJob.Queue.Count;

                    _logger.LogDebug("{0} message queued.", _taskJob.Queue.Count);
                    for (int i = 0; i < queueCount; i++)
                    {
                        var success = _taskJob.Queue.TryDequeue(out var msg);
                        if (success)
                        {
                            var t= SendEmail(msg);
                            taskList.Add(t);
                        }
                    }

                    await Task.WhenAll(taskList);
                    
                  

                }
                
                await Task.Delay(1000, stoppingToken);
                //_logger.LogDebug("Backgroud service executed.");
            }
        }

        public async Task SendEmail(string msg)
        {
            await Task.Delay(2000);
            _logger.LogWarning("Email Send : " + msg);
        }
    }
}
