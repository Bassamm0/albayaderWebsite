using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{
    public class ManageUserModel : PageModel
    {
        public User _PostUser = new User();
        public EUser? _User = null;
        public string token { get; set; }
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetSmode(string Smode, int id, string companyname, int companyid)
        {
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
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/getUserById", data))
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

            string statusCode = "";
            PageActionMode = Request.Form["Smode"];
            if (PageActionMode == "Add")
            {
                try
                {
                    _PostUser.firstName = Request.Form["UserName"];

                  
                    _PostUser.userid = Convert.ToInt16(Request.Form["hdUserId"]);
                    string companyNamefield = Request.Form["companyNamefield"];
                    statusCode = await addUsery(_PostUser);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("Users", new { companyid = _PostUser.userid, companyname = companyNamefield });
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
                    _PostUser.userid = Convert.ToInt16(Request.Form["hdUserId"]);
                    _PostUser.firstName = Request.Form["firstName"];

                   

                   
                    string companyId= Request.Form["hdCompanyId"];
                    string companyNamefield = Request.Form["companyNamefield"];

                    statusCode = await updateUser(_PostUser);
                    if (statusCode == "OK")
                    {

                        return RedirectToPage("Users", new { companyid = companyId, companyname = companyNamefield });
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }


        private async Task<string> addUsery(User User)
        {

            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/add", data))
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





        private async Task<string> updateUser(User User)
        {

            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/update", data))
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
    public class User
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public int userid { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string role { get; set; }
        public int authLevelId { get; set; }
        public int nationality { get; set; }
        public int countryId { get; set; }
        public int positionid { get; set; }
        public string city { get; set; }
        public DateTime birthday { get; set; }
        public string pictureFileName { get; set; }
    }

}
