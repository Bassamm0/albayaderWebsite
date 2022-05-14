using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ERecoverPassword
    {
        [Key]
        public int recoverPasswordId { get; set; }
        public string token { get; set; }
        public DateTime generatedDate { get; set; }
        public int UserId { get; set; }



    }
}
