using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class preventiveStartModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public string ?ServiceType { get; set; }
        public string? companyId { get; set; }

        public EServices postedService = new EServices();
        public EServices _eServices = new EServices();
     
        public string errorMessage { get; set; }

        public int userid { get; set; }
        public string role { get; set; }
        public string pageTitle { get; set; }
        public async Task<IActionResult> OnGet(string serviceType,int companyId)
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
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "technicion")
            {
                return Redirect("Index");
            }

            if (serviceType == "preventive")
            {
              
                pageTitle = "Preventive Service Initiation";
            }
            else if (serviceType == "corrective")
            {
               
                pageTitle = "Corrective Service Initiation";
            }
            else
            {
                pageTitle = "Other Service Initiation";
               
            }

            userid = Convert.ToInt16(HttpContext.Session.GetString("userid"));

          
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            token = HttpContext.Session.GetString("token");

            int BranchId=Convert.ToInt16(Request.Form["ddBranch"]);
            string type = Request.Form["serviceType"];
            int serviceTypeId = 1;
            if (type == "preventive")
            {
                serviceTypeId = 1;
                pageTitle = "Preventive Service Initiation";
            }
            else if (type == "corrective")
            {
                serviceTypeId = 2;
                pageTitle = "Corrective Service Initiation";
            }
            else
            {
                pageTitle = "Other Service Initiation";
                serviceTypeId = 3;
            }
            postedService.TechnicianId = Convert.ToInt16(HttpContext.Session.GetString("userid"));
            postedService.CreatedBy = Convert.ToInt16(HttpContext.Session.GetString("userid"));
            postedService.StatusId = 1;
            postedService.BranchId = BranchId;
            postedService.CreatedDate= DateTime.Now;
            postedService.ServiceTypeId = serviceTypeId;


           _eServices = await addService(postedService);
            if (_eServices == null)
            {
                return null;
            }
            if(type == "other")
            {
                type = "corrective";
            }
            string url = type+ "?ServiceId=" + _eServices.ServiceId; ;

            
            return Redirect(url);
        }


        

        private async Task<EServices> addService(EServices Service)
        {
             apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(Service);
            var data = new StringContent(json, Encoding.UTF8, "application/json");




            EServices returnService=new EServices();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"service/add", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;
                        returnService = JsonConvert.DeserializeObject<EServices>(responseJson);
                       
                    }
                    else
                    {
                        errorMessage = response.Content.ReadAsStringAsync().Result;
                       // return response.StatusCode.ToString();
                    }

                }
            }
            return returnService;
        }

    }
}
