using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ELoginLog
    {
        [Key]
        public int LoginLogId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
