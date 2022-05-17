using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json;

namespace AlbayaderWeb.Pages
{
    public class CompanyModel : PageModel
    {

        public string errorMessage { get; set; }
        public List<ECompanies>? companayList = null;


     


        public async Task<IActionResult> OnGet()
        {
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
         
            
           

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7174/api/company/all"))
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



    }
}
