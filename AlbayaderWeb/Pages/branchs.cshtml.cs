using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace AlbayaderWeb.Pages
{
    public class branchsModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string errorMessage { get; set; }
         public List<EBranchs>? branchs = null;
         public int companyId { get; set; }
        public string? title { get; set; }
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }

        public string role { get; set; }

        public async Task<IActionResult> OnGet(int companyid,string companyName)
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
            if(role.ToLower()!= "administrator" && role.ToLower() != "manager")
            {
                return Redirect("Index");
            }

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            title =companyName;
            companyId = companyid;
            branchs = await getAllCompanyBranchs(companyid);
            return null;
        }

        public async Task<List<EBranchs>> getAllCompanyBranchs(int companyid)
        {

            // if user admin
             apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["companyid"] = companyid;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");



            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.PostAsync(apiurl+"branch/companybranchs",data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        branchs = JsonConvert.DeserializeObject<List<EBranchs>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return branchs;
        }
        public IActionResult OnPostAddBranch(int companyid,string companyname)
        {

            return RedirectToPage("ManageBranch", "Smode", new { Smode = "Add", id = 0, companyname= companyname, companyid = companyid });

        }
        public IActionResult OnPostEditBranch(string id, int companyid, string companyname)
        {

            return RedirectToPage("ManageBranch", "Smode", new { Smode = "Edit", id = id, companyname = companyname, companyid = companyid });

        }

        public async Task<IActionResult> OnPost()
        {
            //delete 
            int id = Convert.ToInt16(Request.Form["deletedbranchId"]);

            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletBranch(id);



            return Page();

        }

        public async Task<IActionResult> OnPostDeletebranch(int id)
        {

            token = HttpContext.Session.GetString("token");
            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletBranch(id);

            return null;

        }
        public async Task<string> deletBranch(int id)
        {

            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"branch/remove", data))
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
