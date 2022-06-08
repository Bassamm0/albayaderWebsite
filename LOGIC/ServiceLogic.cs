using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class ServiceLogic
    {

        DService dservice = new DService();
        public async Task<List<EServiceModel>> getAllService()
        {

            List<EServiceModel> services = dservice.getAllService();

            return services;
        }
        public async Task<EServiceModel> getSingleService(int ServiceId)
        {

            EServiceModel services = dservice.getSingleService(ServiceId);

            return services;
        } 

        public async Task<Boolean> addService(EServices newService)
        {

            var resul = await dservice.addService(newService);
            if (resul.ServiceId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateService(EServices Service)
        {

            var resul = await dservice.updateService(Service);
            if (resul != null && resul.ServiceId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public async Task<Boolean> removeService(int id)
        {

            var resul = await dservice.removeService(id);
            if (resul != null && resul.ServiceId > 0)
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
