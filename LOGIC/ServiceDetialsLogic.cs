﻿using System;
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

        public async Task<Boolean> insertBuldRequiredMaterials(int ServiceDetailsId, int[] requiredmaterials)
        {

            var resul = false;

            // delete all
            resul = dServiceDetails.deletRequiredMaterials(ServiceDetailsId);
            if (requiredmaterials.Length > 0)
            {
                 resul = dServiceDetails.insertBuldRequiredMaterials(ServiceDetailsId, requiredmaterials);

            }
           

            return resul;
            

        }
        public async Task<Boolean> insertBulkMaterialUsed(int ServiceDetailsId, int[] materialUsed)
        {

            var resul = false;

            // delete all 
            resul = dServiceDetails.deleteMaterialUsed(ServiceDetailsId);
            if (materialUsed.Length > 0)
            {
                resul = dServiceDetails.insertBulkMaterialUsed(ServiceDetailsId, materialUsed);
            }

            return resul;

        }


    }
}
