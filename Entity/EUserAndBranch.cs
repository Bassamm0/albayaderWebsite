using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EUserAndBranch
    {
        [Key]
        public int UserAndBranchId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int OpId { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
