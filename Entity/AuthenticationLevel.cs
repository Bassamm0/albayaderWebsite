using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Entity
{
    public class AuthenticationLevel
    {
        [Key]
        public Int32 AuthId { get; set; }
        [Required]
        public string Authname { get; set; }
        [Required]
        [ForeignKey("AuthLevelRefId")]
        public ICollection<EUser> Users { get; set; }


    }
}
