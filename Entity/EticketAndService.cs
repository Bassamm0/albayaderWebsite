using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EticketAndService
    {
        [Key]
        public int ticketAndServiceId { get; set; }
        public int ServiceId { get; set; }
        public int ticketId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime VisitPlanedTime { get; set; }
        public int userId { get; set; }
        [NotMapped]
        public int branchId { get; set; }
        [NotMapped]
        public int TechnicianId { get; set; }
        [NotMapped]
        public int SiteVistTypeId { get; set; }


    }
}
