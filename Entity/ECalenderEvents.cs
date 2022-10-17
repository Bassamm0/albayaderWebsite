using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ECalenderEvents
    {
        [Key]
        public int EventId { get; set; }
        public int? statusId { get; set; }
        public string title { get; set; }
        public string eventStartDate { get; set; }
        public string? eventEndDate { get; set; }
        public bool allDay { get; set; }
        public string? url { get; set; }
        public int? TechnicanId { get; set; }
        public int createdBy { get; set; }
        public int eventTypeId { get; set; }
        public string? description { get; set; }
        public int? branchId { get; set; }
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public string? TechnicainName { get; set; }
        [NotMapped]
        public string? TypeName { get; set; }
        [NotMapped]
        public string? BranchName { get; set; }
        [NotMapped]
        public string? eventType { get; set; }
        [NotMapped]
        public int? compnayId { get; set; }

    }
}
