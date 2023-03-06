using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class TicketViewModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public IActionResult OnGet()
        {
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            return null;
        }
    }
}
