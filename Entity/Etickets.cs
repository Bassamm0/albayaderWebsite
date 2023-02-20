using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Etickets
    {
        [Key]
        public int ticketId { get; set; }
        public string subject { get; set; }
        public string ticketDetails { get; set; }
  
        public DateTime? creationDate { get; set; }

        public int? createdBy { get; set; }
        public int severityId { get; set; }
        public int ticketCategoryId { get; set; }
        public int? onBehafId { get; set; }


        [NotMapped]
        public string? CreatorName { get; set; }
        [NotMapped]
        public string? BranchName { get; set; }
        [NotMapped]
        public string? CompanyName { get; set; }
        [NotMapped]
        public DateTime? StatusDate { get; set; }
        [NotMapped]
        public string? StatusName { get; set; }


    }
}
