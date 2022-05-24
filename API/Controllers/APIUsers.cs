using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC.UserLogic;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;
using System.Security.Claims;

namespace API
{

    [Route("api/user/")]
    [ApiController]
    public class APIUsers : ControllerBase
    {

        private UserLogic userLogic = new UserLogic();

        //[Route("add")]
        //[HttpGet]
        //public async Task<Boolean> AddUser(string username, string emailAdress, string password, int authLevelId)
        //{
        //    bool result = await userLogic.addUser(username, emailAdress, password, authLevelId);

        //    return result;
        //}

        [Route("all")]
        [HttpGet]
        [Authorize(Roles = "Administrator,Seller")]
        public async Task<List<UserViewModel>> getAllUsers()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            var users = await userLogic.getAllUsers();
            if (users.Count > 0)
            {

                foreach (var user in users)
                {
                    UserViewModel currentUser = new UserViewModel
                    {
                       title = user.Title,
                        email = user.Email,
                        userId = user.UserId,
                        username = user.Username,
                        authLevel = user.AuthLevelRefId,
                        firstName = user.FirstName,
                        lastName =user.Lastname,
                        mobile = user.Mobile,
                        telephone = user.Telephone,
                        role=user.Role,
                        nationality = user.Nationality,
                        countryId=user.CountryId,
                        city=user.City,
                        birthday=user.Birthday,
                        pictureFileName=user.PictureFileName,



                    };

                    userList.Add(currentUser);
                }

            }
            return userList;
        }

        [Route("getCompanyUsers")]
        [HttpPost]
        public async Task<List<UserViewModel>> getCompanyUsers([FromBody] JsonElement objData)
        {

            List<UserViewModel> userList = new List<UserViewModel>();
            int companyid = objData.GetProperty("companyid").GetInt16();
            var users = await userLogic.getCompanyUsers(companyid);
            if (users.Count > 0)
            {

                foreach (var user in users)
                {
                    UserViewModel currentUser = new UserViewModel
                    {
                        title = user.Title,
                        email = user.Email,
                        userId = user.UserId,
                        username = user.Username,
                        authLevel = user.AuthLevelRefId,
                        firstName = user.FirstName,
                        lastName = user.Lastname,
                        mobile = user.Mobile,
                        telephone = user.Telephone,
                        role = user.Role,
                        nationality = user.Nationality,
                        countryId = user.CountryId,
                        city = user.City,
                        birthday = user.Birthday,
                       // birthday = user.Birthday.ToString("dd/MM/yyyy"),
                        pictureFileName = user.PictureFileName,
                        CompanyName = user.CompanyName,
                        BranchName = user.BranchName,

                    };

                    userList.Add(currentUser);
                }

            }
            return userList;
        }
        [Route("getBranchUsers")]
        [HttpGet]
        public async Task<List<UserViewModel>> getBranchUsers([FromBody] JsonElement objData)
        {

            List<UserViewModel> userList = new List<UserViewModel>();
            int branchId = objData.GetProperty("branchId").GetInt16();
            var users = await userLogic.getBranchUsers(branchId);
            if (users.Count > 0)
            {

                foreach (var user in users)
                {
                    UserViewModel currentUser = new UserViewModel
                    {
                        title = user.Title,
                        email = user.Email,
                        userId = user.UserId,
                        username = user.Username,
                        authLevel = user.AuthLevelRefId,
                        firstName = user.FirstName,
                        lastName = user.Lastname,
                        mobile = user.Mobile,
                        telephone = user.Telephone,
                        role = user.Role,
                        nationality = user.Nationality,
                        countryId = user.CountryId,
                        city = user.City,
                        birthday = user.Birthday,
                        pictureFileName = user.PictureFileName,
                        CompanyName = user.CompanyName,
                        BranchName = user.BranchName,

                    };

                    userList.Add(currentUser);
                }

            }
            return userList;
        }




