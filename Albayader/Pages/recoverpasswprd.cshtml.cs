using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class recoverpasswprdModel : PageModel
    {
        public string errorMessage { get; set; }
        public string successMessage { get; set; }
        public string token { get; set; }
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



            var parameters = new Dictionary<string, string>();
            parameters["token"] = token;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/validatetoken", data))
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

           

            var parameters = new Dictionary<string, string>();
            parameters["password"] = password;
            parameters["token"] = token;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
               
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/recoverpassword", data))
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
