using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EServiceDetails
    {
        public int ServiceDetailId { get; set; }
        public int ServiceId { get; set; }
        public int EquipmentId { get; set; }
        public bool Elect { get; set; }
        public bool Moving { get; set; }
        public bool Bearings { get; set; }
        public bool Bells { get; set; }
        public bool Motor { get; set; }
        public bool Heater { get; set; }
		public bool ControlBoard { get; set; }
		public bool Compressor { get; set; }
		public bool TmpControl { get; set; }
        public string SerialNo { get; set; }



        [NotMapped]
        public List<EMaterialsUsed> ?MaterialsUsed { get; set; }
        [NotMapped]
        public List<ERequiredMaterials> ?requiredMaterials { get; set; }

        [NotMapped]
        public List<EServicePictures> ?servicePictures { get; set; }
        [NotMapped]
        public string ?EquipmentName { get; set; }



    }
}
