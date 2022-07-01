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
    [Route("api/email")]
    [ApiController]
    public class APISendemail : ControllerBase
    {

        [Route("send")]
        [Authorize]
        [HttpPost]
        public async Task<Boolean> sendEmail()
        {
           
            bool result = false;
            try
            {
                EmailLogic emailLogic = new EmailLogic();

                result = await emailLogic.sendEmail();
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
