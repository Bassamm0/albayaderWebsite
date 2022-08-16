using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace AlbayaderWeb.Pages
{
    public class ManageQuoteModel : PageModel
    {

        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }

         public EServiceQuote? _Quote = null;
        public EServiceQuote? _postQuote = new EServiceQuote();
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;
        public string role { get; set; }

        public async Task<IActionResult> OnGet(string Smode, int qid)
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
            PageActionMode = Smode;
            if (PageActionMode == "Add")
            {
                pageTitle = "Add Quote";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit Quote";
                _Quote = await getSingleQuote(qid);
                editMode = true;

            }
            return null;
        }

        public async Task<EServiceQuote> getSingleQuote(int id)
        {

            apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "servicequote/getservicequotebyid", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _Quote = JsonConvert.DeserializeObject<EServiceQuote>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _Quote;
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
                    //_postQuote.ServiceQuoteId = Convert.ToInt16(Request.Form["hdServiceQuoteId"]);
                    _postQuote.ServiceQuoteDate = DateTime.UtcNow.ToString();

                    string materialCount = Request.Form["itemsids"];
                   

                    int[] nums = Array.ConvertAll(materialCount.Split(','), int.Parse);


                    if(nums.Length == 0)
                    {
                        errorMessage = "Please add at least one Item";
                        return null;
                    }
                    List<EQuotationDetails> lQdetails = new List<EQuotationDetails>();
                    for (int i = 0; i < nums.Length; i++)
                    {
                        EQuotationDetails _qdetails = new EQuotationDetails();
                        _qdetails.MaterialId = Convert.ToInt16(Request.Form["Material" + nums[i]].ToString());

                        if (!String.IsNullOrEmpty(Request.Form["price" + nums[i]]))
                        {
                            _qdetails.QuotationPrice = Convert.ToInt16(Request.Form["price" + nums[i]].ToString());
                        }
                        if (!String.IsNullOrEmpty(Request.Form["qty" + nums[i]]))
                        {
                            _qdetails.Qty = Convert.ToInt16(Request.Form["qty" + nums[i]].ToString());
                        }
                        _qdetails.Description = Request.Form["description" + nums[i]].ToString();
                        
                        lQdetails.Add(_qdetails);

                    }
                    _postQuote.BranchId = Convert.ToInt16(Request.Form["ddBranch"]);
                    _postQuote.ServiceId = Convert.ToInt16(Request.Form["ddService"]);
                    _postQuote.ReferenceId = Request.Form["ReferenceId"];
                    _postQuote.ServiceQuoteFile = Request.Form["uploadedfile"];
                    _postQuote.QouteDetails = lQdetails;


                    statusCode = await addQuote(_postQuote);
                    if (statusCode == "OK")
                    {

                        return RedirectToPage("quote");
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
                    _postQuote.ServiceQuoteId = Convert.ToInt16(Request.Form["hdServiceQuoteId"]);
                    _postQuote.ServiceQuoteDate = DateTime.Now.ToString();
 
                    string materialCount = Request.Form["itemsids"];
                    

                    int[] nums = Array.ConvertAll(materialCount.Split(','), int.Parse);

                    if (nums.Length == 0)
                    {
                        errorMessage = "Please add at least one Item";
                        return null;
                    }
                    List<EQuotationDetails> lQdetails=new List<EQuotationDetails>();
                    for (int i = 0; i < nums.Length; i++)
                    {
                        EQuotationDetails _qdetails = new EQuotationDetails();
                        _qdetails.MaterialId = Convert.ToInt16(Request.Form["Material"+ nums[i]].ToString());
                        if (!String.IsNullOrEmpty(Request.Form["price" + nums[i]]))
                        {
                            _qdetails.QuotationPrice = Convert.ToInt16(Request.Form["price" + nums[i]].ToString());
                        }
                        if (!String.IsNullOrEmpty(Request.Form["qty" + nums[i]]))
                        {
                            _qdetails.Qty = Convert.ToInt16(Request.Form["qty" + nums[i]].ToString());
                        }
                        _qdetails.Description =Request.Form["description" + nums[i]].ToString();
                        _qdetails.OpId = 1;
                        _qdetails.ServiceQuoteId= Convert.ToInt16(Request.Form["hdServiceQuoteId"]);
                        lQdetails.Add(_qdetails);

                    }
                    _postQuote.BranchId = Convert.ToInt16(Request.Form["ddBranch"]);
                    _postQuote.ServiceId = Convert.ToInt16(Request.Form["ddService"]);
                    _postQuote.ReferenceId = Request.Form["ReferenceId"];
                    _postQuote.ServiceQuoteFile = Request.Form["uploadedfile"];
                    _postQuote.QouteDetails = lQdetails;
                    _postQuote.EndDate = DateTime.Now;
                    _postQuote.OpId = 1;

                        statusCode = await updateQuote(_postQuote);
                    if (statusCode == "OK")
                    {

                        return RedirectToPage("quote");
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }




        private async Task<string> addQuote(EServiceQuote serviceQuote)
        {

            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(serviceQuote);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "servicequote/add", data))
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



        //// edit company





        private async Task<string> updateQuote(EServiceQuote serviceQuote)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(serviceQuote);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "servicequote/update", data))
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
}
