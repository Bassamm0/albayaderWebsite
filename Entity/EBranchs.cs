﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
 
    public class EBranchs
    {
        [Key]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompnayId { get; set; }
        public DateTime ?EndDate { get; set; }


    }
}
