using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entity
{
    public class EticketLogFiles
    {
        [Key]
        public int ticketLogFileId { get; set; }
        public string fileName { get; set; }
        public int ticketLogId { get; set; }

    }
}
