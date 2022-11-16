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

      

        [Route("all")]
        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
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
                        role=user.UserRole,
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
        [Authorize(Roles = "Administrator,Manager,Client Manager,Support,Technicion")]
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
                        role = user.UserRole,
                        nationality = user.Nationality,
                        countryId = user.CountryId,
                        city = user.City,
                        birthday = user.Birthday,
                        // birthday = user.Birthday.ToString("dd/MM/yyyy"),
                        pictureFileName = user.PictureFileName,
                        CompanyName = user.CompanyName,
                        BranchName = user.BranchName,
                        NationalityName = user.NationalityName,
                        ResidentContry = user.ResidentContry

                    };

                    userList.Add(currentUser);
                }

            }
            return userList;
        }


        [Route("getalltechnicain")]
        [Authorize(Roles = "Administrator,Manager,Support,Technicion")]
        [HttpGet]
        public async Task<List<EUser>> getAllTechnicain()
        {

         
            var users = await userLogic.getAllTechnicain();
           
            return users;
        }
        [Route("getBranchUsers")]
        [Authorize(Roles = "Administrator,Manager,Client Manager,Support,Technicion")]
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
                        role = user.UserRole,
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
        [Authorize]
        [HttpPost]
        public async Task<EUser> getUserById([FromBody] JsonElement objData )
        {
            int _id = objData.GetProperty("id").GetInt16();
            EUser user = await userLogic.getUserById(_id);
            if (user == null) {
                throw new DomainValidationFundException("No data");
            }
           
            return user;
        }
        [Route("add")]
        [HttpPost]
          public async Task<Boolean> AddUser([FromBody] EUser newUser)
         {

            bool result = false;
            try
            {
                
                if(newUser != null)
                {
                    result = await userLogic.addUser(newUser);
                }

                
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
        [Route("addwithbranch")]
        [Authorize(Roles = "Administrator,Manager,Support,Technicion")]
        [HttpPost]
        public async Task<Boolean> AddUserWithBranch([FromBody] EUser user)
        {

            bool result = false;
            try
            {
               
                if (user != null)
                {
                    result = await userLogic.AddUserWithBranch(user);
                }


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

        [Route("updatewithbranch")]
        [Authorize(Roles = "Administrator,Manager,Support")]
        [HttpPost]
        public async Task<Boolean> updateUserwithbranch([FromBody] EUser UpdatedUser)

        {

            bool result = false;

            try
            {
                if (updateUser != null)
                {
                    result = await userLogic.updateUserwithbranch(UpdatedUser);

                }
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
        [Route("updateprofile")]
        [Authorize]
        [HttpPost]
        public async Task<Boolean> updateProfile([FromBody] EUser user)

        {

            bool result = false;

            try
            {
                if (user != null)
                {
                    result = await userLogic.updateProfile(user);

                }
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


        [Route("isemailexist")]
        [Authorize(Roles = "Administrator,Manager,Support")]
        [HttpPost]
        public async Task<Boolean> IsEmailExist([FromBody] JsonElement objData)
        {
            string email = objData.GetProperty("email").GetString();

            bool result = false;
           
            try
            {
                if (updateUser != null)
                {
                    result = await userLogic.getSingleUserByEmail(email);

                }
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

        [Route("update")]
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<Boolean> updateUser([FromBody] EUser UpdatedUser)
      
        {
           
            bool result = false;

            try
            {
               

                if(updateUser != null)
                {
                    result = await userLogic.updateUser(UpdatedUser);

                }
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
        [Authorize(Roles = "Administrator,Manager")]
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
                var oldPassword = objData.GetProperty("oldPassword").GetString();
                int id = eUser.UserId;

                result = await userLogic.changePassword(id, oldPassword,password);
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
                if (ex.Message == "Invalid Old Password.")
                {
                    throw new DomainValidationFundException("Invalid Old Password.");
                }
                if (ex.Message == "You can't use the same old password.")
                {
                    throw new DomainValidationFundException("You can't use the same old password.");
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
                    throw new DomainExpiredException(ex.Message);
                }
                if (ex.Message == "Token not correct")
                {
                    throw new DomainNotFundException(ex.Message);
                }
                if (ex.InnerException!=null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException(ex.Message);
                }
                if (ex.Message == "The provided password didn't meet the minimum required complexity.")
                {
                    throw new DomainValidationFundException(ex.Message);
                }
                if (ex.Message == "Password has been changed but Email can't be sent")
                {
                    throw new DomainValidationFundException(ex.Message);
                }

                return false;
            }

            return result;
        }

        [Route("validatetoken")]
        [HttpPost]
        public async Task<Boolean> validateToken([FromBody] JsonElement objData)
        {
            bool result = false;
            try
            {

                // EUser eUser = new EUser();
                // eUser = GetCurrentUser();

                 var token = objData.GetProperty("token").GetString();
                result = await userLogic.validateToken(token);

            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.Message == "Token already expired")
                {
                    throw new DomainExpiredException("Token already expired");
                }
                if (ex.Message == "Token not correct")
                {
                    throw new DomainNotFundException("Token not correct");
                }
                if (ex.InnerException != null && ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
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
       
                if (ex.Message == "The email doesn't exist")
                {
                    throw new DomainNotFundException("The email doesn't exist");
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
        [Authorize(Roles = "Administrator,Manager,Client Manager,Technicion,Client User,Support,Supervisor")]
        [HttpGet]
        public async Task<EUser> getLoginUserDetails()
        {
            EUser user=new EUser();
             user =  GetCurrentUser();
            if (user == null)
            {
                throw new DomainValidationFundException("No data");
            }

           


            return user;
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
                    UserRole = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    UserId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value),
                    Mobile = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    PositionId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Actor)?.Value),
                    NationalityName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Country)?.Value,
                    ResidentContry = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Locality)?.Value,
                    Birthday = Convert.ToDateTime(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value),
                    PictureFileName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Thumbprint)?.Value,
                    City = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StateOrProvince)?.Value,
                    CompanyId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value),
                    BranchId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GroupSid)?.Value),
                    CompanyName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimaryGroupSid)?.Value,
                    BranchName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DenyOnlySid)?.Value,

                };

            }
            return null;
        }
        [Route("getpostions")]
        [HttpGet]
        public async Task<List<EPositions>> getPostions()
        {
            List<EPositions> positions = await userLogic.getPostions();

            return positions;
        }



    }
}
