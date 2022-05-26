using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{
    public class ManageUserModel : PageModel
    {
    
        public EUser _User = new EUser();
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
                    _User.PositionId = Convert.ToInt32(Request.Form["ddPosition"]);
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

            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/addwithbranch", data))
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





        private async Task<string> updateUser(EUser User)
        {

            var json = JsonConvert.SerializeObject(User);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/User/updatewithbranch", data))
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
