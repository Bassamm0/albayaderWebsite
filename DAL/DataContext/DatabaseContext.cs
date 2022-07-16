using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entity;

namespace DAL.DataContext
{
    public class DatabaseContext :DbContext
    {

        public class OptionsBuild
        {
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;

            }
            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }
            public DbContextOptions<DatabaseContext> dbOptions { get; set; }
            private AppConfiguration settings { get; set; }


        }
        public static OptionsBuild ops =new OptionsBuild();
        public DatabaseContext(DbContextOptions<DatabaseContext>options): base(options) { }

        public DbSet<EUser> Users { get; set; }
        public DbSet<ECompanies> Companies { get; set; }
        public DbSet<EBranchs> Branchs { get; set; }
        public DbSet<ERecoverPassword> RecoverPassword { get; set; }
        public DbSet<EUserAndBranch> UserAndBranch { get; set; }
        public DbSet<AuthenticationLevel> AuthenticationLevels { get; set; }
        public DbSet<ELoginLog> LoginLog { get; set; }
        public DbSet<EErrorLogin> ErrorLogin { get; set; }
        public DbSet<EServices> Services { get; set; }
        public DbSet<EServiceDetails> ServiceDetails { get; set; }
        public DbSet<ECorrectiveServiceDetails> CorrectiveServiceDetails { get; set; }
        public DbSet<EServicePictures> ServicePictures{ get; set; }
        public DbSet<EServiceComment> ServiceComment{ get; set; }
        public DbSet<EMaterials> materials{ get; set; }
        public DbSet<EServiceQuote> ServiceQuotes { get; set; }
        public DbSet<EQuotationDetails> quotationDetails { get; set; }
        public DbSet<ECalenderEvents> CalenderEvents { get; set; }



    }
}
