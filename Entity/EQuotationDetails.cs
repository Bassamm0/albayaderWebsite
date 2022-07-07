using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public  class EQuotationDetails
    {
        [Key]
        public int QuotationDetailsId { get; set; }
        public int ServiceQuoteId { get; set; }
        public int? MaterialId { get; set; }
        public decimal? QuotationPrice { get; set; }

        public int? Qty { get; set; }
        public string? Description { get; set; }
        public int? OpId { get; set; }
        [NotMapped]
        public string? MaterialName { get; set; }
    }
}
