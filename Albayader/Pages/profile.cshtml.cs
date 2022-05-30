using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{
    public class profileModel : PageModel
    {
        public EUser _User = new EUser();
        public string token { get; set; }
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

            return null;
        }
        public async Task<EUser> getuserById(int id)
        {
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/getUserById", data))
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

            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/updateprofile", data))
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
