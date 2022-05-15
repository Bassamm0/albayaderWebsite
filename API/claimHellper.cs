using Entity;
using System.Security.Claims;

namespace API
{
    public class claimHellper
    {

        //public EUser GetCurrentUser()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        var userClaims = identity.Claims;
        //        return new EUser
        //        {
        //            Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
        //            Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
        //            FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
        //            Lastname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
        //            Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
        //            UserId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value),
        //            Mobile = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
        //            PositionId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Actor)?.Value),
        //            Nationality = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Country)?.Value),
        //            CountryId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Locality)?.Value),
        //            Birthday = Convert.ToDateTime(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value),
        //            PictureFileName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Thumbprint)?.Value,
        //            City = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StateOrProvince)?.Value,
        //        };

        //    }
        //    return null;
        //}
    }
}
