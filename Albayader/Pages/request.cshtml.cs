using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AlbayaderWeb.Pages
{
    public class requestModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public int _BranchId { get; set; }
        public int _ServiceId { get; set; }
        public string errorMessage { get; set; }
        public string timezone { get; set; }
        public string role { get; set; }
        public async Task<IActionResult> OnGet()
        {

            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("token") == "")
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                role = HttpContext.Session.GetString("Role");
                timezone = HttpContext.Session.GetString("timezone");

            }
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "technicion")
            {
                return Redirect("Index");
            }

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            return null;
        }
       
    }
}
