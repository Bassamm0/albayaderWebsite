using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EDashboard
    {
        public int preventiveCount { get; set; }
        public int correctiveCount { get; set; }
        public int correctiveAMCCount { get; set; }
        public int correctiveNoneAMCCount { get; set; }
        public int otherCount { get; set; }
        public int branchCount { get; set; }
        public List<ServicePerMonth> preventMonth { get; set; }
        public List<ServicePerMonth> correctiveMonth { get; set; }
        public List<ServicePerMonth> correctiveMonthAMC { get; set; }
        public List<ServicePerMonth> correctiveMonthNoneAMC { get; set; }

        public List<ServicePerMonth> allServiceMonth { get; set; }

        public List<ServicePerBranch> preventiveBranch { get; set; }
        public List<ServicePerBranch> correctiveBranch { get; set; }
        public List<ServicePerBranch> allServiceBranch { get; set; }
        public List<ServicePerMonthVist> lsservicePerMonthVist { get; set; }   
        public List<ServicePerBranchVisit> lsservicePerBranchVisit { get; set; }

    }

    public class ServicePerMonth
    {
        public string monthName { get; set; }
        public int value { get; set; }

    }


    public class ServicePerBranch
    {
        public string branchName { get; set; }
        public int value { get; set; }

    }
    public class ServicePerMonthVist
    {
        public string monthName { get; set; }
        public int value { get; set; }
         public string VistTypeName { get; set; }

    }
    public class ServicePerBranchVisit
    {
        public string BranchName { get; set; }
        public string Type { get; set; }
        public int value { get; set; }
        public int branchId { get; set; }
         public string VistTypeName { get; set; }

    }
}
