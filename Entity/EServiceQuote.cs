using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EServiceQuote
    {
        [Key]
        public int ServiceQuoteId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceQuoteDate { get; set; }
        public string? ServiceQuoteFile { get; set; }
        public DateTime? EndDate { get; set; }
        public int? OpId { get; set; }
        [NotMapped]
        public List<EQuotationDetails>? QouteDetails { get; set; }
        [NotMapped]
         public string? BranchName { get; set; }
        [NotMapped]
        public string? CompanyName { get; set; }
        [NotMapped]
        public int? CompanyId { get; set; }
      
        public int? BranchId { get; set; }
        public string? ReferenceId { get; set; }
        [NotMapped]
        public int QuotationStatusId { get; set; }
        [NotMapped]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public string? QuotationStatus { get; set; }

    }

}
