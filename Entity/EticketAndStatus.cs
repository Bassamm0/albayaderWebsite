using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    public class EticketAndStatus
    {
        [Key]
        public int ticketAndStatusId { get; set; }
        public int ticketId { get; set; }
        public int ticketStatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public int? UserId { get; set; }

    }
}
