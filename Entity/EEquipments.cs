using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class EEquipments
    {
        [Key]
        public int EquipmentId { get; set; }
        public string Name { get; set; }

        public DateTime? EndDate { get; set; }


     }
}   
