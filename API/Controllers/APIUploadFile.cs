using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/fileupload/")]
    [ApiController]
    public class APIUploadFile : ControllerBase
    {

        private readonly IWebHostEnvironment env;
        public APIUploadFile(IWebHostEnvironment webHostEnvironment)
        {
                env = webHostEnvironment;
        }
        [Route("upload")]

        [HttpPost]
        public IActionResult UploadFile(List<IFormFile> files)
            {

            string fileName = "";
                if (files.Count == 0)
                {
                    return BadRequest();
                }
                string directoryPath = Path.Combine(env.ContentRootPath, "Uploads");
                foreach (var file in files)
                {
                 fileName = changeFilename(file.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                }
                return Ok(fileName);
            }


        public static string changeFilename(string fileName)
        {

            string filename = Path.GetFileNameWithoutExtension(fileName);
            if (filename != "")
            {
                string strExtn = Path.GetExtension(fileName);
                filename = AppendDateTime(filename, strExtn);

            }
            return filename;
        }
        public static string AppendDateTime(string filename, string fileExt)
        {
            filename = filename + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfffffff") + fileExt;
            return filename;
        }
    }
    }

