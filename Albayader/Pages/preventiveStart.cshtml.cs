using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlbayaderWeb.Pages
{
    public class preventiveStartModel : PageModel
    {
        public string ?ServiceType { get; set; }
        public string? companyId { get; set; }
        public void OnGet(string serviceType,int companyId)
        {
            string ServiceType=serviceType;
        }

        public async Task<IActionResult> OnPost()
        {


            int BranchId=Convert.ToInt16(Request.Form["ddBranch"]);
            string type =Request.Form["serviceType"];

           string url= createSearvice(type);
            return Redirect(url);
        }


        public string createSearvice(string type)
        {


            string serviceId = "1";
            string url = "";
            // create service and redirect with service ID
            if (type == "preventive")
            {
                url = "Preventive?service="+serviceId;
            }
            else if(type == "corrective")
            {
                url = "corrective?service=" + serviceId;
            }
            else
            {
                url = "other?service=" + serviceId;
            }



            return url ;
        }
      }
}
