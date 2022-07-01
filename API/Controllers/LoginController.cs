using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LOGIC.UserLogic;


namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public   IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user =  Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User name or password is not correct.");
        }


        private string Generate(EUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Role, user.UserRole),
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Mobile.ToString()),
                new Claim(ClaimTypes.Actor, user.PositionId.ToString()),
                new Claim(ClaimTypes.Country, user.NationalityName.ToString()),
                new Claim(ClaimTypes.Locality, user.ResidentContry.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()),
                new Claim(ClaimTypes.Thumbprint, user.PictureFileName.ToString()),
                new Claim(ClaimTypes.StateOrProvince, user.City),
                new Claim(ClaimTypes.PrimarySid, user.CompanyId.ToString()),
                new Claim(ClaimTypes.GroupSid, user.CompanyTypeId.ToString()),
                new Claim(ClaimTypes.PrimaryGroupSid, user.CompanyName.ToString()),
                new Claim(ClaimTypes.DenyOnlySid, user.BranchName.ToString()),

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private EUser Authenticate(UserLogin userLogin)
        {

            // 
            UserLogic _userLogic = new UserLogic();

            //var users =  _userLogic.getAllUsersForAuthNew();
            EUser currentUser = _userLogic.getLoginUser(userLogin.Email.ToLower(), userLogin.Password);

            //var currentUser = users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);
           // var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}

