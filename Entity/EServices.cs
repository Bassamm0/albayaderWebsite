using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EServices
    {
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public int StatusId { get; set; }
        public int TechnicianId { get; set; }
        public int BranchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime EndDate { get; set; }

    }

}
