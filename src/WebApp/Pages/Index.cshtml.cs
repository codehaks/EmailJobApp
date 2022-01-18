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
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //using var socket = new RequestSocket(">tcp://localhost:5500");
            //socket.SendReady += Socket_SendReady;
            //System.Threading.Thread.Sleep(2000);


            //socket.SendFrame(Message);
            _taskJob.Queue.Enqueue(Message);

            for (int i = 0; i < 5; i++)
            {
                _taskJob.Queue.Enqueue(i.ToString() + '-' + Message);
            }

            TempData["status"] = "Email sent.";
            return Page();
        }

        private void Socket_SendReady(object? sender, NetMQSocketEventArgs e)
        {
            e.Socket.SendFrame(Message);
        }
    }
}