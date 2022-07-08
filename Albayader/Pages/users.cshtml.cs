using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace AlbayaderWeb.Pages
{
    public class UserModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public string errorMessage { get; set; }
        public List<EUser>? User = null;
        public List<UserViewModel>? ViewUser = null;

        public int companyId { get; set; }
        public string? title { get; set; }
        public async Task<IActionResult> OnGet(int companyid, string companyName)
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
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" )
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            title = companyName;
            companyId = companyid;
            ViewUser = await getAllCompanyUser(companyid);
            return null;
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
                using (var response = await httpClient.PostAsync(apiurl+"User/getCompanyUsers", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        User = JsonConvert.DeserializeObject<List<EUser>>(responseJson);
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
        public IActionResult OnPostAddUser(int companyid, string companyname)
        {

            return RedirectToPage("ManageUser", "Smode", new { Smode = "Add", id = 0, companyname = companyname, companyid = companyid });

        }
        public IActionResult OnPostEditUser(string id, int companyid, string companyname)
        {

            return RedirectToPage("ManageUser", "Smode", new { Smode = "Edit", id = id, companyname = companyname, companyid = companyid });

        }

        public async Task<IActionResult> OnPost()
        {
            token = HttpContext.Session.GetString("token");
            //delete 
            int id = Convert.ToInt16(Request.Form["deletedUserId"]);

            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletUser(id);



            return Page();

        }

        public async Task<IActionResult> OnPostDeleteUser(int id)
        {


            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletUser(id);

            return null;

        }
        public async Task<string> deletUser(int id)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/remove", data))
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
