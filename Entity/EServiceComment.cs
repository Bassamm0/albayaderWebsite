using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EServiceComment
    {
        [Key]
        public int ServiceCommentId { get; set; }
        public int ServiceId { get; set; }
        public string? Comment { get; set; }
     
        [NotMapped]
        public string? FullName { get; set; }
        public int CommentBy { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
