using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class testModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? storeStartDate { get; set; }
        public string? storeEndDate { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string timezone { get; set; }


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

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
   
            return null;
        }
    }
}
