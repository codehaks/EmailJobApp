using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediatRJobDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await _mediator.Publish(Message);
            TempData["status"] = "Email sent.";
            return Page();
        }

    }
}