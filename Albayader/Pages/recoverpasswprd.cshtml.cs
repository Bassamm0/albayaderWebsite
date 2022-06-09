using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class recoverpasswprdModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public string errorMessage { get; set; }
        public string successMessage { get; set; }

        public bool tokenStatus { get; set; }
        public async Task<IActionResult> OnGet()
        {
            token = HttpContext.Request.Query["token"];
            if (!string.IsNullOrEmpty(token))
            {
                tokenStatus = await validteToken(token);
                if (!tokenStatus)
                {
                    
                   
                }
            }
            else
            {
                tokenStatus = false;
                errorMessage = "Not correct page!";
            }

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            return null;
        }


        public async Task<IActionResult> OnPost()
        {

            token = HttpContext.Request.Query["token"];
            string password = Request.Form["Password"];
            string passwordConf = Request.Form["passwordconf"];

            bool changeResut = false;
            if (!String.IsNullOrEmpty(password))
            {
                //call change password
                changeResut = await changePasswrod(password, token);
                if (changeResut)
                {
                    errorMessage = "";
                    successMessage = "Your password changed successfuly";
                    tokenStatus = false;
                }
            }

            return null;
        }

        private async Task<Boolean> validteToken(string token)
        {
            Boolean result = false;

             apiurl = AppConfig.APIUrl;

            var parameters = new Dictionary<string, string>();
            parameters["token"] = token;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.PostAsync(apiurl+"User/validatetoken", data))
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


        private async Task<Boolean> changePasswrod(string password, string token)
        {
            Boolean result = false;


            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, string>();
            parameters["password"] = password;
            parameters["token"] = token;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
               
                using (var response = await httpClient.PostAsync(apiurl+"User/recoverpassword", data))
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
