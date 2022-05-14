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
    public class DUserAndBranch
    {
        public List<EUserAndBranch> getAllUserAndBranch()
        {
            List<EUserAndBranch> users = new List<EUserAndBranch>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from UserAndBranch C ");
                    sQuery.Append(" where C.EndDate is null ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EUserAndBranch oEUserAndBranch = new EUserAndBranch();
                            if (dataReader["BranchID"] != DBNull.Value) { oEUserAndBranch.BranchId = (int)dataReader["BranchID"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEUserAndBranch.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["UserAndBranchId"] != DBNull.Value) { oEUserAndBranch.UserAndBranchId = (int)dataReader["UserAndBranchId"]; }


                            users.Add(oEUserAndBranch);
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

        public EUserAndBranch getSingleUserAndBranch(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EUserAndBranch oEUserAndBranch = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from UserAndBranch C ");
                    sQuery.AppendFormat(" where C.UserAndBranchId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEUserAndBranch = new EUserAndBranch();

                            if (dataReader["BranchID"] != DBNull.Value) { oEUserAndBranch.BranchId = (int)dataReader["BranchID"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEUserAndBranch.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["UserAndBranchId"] != DBNull.Value) { oEUserAndBranch.UserAndBranchId = (int)dataReader["UserAndBranchId"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEUserAndBranch;
        }

        public async Task<EUserAndBranch> addUserAndBranch(EUserAndBranch newBranch)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.UserAndBranch.AddAsync(newBranch);
                await context.SaveChangesAsync();
            }

            return newBranch;
        }
        public async Task<EUserAndBranch> updateUserAndBranch(EUserAndBranch UserAndBranch)
        {
            EUserAndBranch eBranch = new EUserAndBranch();
            eBranch = getSingleUserAndBranch(UserAndBranch.UserAndBranchId);
            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are updating the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.UserAndBranch.Attach(UserAndBranch);
                context.Entry(UserAndBranch).Property(x => x.UserId).IsModified = true;
                context.Entry(UserAndBranch).Property(x => x.BranchId).IsModified = true;

                await context.SaveChangesAsync();
            }

            return UserAndBranch;
        }
        public async Task<EUserAndBranch> deleteUserAndBranch(int Id)
        {
            EUserAndBranch eBranch = new EUserAndBranch();
            eBranch = getSingleUserAndBranch(Id);
            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are deleting the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.UserAndBranch.Remove(eBranch);
                await context.SaveChangesAsync();

            }

            return eBranch;
        }


        public async Task<EUserAndBranch> removeUserAndBranch(int id)
        {
            EUserAndBranch eBranch = new EUserAndBranch();


            eBranch = getSingleUserAndBranch(id);
            eBranch.EndDate = DateTime.Now;

            if (eBranch == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.UserAndBranch.Attach(eBranch);
                context.Entry(eBranch).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eBranch;
        }
    }
}
