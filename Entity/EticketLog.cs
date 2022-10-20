using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EticketLog
    {
        [Key]
        public int ticketLogId { get; set; }
        public int ticketId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }

    }
}
