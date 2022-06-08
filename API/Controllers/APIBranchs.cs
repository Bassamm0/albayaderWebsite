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
    [Route("api/Branch/")]
    [ApiController]
    public class APIBranchs : ControllerBase
    {

        private BranchLogic BranchLogic = new BranchLogic();

        [Route("all")]
        [HttpGet]
        public async Task<List<EBranchs>> getAllBranchs()
        {
            List<EBranchs> branchs = new List<EBranchs>();
            branchs = await BranchLogic.getAllBranchs();

            return branchs;
        }

        [Route("companybranchs")]
        [HttpPost]
        public async Task<List<EBranchs>> getAllCompanyBranchs([FromBody] JsonElement objData)
        {
            int companyid = objData.GetProperty("companyid").GetInt16();
            List<EBranchs> branchs = new List<EBranchs>();
            branchs = await BranchLogic.getAllCompanyBranchs(companyid);

            return branchs;
        }

        [Route("getBranchById")]
        [HttpPost]
        public async Task<EBranchs> getBranchById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            EBranchs Branch = new EBranchs();
            Branch = await BranchLogic.getBranchById(_id);
            if (Branch == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return Branch;
        }

        [Route("add")]
        [HttpPost]
        public async Task<Boolean> addBranch([FromBody] JsonElement branch)
        {

            bool result = false;
            try
            {
                var BranchName = branch.GetProperty("branchname").GetString();
                var EmirateId = branch.GetProperty("emirateId").GetInt16();
                var CompnayId = branch.GetProperty("companyid").GetInt16();
                var Latitude = branch.GetProperty("latitude").GetDecimal();
                var Longitude = branch.GetProperty("longitude").GetDecimal();
                var District = branch.GetProperty("district").GetString();

                EBranchs newBranch = new EBranchs
                {
                    BranchName = BranchName,
                    CompnayId = CompnayId,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    EndDate = null,
                    EmirateId = EmirateId,
                    District = District,


                };
                result = await BranchLogic.addBranch(newBranch);
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
        public async Task<Boolean> updateBranch([FromBody] JsonElement branch)
        {

            bool result = false;

            try
            {
                var BranchId = branch.GetProperty("branchid").GetInt16();
                var EmirateId = branch.GetProperty("emirateId").GetInt16();
                var BranchName = branch.GetProperty("branchname").GetString();
                var District = branch.GetProperty("district").GetString();
                var CompnayId = branch.GetProperty("companyid").GetInt16();
                var Latitude = branch.GetProperty("latitude").GetDecimal();
                var Longitude = branch.GetProperty("longitude").GetDecimal();

                EBranchs UpdatedBranch = new EBranchs
                {
                    BranchId = BranchId,
                    BranchName = BranchName,
                    CompnayId = CompnayId,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    EmirateId = EmirateId,
                    District = District,
                };
                result = await BranchLogic.updateBranch(UpdatedBranch);
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
        public async Task<Boolean> deleteBranch([FromBody] JsonElement objData)

        {
            var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await BranchLogic.deleteBranch(Id);

            return result;
        }
        [Route("remove")]
        [HttpPost]
        public async Task<Boolean> removeBranch([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await BranchLogic.removeBranch(id);
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
