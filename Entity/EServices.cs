using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EServices
    {
        [Key]
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public int StatusId { get; set; }
        public int TechnicianId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? EndDate { get; set; }


        public int StatusAfterId { get; set; }

        public string? Remark { get; set; }

        public string? SupervisourName { get; set; }

        public string? SupervisourSignature { get; set; }


        public string? SupervisourFeedback { get; set; }
        public int SiteVistTypeId { get; set; }





        [NotMapped]
        public string? CreaterName { get; set; }
        [NotMapped]
        public string? StatusAfterName { get; set; }
        [NotMapped]
        public string? TechnicianName { get; set; }
        [NotMapped]
        public string? PictureFileName { get; set; }
        [NotMapped]
        public string? ServiceTypeName { get; set; }
        [NotMapped]
        public string? BranchName { get; set; }
        [NotMapped]
        public string? CompanyName { get; set; }
   

    }

}