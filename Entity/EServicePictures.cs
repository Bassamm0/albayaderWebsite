using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EServicePictures
    {
        public int PictureId { get; set; }
        public int ServiceDetailId { get; set; }

        public string FileName { get; set; }
        public int PictureTypeId { get; set; }

    }
}
