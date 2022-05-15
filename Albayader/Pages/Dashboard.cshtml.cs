using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class DashboardModel : PageModel
    {
        public string token { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                email = HttpContext.Session.GetString("email");
                password = HttpContext.Session.GetString("password");
            }

            return null;
        }
    }
}
