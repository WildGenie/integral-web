using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integral.Models
{
    public abstract class ExtendedPageModel : PageModel
    {
        [TempData]
        public IList<string> Status { get; set; } = new List<string>();

        protected IActionResult ModelErrorResult(string error) => ModelErrorResult(string.Empty, error);

        protected IActionResult ModelErrorResult(string category, string error)
        {
            ModelState.AddModelError(category, error);
            return Page();
        }
    }
}
