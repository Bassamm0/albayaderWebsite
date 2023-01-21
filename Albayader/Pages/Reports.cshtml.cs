using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.ComponentModel.Design;

namespace AlbayaderWeb.Pages
{
    public class ReportsModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string role { get; set; }
        public string email { get; set; }
       // public List<EServiceModel> _services = new List<EServiceModel>();
        public string errorMessage { get; set; }
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
            if (role.ToLower() != "administrator" && role.ToLower() != "manager" && role.ToLower() != "client manager")
            {
                return Redirect("Index");
            }
            apiurl = AppConfig.APIUrl;
            uploadurl = AppConfig.UploadURL;

            //_services = await getAllCompletedservices();


            return null;
        }


        public async Task<IActionResult> OnPost(string handler, string serviceId, string newDate)
        {
            token = HttpContext.Session.GetString("token");

            if (handler == "changeDate")
            {

            }
            if (serviceId == "0" || newDate == "")
            {
                return Page();
            }
            string statusCode = await changeserviceDate(serviceId, newDate);
           
            return Page();
        }
      
        public async Task<string> changeserviceDate(string serviceId,string newDate)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, string>();
            parameters["serviceId"] = serviceId;
            parameters["newDate"] = newDate;

            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/updateservicedate", data))
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




        public async Task<IActionResult> OnPostDeleteService(int id)
        {

            token = HttpContext.Session.GetString("token");
            if (id == 0)
            {
                return Page();
            }
            string statusCode = await deletService(id);

            return null;

        }
        public async Task<string> deletService(int id)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/remove", data))
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


        public async Task<IActionResult> OnPostChangeBranch(string serviceId, string branchId)
        {

            token = HttpContext.Session.GetString("token");
            if (branchId == "")
            {
                return Page();
            }
            string statusCode = await changeBranch(serviceId, branchId);

            return null;

        }
        public async Task<string> changeBranch(string serviceId, string branchId)
        {
            string apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, string>();
            parameters["serviceId"] = serviceId;
            parameters["branchId"] = branchId;

            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/updateservicebranch", data))
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


        public async Task<IActionResult> OnPostDownloadfile(string sType, string visitType, string branch, string searchValue,string startDate,string endDate)
        {
            //delete 
            // int companyid = Convert.ToInt16(Request.Form["deletedCompanyId"]);

            List<EServiceModel> services = new List<EServiceModel>();
            if (!string.IsNullOrEmpty(startDate)&& !string.IsNullOrEmpty(endDate))
            {

                services = await downloadByDate( startDate,  endDate);
            }
            else
            {

                services = await download();
            }




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

            return File(temp, "application/vnd.ms-excel", "report.xls");

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

        public async Task<List<EServiceModel>> downloadByDate(string startDate,string endDate)
        {
            token = HttpContext.Session.GetString("token");
            List<EServiceModel> sr = new List<EServiceModel>();
            string apiurl = AppConfig.APIUrl;

            var parameters = new Dictionary<string, string>();
            parameters["startDate"] = startDate;
            parameters["endDate"] = endDate;
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "service/completedservicedate", data))
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
