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
        public async Task<Boolean> UploadServiceImages(string file,int serviceDetailsId, int pictureTypeId)
        {
            var result =await _dUploadFile.UploadServiceImages(file, serviceDetailsId, pictureTypeId);
            if ( result.PictureId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> UploadServiceImagesCorrective(string file, int CorrectiveServiceDetailsId, int pictureTypeId)
        {
            var result = await _dUploadFile.UploadServiceImagesCorrective(file, CorrectiveServiceDetailsId, pictureTypeId);
            if (result.PictureId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Boolean> UploadTicketImages(string file, int ticketid)
        {
            var result = await _dUploadFile.UploadTicketImages(file, ticketid);
            if (result.ticketFileId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> UploadTicketLogImages(string file, int ticketLogId)
        {
            var result = await _dUploadFile.UploadTicketLogImages(file, ticketLogId);
            if (result.ticketLogId > 0)
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
