using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity;
using LOGIC;

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

    }
}
