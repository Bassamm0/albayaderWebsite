using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Entity;

namespace AlbayaderWeb.Pages
{
    public class materialsModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string errorMessage { get; set; }
        public List<EMaterials>? materials = null;
         public string? title { get; set; }
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
            materials = await gatAllMaterial();
            return null;
        }

        public IActionResult OnPostAddMaterial()
        {

            return RedirectToPage("ManageMaterial", "Smode", new { Smode = "Add", id = 0 });

        }
        public IActionResult OnPostEditMaterial(string id)
        {

            return RedirectToPage("ManageMaterial", "Smode", new { Smode = "Edit", id = id });

        }

        public async Task<List<EMaterials>> gatAllMaterial()
        {

            // if user admin


            apiurl = AppConfig.APIUrl;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.GetAsync(apiurl + "material/all"))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        materials = JsonConvert.DeserializeObject<List<EMaterials>>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return materials;
        }
        public async Task<IActionResult> OnPost()
        {
            //delete 
            int id = Convert.ToInt16(Request.Form["deletedMaterialId"]);

            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletMaterial(id);



            return Page();

        }


        public async Task<IActionResult> OnPostDeleteMaterial(int id)
        {
            //delete 
            // int companyid = Convert.ToInt16(Request.Form["deletedCompanyId"]);

            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletMaterial(id);

            return null;

        }

        public async Task<string> deletMaterial(int id)
        {
            token = HttpContext.Session.GetString("token");

            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "material/remove", data))
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
