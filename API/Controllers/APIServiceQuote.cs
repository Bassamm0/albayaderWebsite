using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;

using System.Security.Claims;
namespace API.Controllers
{
    [Route("api/servicequote")]
    [ApiController]
    public class APIServiceQuote : ControllerBase
    {
        private ServiceQuoteLogic ServiceQuoteLogic = new ServiceQuoteLogic();

        [Route("all")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpGet]
        public async Task<List<EServiceQuote>> getAllServiceQuote()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceQuote> ServiceQuote = new List<EServiceQuote>();
            ServiceQuote = await ServiceQuoteLogic.getAllServiceQuote(logeduser);

            return ServiceQuote;
        }
        [Route("allbydate")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<List<EServiceQuote>> getAllServiceQuoteByDate(JsonElement objData)
        {


            string startDate = objData.GetProperty("startDate").GetString();
            string endDate = objData.GetProperty("endDate").GetString();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EServiceQuote> ServiceQuote = new List<EServiceQuote>();
            ServiceQuote = await ServiceQuoteLogic.getAllServiceQuoteByDate(logeduser,startDate,endDate);

            return ServiceQuote;
        }


        [Route("companyServiceQuote")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<List<EServiceQuote>> getAllCompanyServiceQuote([FromBody] JsonElement objData)
        {
            int companyid = objData.GetProperty("companyid").GetInt16();
            List<EServiceQuote> ServiceQuote = new List<EServiceQuote>();
            if (companyid > 0)
            {
                ServiceQuote = await ServiceQuoteLogic.getAllCompanyServiceQuote(companyid);

            }

            return ServiceQuote;
        }

        [Route("getservicequotebyid")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<EServiceQuote> getServiceQuoteById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            EServiceQuote ServiceQuote = new EServiceQuote();
            ServiceQuote = await ServiceQuoteLogic.getServiceQuoteById(_id);
            if (ServiceQuote == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return ServiceQuote;
        }

        


        [Route("add")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<Boolean> addServiceComment([FromBody] EServiceQuote newEServiceQuote)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            bool result = false;

            try
            {
                if (newEServiceQuote != null)
                {
                    result = await ServiceQuoteLogic.addServiceQuote(newEServiceQuote,logeduser);

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
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpPost]
        public async Task<Boolean> updateServiceQuote([FromBody] EServiceQuote newEServiceQuote)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            bool result = false;

            try
            {

                if (newEServiceQuote != null)
                {
                    result = await ServiceQuoteLogic.updateServiceQuote(newEServiceQuote, logeduser);

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

        [Route("delete")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> deleteServiceQuote([FromBody] JsonElement objData)

        {
            var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await ServiceQuoteLogic.deleteServiceQuote(Id);

            return result;
        }
        [Route("remove")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> removeServiceQuote([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await ServiceQuoteLogic.removeServiceQuote(id);
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
