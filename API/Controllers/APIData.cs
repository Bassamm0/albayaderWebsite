using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity;
using LOGIC;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;

namespace API.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class APIData : ControllerBase
    {

        private DataLogic dataLogic= new DataLogic();
        [Route("getEquipments")]
        [HttpGet]
        public async Task<List<EEquipments>> getEquipments()
        {
            List<EEquipments> oEEquipments = new List<EEquipments>();
            oEEquipments = await dataLogic.getEquipments();

            return oEEquipments;
        }
        [Route("getmaterials")]
        [HttpGet]
        public async Task<List<EMaterials>> getmaterials()
        {
            List<EMaterials> oEMaterials = new List<EMaterials>();
            oEMaterials = await dataLogic.getmaterials();

            return oEMaterials;
        }

        [Route("getEquipmentbyid")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<EEquipments> getSingleEquipment(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EEquipments equipment = new EEquipments();
            equipment = await dataLogic.getEquipmentById(_id);

            return equipment;
        }


        [Route("addequipment")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<EEquipments> addEquipment([FromBody] EEquipments equipment)
        {

            var result = new EEquipments();

            try
            {
                result = await dataLogic.addEquipment(equipment);
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


        [Route("updateequipment")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> updateEquipment([FromBody] EEquipments updatedEquipment)

        {

            bool result = false;

            try
            {
                if (updatedEquipment != null)
                {
                    result = await dataLogic.updateEquipment(updatedEquipment);

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

        [Route("removeequipment")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<Boolean> removeEquipment([FromBody] JsonElement objData)
        {

            bool result = false;
            try
            {
                var id = objData.GetProperty("id").GetInt16();


                result = await dataLogic.removeEquipmentl(id);
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
