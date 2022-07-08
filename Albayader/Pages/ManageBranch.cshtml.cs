using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{

    public class ManageBranchModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

        public branch _Postbranch=new branch();
        public EBranchs? _branch = null;
         public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;
        public string role { get; set; }
        


        public async Task<IActionResult> OnGetSmode(string Smode, int id,string companyname,int companyid)
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

            CompanyId = companyid;
            PageActionMode = Smode;
            CompanyName= companyname;

            if (PageActionMode == "Add")
            {
                pageTitle = "Add Branch";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit Branch";
                _branch = await getBranchById(id);
                editMode = true;

            }
            return null;
        }


        public async Task<EBranchs> getBranchById(int id)
        {

             apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"Branch/getBranchById", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _branch = JsonConvert.DeserializeObject<EBranchs>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _branch;
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
                    _Postbranch.branchname = Request.Form["BranchName"];
                    _Postbranch.emirateId =Convert.ToInt16(Request.Form["ddEmirates"]);
                    _Postbranch.district = Request.Form["District"];
                    
                    if (!String.IsNullOrEmpty(Request.Form["latitude"]))
                    {
                        _Postbranch.latitude = Convert.ToDecimal(Request.Form["latitude"]);
                    }
                    if (!String.IsNullOrEmpty(Request.Form["longitude"]))
                    {
                        _Postbranch.latitude = Convert.ToDecimal(Request.Form["longitude"]);
                    }
                    _Postbranch.companyid = Convert.ToInt16(Request.Form["hdCompanyId"]);
                    string companyNamefield = Request.Form["companyNamefield"];
                    statusCode = await addBranchy(_Postbranch);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("branchs", new { companyid = _Postbranch.companyid, companyname = companyNamefield });
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
                    _Postbranch.branchid = Convert.ToInt16(Request.Form["hdBranchId"]);
                    _Postbranch.branchname = Request.Form["BranchName"];
                    _Postbranch.emirateId = Convert.ToInt16(Request.Form["ddEmirates"]);
                    _Postbranch.district = Request.Form["District"];

                    if (!String.IsNullOrEmpty(Request.Form["latitude"]))
                    {
                        _Postbranch.latitude = Convert.ToDecimal(Request.Form["latitude"]);
                    }
                    if (!String.IsNullOrEmpty(Request.Form["longitude"]))
                    {
                        _Postbranch.longitude = Convert.ToDecimal(Request.Form["longitude"]);
                    }

                    _Postbranch.companyid = Convert.ToInt16(Request.Form["hdCompanyId"]);

                    string companyNamefield = Request.Form["companyNamefield"];
                    statusCode = await updateBranch(_Postbranch);
                    if (statusCode == "OK")
                    {
                        
                        return RedirectToPage("branchs", new { companyid = _Postbranch.companyid, companyname= companyNamefield });
                     }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }




        private async Task<string> addBranchy(branch branch)
        {

            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(branch);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"branch/add", data))
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

       



        private async Task<string> updateBranch(branch branch)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(branch);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"branch/update", data))
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


        public class branch
        {
            public int branchid { get; set; }
            public string branchname { get; set; }
            public int companyid { get; set; }
            public decimal latitude { get; set; }
            public decimal longitude { get; set; }
            public int emirateId { get; set; }
            public string district { get; set; }

        }
    }


}

