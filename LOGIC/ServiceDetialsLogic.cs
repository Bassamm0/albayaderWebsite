using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class ServiceDetialsLogic
    {
        DServiceDetials dServiceDetails=new DServiceDetials();


        public async Task<List<EServiceDetails>> getAllServiceDetails(int ServiceDetailsId)
        {

            List<EServiceDetails> serviceDetails = dServiceDetails.getAllServiceDetails(ServiceDetailsId);

            return serviceDetails;
        }

        public async Task<EServiceDetails> getSingleServiceDetails(int serviceDetailsId)
        {

            EServiceDetails serviceDetails = dServiceDetails.getSingleServiceDetails(serviceDetailsId);

            return serviceDetails;
        }

        public async Task<EServiceDetails> addServiceDetails(EServiceDetails newServiceDetails)
        {

            var resul = await dServiceDetails.addServiceDetails(newServiceDetails);
            return resul;
        }

        public async Task<Boolean> updateServiceDetails(EServiceDetails newServiceDetails)
        {

            var resul = await dServiceDetails.updateServiceDetails(newServiceDetails);
            if (resul != null && resul.ServiceDetailId > 0)
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
