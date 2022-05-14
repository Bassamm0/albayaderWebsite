using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Core_3Tire.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public string Message { get; set; } = "any thing";
        public string email { get; set; }
        public string password { get; set; }
        public bool Authenticated { get; set; }=false;
        private string url= "/ChangePassword";

        private string loginUrl = "https://localhost:7174/api/Login";

        public string token { get; set; }
        public string errorMessage { get; set; }

        static HttpClient client = new HttpClient();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            
            return null ;
        }
      

        public async  Task<IActionResult> OnPost()
        {

            string statusCode = "";
            email = Request.Form["LoginEmailName"];
            password = Request.Form["LoginPasswordName"];
           
            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {

                statusCode = await GetLogindetails(email,password);

                if (statusCode == "OK")
                {
                    Authenticated = true;
                    return Redirect(url);
                }
                else if(statusCode == "NotFound")
                {
                    StringBuilder str = new StringBuilder();
                    // str.AppendFormat("hi {0} what kind of password is this {1}", email, password);
                   
                    Message = errorMessage;
                }
                else
                {
                    Message = errorMessage;
                }

            }
            else
            {
                Message = "Email and Password are required, please provide both field";
            }
          

           

            return null;
        }
            
        public void OnPostDelete()
        {
            Message = "Delete handler fired";
        }

        public void OnPostEdit(int id)
        {
            Message = "Edit handler fired";
        }

        public void OnPostView(int id)
        {
            Message = "View handler fired";
        }


        private async Task<string> GetLogindetails(string email,string password)
        {

         



            var userLogin = new userLogin();
            userLogin.Email = email;
            userLogin.Password = password;


            var json = JsonConvert.SerializeObject(userLogin);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/Login",data))
                {
                   // string apiResponse = await response.Content.ReadAsStringAsync();
                   if(response.StatusCode.ToString()=="OK")
                    {
                        token= response.Content.ReadAsStringAsync().Result;
                        return response.StatusCode.ToString();
                    }
                    else 
                    {
                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        return response.StatusCode.ToString();
                    }
                   
               
                 
                }
            }
            return errorMessage;
        }
       private class userLogin
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }
    }
}