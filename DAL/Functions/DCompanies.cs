using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static DAL.DALException;

namespace DAL.Functions
{
    public class DCompanies
    {
        public List<ECompanies> getAllCompanies()
        {
            List<ECompanies> users = new List<ECompanies>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select C.*,CO.Name CountryName  from Companies C  ");
                    sQuery.Append(" inner join Countries CO on CO.CountryId=C.CountryId ");
                    sQuery.Append(" where C.EndDate is null order by C.CompanyID");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECompanies oECompanies = new ECompanies();
                            if (dataReader["CompanyID"] != DBNull.Value) { oECompanies.CompanyID = (int)dataReader["CompanyID"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oECompanies.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["Name"] != DBNull.Value) { oECompanies.Name = (string)dataReader["Name"]; }
                            if (dataReader["Description"] != DBNull.Value) { oECompanies.Description = (string)dataReader["Description"]; }
                            if (dataReader["City"] != DBNull.Value) { oECompanies.City = (string)dataReader["City"]; }
                            if (dataReader["Street"] != DBNull.Value) { oECompanies.Street = (string)dataReader["Street"]; }
                            if (dataReader["StreetNo"] != DBNull.Value) { oECompanies.StreetNo = (string)dataReader["StreetNo"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oECompanies.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["Fax"] != DBNull.Value) { oECompanies.Fax = (string)dataReader["Fax"]; }
                            if (dataReader["Latitude"] != DBNull.Value) { oECompanies.Latitude = (decimal)dataReader["Latitude"]; }
                            if (dataReader["Longitude"] != DBNull.Value) { oECompanies.Longitude = (decimal)dataReader["Longitude"]; }
                            if (dataReader["Altitude"] != DBNull.Value) { oECompanies.Altitude = (decimal)dataReader["Altitude"]; }
                            if (dataReader["CompanyLogo"] != DBNull.Value) { oECompanies.CompanyLogo = (string)dataReader["CompanyLogo"]; }
                            if (dataReader["CountryName"] != DBNull.Value) { oECompanies.CountryName = (string)dataReader["CountryName"]; }
                            if (dataReader["OpId"] != DBNull.Value) { oECompanies.OpId = (int)dataReader["OpId"]; }
                            if (dataReader["CompanyTypeId"] != DBNull.Value) { oECompanies.CompanyTypeId = (int)dataReader["CompanyTypeId"]; }
 

                            users.Add(oECompanies);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return users;
        }

        public ECompanies getSingleCompany(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            ECompanies oECompanies = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select C.*,CO.Name CountryName  from Companies C  ");
                    sQuery.Append(" inner join Countries CO on CO.CountryId=C.CountryId ");
                    sQuery.AppendFormat(" where C.CompanyID ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oECompanies = new ECompanies();

                            if (dataReader["CompanyID"] != DBNull.Value) { oECompanies.CompanyID = (int)dataReader["CompanyID"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oECompanies.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["Name"] != DBNull.Value) { oECompanies.Name = (string)dataReader["Name"]; }
                            if (dataReader["Description"] != DBNull.Value) { oECompanies.Description = (string)dataReader["Description"]; }
                            if (dataReader["City"] != DBNull.Value) { oECompanies.City = (string)dataReader["City"]; }
                            if (dataReader["Street"] != DBNull.Value) { oECompanies.Street = (string)dataReader["Street"]; }
                            if (dataReader["StreetNo"] != DBNull.Value) { oECompanies.StreetNo = (string)dataReader["StreetNo"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oECompanies.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["Fax"] != DBNull.Value) { oECompanies.Fax = (string)dataReader["Fax"]; }
                            if (dataReader["Latitude"] != DBNull.Value) { oECompanies.Latitude = (decimal)dataReader["Latitude"]; }
                            if (dataReader["Longitude"] != DBNull.Value) { oECompanies.Longitude = (decimal)dataReader["Longitude"]; }
                            if (dataReader["Altitude"] != DBNull.Value) { oECompanies.Altitude = (decimal)dataReader["Altitude"]; }
                            if (dataReader["CompanyLogo"] != DBNull.Value) { oECompanies.CompanyLogo = (string)dataReader["CompanyLogo"]; }
                            if (dataReader["OpId"] != DBNull.Value) { oECompanies.OpId = (int)dataReader["OpId"]; }
                            if (dataReader["CompanyTypeId"] != DBNull.Value) { oECompanies.CompanyTypeId = (int)dataReader["CompanyTypeId"]; }
                            if (dataReader["CountryName"] != DBNull.Value) { oECompanies.CountryName = (string)dataReader["CountryName"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oECompanies;
        }

        public async Task<ECompanies> addCompany(ECompanies newCompany)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Companies.AddAsync(newCompany);
                await context.SaveChangesAsync();
            }

            return newCompany;
        }
        public async Task<ECompanies> updateCompany(ECompanies company)
        {
            ECompanies eCompany = new ECompanies();
            eCompany = getSingleCompany(company.CompanyID);
            if (eCompany == null)
            {
                throw new DomainValidationFundException("Validation : The company is not found, make sure you are updating the correct company");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Companies.Attach(company);
                context.Entry(company).Property(x => x.CountryId).IsModified = true;
                context.Entry(company).Property(x => x.Name).IsModified = true;
                context.Entry(company).Property(x => x.Description).IsModified = true;
                context.Entry(company).Property(x => x.City).IsModified = true;
                context.Entry(company).Property(x => x.Street).IsModified = true;
                context.Entry(company).Property(x => x.StreetNo).IsModified = true;
                context.Entry(company).Property(x => x.Telephone).IsModified = true;
                context.Entry(company).Property(x => x.Fax).IsModified = true;
                context.Entry(company).Property(x => x.Latitude).IsModified = true;
                context.Entry(company).Property(x => x.Longitude).IsModified = true;
                context.Entry(company).Property(x => x.CompanyLogo).IsModified = true;
                context.Entry(company).Property(x => x.CompanyTypeId).IsModified = true;
                await context.SaveChangesAsync();
            }

            return company;
        }
        public async Task<ECompanies> deleteCompany(int Id)
        {
            ECompanies eCompany = new ECompanies();
            eCompany = getSingleCompany(Id);
            if (eCompany == null)
            {
                throw new DomainValidationFundException("Validation : The company is not found, make sure you are deleting the correct company");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Companies.Remove(eCompany);
                await context.SaveChangesAsync();

            }

            return eCompany;
        }


        public async Task<ECompanies> removeCompany(int id)
        {
            ECompanies eCompany = new ECompanies();
           

            eCompany = getSingleCompany(id);
            eCompany.EndDate = DateTime.Now;

            if (eCompany == null)
            {
                throw new DomainValidationFundException("Validation : The company is not found, make sure you are removing the correct company");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Companies.Attach(eCompany);
                context.Entry(eCompany).Property(x => x.EndDate).IsModified = true;
              
                await context.SaveChangesAsync();
            }

            return eCompany;
        }

        public List<ECountries> getCountries()
        {
            List<ECountries> users = new List<ECountries>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Countries ");
                  

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECountries oECountries = new ECountries();
                            if (dataReader["CountryId"] != DBNull.Value) { oECountries.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["Name"] != DBNull.Value) { oECountries.Name = (string)dataReader["Name"]; }
                            if (dataReader["sortname"] != DBNull.Value) { oECountries.sortname = (string)dataReader["sortname"]; }
                        


                            users.Add(oECountries);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return users;
        }
    }
}
