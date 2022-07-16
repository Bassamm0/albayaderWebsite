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
    public class DBranchs
    {
        public List<EBranchs> getAllBranchs()
        {
            List<EBranchs> users = new List<EBranchs>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Branchs C ");
                    sQuery.Append(" where C.EndDate is null order by C.BranchName");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EBranchs oEBranchs = new EBranchs();
                            if (dataReader["BranchID"] != DBNull.Value) { oEBranchs.BranchId = (int)dataReader["BranchID"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEBranchs.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompnayId"] != DBNull.Value) { oEBranchs.CompnayId = (int)dataReader["CompnayId"]; }
                            if (dataReader["Latitude"] != DBNull.Value) { oEBranchs.Latitude = (decimal)dataReader["Latitude"]; }
                            if (dataReader["Longitude"] != DBNull.Value) { oEBranchs.Longitude = (decimal)dataReader["Longitude"]; }
                            if (dataReader["EmirateId"] != DBNull.Value) { oEBranchs.EmirateId = (int)dataReader["EmirateId"]; }
                            if (dataReader["District"] != DBNull.Value) { oEBranchs.District = (string)dataReader["District"]; }

                            users.Add(oEBranchs);
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


        public List<EBranchs> getAllCompanyBranchs(int companyid)
        {
            List<EBranchs> users = new List<EBranchs>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Branchs C ");
                    sQuery.AppendFormat(" where C.CompnayId={0} and C.EndDate is null order by C.BranchName", companyid);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EBranchs oEBranchs = new EBranchs();
                            if (dataReader["BranchID"] != DBNull.Value) { oEBranchs.BranchId = (int)dataReader["BranchID"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEBranchs.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompnayId"] != DBNull.Value) { oEBranchs.CompnayId = (int)dataReader["CompnayId"]; }
                            if (dataReader["Latitude"] != DBNull.Value) { oEBranchs.Latitude = (decimal)dataReader["Latitude"]; }
                            if (dataReader["Longitude"] != DBNull.Value) { oEBranchs.Longitude = (decimal)dataReader["Longitude"]; }
                            if (dataReader["EmirateId"] != DBNull.Value) { oEBranchs.EmirateId = (int)dataReader["EmirateId"]; }
                            if (dataReader["District"] != DBNull.Value) { oEBranchs.District = (string)dataReader["District"]; }


                            users.Add(oEBranchs);
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

        public EBranchs getSingleBranch(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EBranchs oEBranchs = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Branchs C ");
                    sQuery.AppendFormat(" where C.BranchId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEBranchs = new EBranchs();

                            if (dataReader["BranchId"] != DBNull.Value) { oEBranchs.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEBranchs.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompnayId"] != DBNull.Value) { oEBranchs.CompnayId = (int)dataReader["CompnayId"]; }
                            if (dataReader["Latitude"] != DBNull.Value) { oEBranchs.Latitude = (decimal)dataReader["Latitude"]; }
                            if (dataReader["Longitude"] != DBNull.Value) { oEBranchs.Longitude = (decimal)dataReader["Longitude"]; }
                            if (dataReader["EmirateId"] != DBNull.Value) { oEBranchs.EmirateId = (int)dataReader["EmirateId"]; }
                            if (dataReader["District"] != DBNull.Value) { oEBranchs.District = (string)dataReader["District"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEBranchs;
        }

        public async Task<EBranchs> addBranch(EBranchs newBranch)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Branchs.AddAsync(newBranch);
                await context.SaveChangesAsync();
            }

            return newBranch;
        }
        public async Task<EBranchs> updateBranch(EBranchs Branch)
        {
            EBranchs eBranch = new EBranchs();
            eBranch = getSingleBranch(Branch.BranchId);
            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are updating the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Branchs.Attach(Branch);
                context.Entry(Branch).Property(x => x.CompnayId).IsModified = true;
                context.Entry(Branch).Property(x => x.Latitude).IsModified = true;
                context.Entry(Branch).Property(x => x.Longitude).IsModified = true;
                context.Entry(Branch).Property(x => x.BranchName).IsModified = true;
                context.Entry(Branch).Property(x => x.EmirateId).IsModified = true;
                context.Entry(Branch).Property(x => x.District).IsModified = true;
             
                await context.SaveChangesAsync();
            }

            return Branch;
        }
        public async Task<EBranchs> deleteBranch(int Id)
        {
            EBranchs eBranch = new EBranchs();
            eBranch = getSingleBranch(Id);
            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are deleting the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Branchs.Remove(eBranch);
                await context.SaveChangesAsync();

            }

            return eBranch;
        }


        public async Task<EBranchs> removeBranch(int id)
        {
            EBranchs eBranch = new EBranchs();


            eBranch = getSingleBranch(id);
            eBranch.EndDate = DateTime.UtcNow;

            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Branchs.Attach(eBranch);
                context.Entry(eBranch).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eBranch;
        }


        public int gettotalBranchCount()
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" select count(*)as branchCount from Branchs where EndDate is null ");
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public int getCompanytotalBranchCount(int companyId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" select count(*)as branchCount from Branchs br ");
                sQuery.Append(" inner join Companies C on C.CompanyID=BR.compnayId ");
                sQuery.AppendFormat(" where BR.EndDate is null and C.CompanyID={0} ", companyId);
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }
    }
}
