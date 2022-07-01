using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {

        [HttpGet("Admins")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FirstName}, you are an {currentUser.UserRole}");
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FirstName}, you are a {currentUser.UserRole}");
        }

        [HttpGet("AdminsAndSellers")]
        [Authorize(Roles = "Administrator,Seller")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.FirstName}, you are an {currentUser.UserRole}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi you are in public");
        }


        private EUser GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new EUser
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Lastname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    UserRole = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    UserId =Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value),
                    Mobile = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    PositionId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Actor)?.Value),
                    NationalityName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Country)?.Value,
                    ResidentContry = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Locality)?.Value,
                    Birthday = Convert.ToDateTime(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value),
                    PictureFileName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Thumbprint)?.Value,
                    City = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StateOrProvince)?.Value,
                };

            }
            return null;
        }
    }
}