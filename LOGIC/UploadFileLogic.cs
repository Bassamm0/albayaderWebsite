using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Functions;

namespace LOGIC
{
    public class UploadFileLogic
    {
        DUploadFile _dUploadFile = new DUploadFile();
        public async Task<Boolean> UploadServiceImages(string file,int serviceid)
        {
            int result =await _dUploadFile.UploadServiceImages(file, serviceid);
            if ( result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
