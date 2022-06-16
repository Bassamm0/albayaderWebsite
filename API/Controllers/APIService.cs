using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;


namespace API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class APIService : ControllerBase
    {

        private ServiceLogic serviceLogic = new ServiceLogic();

        [Route("all")]
        [HttpGet]
        public async Task<List<EServiceModel>> getAllService()
        {
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllService();

            return services;
        }
        [Route("allByStatus")]
        [HttpGet]
        public async Task<List<EServiceModel>> getAllServiceByStatus(JsonElement objData)
        {
            int StatusId = objData.GetProperty("id").GetInt16();
            List<EServiceModel> services = new List<EServiceModel>();
            services = await serviceLogic.getAllServiceByStatus(StatusId);

            return services;
        }

        [Route("getservicebyid")]
        [HttpPost]
        public async Task<EServiceModel> getSingleService(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EServiceModel services = new EServiceModel();
            services = await serviceLogic.getSingleService(_id);

            return services;
        }


        [Route("add")]
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
        [HttpPost]
        public async Task<Boolean> updateStatus([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var serviceId = objData.GetProperty("serviceId").GetInt16();
                var statusId = objData.GetProperty("statusId").GetInt16();
                var remark = objData.GetProperty("remark").GetString();


                result = await serviceLogic.updateStatus(serviceId, statusId,remark);
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


                result = await serviceLogic.clientSignature(serviceId, SupervisourSignature, SupervisourName, SupervisourFeedback);
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
