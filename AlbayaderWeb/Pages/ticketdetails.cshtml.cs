using DAL.Functions;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class ticketdetailsModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int _BranchId { get; set; }
        public int _ServiceId { get; set; }
        public string errorMessage { get; set; }
        public string timezone { get; set; }
        public string CompanyId { get; set; }
        public EticketViewsDetails ticketViewsDetails { get; set; }

        public async Task<IActionResult> OnGet(int ticketId)
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("token") == "")
            {
                return Redirect("Index");
            }
            else
            {
                CompanyId = HttpContext.Session.GetString("CompanyId");
                token = HttpContext.Session.GetString("token");
                role = HttpContext.Session.GetString("Role");
                timezone = HttpContext.Session.GetString("timezone");

            }



            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            ticketViewsDetails =await getTicketDetails(ticketId);
         
            return null;
        }

       
        public async Task<EticketViewsDetails> getTicketDetails(int ticketId)
        {
            apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["ticketId"] = ticketId;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.PostAsync(apiurl + "tickets/ticketdetails", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        ticketViewsDetails = JsonConvert.DeserializeObject<EticketViewsDetails>(responseJson);
                        timezone = HttpContext.Session.GetString("timezone");

                        ticketViewsDetails.creationDate = UtilityHelper.convertUTCtoTimeZone(ticketViewsDetails.creationDate, timezone);
                        ticketViewsDetails.StatusDate = UtilityHelper.convertUTCtoTimeZone(ticketViewsDetails.StatusDate, timezone);

                        if (ticketViewsDetails.lticketLog.Count > 0)
                        {
                            for (int i = 0; i < ticketViewsDetails.lticketLog.Count; i++)
                            {
                                ticketViewsDetails.lticketLog[i].CreationDate = UtilityHelper.convertUTCtoTimeZone(ticketViewsDetails.lticketLog[i].CreationDate, timezone);
                            }
                        }
                       
                    }
                    else
                    {
                        errorMessage = response.Content.ReadAsStringAsync().Result;
                     }
                }
            }



            return ticketViewsDetails;
        }


    }
}
