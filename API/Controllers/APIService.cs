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
    [Route("api/services")]
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

        [Route("getservicebyid")]
        [HttpGet]
        public async Task<EServiceModel> getSingleService(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EServiceModel services = new EServiceModel();
            services = await serviceLogic.getSingleService(_id);

            return services;
        }


        [Route("add")]
        [HttpPost]
        public async Task<Boolean> addService([FromBody] EServices service)
        {

            bool result = false;
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
                return false;
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




    }
}
