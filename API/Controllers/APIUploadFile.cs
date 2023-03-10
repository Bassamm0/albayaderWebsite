using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LOGIC;
using static DAL.DALException;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/fileupload/")]
    [ApiController]
    public class APIUploadFile : ControllerBase
    {
        UploadFileLogic _uploadFileLogic= new UploadFileLogic();
        private readonly IWebHostEnvironment env;
        public APIUploadFile(IWebHostEnvironment webHostEnvironment)
        {
                env = webHostEnvironment;
        }
        [Route("upload")]
        [Authorize(Roles = "Administrator,Manager,Technicion,Client Manager,Client User")]
        [HttpPost]
        public IActionResult UploadFile(List<IFormFile> files)
            {

            string fileName = "";
                if (files.Count == 0)
                {
                    return BadRequest();
                }

                //string directoryPath = Path.Combine(AppContext.BaseDirectory, "/Albayader/Uploads");
                
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                    fileName = UtilityHelper.changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);

                   
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);

                }
            }
                return Ok(fileName);
            }


        [Route("uploadserviceimages")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<List<string>> UploadServiceImages(List<IFormFile> files, [FromForm] int serviceDetailsId, [FromForm]  int pictureTypeId)
        {
      
            List<string> lfileName=new List<string>();
            
            string fileName = "";
            if (files.Count == 0)
            {

                return null;
            }

            //string directoryPath = Path.Combine(AppContext.BaseDirectory, "/Albayader/Uploads");
            try
            {
                bool StoreImage=false;
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                    fileName = UtilityHelper.changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);
                   
                    if (Path.GetExtension(fileName).ToLower() != ".pdf")
                    {
                        StoreImage = UtilityHelper.comperssImage(file, filePath);
                    }
                    else
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                  
                    var result =await _uploadFileLogic.UploadServiceImages(fileName, serviceDetailsId, pictureTypeId);
                    if (result)
                    {
                        lfileName.Add(fileName);
                    }
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
           
          
            return lfileName;
        }



        [Route("uploadserviceimagescorrective")]
        [Authorize(Roles = "Administrator,Manager,Technicion")]
        [HttpPost]
        public async Task<List<string>> UploadServiceImagesCorrective(List<IFormFile> files, [FromForm] int correctiveServiceDetailsId, [FromForm] int pictureTypeId)
        {

            List<string> lfileName = new List<string>();

            string fileName = "";
            if (files.Count == 0)
            {

                return null;
            }

            //string directoryPath = Path.Combine(AppContext.BaseDirectory, "/Albayader/Uploads");
            try
            {
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                    fileName = UtilityHelper.changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var result = await _uploadFileLogic.UploadServiceImagesCorrective(fileName, correctiveServiceDetailsId, pictureTypeId);
                    if (result)
                    {
                        lfileName.Add(fileName);
                    }
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


            return lfileName;
        }



        [Route("uploadticketimages")]
        [Authorize]
        [HttpPost]
        public async Task<List<string>> UploadTicketImages(List<IFormFile> files, [FromForm] int ticketId)
        {

            List<string> lfileName = new List<string>();

            string fileName = "";
            if (files.Count == 0)
            {

                return null;
            }

            //string directoryPath = Path.Combine(AppContext.BaseDirectory, "/Albayader/Uploads");
            try
            {
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                    fileName = UtilityHelper.changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var result = await _uploadFileLogic.UploadTicketImages(fileName, ticketId);
                    if (result)
                    {
                        lfileName.Add(fileName);
                    }
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


            return lfileName;
        }


        [Route("uploadticketLogimages")]
        [Authorize]
        [HttpPost]
        public async Task<List<string>> UploadTicketLogImages(List<IFormFile> files, [FromForm] int ticketLogId)
        {

            List<string> lfileName = new List<string>();

            string fileName = "";
            if (files.Count == 0)
            {

                return null;
            }
            try
            {
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                    fileName = UtilityHelper.changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var result = await _uploadFileLogic.UploadTicketLogImages(fileName, ticketLogId);
                    if (result)
                    {
                        lfileName.Add(fileName);
                    }
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


            return lfileName;
        }
    }
}

