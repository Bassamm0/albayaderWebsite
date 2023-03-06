
using Microsoft.Extensions.Configuration;

namespace AlbayaderWeb
{
    public class AppConfiguration
    {
        //constructor
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            

            var URL = root.GetSection("APISetting:APIUrl");
            APIUrl = URL.Value;
            var Upload = root.GetSection("APISetting:UploadURL");
            UploadURL = Upload.Value;
       

        }
        public string APIUrl { get; set; }
        public string UploadURL { get; set; }
       

    }
}
