using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class draftServiceModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public List<EServiceModel> _services = new List<EServiceModel>();
        public string timezone { get; set; }
        public string errorMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("token") == "")
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                role = HttpContext.Session.GetString("Role");
                timezone = HttpContext.Session.GetString("timezone");

            }
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "technicion")
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            _services = await getAllDraftservices(3);
            foreach (EServiceModel eService in _services)
            {
                eService.CreatedDate = UtilityHelper.convertUTCtoTimeZone(eService.CreatedDate, timezone);

            }

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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
