using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EMaterialsUsed
    {
        public int MaterialsUsedId { get; set; }
        public int ServiceDetailId { get; set; }
        public int MaterialId { get; set; }
        [NotMapped]
        public int CorrectiveServiceDetailsId { get; set; }
        

        [NotMapped]
        public string MateriaUsedlName { get; set; }

    }
}
