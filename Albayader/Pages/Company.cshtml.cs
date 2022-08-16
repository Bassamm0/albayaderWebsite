using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace AlbayaderWeb.Pages
{
    public class CompanyModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string errorMessage { get; set; }
        public List<ECompanies>? companayList = null;

        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }

        public string token { get; set; }

        public string role { get; set; }

        public async Task<IActionResult> OnGet()
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
            companayList = await getAllCompanies();
            return null;
        }

        public IActionResult OnPostAddCompany()
        {
         
            return RedirectToPage("ManageCompany","Smode", new { Smode = "Add",id=0 });
         
        }
        public IActionResult OnPostEditCompany(string id)
        {

            return RedirectToPage("ManageCompany", "Smode", new { Smode = "Edit",id=id });

        }
        
            public async Task<List<ECompanies>> getAllCompanies()
        {

            // if user admin


            apiurl = AppConfig.APIUrl;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);
              
                using (var response = await httpClient.GetAsync(apiurl+"company/all"))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;
                        
                        companayList = JsonConvert.DeserializeObject<List<ECompanies>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                      //  return response.StatusCode.ToString();
                    }



                }
            }


            return companayList;
        }


        public async  Task<IActionResult> OnPost()
        {
            //delete 
            int id = Convert.ToInt16(Request.Form["deletedCompanyId"]);

            if(id == 0)
            {
                return Page();
            }
            string statusCode =await deletCompany(id);



            return Page();

        }

        public async Task<IActionResult> OnPostDeleteCompany(int id)
        {
            //delete 
            // int companyid = Convert.ToInt16(Request.Form["deletedCompanyId"]);
            token = HttpContext.Session.GetString("token");
            if (id == 0)
            {
                return Page();
            }
           string statusCode = await deletCompany(id);

            return null;

        }



        public async Task<string> deletCompany(int id)
        {

            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl+"company/remove", data))
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
