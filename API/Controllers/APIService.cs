using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;
using System.Security.Claims;
using System.IO;
using System.Text;

namespace API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class APIService : ControllerBase
    {

        private ServiceLogic serviceLogic = new ServiceLogic();

        [Route("all")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpGet]
        public async Task<List<EServiceModel>> getAllService()
        {
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllService();

            return services;
        }
        [Route("allByStatus")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<List<EServiceModel>> getAllServiceByStatus(JsonElement objData)
        {
            int StatusId = objData.GetProperty("id").GetInt16();
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllServiceByStatus(StatusId);

            return services;
        }
        [Route("completedservice")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpGet]
        public async Task<List<EServiceModel>> getAllCompletedService()
        {



            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllCompletedService(logeduser);

            return services;
        }


        [Route("servicereport")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task <IActionResult> getServiceReport()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getServiceReport(logeduser);


            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sType = Request.Form["sType"].FirstOrDefault();
            var visitType = Request.Form["visitType"].FirstOrDefault();
            var branch = Request.Form["branch"].FirstOrDefault();

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var result= from s in services select s; ;

            if (sType!= "All Type")
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


            recordsTotal = result.Count();
            var data = result.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };



            return Ok(jsonData);
        }



        [Route("servicereportdatefilter")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<IActionResult> getServiceReportdateFilter()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceModel> services = new List<EServiceModel>();

            var startDate = Request.Form["startDate"].FirstOrDefault();
            var endDate = Request.Form["endDate"].FirstOrDefault();

            services = await serviceLogic.getAllCompletedServiceByDate(logeduser, startDate,endDate);


            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sType = Request.Form["sType"].FirstOrDefault();
            var visitType = Request.Form["visitType"].FirstOrDefault();
            var branch = Request.Form["branch"].FirstOrDefault();

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

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


            recordsTotal = result.Count();
            var data = result.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };



            return Ok(jsonData);
        }

        [Route("exportexcel")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<ActionResult> exportexcel()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllCompletedService(logeduser);

            StringBuilder str = new StringBuilder();
            str.Append("<table border=\"1px\" >");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>serivceId</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>BranchName</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>completion Date</font></b></td>");
            str.Append("</tr>");
            foreach (EServiceModel val in services)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ServiceId.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.BranchName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.CompletionDate.ToString() + "</font></td>");
                str.Append("</tr>");
            }
            str.Append("</table>");

            HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
           
            // return System.Convert.ToBase64String(temp);

        }

        [Route("completedservicebyBranch")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
            [HttpPost]
        public async Task<List<EServiceModel>> getAllCompletedServiceByBranch(JsonElement objData)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            int barnchId = objData.GetProperty("barnchId").GetInt16();
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllCompletedServiceBranch(logeduser, barnchId);

            return services;
        }

       
        [Route("completedservicedate")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<List<EServiceModel>> getAllCompletedServiceByDate(JsonElement objData)
        {

            string startDate = objData.GetProperty("startDate").GetString();
            string endDate = objData.GetProperty("endDate").GetString();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllCompletedServiceByDate(logeduser,startDate,endDate);

            return services;
        }
        [Route("getservicebyid")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Client Manager")]
        [HttpPost]
        public async Task<EServiceModel> getSingleService(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EServiceModel services = new EServiceModel();
            services = await serviceLogic.getSingleService(_id);

            return services;
        }

        
        [Route("getcorrectiveservicebyid")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Client Manager")]
        [HttpPost]
        public async Task<ECorrectiveServiceModel> getCorrectiveSingleService(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            ECorrectiveServiceModel services = new ECorrectiveServiceModel();
            services = await serviceLogic.getCorrectiveSingleService(_id);

            return services;
        }

        [Route("add")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<EServices> addService([FromBody] EServices service)
        {

            var result = new EServices();

            try
            {
                result = await serviceLogic.addService(service);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
             
            }
            return result;
        }


        [Route("update")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> updateService([FromBody] EServices updatedService)

        {

            bool result = false;

            try
            {
                if (updatedService != null)
                {
                    result = await serviceLogic.updateService(updatedService);

                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Route("remove")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> removeService([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await serviceLogic.removeService(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Route("updatestatus")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> updateStatus([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var serviceId = objData.GetProperty("serviceId").GetInt16();
                var statusId = objData.GetProperty("statusId").GetInt16();
                var remark = objData.GetProperty("remark").GetString();
                var statusAfterId = objData.GetProperty("statusAfterId").GetInt16();
                var siteVistTypeId = objData.GetProperty("siteVistTypeId").GetInt16();
                var Recommendation = objData.GetProperty("recommendation").GetString();

                var serviceType = objData.GetProperty("serviceType").GetInt16();
                var serviceRender = "";
                var rootOfCause = "";
                if (serviceType == 1)
                {
                     serviceRender = objData.GetProperty("serviceRender").GetString();

                }
                else
                {
                     rootOfCause = objData.GetProperty("rootOfCause").GetString();

                }



                result = await serviceLogic.updateStatus(serviceId, statusId,remark, statusAfterId, siteVistTypeId, Recommendation, serviceRender,rootOfCause);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Route("clientsignature")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> clientSignature([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var serviceId = objData.GetProperty("serviceId").GetInt16();
                var SupervisourSignature = objData.GetProperty("supervisourSignature").GetString();
                var SupervisourName = objData.GetProperty("supervisourName").GetString();
                var SupervisourFeedback = objData.GetProperty("supervisourFeedback").GetString();
                var SupervisourMobile = objData.GetProperty("SupervisourMobile").GetString();
                var SupervisourDesignation = objData.GetProperty("SupervisourDesignation").GetString();


                result = await serviceLogic.clientSignature(serviceId, SupervisourSignature, SupervisourName, SupervisourFeedback,  SupervisourMobile,  SupervisourDesignation);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }
        [Route("updateservicedate")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<Boolean> updateServiceDate([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var serviceId = objData.GetProperty("serviceId").GetString();
                var newDate = objData.GetProperty("newDate").GetString();



                result = await serviceLogic.updateServiceDate(Convert.ToInt16(serviceId), newDate);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Route("updateservicebranch")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<Boolean> updateBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var serviceId = objData.GetProperty("serviceId").GetString();
                var branchId = objData.GetProperty("branchId").GetString();



                result = await serviceLogic.updateBranch(Convert.ToInt16(serviceId), Convert.ToInt16(branchId));
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }
    }
}
