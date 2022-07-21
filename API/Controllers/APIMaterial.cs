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
    [Route("api/material")]
    [ApiController]
    public class APIMaterial : ControllerBase
    {
        private materialLogic _materialLogic=new materialLogic();

        [Route("all")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpGet]
        public async Task<List<EMaterials>> getMaterials()
        {
            List<EMaterials> material = new List<EMaterials>();
            material = await _materialLogic.getMaterials();

            return material;
        }

        [Route("getmaterial")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<EMaterials> getSinglematerial(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EMaterials material = new EMaterials();
            material = await _materialLogic.getMaterialById(_id);

            return material;
        }


        [Route("add")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<EMaterials> addMaterial([FromBody] EMaterials material)
        {

            var result = new EMaterials();

            try
            {
                result = await _materialLogic.addMaterial(material);
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
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> updateMaterial([FromBody] EMaterials updatedMaterial)

        {

            bool result = false;

            try
            {
                if (updatedMaterial != null)
                {
                    result = await _materialLogic.updateMaterial(updatedMaterial);

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

        [Route("remove")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> removeMaterial([FromBody] JsonElement objData)
        {
            
            bool result = false;
            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await _materialLogic.removeMaterial(id);
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
