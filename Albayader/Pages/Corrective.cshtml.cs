using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class CorrectiveModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public int _BranchId { get; set; }
        public int _ServiceId { get; set; }
        public string errorMessage { get; set; }

        public string role { get; set; }

        public ECorrectiveServiceModel _service = new ECorrectiveServiceModel();

        public async Task<IActionResult> OnGet( int ServiceId)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return Redirect("Index");
            }
            else
            {
                token = HttpContext.Session.GetString("token");
                role = HttpContext.Session.GetString("Role");

            }
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "technicion")
            {
                return Redirect("Index");
            }

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            _ServiceId = ServiceId;
            _service = await getService(ServiceId);
            int statusId = _service.StatusId;
            if (statusId != 1 && statusId != 3)
            {
                return Redirect("Dashboard");
            }
            return null;
        }

        public async Task<ECorrectiveServiceModel> getService(int id)
        {

            apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/getcorrectiveservicebyid", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _service = JsonConvert.DeserializeObject<ECorrectiveServiceModel>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }
                }
            }
            return _service;
        }

    }
}
