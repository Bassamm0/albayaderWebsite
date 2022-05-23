using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{

    public class ManageBranchModel : PageModel
    {
       public branch _Postbranch=new branch();
        public EBranchs? _branch = null;
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


        public async Task<IActionResult> OnGetSmode(string Smode, int id,string companyname,int companyid)
        {
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


            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/Branch/getBranchById", data))
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

            string statusCode = "";
            PageActionMode = Request.Form["Smode"];
            if (PageActionMode == "Add")
            {
                try
                {
                    _Postbranch.branchname = Request.Form["BranchName"];
                    
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

            var json = JsonConvert.SerializeObject(branch);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/branch/add", data))
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

            var json = JsonConvert.SerializeObject(branch);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/branch/update", data))
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
        }
    }


}

