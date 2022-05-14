using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ECountries
    {
        [Key]
        public int CountryId { get; set; }
        [StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters.")]
        public string ?Name { get; set; }

        [StringLength(3, ErrorMessage = "Name cannot be longer than 150 characters.")]
        public string ?sortname { get; set; }

        [Required]
        [ForeignKey("CountryId,Nationality")]
        public ICollection<EUser> Users { get; set; }


    }
}
