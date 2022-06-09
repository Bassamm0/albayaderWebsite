﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EServiceModel
    {
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public int StatusId { get; set; }
        public int TechnicianId { get; set; }
        public int BranchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletionDate { get; set; }


        [NotMapped]
        public string CreaterName { get; set; }
        [NotMapped]
        public string TechnicianName { get; set; }
        [NotMapped]
        public string PictureFileName { get; set; }
        [NotMapped]
        public string ServiceTypeName { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
       public List<EServiceDetails> ServiceDetails { get; set; }
        
        

    }
}
