using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public  class EServiceQuotes
    {
        public int ServiceQuoteId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ServiceQuoteDate { get; set; }
        public string ServiceQuoteFile { get; set; }
        public int OpId { get; set; }


    }
}
