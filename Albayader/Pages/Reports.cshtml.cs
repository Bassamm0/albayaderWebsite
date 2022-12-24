using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class ReportsModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string role { get; set; }
        public string email { get; set; }
       // public List<EServiceModel> _services = new List<EServiceModel>();
        public string errorMessage { get; set; }
        public string timezone { get; set; }

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
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "client manager")
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            //_services = await getAllCompletedservices();


            return null;
        }


        public async Task<IActionResult> OnPost(string handler, string serviceId, string newDate)
        {
            token = HttpContext.Session.GetString("token");

            if (handler == "changeDate")
            {

            }
            if (serviceId == "0" || newDate == "")
            {
                return Page();
            }
            string statusCode = await changeserviceDate(serviceId, newDate);
           
            return Page();
        }
      
        public async Task<string> changeserviceDate(string serviceId,string newDate)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, string>();
            parameters["serviceId"] = serviceId;
            parameters["newDate"] = newDate;

            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/updateservicedate", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        //string res = JsonConvert.DeserializeObject<string>(responseJson);
                        return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return "";
        }




        public async Task<IActionResult> OnPostDeleteService(int id)
        {

            token = HttpContext.Session.GetString("token");
            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletService(id);

            return null;

        }
        public async Task<string> deletService(int id)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/remove", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        //string res = JsonConvert.DeserializeObject<string>(responseJson);
                        return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return "";
        }


        public async Task<IActionResult> OnPostChangeBranch(string serviceId, string branchId)
        {

            token = HttpContext.Session.GetString("token");
            if (branchId == "")
            {
                return Page();
            }
            string statusCode = await changeBranch(serviceId, branchId);

            return null;

        }
        public async Task<string> changeBranch(string serviceId, string branchId)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, string>();
            parameters["serviceId"] = serviceId;
            parameters["branchId"] = branchId;

            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/updateservicebranch", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        //string res = JsonConvert.DeserializeObject<string>(responseJson);
                        return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return "";
        }


    }
}
