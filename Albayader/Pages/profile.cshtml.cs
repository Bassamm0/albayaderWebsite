using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class profileModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string role { get; set; }

        public EUser _User = new EUser();
      
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public int UserId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                UserId =Convert.ToInt16(HttpContext.Session.GetString("userid"));

                _User = await getuserById(UserId);

            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            return null;
        }
        public async Task<EUser> getuserById(int id)
        {
             apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/getUserById", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _User = JsonConvert.DeserializeObject<EUser>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }
                }
            }
            return _User;
        }


        public async Task<IActionResult> OnPost()
        {

            token = HttpContext.Session.GetString("token");

            _User.FirstName = Request.Form["firstname"];
            _User.Lastname = Request.Form["lastname"];
            _User.Birthday = Convert.ToDateTime(Request.Form["birthday"]);
            _User.Email = Request.Form["email"];
         
            _User.Mobile = Request.Form["mobile"];
            _User.Telephone = Request.Form["tel"];
            _User.Nationality = Convert.ToInt32(Request.Form["ddNationality"]);
            _User.CountryId = Convert.ToInt32(Request.Form["ddCountry"]);
            _User.City = Request.Form["city"];
            _User.PositionId = Convert.ToInt16(Request.Form["ddPosition"]);
            
            _User.PictureFileName = Request.Form["uploadedfile"];
            _User.UserId = Convert.ToInt16(HttpContext.Session.GetString("userid"));

            string statusCode = "";
            statusCode = await updateUser(_User);

            return null;
        }





        private async Task<string> updateUser(EUser User)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/updateprofile", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseMssage = response.Content.ReadAsStringAsync().Result;
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
    }
}
