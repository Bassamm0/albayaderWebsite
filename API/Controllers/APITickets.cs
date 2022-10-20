using Entity;
using LOGIC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DAL.DALException;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class APITickets : ControllerBase
    {
        private ticketLogic ticketLogic = new ticketLogic();

        [Route("all")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpGet]
        public async Task<List<EticketViews>> getAlltickets()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAlltickets(logeduser);

            return tickets;
        }
        [Route("open")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpGet]
        public async Task<List<EticketViews>> getAllOpentickets()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAllOpentickets(logeduser);

            return tickets;
        }
        [Route("closed")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpGet]
        public async Task<List<EticketViews>> getAllClosedtickets()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAllClosedtickets(logeduser);

            return tickets;
        }

        [Route("companytickets")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpPost]
        public async Task<List<EticketViews>> getAllCompanytickets([FromBody] JsonElement objData)
        {
            int companyid = objData.GetProperty("companyid").GetInt16();
            List<EticketViews> tickets = new List<EticketViews>();
            if (companyid > 0)
            {
                tickets = await ticketLogic.getAllCompanytickets(companyid);

            }

            return tickets;
        }

        [Route("getticketById")]
      [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpPost]
        public async Task<EticketViews> getticketById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            EticketViews ticket = new EticketViews();
            ticket = await ticketLogic.getticketById(_id);
            if (ticket == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return ticket;
        }

        [Route("add")]
       [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> addticket([FromBody] Etickets ticket)
        {

            bool result = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticket.createdBy = logeduser.UserId;
            try
            {
               
                result = await ticketLogic.addticket(ticket);
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
       // [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> updateticket([FromBody] Etickets ticket)
        {

            bool result = false;

            try
            {
                              
                result = await ticketLogic.updateticket(ticket);
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
