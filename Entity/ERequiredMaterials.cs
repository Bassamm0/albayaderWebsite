using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    public class ERequiredMaterials
    {
        public int RequiredMaterialId { get; set; }
        public int ServiceDetailId { get; set; }
        public int MaterialId { get; set; }

        [NotMapped]
        public string? RequireMaterialName { get; set; }
    }
}
