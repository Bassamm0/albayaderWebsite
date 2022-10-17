using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class ServiceQuoteLogic
    {
        DServiceQuote dServiceQuote = new DServiceQuote();
        public async Task<List<EServiceQuote>> getAllServiceQuote(EUser logeduser)
        {

            List<EServiceQuote> ServiceQuote = new List<EServiceQuote>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                ServiceQuote = dServiceQuote.getAllServiceQuote();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                ServiceQuote = dServiceQuote.getAllCompanyServiceQuote(logeduser.CompanyId);
            }


            return ServiceQuote;
        }

        public async Task<List<EServiceQuote>> getAllServiceQuoteByDate(EUser logeduser,string startDate,string endDate)
        {

            List<EServiceQuote> ServiceQuote = new List<EServiceQuote>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                ServiceQuote = dServiceQuote.getAllServiceQuoteByDate(startDate,  endDate);
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                ServiceQuote = dServiceQuote.getAllCompanyServiceQuoteDate(logeduser.CompanyId, startDate, endDate);
            }


            return ServiceQuote;
        }

        public async Task<List<EServiceQuote>> getAllCompanyServiceQuote(int companyid)
        {

            List<EServiceQuote> ServiceQuote = dServiceQuote.getAllCompanyServiceQuote(companyid);

            return ServiceQuote;
        }
        public async Task<EServiceQuote> getServiceQuoteById(int id)
        {

            EServiceQuote ServiceQuote = dServiceQuote.getSingleServiceQuote(id);

            return ServiceQuote;
        }
        public async Task<Boolean> addServiceQuote(EServiceQuote newServiceQuote, EUser logeduser)
        {
            newServiceQuote.OpId = logeduser.UserId;
            var resul = await dServiceQuote.addServiceQuote(newServiceQuote);

             var reusltDetails = dServiceQuote.insertBuldQuoteMaterials(resul.ServiceQuoteId,newServiceQuote.QouteDetails,logeduser.UserId);
            if (resul.ServiceQuoteId > 0)
            {


                // insert status
                dServiceQuote.insertQuoteStatus(resul.ServiceQuoteId, 1);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateServiceQuote(EServiceQuote ServiceQuote,EUser logeduser)
        {

            var resul = await dServiceQuote.updateServiceQuote(ServiceQuote);
            var reusltDetails = false;
            if (resul != null && resul.ServiceQuoteId > 0)
            {
                // delete update service quote details

                reusltDetails =  dServiceQuote.deleteAllQuoteDetails(ServiceQuote.ServiceQuoteId);
                // add details
                reusltDetails = dServiceQuote.insertBuldQuoteMaterials(resul.ServiceQuoteId,ServiceQuote.QouteDetails,logeduser.OpId);


                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteServiceQuote(int Id)
        {

            var resul = await dServiceQuote.deleteServiceQuote(Id);
            if (resul != null && resul.ServiceQuoteId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> removeServiceQuote(int id)
        {

            var resul = await dServiceQuote.removeServiceQuote(id);
            if (resul != null && resul.ServiceQuoteId > 0)
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
