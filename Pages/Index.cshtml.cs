using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicScheduler_Web.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            return Page();
        }
    }
}