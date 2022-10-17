using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/calendarevent")]
    [ApiController]
    public class APICalenderEvent : ControllerBase
    {

        private CalenderEventsLogic _calender = new CalenderEventsLogic();

        [Route("all")]
        [Authorize(Roles = "Administrator,Manager,Client Manager")]
        [HttpGet]
        public async Task<List<ECalenderEvents>> getAllCalenderEvents()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<ECalenderEvents> events = new List<ECalenderEvents>();
            events = await _calender.getAllCalenderEvents(logeduser);

            return events;
        }

        [Route("display")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpGet]
        public async Task<List<ECalenderEvents>> getAllCalenderEventsForDisplay()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            List<ECalenderEvents> events = new List<ECalenderEvents>();
            events = await _calender.getAllCalenderEventsForDisplay(logeduser);

            return events;
        }
        [Route("branchevent")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpPost]
        public async Task<List<ECalenderEvents>> getAllCompanyBranchs([FromBody] JsonElement objData)
        {
            int branchid = objData.GetProperty("branchid").GetInt16();
            List<ECalenderEvents> events = new List<ECalenderEvents>();
            if (branchid > 0)
            {
                events = await _calender.getAllCompanyCalenderEvents(branchid);

            }

            return events;
        }

        [Route("geteventById")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion")]
        [HttpPost]
        public async Task<ECalenderEvents> getCalenderEventById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            ECalenderEvents events = new ECalenderEvents();
            events = await _calender.getCalenderEventById(_id);
            if (events == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return events;
        }

        [Route("add")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> addEvent([FromBody] ECalenderEvents calenderevent)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            calenderevent.createdBy = logeduser.UserId;
            bool result = false;
            try
            {
               
                result = await _calender.addCalenderEvent(calenderevent);
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
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> updateevent([FromBody] ECalenderEvents calenderevent)
        {

            bool result = false;

            try
            {
               
                
                result = await _calender.updateCalenderEvent(calenderevent);
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
        public async Task<Boolean> deleteEvent([FromBody] JsonElement objData)

        {
            var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await _calender.deleteCalenderEvent(Id);

            return result;
        }
        [Route("remove")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> removeBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await _calender.removeCalenderEvent(id);
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
        [Route("completeevent")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> completeCalenderEvent([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await _calender.completeCalenderEvent(id);
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
