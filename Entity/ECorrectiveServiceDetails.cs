using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Entity
{
    public class ECorrectiveServiceDetails
    {
        [Key]
        public int CorrectiveServiceDetailsId { get; set; }
        public int ServiceId { get; set; }
        public int EquipmentId { get; set; }
        public int ConditionId { get; set; }
        public int EquipmentTypeId { get; set; }
        public string? ReportedDate { get; set; }
        public string? ReportedBy { get; set; }
        public string? Model { get; set; }
        public string? ServiceRendered { get; set; }
      
        public string? SerialNo { get; set; }
        public string? ProblemReported { get; set; }
        public int AMCTypeId { get; set; }




        [NotMapped]
        public List<EMaterialsUsed>? MaterialsUsed { get; set; }
        [NotMapped]
       
        public List<EServicePictures>? servicePictures { get; set; }
        [NotMapped]
        public string? EquipmentName { get; set; }

        [NotMapped]
        public string? ConditionName { get; set; }

        [NotMapped]
        public string? EquipmentTypeName { get; set; }



    }
}
