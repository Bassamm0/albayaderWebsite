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
    [Route("api/servicecomment")]
    [Authorize(Roles = "Administrator,Manager,Client Manager")]
    [ApiController]
    public class APIServiceComment : ControllerBase
    {

        private  ServiceCommentLogic serviceCommentLogic= new ServiceCommentLogic();

        

        [Route("all")]
        [HttpPost]
        public async Task<List<EServiceComment>> getAllServiceComment([FromBody] JsonElement objData)
        {
            int serviceid = objData.GetProperty("serviceid").GetInt16();
            List<EServiceComment> serviceComment = new List<EServiceComment>();
            if (serviceid > 0)
            {
                serviceComment = await serviceCommentLogic.getAllServiceComment(serviceid);

            }

            return serviceComment;
        }

        [Route("single")]
        [HttpPost]
        public async Task<EServiceComment> getSingleComment([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            EServiceComment serviceComment = new EServiceComment();
            serviceComment = await serviceCommentLogic.getSingleServiceComment(_id);
            if (serviceComment == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return serviceComment;
        }

        [Route("add")]
        [HttpPost]
        public async Task<Boolean> addServiceComment([FromBody] EServiceComment newServiceComment)
        {

            bool result = false;
            try
            {
                if(newServiceComment != null)
                {
                    result = await serviceCommentLogic.addServiceComment(newServiceComment);

                }

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
        public async Task<Boolean> updateServiceComment([FromBody] EServiceComment newServiceComment)
        {

            bool result = false;

            try
            {

                if (newServiceComment != null)
                {
                    result = await serviceCommentLogic.updateServiceComment(newServiceComment);

                }

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
       
        [Route("remove")]
        [HttpPost]
        public async Task<Boolean> removeBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await serviceCommentLogic.removeServiceComment(id);
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
