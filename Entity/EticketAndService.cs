using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EticketAndService
    {
        public int ticketAndServiceId { get; set; }
        public int ServiceId { get; set; }
        public int ticketId { get; set; }
        public DateTime CreationDate { get; set; }
        public int userId { get; set; }

    }
}
