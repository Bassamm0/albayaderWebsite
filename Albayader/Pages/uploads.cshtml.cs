using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class uploadsModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public void OnGet()
        {

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

        }
    }
}
