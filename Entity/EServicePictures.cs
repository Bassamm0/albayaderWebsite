using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class EServicePictures
    {
        [Key]
        public int PictureId { get; set; }
        public int? ServiceDetailId { get; set; }
        public int? CorrectiveServiceDetailsId { get; set; }

        public string FileName { get; set; }
        public int PictureTypeId { get; set; }

    }
}
