using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LOGIC;
using Entity;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using static DAL.DALException;
using System.Globalization;

namespace API.Controllers
{
    [Route("api/company/")]
    [ApiController]
    public class APICompanies : ControllerBase
    {

        private CompanyLogic CompanyLogic=new CompanyLogic();

        [Route("all")]
        [HttpGet]
        public async Task<List<ECompanies>> getAllCompanies()
        {
            List<ECompanies> companayList = new List<ECompanies>();
            companayList = await CompanyLogic.getAllCompanies();
            
            return companayList;
        }

        [Route("getCompanyById")]
        [HttpGet]
        public async Task<ECompanies> getCompanyById([FromBody] JsonElement objData)
        {
            int _id = objData.GetProperty("id").GetInt16();
            ECompanies company=new ECompanies();
             company = await CompanyLogic.getCompanyById(_id);
            if (company == null)
            {
                throw new DomainValidationFundException("No data");
            }
            return company;
        }

        [Route("add")]
        [HttpPost]
        public async Task<Boolean> addCompany([FromBody] JsonElement objData)
        {

            bool result = false;
            try
            {
                var CountryId = objData.GetProperty("countrid").GetInt16();
                var Name = objData.GetProperty("name").GetString();
                var Description = objData.GetProperty("description").GetString();
                var City = objData.GetProperty("city").GetString();
                var Street = objData.GetProperty("street").GetString();
                var StreetNo = objData.GetProperty("streetno").GetString();
                 var Telephone = objData.GetProperty("telephone").GetString();
                var Fax = objData.GetProperty("fax").GetString();
                var Latitude = objData.GetProperty("latitude").GetDecimal();
                var Longitude = objData.GetProperty("longitude").GetDecimal();
                var CompanyLogo = objData.GetProperty("companylogo").GetString();
                var CompanyTypeId = objData.GetProperty("companytypeid").GetInt16();

                ECompanies newCompany = new ECompanies
                {
                    CountryId = CountryId,
                    Name = Name,
                    Description = Description,
                    City = City,
                    Street = Street,
                    StreetNo = StreetNo,
                    Telephone = Telephone,
                    Fax = Fax,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    CompanyLogo = CompanyLogo,
                    CompanyTypeId = CompanyTypeId,
                    EndDate = null,
                   

                };
                result = await CompanyLogic.addCompany(newCompany);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }
            return result;
        }

        [Route("update")]
        [HttpPost]
        public async Task<Boolean> updateUser([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var CompanyId = objData.GetProperty("companyid").GetInt16();
                var CountryId = objData.GetProperty("countrid").GetInt16();
                var Name = objData.GetProperty("name").GetString();
                var Description = objData.GetProperty("description").GetString();
                var City = objData.GetProperty("city").GetString();
                var Street = objData.GetProperty("street").GetString();
                var StreetNo = objData.GetProperty("streetno").GetString();
                var Telephone = objData.GetProperty("telephone").GetString();
                var Fax = objData.GetProperty("fax").GetString();
                var Latitude = objData.GetProperty("latitude").GetDecimal();
                var Longitude = objData.GetProperty("longitude").GetDecimal();
                var CompanyLogo = objData.GetProperty("companylogo").GetString();
                var CompanyTypeId = objData.GetProperty("companytypeid").GetInt16();
                ECompanies UpdatedCompany = new ECompanies
                {
                    CompanyID = CompanyId,
                    CountryId = CountryId,
                    Name = Name,
                    Description = Description,
                    City = City,
                    Street = Street,
                    StreetNo = StreetNo,
                    Telephone = Telephone,
                    Fax = Fax,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    CompanyLogo = CompanyLogo,
                    CompanyTypeId = CompanyTypeId,

                };
                result = await CompanyLogic.updateCompany(UpdatedCompany);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }
        [Route("delete")]
        [HttpPost]
        public async Task<Boolean> deleteCompany([FromBody] JsonElement objData)

        {
            var Id = objData.GetProperty("id").GetInt16();

            bool result = false;
            result = await CompanyLogic.deleteCompany(Id);

            return result;
        }
        [Route("remove")]
        [HttpPost]
        public async Task<Boolean> removeCompany([FromBody] JsonElement objData)
        {

            bool result = false;

            try
            {
                var id = objData.GetProperty("id").GetInt16();
               
               
                result = await CompanyLogic.removeCompany(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The given key was not present in the dictionary.")
                {
                    throw new DomainValidationFundException("Validation : One or more paramter are missing in the request,Error could be becuase of case sensetive");
                }
                if (ex.InnerException.ToString().Contains("Cannot insert the value NULL into column"))
                {
                    throw new DomainValidationFundException("Validation : null value not allowed to one of the parameters");
                }
                return false;
            }

            return result;
        }

    }
}
