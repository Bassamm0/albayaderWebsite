using Microsoft.Extensions.Configuration;

namespace DAL.DataContext
{
    public class AppConfiguration
    {
        //constructor
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
           // var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnection");
            sqlConnectionString = appSetting.Value;

            var smtp = root.GetSection("mailSetting:smtpClient");
            smtpClient= smtp.Value;
            var eFrom = root.GetSection("mailSetting:emailFrom");
            emailFrom = eFrom.Value;
            var pwd = root.GetSection("mailSetting:pwd");
            ePassword = pwd.Value;

            // sqlConnectionString = "Server=DESKTOP-BLE8A7R\\SQLEXPRESS;Database=Tire-f77f71eb-2c79-4b0b-ad3e-4c3afa314ed7;Trusted_Connection=True;MultipleActiveResultSets=true;trustServerCertificate=true";
        }
        public string sqlConnectionString { get; set; }
        public string smtpClient { get; set; }
        public string emailFrom { get; set; }
        public string ePassword { get; set; }

    }
}
