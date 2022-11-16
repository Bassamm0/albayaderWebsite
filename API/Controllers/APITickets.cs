using Entity;
using LOGIC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DAL.DALException;
using System.Data;
using System.Security.Claims;
using System.Text.Json;
using API.Hubs;
using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using static Org.BouncyCastle.Math.EC.ECCurve;
using LOGIC.UserLogic;

namespace API.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class APITickets : ControllerBase
    {
        private readonly IHubContext<TicketHub> _hubContext;
        private ticketLogic ticketLogic = new ticketLogic();
        private TicketLogLogic ticketLogLogic = new TicketLogLogic();
       

        public APITickets(IHubContext<TicketHub> hubContext)
        {
            _hubContext = hubContext;
           
        }

        [Route("all")]
       [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize]
        [HttpGet]
        public async Task<List<EticketViews>> getAllOpentickets()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAllOpentickets(logeduser);

            return tickets;
        }
        [Route("openforview")]

        [HttpGet]
        public async Task<List<EticketViews>> getAllOpenticketsview()
        {

          
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAllOpenticketsview();

            return tickets;
        }
        [Route("opendate")]
        [Authorize]
        [HttpPost]
        public async Task<List<EticketViews>> getAllOpenticketsDate([FromBody] JsonElement objData)
        {
            var startDate = objData.GetProperty("startDate").GetString();
            var endDate = objData.GetProperty("endDate").GetString();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<EticketViews> tickets = new List<EticketViews>();
            tickets = await ticketLogic.getAllOpenticketsDate(logeduser,startDate,endDate);

            return tickets;
        }
        [Route("closed")]
        [Authorize]
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
        [Authorize]
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

        [Route("ticketdetails")]
      [Authorize]
        [HttpPost]
        public async Task<EticketViewsDetails> getticketById([FromBody] JsonElement objData)
        {
            int ticketId = objData.GetProperty("ticketId").GetInt16();
            EticketViewsDetails ticket = new EticketViewsDetails();
            ticket = await ticketLogic.getticketById(ticketId);
            if (ticket == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return ticket;
        }

       [Route("add")]
       [Authorize]
        [HttpPost]
        public async Task<Etickets> addticket([FromBody] Etickets ticket)
        {

            var result =new Etickets();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticket.createdBy = logeduser.UserId;
            try
            {
               
                result = await ticketLogic.addticket(ticket, logeduser);
                await _hubContext.Clients.All.SendAsync("newTicketAdded", result,logeduser);
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
        [Route("addticketclient")]
        [Authorize]
        [HttpPost]
        public async Task<Etickets> addticketClient([FromBody] Etickets ticket)
        {

            var result = new Etickets();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            
            try
            {

                result = await ticketLogic.addticketclient(ticket, logeduser);
                await _hubContext.Clients.All.SendAsync("newTicketAdded", result, logeduser);
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

        

        [Route("ticketLog")]
        [Authorize]
        [HttpPost]
        public async Task<List<EticketLog>> getTicketLog([FromBody] JsonElement objData)
        {
            int ticketId = objData.GetProperty("ticketId").GetInt16();
            List<EticketLog> ticketLogs = new List<EticketLog>();
            if (ticketId > 0)
            {
                ticketLogs = await ticketLogLogic.getticketLog(ticketId);

            }

            return ticketLogs;
        }



        [Route("addlog")]
        [Authorize]
        [HttpPost]
        public async Task<EticketLog> addticketlog([FromBody] EticketLog ticketlog)
        {

            EticketLog result = new EticketLog();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticketlog.UserId = logeduser.UserId;
            ticketlog.CreationDate = DateTime.UtcNow;
            try
            {
                result = await ticketLogLogic.addticketLog(ticketlog,logeduser);
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
                return result;
            }
            return result;
        }


        [Route("changestatus")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Support,Supervisor,Client User")]
        [HttpPost]
        public async Task<Boolean> changeStatus([FromBody] EticketAndStatus ticketAndStatus)
        {

            bool result = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticketAndStatus.UserId = logeduser.UserId;
            try
            {
                result = await ticketLogic.insertNewStatus(ticketAndStatus);
                await _hubContext.Clients.All.SendAsync("statusChanged", ticketAndStatus, logeduser);
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

        [Route("closeticket")]
        [Authorize]
        [HttpPost]
        public async Task<Boolean> closeticket([FromBody] EticketAndStatus ticketAndStatus)
        {

            bool result = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticketAndStatus.UserId = logeduser.UserId;
            ticketAndStatus.ticketStatusId = 7;
            try
            {
                result = await ticketLogic.insertNewStatus(ticketAndStatus);
                await _hubContext.Clients.All.SendAsync("statusChanged", ticketAndStatus, logeduser);
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
        [Route("assignuser")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Support")]
        [HttpPost]
        public async Task<Boolean> assginTicketuser([FromBody] EticketAndUser ticketAndUser)
        {

            bool result = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticketAndUser.UserId = logeduser.UserId;
            try
            {
                result = await ticketLogic.assginTicketuser(ticketAndUser, logeduser);
                UserLogic logicuser = new UserLogic();

                EUser assigneduser = await logicuser.getUserById(ticketAndUser.AssginUserId);

                await _hubContext.Clients.All.SendAsync("assginTicketUser", ticketAndUser, assigneduser, logeduser);
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

        [Route("createservice")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Support")]
        [HttpPost]
        public async Task<Boolean> createTicketService([FromBody] EticketAndService ticketAndService)
        {

            bool result = false;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            ticketAndService.userId = logeduser.UserId;
            try
            {
                result = await ticketLogic.createTicketService(ticketAndService);
                await _hubContext.Clients.All.SendAsync("createTicketService", ticketAndService, logeduser);

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
