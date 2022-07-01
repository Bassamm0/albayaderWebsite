using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{
    public class ManageUserModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public EUser _User = new EUser();
        
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;
        public string role { get; set; }

        public void OnGet()
        {
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            token = HttpContext.Session.GetString("token");
        }

        public async Task<IActionResult> OnGetSmode(string Smode, int id, string companyname, int companyid)
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
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" )
            {
                return Redirect("Index");
            }


            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
            CompanyId = companyid;
            PageActionMode = Smode;
            CompanyName = companyname;

            if (PageActionMode == "Add")
            {
                pageTitle = "Add User";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit User";
                _User = await getuserById(id);
                editMode = true;

            }
            return null;
        }

        public async Task<EUser> getuserById(int id)
        {

             apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/getUserById", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _User = JsonConvert.DeserializeObject<EUser>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }
                }
            }
            return _User;
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
                  
                    _User.BranchId =Convert.ToInt32(Request.Form["ddBranch"]);
                    _User.Title = Request.Form["ddTitle"];
                    _User.FirstName = Request.Form["firstname"];
                    _User.Lastname = Request.Form["lastname"];
                    _User.Birthday =Convert.ToDateTime(Request.Form["birthday"]);
                    _User.Email = Request.Form["email"];
                    _User.Username = Request.Form["email"];
                    _User.Mobile = Request.Form["mobile"];
                    _User.Telephone = Request.Form["tel"];
                    _User.Nationality = Convert.ToInt32(Request.Form["ddNationality"]);
                    _User.CountryId = Convert.ToInt32(Request.Form["ddCountry"]);
                    _User.City = Request.Form["city"];
                    _User.PositionId = Convert.ToInt16(Request.Form["ddPosition"]);
                    _User.Password = Request.Form["password"];
                    
                    _User.PictureFileName = Request.Form["uploadedfile"];
                    _User.AuthLevelRefId = Convert.ToInt16(Request.Form["ddAuth"]);


                    //_User.UserId = Convert.ToInt16(Request.Form["hdUserId"]);
                    string companyNamefield = Request.Form["companyNamefield"];
                    int _companyId =Convert.ToInt16(Request.Form["hdCompanyId"]);
                    statusCode = await addUsery(_User);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("Users", new { companyid = _companyId, companyname = companyNamefield });
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
                    _User.BranchId = Convert.ToInt32(Request.Form["ddBranch"]);
                    _User.Title = Request.Form["ddTitle"];
                    _User.FirstName = Request.Form["firstname"];
                    _User.Lastname = Request.Form["lastname"];
                    _User.Birthday = Convert.ToDateTime(Request.Form["birthday"]);
                    _User.Email = Request.Form["email"];
                    _User.Username = Request.Form["email"];
                    _User.Mobile = Request.Form["mobile"];
                    _User.Telephone = Request.Form["tel"];
                    _User.Nationality = Convert.ToInt32(Request.Form["ddNationality"]);
                    _User.CountryId = Convert.ToInt32(Request.Form["ddCountry"]);
                    _User.City = Request.Form["city"];
                    _User.PositionId = Convert.ToInt16(Request.Form["ddPosition"]);
                    _User.Password = Request.Form["password"];
                    _User.PositionId = Convert.ToInt16(Request.Form["ddPosition"]);
                    _User.PictureFileName = Request.Form["uploadedfile"];
                    _User.AuthLevelRefId = Convert.ToInt16(Request.Form["ddAuth"]);

                    _User.UserId = Convert.ToInt16(Request.Form["hdUserId"]);
                    _User.UserAndBranchId = Convert.ToInt16(Request.Form["hdUserAndBranchId"]);
                    string companyNamefield = Request.Form["companyNamefield"];
                    int _companyId = Convert.ToInt16(Request.Form["hdCompanyId"]);

                    statusCode = await updateUser(_User);
                    if (statusCode == "OK")
                    {

                        return RedirectToPage("Users", new { companyid = _companyId, companyname = companyNamefield });
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }


        private async Task<string> addUsery(EUser User)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/addwithbranch", data))
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





        private async Task<string> updateUser(EUser user)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(user);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"User/updatewithbranch", data))
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
    }
   

}
