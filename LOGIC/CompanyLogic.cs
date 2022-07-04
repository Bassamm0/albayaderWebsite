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
    public class CompanyLogic
    {
        DCompanies dCompanies = new DCompanies();
       
        public async Task<List<ECompanies>> getAllCompanies()
        {
           
            List<ECompanies> companies = dCompanies.getAllCompanies();

            return companies;
        }
        public async Task<ECompanies> getCompanyById(int id)
        {

            ECompanies company = dCompanies.getSingleCompany(id);

            return company;
        }
        public async Task<Boolean> addCompany(ECompanies newCompany)
        {

            var resul = await dCompanies.addCompany(newCompany);
            if (resul.CompanyID > 0)
            {

                // create main branch
                DBranchs dbranchs = new DBranchs();
                EBranchs ebranchs = new EBranchs();
                ebranchs.BranchName = newCompany.Name;
                ebranchs.CompnayId = newCompany.CompanyID;
           
                ebranchs.Latitude = newCompany.Latitude;
                ebranchs.Longitude = newCompany.Longitude;
                var addbranch=await dbranchs.addBranch(ebranchs);

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateCompany(ECompanies company)
        {

            var resul = await dCompanies.updateCompany(company);
            if (resul != null && resul.CompanyID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteCompany(int Id)
        {

            var resul = await dCompanies.deleteCompany(Id);
            if (resul != null && resul.CompanyID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean>removeCompany(int id)
        {

            var resul = await dCompanies.removeCompany(id);
            if (resul != null && resul.CompanyID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<List<ECountries>> getCountries()
        {

            List<ECountries> countries = dCompanies.getCountries();

            return countries;
        }
    }
}
