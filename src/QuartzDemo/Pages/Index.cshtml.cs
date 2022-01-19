using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;

namespace QuartzDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISchedulerFactory _factory;
        public IndexModel(ILogger<IndexModel> logger, ISchedulerFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            IScheduler scheduler = await _factory.GetScheduler();

            var jobKey = new JobKey("HelloWorldJob");

            await scheduler.TriggerJob(jobKey);
            TempData["status"] = "Email sent.";
            return Page();
        }

    }
}