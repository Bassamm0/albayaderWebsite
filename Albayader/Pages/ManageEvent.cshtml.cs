using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class ManageEventModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }


        public ECalenderEvents? _event = new ECalenderEvents();
        public ECalenderEvents? postEvent = new ECalenderEvents();
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;
        public string role { get; set; }



        public async Task<IActionResult> OnGetSmode(string Smode, int id)
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


            PageActionMode = Smode;


            if (PageActionMode == "Add")
            {
                pageTitle = "Add Event";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit Event";
                _event = await getEvent(id);
                editMode = true;

            }
            return null;
        }


        public async Task<ECalenderEvents> getEvent(int id)
        {

            apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "calendarevent/geteventById", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _event = JsonConvert.DeserializeObject<ECalenderEvents>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _event;
        }



        public async Task<IActionResult> OnPost()
        {
            token = HttpContext.Session.GetString("token");
            
            string statusCode = "";
            PageActionMode = Request.Form["Smode"];
            if (PageActionMode == "Add")
            {
                try
                {
                    postEvent.title = Request.Form["title"];
                    postEvent.eventStartDate = Request.Form["startDate"];
                    postEvent.eventEndDate = Request.Form["endDate"];
                    postEvent.allDay =true;
                    postEvent.url = Request.Form["url"];
                    
                    postEvent.description = Request.Form["description"];
                    postEvent.eventTypeId = Convert.ToInt16(Request.Form["ddType"]);

                    if(postEvent.eventTypeId == 1 || postEvent.eventTypeId == 2)
                    {
                        postEvent.TechnicanId = Convert.ToInt16(Request.Form["ddTechnicain"]);
                        postEvent.branchId = Convert.ToInt16(Request.Form["ddBranch"]);

                    }


                    statusCode = await addEvent(postEvent);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("event", null);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            else if (PageActionMode == "Edit")
            {
                try
                {
                    postEvent.title = Request.Form["title"];
                    postEvent.eventStartDate = Request.Form["startDate"];
                    postEvent.eventEndDate = Request.Form["endDate"];
                    postEvent.allDay = true;
                    postEvent.url = Request.Form["url"];

                    postEvent.description = Request.Form["description"];
                    postEvent.eventTypeId = Convert.ToInt16(Request.Form["ddType"]);

                    if (postEvent.eventTypeId == 1 || postEvent.eventTypeId == 2)
                    {
                        postEvent.TechnicanId = Convert.ToInt16(Request.Form["ddTechnicain"]);
                        postEvent.branchId = Convert.ToInt16(Request.Form["ddBranch"]);


                    }


                    postEvent.EventId = Convert.ToInt16(Request.Form["hdEventId"]);
                    statusCode = await updateEvent(postEvent);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("event", null);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }




        private async Task<string> addEvent(ECalenderEvents Event)
        {

            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(Event);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "calendarevent/add", data))
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



        // edit company





        private async Task<string> updateEvent(ECalenderEvents Event)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(Event);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "calendarevent/update", data))
                {
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
