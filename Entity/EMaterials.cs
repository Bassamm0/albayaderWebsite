using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class EMaterials
    {
        [Key]
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string Description { get; set; }
    
        public Decimal? Price { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
