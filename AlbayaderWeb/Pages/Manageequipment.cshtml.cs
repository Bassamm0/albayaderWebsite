using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Entity;
using System.Net.Http.Headers;

namespace AlbayaderWeb.Pages
{

    public class ManageequipmentModel : PageModel
    {
        AppConfiguration AppConfig = new AppConfiguration();
        public string? apiurl { get; set; }
        public string? uploadurl { get; set; }
        public string token { get; set; }
        public string email { get; set; }


        public EEquipments? _Equipment = new EEquipments();
        public EEquipments? postEquipment = new EEquipments();
        public string errorMessage { get; set; }
        public string pageTitle { get; set; }
        public string PageActionMode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public bool editMode { get; set; } = false;
        public string role { get; set; }



        public async Task<IActionResult> OnGetSmode(string Smode, int id)
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
                pageTitle = "Add Equipment";
                editMode = false;
            }
            else if (PageActionMode == "Edit")
            {
                pageTitle = "Edit Equipment";
                _Equipment = await getEquipment(id);
                editMode = true;

            }
            return null;
        }


        public async Task<EEquipments> getEquipment(int id)
        {

            apiurl = AppConfig.APIUrl;
            var parameters = new Dictionary<string, int>();
            parameters["id"] = id;
            var json = JsonConvert.SerializeObject(parameters);

            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "data/getEquipmentbyid", data))
                {
                    // string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string responseJson = response.Content.ReadAsStringAsync().Result;

                        _Equipment = JsonConvert.DeserializeObject<EEquipments>(responseJson);
                        //return response.StatusCode.ToString();
                    }
                    else
                    {

                        errorMessage = response.Content.ReadAsStringAsync().Result;
                        //  return response.StatusCode.ToString();
                    }



                }
            }


            return _Equipment;
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
                    postEquipment.Name = Request.Form["EquipmentName"];

                    
                    postEquipment.EquipmentId = Convert.ToInt16(Request.Form["hdEquipmentId"]);
                    statusCode = await addEquipment(postEquipment);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("Equipments", null);
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
                    postEquipment.Name = Request.Form["EquipmentName"];

                   
                    postEquipment.EquipmentId = Convert.ToInt16(Request.Form["hdEquipmentId"]);
                    statusCode = await updateEquipment(postEquipment);
                    if (statusCode == "OK")
                    {
                        return RedirectToPage("Equipments", null);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }




        private async Task<string> addEquipment(EEquipments Equipment)
        {

            string apiurl = AppConfig.APIUrl;

            var json = JsonConvert.SerializeObject(Equipment);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "data/addequipment", data))
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





        private async Task<string> updateEquipment(EEquipments Equipment)
        {

            string apiurl = AppConfig.APIUrl;
            var json = JsonConvert.SerializeObject(Equipment);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.PostAsync(apiurl + "data/updateequipment", data))
                {
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
