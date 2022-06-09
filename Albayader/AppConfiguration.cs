
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
       

            // sqlConnectionString = "Server=DESKTOP-BLE8A7R\\SQLEXPRESS;Database=Tire-f77f71eb-2c79-4b0b-ad3e-4c3afa314ed7;Trusted_Connection=True;MultipleActiveResultSets=true;trustServerCertificate=true";
        }
        public string APIUrl { get; set; }
        public string UploadURL { get; set; }
       

    }
}
