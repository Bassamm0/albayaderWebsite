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

        public async Task<List<EServiceModel>> getAllServiceByStatus(int StatusId)
        {

            //if admin
            List<EServiceModel> services = dservice.getAllServiceByStatus(StatusId);

            return services;
        }
        public async Task<List<EServiceModel>> getAllCompletedService(EUser logeduser)
        {
            List<EServiceModel> services = new List<EServiceModel>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager"))
            {
                services = dservice.getAllCompletedService();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                services = dservice.getAllCompletedServiceCompany(logeduser.CompanyId);
            }

           // if manager

            return services;
        }


        public async Task<List<EServiceModel>> getServiceReport(EUser logeduser)
        {
            List<EServiceModel> services = new List<EServiceModel>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager"))
            {
                services = dservice.getServiceReport();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                services = dservice.getAllCompletedServiceCompany(logeduser.CompanyId);
            }

            // if manager

            return services;
        }
        public async Task<List<EServiceModel>> getAllCompletedServiceBranch(EUser logeduser,int BranchId)
        {
            List<EServiceModel> services = new List<EServiceModel>();
           
                services = dservice.getAllCompletedServiceBranch(BranchId);
           

            // if manager

            return services;
        }

        public async Task<List<EServiceModel>> getAllCompletedServiceByDate(EUser logeduser,string startDate,string endDate)
        {
            List<EServiceModel> services = new List<EServiceModel>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager"))
            {
                services = dservice.getAllCompletedServiceDate(startDate,endDate);
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                services = dservice.getAllCompletedServiceCompanydate(logeduser.CompanyId,startDate,endDate);
            }

            // if manager

            return services;
        }


        public async Task<EServiceModel> getSingleService(int ServiceId)
        {

            EServiceModel services = dservice.getSingleService(ServiceId);

            return services;
        }
        
        public async Task<ECorrectiveServiceModel> getCorrectiveSingleService(int ServiceId)
        {

            ECorrectiveServiceModel services = dservice.getCorrectiveSingleService(ServiceId);

            return services;
        }
        public async Task<EServices> addService(EServices newService)
        {

            var resul = await dservice.addService(newService);
            return resul;
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
        public async Task<Boolean> updateStatus(int serviceId,int statusId, string remark,int statusAfterId,int siteVistTypeId,string Recommendation,string serviceRender,string rootOfCause)
        {

            var resul = await dservice.updateStatus(serviceId, statusId,remark, statusAfterId, siteVistTypeId, Recommendation, serviceRender, rootOfCause);
            if (resul != null && resul.ServiceId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> clientSignature(int serviceId,string SupervisourSignature,string SupervisourName,string SupervisourFeedback, string SupervisourMobile, string SupervisourDesignation)
        {

            var resul = await dservice.clientSignature(serviceId, SupervisourSignature, SupervisourName, SupervisourFeedback,  SupervisourMobile,  SupervisourDesignation);
            if (resul != null && resul.ServiceId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<Boolean> updateServiceDate(int serviceid, string newDate)
        {

            var resul = dservice.updateServiceDate(serviceid, newDate);
            if (resul)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> updateBranch(int serviceid, int branchId)
        {

            var resul = dservice.updateBranch(serviceid, branchId);
            if (resul)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //updateBranch
    }
}
