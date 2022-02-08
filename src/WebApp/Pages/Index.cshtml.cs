using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

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
        _taskJob.Queue.Enqueue(Message);
        TempData["status"] = "Email sent.";
        _logger.LogDebug("Email sent by user.");
        return Page();
    }

}
