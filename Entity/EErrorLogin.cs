using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public  class EErrorLogin
    {
        [Key]
        public int ErrorLoginId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}
