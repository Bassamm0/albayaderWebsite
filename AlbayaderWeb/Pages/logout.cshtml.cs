using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AlbayaderWeb.Pages
{
    public class logoutModel : PageModel
    {
        public IActionResult OnGet()
        { 

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("userid");
            HttpContext.Session.Remove("CompanyId");
            HttpContext.Session.Remove("CompanyName");
            return Redirect("Index");
         
        }
    }
}
