using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

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
            bool result=false;


            string email = Request.Form["email"];
            if (!string.IsNullOrEmpty(email))
            {
                //calll reterive password forgetpassword
                result =await forgetPassword(email);
                if (result)
                {
                    errorMessage = "";
                    successMessage = "Please follow the instruction sent to  " + email;
                }
                else
                {
                    
                }
            }

            return null;
        }

        private async Task<bool> forgetPassword(string  email)
        {
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/forgetpassword", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        //string res = JsonConvert.DeserializeObject<string>(responseJson);
                        response.StatusCode.ToString();
                        return true;
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        return false;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


          
        }



    }
}
