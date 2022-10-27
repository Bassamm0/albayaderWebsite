using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class ticketsModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public string errorMessage { get; set; }
        public List<EticketViews>? tickets = null;
 
       
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

            }
            if (role.ToLower() != "administrator" && role.ToLower() != "manager")
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            tickets = await getAllOpentickets();
            return null;
        }

        public async Task<List<EticketViews>> getAllOpentickets()
        {
            apiurl = AppConfig.APIUrl;
            // if user admin
           
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(apiurl + "tickets/open"))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        tickets = JsonConvert.DeserializeObject<List<EticketViews>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return tickets;
        }


        public async Task<IActionResult> OnPost()
        {
            token = HttpContext.Session.GetString("token");
           

            string type = Request.Form["actionType"];
            if(type== "filter")
            {
                string startDate1 = Request.Form["startDate"];
                string endDate1 = Request.Form["endDate"];

                DateTime osDate = DateTime.ParseExact(startDate1, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime oeDate = DateTime.ParseExact(endDate1, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                string startDate = osDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = oeDate.ToString("yyyy-MM-dd HH:mm:ss");

                tickets = await getAllOpenticketsDate(startDate, endDate);
            }
            else
            {
                tickets = await getAllOpentickets();
            }
            return null;
        }
        public async Task<List<EticketViews>> getAllOpenticketsDate(string startDate,string endDate)
        {
            apiurl = AppConfig.APIUrl;
          
            var parameters = new Dictionary<string, string>();
            parameters["startDate"] = startDate;
            parameters["endDate"] = endDate;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "tickets/opendate",data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        tickets = JsonConvert.DeserializeObject<List<EticketViews>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return tickets;
        }

    }
}
