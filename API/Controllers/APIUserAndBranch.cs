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
    [Route("api/UserAndBranch/")]
    [ApiController]
    public class APIUserAndBranch : ControllerBase
    {

        private UserAndBranchLogic UserAndBranchLogic = new UserAndBranchLogic();

        [Route("all")]
        [HttpGet]
        public async Task<List<EUserAndBranch>> getAllUserAndBranch()
        {
            List<EUserAndBranch> companayList = new List<EUserAndBranch>();
            companayList = await UserAndBranchLogic.getAllUserAndBranch();

            return companayList;
        }

        [Route("getUserAndBranchById")]
        [HttpGet]
        public async Task<EUserAndBranch> getUserAndBranchById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            EUserAndBranch UserAndBranch = new EUserAndBranch();
            UserAndBranch = await UserAndBranchLogic.getUserAndBranchById(_id);
            if (UserAndBranch == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return UserAndBranch;
        }

        [Route("add")]
        [HttpPost]
        public async Task<Boolean> addUserAndBranch([FromBody] JsonElement objData)
        {

            bool result = false;
            try
            {
                var BranchId = objData.GetProperty("branchid").GetInt16();
                var UserId = objData.GetProperty("userid").GetInt16();


                EUserAndBranch newUserAndBranch = new EUserAndBranch
                {
                    BranchId = BranchId,
                    UserId = UserId,
                    EndDate = null,


                };
                result = await UserAndBranchLogic.addUserAndBranch(newUserAndBranch);
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
        [HttpPost]
        public async Task<Boolean> updateUserAndBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var UserAndBranchId = objData.GetProperty("userandbranchid").GetInt16();
                var BranchId = objData.GetProperty("branchid").GetInt16();
                var UserId = objData.GetProperty("userid").GetInt16();

                EUserAndBranch UpdatedUserAndBranch = new EUserAndBranch
                {
                    UserAndBranchId = UserAndBranchId,
                    BranchId = BranchId,
                    UserId = UserId,

                };
                result = await UserAndBranchLogic.updateUserAndBranch(UpdatedUserAndBranch);
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
        [HttpPost]
        public async Task<Boolean> deleteUserAndBranch([FromBody] JsonElement objData)

        {
            var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await UserAndBranchLogic.deleteUserAndBranchh(Id);

            return result;
        }
        [Route("remove")]
        [HttpPost]
        public async Task<Boolean> removeUserAndBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await UserAndBranchLogic.removeUserAndBranch(id);
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
