using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EticketAndUser
    {
        [Key]
        public int ticketAndUserId { get; set; }
        public int ticketId { get; set; }
        public int AssginUserId { get; set; }
        public DateTime assginDate { get; set; }
        public int UserId { get; set; }
    }
}
