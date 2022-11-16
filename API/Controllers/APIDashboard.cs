using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
   
    [Route("api/dashboard")]
    [ApiController]
 
    public class APIDashboard : ControllerBase
    {
       
        private DashboardLogic dashboardLogic=new DashboardLogic();

  
        [Route("dashboarddata")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Supervisor,Support")]
        [HttpPost]
        public async Task<EDashboard> getDashboardData([FromBody] JsonElement objData)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            EUser logeduser = claimHellper.GetCurrentUser(identity);
            string year=objData.GetProperty("year").GetString();
            EDashboard oEDashboard = new EDashboard();
            if (!string.IsNullOrEmpty(year)) { 
            oEDashboard = await dashboardLogic.getDashboardData(year, logeduser);

            }

            return oEDashboard;
        }

       


    }
}
