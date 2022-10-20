using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EticketFiles
    {
        [Key]
        public int ticketFileId { get; set; }
        public string fileName { get; set; }
        public int ticketId { get; set; }

    }
}
