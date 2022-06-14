using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;


namespace DAL.Functions
{
    public class DUploadFile
    {

        public async Task<EServicePictures> UploadServiceImages(string file, int serviceDetailsId, int pictureTypeId)
        {


            EServicePictures newServicePictures = new EServicePictures();
            newServicePictures.ServiceDetailId = serviceDetailsId;
            newServicePictures.PictureTypeId = pictureTypeId;
            newServicePictures.FileName = file;


            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ServicePictures.AddAsync(newServicePictures);
                await context.SaveChangesAsync();
            }

            return newServicePictures;
        }

      

     
    }
}
