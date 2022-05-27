using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class ChangePasswordModel : PageModel
    {
        public string token { get; set; }
        public string errorMessage { get; set; }
        public string successMessage { get; set; }
        public string pageTitle { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

         public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }


            string oldPassword = Request.Form["oldpassword"];
            string password = Request.Form["password"];

            bool changeResut = false;
            if (!String.IsNullOrEmpty(oldPassword))
            {
                //call change password
                changeResut=await changePasswrod(oldPassword, password);
                if (changeResut)
                {
                    errorMessage = "";
                    successMessage = "Your password changed successfuly";
                }
            }

            return null;
        }

        private async Task<Boolean> changePasswrod(string oldPassword,string password)
        {
            Boolean result=false;

            token = HttpContext.Session.GetString("token");

            var parameters = new Dictionary<string, string>();
            parameters["oldPassword"] = oldPassword;
            parameters["password"] = password;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/changepassword", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        result = JsonConvert.DeserializeObject<bool>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }
                }
            }

            return result;
        }




    }
}
