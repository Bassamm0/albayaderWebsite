using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
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
        public string? storeStartDate { get; set; }
        public string? storeEndDate { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string timezone { get; set; }

        public string errorMessage { get; set; }
        public List<EticketViews>? tickets = null;
        public List<UserViewModel>? ViewUser = null;

        public EServices postedService = new EServices();
        public EServices _eServices = new EServices();
        public string CompanyId { get; set; }


        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("token") == "")
            {
                //return Redirect("Index");
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
            ViewUser = await getAllCompanyUser(2);
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

                    timezone = HttpContext.Session.GetString("timezone");
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        tickets = JsonConvert.DeserializeObject<List<EticketViews>>(responseJson);
                        if (tickets.Count > 0)
                        {
                            for (int i = 0; i < tickets.Count; i++)
                            {
                                tickets[i].creationDate = UtilityHelper.convertUTCtoTimeZone(tickets[i].creationDate, timezone);
                            }
                        }

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
            CompanyId = HttpContext.Session.GetString("CompanyId");
            token = HttpContext.Session.GetString("token");
            role = HttpContext.Session.GetString("Role");
            timezone = HttpContext.Session.GetString("timezone");


            storeStartDate = Request.Form["startDate"];
            storeEndDate = Request.Form["endDate"];

            string type = Request.Form["actionType"];
            if(type== "filter")
            {

                string startDate1 = Request.Form["startDate"];
                string endDate1 = Request.Form["endDate"];
               

                if (String.IsNullOrEmpty(startDate1) || String.IsNullOrEmpty(endDate1)) return null;

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
            return Page();
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

      
        public async Task<IActionResult> OnPostChangeStatus(int id, int statusId)
        {

            CompanyId = HttpContext.Session.GetString("CompanyId");
            token = HttpContext.Session.GetString("token");
            role = HttpContext.Session.GetString("Role");
            timezone = HttpContext.Session.GetString("timezone");

            if (id == 0)
            {
                return Page();
            }
            EticketAndStatus ticketAndStatus =new EticketAndStatus();
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


        public async Task<List<UserViewModel>> getAllCompanyUser(int companyid)
        {
            apiurl = AppConfig.APIUrl;
            // if user admin
            var parameters = new Dictionary<string, int>();
            parameters["companyid"] = companyid;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");



            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "User/getCompanyUsers", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        ViewUser = JsonConvert.DeserializeObject<List<UserViewModel>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return ViewUser;
        }

        public async Task<IActionResult> OnPostAssign(int id, int userId)
        {

            CompanyId = HttpContext.Session.GetString("CompanyId");
            token = HttpContext.Session.GetString("token");
            role = HttpContext.Session.GetString("Role");
            timezone = HttpContext.Session.GetString("timezone");

            if (id == 0)
            {
                return Page();
            }
            EticketAndUser ticketAndUser = new EticketAndUser();
            ticketAndUser.ticketId = id;
            ticketAndUser.AssginUserId = userId;
            string statusCode = await assignUser(ticketAndUser);



            return null;

        }

        public async Task<string> assignUser(EticketAndUser ticketAndUser)
        {
            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(ticketAndUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "tickets/assignuser", data))
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


        public async Task<IActionResult> OnPostCreateService(int id, int branchId, int technicainId,int SiteVistTypeId)
        {

            CompanyId = HttpContext.Session.GetString("CompanyId");
            token = HttpContext.Session.GetString("token");
            role = HttpContext.Session.GetString("Role");
            timezone = HttpContext.Session.GetString("timezone");

            if (id == 0)
            {
                return Page();
            }
            EticketAndService ticketAndService = new EticketAndService();
            ticketAndService.ticketId = id;
            ticketAndService.branchId = branchId;
            ticketAndService.TechnicianId = technicainId;
            ticketAndService.SiteVistTypeId = SiteVistTypeId;

            string statusCode = await CreateAService(ticketAndService);



           


            return null;

        }

        public async Task<string> CreateAService(EticketAndService ticketAndService)
        {
            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(ticketAndService);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "tickets/createservice", data))
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
