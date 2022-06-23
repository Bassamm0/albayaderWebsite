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
    [Route("api/servicedetails")]
    [ApiController]
    public class APIServiceDetials : ControllerBase
    {
        private ServiceDetialsLogic serviceDetailsLogic = new ServiceDetialsLogic();


        [Route("all")]
        [HttpGet]
        public async Task<List<EServiceDetails>> getAllServiceDetails(JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            List<EServiceDetails> servicesDetails = new List<EServiceDetails>();
            servicesDetails = await serviceDetailsLogic.getAllServiceDetails(_id);

            return servicesDetails;
        }

        [Route("getservicedetailsbyid")]
        [HttpPost]
        public async Task<EServiceDetails> getSingleService(JsonElement objData)
        {

            int _id = objData.GetProperty("id").GetInt16();
            EServiceDetails servicesDetails = new EServiceDetails();
            servicesDetails = await serviceDetailsLogic.getSingleServiceDetails(_id);

            return servicesDetails;
        }

        [Route("add")]
        [HttpPost]
        public async Task<EServiceDetails> addServiceDetails([FromBody] EServiceDetails service)
        {

            var result = new EServiceDetails();

            try
            {
                result = await serviceDetailsLogic.addServiceDetails(service);
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
        [HttpPost]
        public async Task<Boolean> updateService([FromBody] EServiceDetails updatedServiceDetails)

        {

            bool result = false;

            try
            {
                if (updatedServiceDetails != null)
                {
                    result = await serviceDetailsLogic.updateServiceDetails(updatedServiceDetails);

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
        //corecctive

        [Route("addcorrective")]
        [HttpPost]
        public async Task<ECorrectiveServiceDetails> addCorrectiveServiceDetails([FromBody] ECorrectiveServiceDetails service)
        {

            var result = new ECorrectiveServiceDetails();
        
            try
            {
                result = await serviceDetailsLogic.addCorrectiveServiceDetails(service);
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


        [Route("updatecorrective")]
        [HttpPost]
        public async Task<Boolean> updateCorrecgiveService([FromBody] ECorrectiveServiceDetails updatedServiceDetails)

        {

            bool result = false;

            try
            {
                if (updatedServiceDetails != null)
                {
                    result = await serviceDetailsLogic.updateCorrecgiveService(updatedServiceDetails);

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

        [Route("addMaterial")]
        [HttpPost]
        public async Task<Boolean> addMaterials([FromBody] MaterialReqAndUse materialsUsed)
        {
           
            bool result = false;
            
            try
            {
                result = await serviceDetailsLogic.insertBuldRequiredMaterials(materialsUsed.ServiceDetailsId, materialsUsed.requiredmaterials);
                result = await serviceDetailsLogic.insertBulkMaterialUsed(materialsUsed.ServiceDetailsId, materialsUsed.materialUsed);
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
        [Route("addCorrectiveMaterial")]
        [HttpPost]
        public async Task<Boolean> addCorrectiveMaterials([FromBody] MaterialUsed materialsUsed)
        {

            bool result = false;

            try
            {
                result = await serviceDetailsLogic.insertBulkMaterialUsedCorrective(materialsUsed.ServiceDetailsId, materialsUsed.materialUsed);
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
        private readonly IWebHostEnvironment env;
        public APIServiceDetials(IWebHostEnvironment webHostEnvironment)
        {
            env = webHostEnvironment;
        }

        [Route("deleteimage")]
        [HttpPost]
        public async Task<Boolean> deleteImage([FromBody] JsonElement objData)

        {
            var fileName = objData.GetProperty("image").GetString();

            bool result = false;
            if (string.IsNullOrEmpty(fileName))
            {
                return result;
            }
            result = await serviceDetailsLogic.deleteImage(fileName);
            try
            {
                UtilityHelper.deleteFile(env.ContentRootPath, fileName);
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
        [Route("deleteservicedetails")]
        [HttpPost]
        public async Task<Boolean> deleteServiceDetails([FromBody] JsonElement objData)

        {
            var serviceDetailsid = objData.GetProperty("serviceDetailsId").GetInt16();

            bool result = false;
            try
            {


             List<EServicePictures> epicture = new List<EServicePictures>();
            epicture = await serviceDetailsLogic.removeServiceDetails(serviceDetailsid);
            foreach (EServicePictures eServicePictures in epicture)
            {

                result = await serviceDetailsLogic.deleteImage(eServicePictures.FileName);
                UtilityHelper.deleteFile(env.ContentRootPath, eServicePictures.FileName);
            }
         
           
               
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

        public class MaterialReqAndUse
        {
            public int ServiceDetailsId { get; set; }

            public int[] requiredmaterials { get; set; }
            public int[] materialUsed { get; set; }
        }
        public class MaterialUsed
        {
            public int ServiceDetailsId { get; set; }

            public int[] materialUsed { get; set; }
        }


    }

}
