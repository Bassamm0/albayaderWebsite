using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{

    
    public class quoteModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        // public List<EServiceModel> _services = new List<EServiceModel>();
        public string errorMessage { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                role = HttpContext.Session.GetString("Role");

            }
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "client manager")
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

          


            return null;
        }
    }
}
