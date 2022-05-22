using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
namespace AlbayaderWeb.Pages
{
    public class branchsModel : PageModel
    {
        public string errorMessage { get; set; }
        public List<EBranchs>? branchs = null;
        public async Task<IActionResult> OnGet(int companyid)
        {
            branchs = await getAllCompanyBranchs(companyid);
            return null;
        }

        public async Task<List<EBranchs>> getAllCompanyBranchs(int companyid)
        {

            // if user admin
            var parameters = new Dictionary<string, int>();
            parameters["companyid"] = companyid;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");



            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7174/api/branch/companybranchs",data))
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

    }
}
