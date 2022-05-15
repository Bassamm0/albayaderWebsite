﻿using System;
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
                context.Entry(Branch).Property(x => x.BranchName).IsModified = true;
             
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
            eBranch.EndDate = DateTime.Now;

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
    }
}