using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Newtonsoft.Json;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class draftServiceModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public List<EServiceModel> _services = new List<EServiceModel>();
        public string errorMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                email = HttpContext.Session.GetString("email");
              
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            _services = await getAllDraftservices(3);


            return null;
        }

       

        public async Task<List<EServiceModel>> getAllDraftservices(int stautsId)
        {
            apiurl = AppConfig.APIUrl;
            // if user admin
            var parameters = new Dictionary<string, int>();
            parameters["id"] = stautsId;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");



            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiurl + "service/allByStatus", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _services = JsonConvert.DeserializeObject<List<EServiceModel>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _services;
        }


    }
}
