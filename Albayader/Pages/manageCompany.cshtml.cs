using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{
    public class ManageCompany : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }

        public company _company = new company();

        public ECompanies? _companay = null;
        public string token { get; set; }
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public bool editMode { get; set; } = false;


        public void OnGet()
        {
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
        }

        public async Task<IActionResult> OnGetSmode(string Smode, int id)
        {
            PageActionMode = Smode;
            if (PageActionMode == "Add")
            {
                pageTitle = "Add Company";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit Company";
                _companay = await getCompanyById(id);
                editMode = true;

            }
            return null;
        }
        
        public async Task<IActionResult> OnPost()
        {

            string statusCode = "";
            PageActionMode = Request.Form["Smode"];
            if (PageActionMode == "Add")
            {
                try
                {
                    _company.name = Request.Form["CompanyName"];
                    _company.countrid = Convert.ToInt16(Request.Form["ddCountry"]);
                    _company.city = Request.Form["city"];
                    _company.street = Request.Form["street"];
                    _company.streetno = Request.Form["streetno"];
                    _company.telephone = Request.Form["tel"];
                    _company.fax = Request.Form["fax"];
                    if (!String.IsNullOrEmpty(Request.Form["latitude"]))
                    {
                        _company.latitude = Convert.ToDecimal(Request.Form["latitude"]);
                    }
                    if (!String.IsNullOrEmpty(Request.Form["longitude"]))
                    {
                        _company.longitude = Convert.ToDecimal(Request.Form["longitude"]);
                    }
                    _company.companylogo = Request.Form["uploadedfile"];
                    _company.companytypeid = 2;

                    statusCode = await addCompany(_company);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("company");
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
                    _company.companyid = Convert.ToInt16(Request.Form["hdCompanyId"]);
                    _company.name = Request.Form["CompanyName"];
                    _company.countrid = Convert.ToInt16(Request.Form["ddCountry"]);
                    _company.city = Request.Form["city"];
                    _company.street = Request.Form["street"];
                    _company.streetno = Request.Form["streetno"];
                    _company.telephone = Request.Form["tel"];
                    _company.fax = Request.Form["fax"];
                    if (!String.IsNullOrEmpty(Request.Form["latitude"]))
                    {
                        _company.latitude = Convert.ToDecimal(Request.Form["latitude"]);
                    }
                    if (!String.IsNullOrEmpty(Request.Form["longitude"]))
                    {
                        _company.longitude = Convert.ToDecimal(Request.Form["longitude"]);
                    }
                    _company.companylogo = Request.Form["uploadedfile"];
                    _company.companytypeid = 2;

                    statusCode = await updateCompany(_company);
                    if(statusCode == "OK")
                    {
                        return RedirectToPage("company");
                    }
                }
                catch (Exception ex)
                {

                }



            }


            return null;


        }




        private async Task<string> addCompany(company company)
        {
            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(company);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiurl+"company/add", data))
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

        public async Task<ECompanies> getCompanyById(int id)
        {

            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiurl+"company/getCompanyById", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _companay = JsonConvert.DeserializeObject<ECompanies>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _companay;
        }



        private async Task<string> updateCompany(company company)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(company);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiurl+"company/update", data))
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
    public class company
    {
        public int companyid { get; set; }
        public int countrid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string streetno { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string companylogo { get; set; }
        public int companytypeid { get; set; }

    }
}
