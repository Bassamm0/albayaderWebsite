using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class forgetpasswordModel : PageModel
    {
        public string errorMessage { get; set; }
        public string successMessage { get; set; }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {


            string email = Request.Form["email"];
            if (!string.IsNullOrEmpty(email))
            {
                //calll reterive password forgetpassword

            }

            return null;
        }





    }
}
