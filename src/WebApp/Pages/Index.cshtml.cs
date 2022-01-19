using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetMQ;
using NetMQ.Sockets;

namespace WebApp.Pages
{
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
        public string Message { get; set; }
      
        public async Task<IActionResult> OnPost()
        {
            _taskJob.Queue.Enqueue(Message);

            TempData["status"] = "Email sent.";
            return Page();
        }

        private void Socket_SendReady(object? sender, NetMQSocketEventArgs e)
        {
            e.Socket.SendFrame(Message);
        }
    }
}