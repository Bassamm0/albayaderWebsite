using DAL.Functions;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using static AlbayaderWeb.Pages.ManageBranchModel;

namespace AlbayaderWeb.Pages
{
    public class testModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? storeStartDate { get; set; }
        public string? storeEndDate { get; set; }
        public string? uploadurl { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string timezone { get; set; }


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
                timezone = HttpContext.Session.GetString("timezone");


            }

            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;
   
            return null;
        }

      

        public async Task<IActionResult> OnPostDownloadfile(string sType,string visitType,string branch,string searchValue)
        {
            //delete 
            // int companyid = Convert.ToInt16(Request.Form["deletedCompanyId"]);

            List<EServiceModel> services = new List<EServiceModel>();
             services = await download();



            var result = from s in services select s; ;

            if (sType != "All Type")
            {
                result = from s in services
                         where s.ServiceTypeName == sType
                         select s;
            }

            if (visitType != "All Site Vist")
            {
                result = from s in result
                         where s.VistTypeName == visitType
                         select s;
            }
            if (branch != "All Branch")
            {
                result = from s in result
                         where s.BranchName.ToLower().Contains(branch.ToLower())
                         select s;
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                result = result.Where(m => m.ServiceId.ToString().Contains(searchValue.ToLower())
                                            || m.BranchName.ToLower().Contains(searchValue.ToLower())
                                            || m.ServiceTypeName.ToLower().Contains(searchValue.ToLower())
                                            || m.VistTypeName.ToLower().Contains(searchValue.ToLower())
                                            || m.Remark.ToLower().Contains(searchValue.ToLower()));
            }


            StringBuilder str = new StringBuilder();
            str.Append("<table border=\"1px\" >");
            str.Append("<tr style=\"background:#f3f3f3\">");
            str.Append("<td><b><font face=Arial Narrow size=3>Ref</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Branch Name</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Service Type </font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Vist Type</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Completion Date</font></b></td>");
            str.Append("</tr>");
            foreach (EServiceModel val in result)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ServiceId.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.BranchName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ServiceTypeName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.VistTypeName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + Convert.ToDateTime(val.CompletionDate).ToString("dd/MM/yyyy hh:mm tt") + "</font></td>");
                str.Append("</tr>");
            }
            str.Append("</table>");

            HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());

            return File(temp, "application/vnd.ms-excel", "test.xls");

        }
     
        public async Task<List<EServiceModel>> download()
        {
            token = HttpContext.Session.GetString("token");
            List<EServiceModel> sr = new List<EServiceModel>();
            string apiurl = AppConfig.APIUrl;
           

 

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(apiurl + "service/completedservice"))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;
                       return sr = JsonConvert.DeserializeObject<List<EServiceModel>>(responseJson);
                        //string res = JsonConvert.DeserializeObject<string>(responseJson);
                       
                    }
                    else
                    {

                       
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return sr;
        }
    }
}
