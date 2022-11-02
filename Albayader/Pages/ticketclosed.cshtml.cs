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
    public class ticketclosedModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? storeStartDate { get; set; }
        public string? storeEndDate { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public string errorMessage { get; set; }
        public List<EticketViews>? tickets = null;
        public List<UserViewModel>? ViewUser = null;


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
             tickets = await getAllClosedtickets();

            return null;
        }
        public async Task<List<EticketViews>> getAllClosedtickets()
        {
            apiurl = AppConfig.APIUrl;
            // if user admin

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(apiurl + "tickets/closed"))
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

        public async Task<IActionResult> OnPostChangeStatus(int id, int statusId)
        {

            token = HttpContext.Session.GetString("token");
            if (id == 0)
            {
                return Page();
            }
            EticketAndStatus ticketAndStatus = new EticketAndStatus();
            ticketAndStatus.ticketId = id;
            ticketAndStatus.ticketStatusId = statusId;
            string statusCode = await ticketchangeStatus(ticketAndStatus);



            return null;

        }
        public async Task<string> ticketchangeStatus(EticketAndStatus ticketAndStatus)
        {
            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(ticketAndStatus);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "tickets/changestatus", data))
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