        [Route("getUserById")]
        [HttpPost]
        public async Task<UserViewModel> getUserById([FromBody] JsonElement objData )
        {
            int _id = objData.GetProperty("id").GetInt16();
            var user = await userLogic.getUserById(_id);
            if (user == null) {
                throw new DomainValidationFundException("No data");
            }
           
                    UserViewModel currentUser = new UserViewModel
                    {
                        title = user.Title,
                        email = user.Email,
                        userId = user.UserId,
                        username = user.Username,
                        authLevel = user.AuthLevelRefId,
                        firstName = user.FirstName,
                        lastName = user.Lastname,
                        mobile = user.Mobile,
                        telephone = user.Telephone,
                        role = user.Role,
                        nationality = user.Nationality,
                        countryId = user.CountryId,
                        city = user.City,
                        birthday = user.Birthday,
                        pictureFileName = user.PictureFileName,
                        BranchName=user.BranchName,
                        CompanyName = user.CompanyName, 
                        ResidentContry =user.ResidentContry,
                        NationalityName = user.NationalityName,
                    };

            return currentUser;
        }
        [Route("add")]
        [HttpPost]
          public async Task<Boolean> AddUser([FromBody] JsonElement objData)
         {

            bool result = false;
            try
            {
                var Title = objData.GetProperty("title").GetString();
                var username = objData.GetProperty("username").GetString();
                var FirstName = objData.GetProperty("firstName").GetString();
                var Lastname = objData.GetProperty("lastname").GetString();
                var Email = objData.GetProperty("email").GetString();
                var Password = objData.GetProperty("password").GetString();
                var Mobile = objData.GetProperty("mobile").GetString();
                var Telephone = objData.GetProperty("telephone").GetString();
                var Role = objData.GetProperty("role").GetString();
                var authLevelId = objData.GetProperty("authLevelId").GetInt16();
                var Nationality = objData.GetProperty("nationality").GetInt16();
                var CountryId = objData.GetProperty("countryId").GetInt16();
                var PositionId = objData.GetProperty("positionId").GetInt16();
                var city = objData.GetProperty("city").GetString();
                var Birthday = objData.GetProperty("birthday").GetString();
                var PictureFileName = objData.GetProperty("pictureFileName").GetString();

             
                
                EUser newUser = new EUser
                {
                    Title = Title,
                    Username = username,
                    FirstName = FirstName,
                    Lastname = Lastname,
                    Email = Email,
                    Password = Password,
                    Mobile = Mobile,
                    Telephone = Telephone,
                    Role = Role,
                    AuthLevelRefId = authLevelId,
                    Birthday = DateTime.ParseExact(Birthday, "dd/M/yyyy", CultureInfo.InvariantCulture),
                    PictureFileName = PictureFileName,
                    Nationality = Nationality,
                    CountryId = CountryId,
                    PositionId=PositionId,
                    City = city

                };
                 result = await userLogic.addUser(newUser);
            }
            catch (Exception ex)
            {
                if(ex.Message== "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                { throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }
           


            return result;
        }
        [Route("update")]
        [HttpPost]
        public async Task<Boolean> updateUser([FromBody] JsonElement objData)
      
        {
           
            bool result = false;

            try
            {
                var UserId = objData.GetProperty("userid").GetInt16();
                var Title = objData.GetProperty("title").GetString();
             
                var FirstName = objData.GetProperty("firstName").GetString();
                var Lastname = objData.GetProperty("lastname").GetString();
            
                var Mobile = objData.GetProperty("mobile").GetString();
                var Telephone = objData.GetProperty("telephone").GetString();
                var Role = objData.GetProperty("role").GetString();
                var authLevelId = objData.GetProperty("authLevelId").GetInt16();
                var Nationality = objData.GetProperty("nationality").GetInt16();
                var CountryId = objData.GetProperty("countryId").GetInt16();
                var PositionId = objData.GetProperty("positionid").GetInt16();
                var city = objData.GetProperty("city").GetString();
                var Birthday = objData.GetProperty("birthday").GetString();
                var PictureFileName = objData.GetProperty("pictureFileName").GetString();



                EUser UpdatedUser = new EUser
                {
                    UserId = UserId,
                    Title = Title,
                  
                    FirstName = FirstName,
                    Lastname = Lastname,
                  
                  
                    Mobile = Mobile,
                    Telephone = Telephone,
                    Role = Role,
                    AuthLevelRefId = authLevelId,
                    Birthday = DateTime.ParseExact(Birthday, "dd/M/yyyy", CultureInfo.InvariantCulture),
                    PictureFileName = PictureFileName,
                    Nationality = Nationality,
                    CountryId = CountryId,
                    PositionId = PositionId,
                    City = city

                };
                result = await userLogic.updateUser(UpdatedUser);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
            if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
            {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }
        [Route("delete")]
        [HttpPost]
        public async Task<Boolean> deleteUser([FromBody] JsonElement objData)

        {
           var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await userLogic.deleteUser(Id);

            return result;
        }
        [Route("remove")]
        [HttpPost]
        public async Task<Boolean> removeUser([FromBody] JsonElement objData)
        {
            bool result = false;
            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await userLogic.removUser(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
            if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
            {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Authorize]
        [Route("changepassword")]
        [HttpPost]
        public async Task<Boolean> changePassword([FromBody] JsonElement objData)
        {
            bool result = false;
            try
            {

                EUser eUser = new EUser();
                eUser=GetCurrentUser();

                var password = objData.GetProperty("password").GetString();
                int id = eUser.UserId;

                result = await userLogic.changePassword(id,password);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                 if (ex.InnerException!=null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                if (ex.Message == "The provided password didn't meet the minimum required complexity.")
                {
                     throw new DomainValidationFundException("The provided password didn't meet the minimum required complexity.");
                }
                return false;
            }

            return result;
        }

        [Route("recoverpassword")]
        [HttpPost]
        public async Task<Boolean> recoverPassword([FromBody] JsonElement objData)
        {
            bool result = false;
            try
            {

               // EUser eUser = new EUser();
               // eUser = GetCurrentUser();

                var password = objData.GetProperty("password").GetString();
                var token = objData.GetProperty("token").GetString();

               

                result = await userLogic.recoverPassword( password,token);

            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if(ex.Message == "Token already expired")
                {
                    throw new DomainExpiredException("Token already expired");
                }
                if (ex.Message == "Token not correct")
                {
                    throw new DomainNotFundException("Token not correct");
                }
                if (ex.InnerException!=null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                if (ex.Message == "The provided password didn't meet the minimum required complexity.")
                {
                    throw new DomainValidationFundException("The provided password didn't meet the minimum required complexity.");
                }

                return false;
            }

            return result;
        }

        [Route("forgetpassword")]
        [HttpPost]
        public async Task<Boolean> forgetpassword([FromBody] JsonElement objData)
        {
            bool result = false;
   
            try
            {
 

                var email = objData.GetProperty("email").GetString();
              

                result = await userLogic.forgetpassword(email);

            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
       
                if (ex.Message == "Validation : The email doesn't exist")
                {
                    throw new DomainNotFundException("Validation: The email doesn't exist");
                }
                if (ex.Message == "Email can't be sent, plesae try again later")
                {
                    throw new DomainInternalException("Email can't be sent, plesae try again later");
                }
                if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

        [Authorize]
        [Route("getLoginUser")]
        [HttpGet]
        public async Task<UserViewModel> getLoginUserDetails()
        {
            
            var user =  GetCurrentUser();
            if (user == null)
            {
                throw new DomainValidationFundException("No data");
            }

            UserViewModel currentUser = new UserViewModel
            {
               
                email = user.Email,
                userId = user.UserId,
                username = user.Username,
                //authLevel = user.AuthLevelRefId,
                firstName = user.FirstName,
                lastName = user.Lastname,
                mobile = user.Mobile,
               // telephone = user.Telephone,
                role = user.Role,
                nationality = user.Nationality,
                countryId = user.CountryId,
                city = user.City,
                birthday = user.Birthday,
                pictureFileName = user.PictureFileName,
            };

            return currentUser;
        }

        //required  [Authorize]
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
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    UserId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value),
                    Mobile = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    PositionId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Actor)?.Value),
                    Nationality = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Country)?.Value),
                    CountryId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Locality)?.Value),
                    Birthday = Convert.ToDateTime(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value),
                    PictureFileName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Thumbprint)?.Value,
                    City = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StateOrProvince)?.Value,
                };

            }
            return null;
        }
    }
}
