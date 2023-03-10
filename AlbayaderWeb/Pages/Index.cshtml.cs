using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;
using AlbayaderWeb;

namespace Core_3Tire.Pages
{
    public class IndexModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();

        private readonly ILogger<IndexModel> _logger;
        public EUser _userView=new EUser();

        public string Message { get; set; } 
        public string email { get; set; }
        public string password { get; set; }
        public bool Authenticated { get; set; }=false;
        private string url= "/Dashboard";

        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }


        public string token { get; set; }
        public string errorMessage { get; set; }

        static HttpClient client = new HttpClient();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {


            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            return null ;
        }
      

        public async  Task<IActionResult> OnPost()
        {
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            string timezone = Request.Form["timezoneclient"];


            string statusCode = "";
            email = Request.Form["LoginEmailName"];
            password = Request.Form["LoginPasswordName"];
           
            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {

                statusCode = await GetLogindetails(email,password);

                if (statusCode == "OK")
                {
                    EUser userdetails = new EUser();
                    userdetails = await getLoginUserDetails(token);

                    Authenticated = true;
                    HttpContext.Session.SetString("timezone", timezone);//set tme one
                    HttpContext.Session.SetString("token",token);
                    HttpContext.Session.SetString("email", userdetails.Email);
                    HttpContext.Session.SetString("FullName", userdetails.FirstName +" "+ userdetails.Lastname);
                    HttpContext.Session.SetString("mobile", userdetails.Mobile);
                    HttpContext.Session.SetString("Role", userdetails.UserRole);
                    HttpContext.Session.SetString("userid", userdetails.UserId.ToString());
                    HttpContext.Session.SetString("NationalityName", userdetails.NationalityName.ToString());
                    HttpContext.Session.SetString("ResidentContry", userdetails.ResidentContry.ToString());
                    HttpContext.Session.SetString("PictureFileName", userdetails.PictureFileName.ToString());
                    HttpContext.Session.SetString("CompanyId", userdetails.CompanyId.ToString());
                    HttpContext.Session.SetString("BranchId", userdetails.BranchId.ToString());
                    HttpContext.Session.SetString("CompanyName", userdetails.CompanyName.ToString());
                    HttpContext.Session.SetString("BranchName", userdetails.BranchName.ToString());

                    HttpContext.Session.SetString("apiurl", apiurl);
                    HttpContext.Session.SetString("uploadurl", uploadurl);

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
            
       

        private async Task<string> GetLogindetails(string email,string password)
        {

            string apiurl = AppConfig.APIUrl;
            var userLogin = new userLogin();
            userLogin.Email = email;
            userLogin.Password = password;


            var json = JsonConvert.SerializeObject(userLogin);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiurl+"Login",data))
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


        // get loged user details

        public async Task<EUser> getLoginUserDetails(string token)
        {
            string apiurl = AppConfig.APIUrl;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(apiurl + "User/getLoginUser"))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _userView = JsonConvert.DeserializeObject<EUser>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }
                }
            }
            return _userView;
        }


        public ActionResult SetVariable(string key, string value)
        {

            HttpContext.Session.SetString(key, value);
         
            return null;
        }
    }
}