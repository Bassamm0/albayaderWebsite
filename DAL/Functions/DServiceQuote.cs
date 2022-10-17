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
    public class DServiceQuote
    {
        public List<EServiceQuote> getAllServiceQuote()
        {
            List<EServiceQuote> users = new List<EServiceQuote>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select  QAS.QuotationStatusId,QAS.CreatedDate,QS.QuotationStatus, B.BranchName,CO.Name CompanyName,SQ.*,SR.* from ServiceQuotes SQ ");
                    sQuery.Append(" left join Services SR on SR.ServiceId=SQ.ServiceId ");
                    sQuery.Append(" inner  join Branchs b on b.branchId=SQ.BranchId  ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=B.compnayId ");
                    sQuery.Append("  inner join QuotationAndStatus QAS on QAS.ServiceQuoteId=SQ.ServiceQuoteId ");
                    sQuery.Append("  inner join QuotationStatus QS on QS.QuotationStatusId=QAS.QuotationStatusId ");
                    sQuery.Append(" where SQ.Enddate is null and QAS.EndDate is null  order by SQ.ServiceQuoteId desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceQuote oEServiceQuote = new EServiceQuote();
                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceQuote.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceQuoteDate"] != DBNull.Value) { oEServiceQuote.ServiceQuoteDate = dataReader["ServiceQuoteDate"].ToString(); }
                            if (dataReader["ServiceQuoteFile"] != DBNull.Value) { oEServiceQuote.ServiceQuoteFile = (string)dataReader["ServiceQuoteFile"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceQuote.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceQuote.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ReferenceId"] != DBNull.Value) { oEServiceQuote.ReferenceId = (string)dataReader["ReferenceId"]; }

                            if (dataReader["QuotationStatusId"] != DBNull.Value) { oEServiceQuote.QuotationStatusId = (int)dataReader["QuotationStatusId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceQuote.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["QuotationStatus"] != DBNull.Value) { oEServiceQuote.QuotationStatus = (string)dataReader["QuotationStatus"]; }

                            if (dataReader["OpId"] != DBNull.Value) { oEServiceQuote.OpId = (int)dataReader["OpId"]; }
                            oEServiceQuote.QouteDetails = getAllQuotationDetailse(oEServiceQuote.ServiceQuoteId);

                            users.Add(oEServiceQuote);
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
        public List<EServiceQuote> getAllServiceQuoteByDate(string startDate, string endDate)
        {
            List<EServiceQuote> users = new List<EServiceQuote>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select QAS.QuotationStatusId,QAS.CreatedDate,QS.QuotationStatus,  B.BranchName,CO.Name CompanyName,SQ.*,SR.* from ServiceQuotes SQ ");
                    sQuery.Append(" left join Services SR on SR.ServiceId=SQ.ServiceId ");
                    sQuery.Append(" inner  join Branchs b on b.branchId=SQ.BranchId  ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=B.compnayId ");
                    sQuery.Append("  inner join QuotationAndStatus QAS on QAS.ServiceQuoteId=SQ.ServiceQuoteId ");
                    sQuery.Append("  inner join QuotationStatus QS on QS.QuotationStatusId=QAS.QuotationStatusId ");

                    sQuery.AppendFormat(" where SQ.Enddate is null and QAS.EndDate is null  and SQ.ServiceQuoteDate between '{0}' and '{1}'  order by SQ.ServiceQuoteId desc", startDate, endDate);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceQuote oEServiceQuote = new EServiceQuote();
                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceQuote.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceQuoteDate"] != DBNull.Value) { oEServiceQuote.ServiceQuoteDate = dataReader["ServiceQuoteDate"].ToString(); }
                            if (dataReader["ServiceQuoteFile"] != DBNull.Value) { oEServiceQuote.ServiceQuoteFile = (string)dataReader["ServiceQuoteFile"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceQuote.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceQuote.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ReferenceId"] != DBNull.Value) { oEServiceQuote.ReferenceId = (string)dataReader["ReferenceId"]; }
                           
                            if (dataReader["QuotationStatusId"] != DBNull.Value) { oEServiceQuote.QuotationStatusId = (int)dataReader["QuotationStatusId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceQuote.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["QuotationStatus"] != DBNull.Value) { oEServiceQuote.QuotationStatus = (string)dataReader["QuotationStatus"]; }


                            if (dataReader["OpId"] != DBNull.Value) { oEServiceQuote.OpId = (int)dataReader["OpId"]; }
                            oEServiceQuote.QouteDetails = getAllQuotationDetailse(oEServiceQuote.ServiceQuoteId);

                            users.Add(oEServiceQuote);
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

        public List<EServiceQuote> getAllCompanyServiceQuote(int companyid)
        {
            List<EServiceQuote> users = new List<EServiceQuote>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select  QAS.QuotationStatusId,QAS.CreatedDate,QS.QuotationStatus, B.BranchName,CO.Name CompanyName,SQ.*,SR.* from ServiceQuotes SQ ");
                    sQuery.Append(" left join Services SR on SR.ServiceId=SQ.ServiceId ");
                    sQuery.Append(" inner  join Branchs b on b.branchId=SQ.BranchId  ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=B.compnayId ");
                    sQuery.Append("  inner join QuotationAndStatus QAS on QAS.ServiceQuoteId=SQ.ServiceQuoteId ");
                    sQuery.Append("  inner join QuotationStatus QS on QS.QuotationStatusId=QAS.QuotationStatusId ");

                    sQuery.AppendFormat(" where CO.CompanyID={0} and and QAS.EndDate is null  SQ.Enddate is null  order by SQ.ServiceQuoteId desc", companyid);
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceQuote oEServiceQuote = new EServiceQuote();
                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceQuote.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceQuoteDate"] != DBNull.Value) { oEServiceQuote.ServiceQuoteDate = dataReader["ServiceQuoteDate"].ToString(); }
                            if (dataReader["ServiceQuoteFile"] != DBNull.Value) { oEServiceQuote.ServiceQuoteFile = (string)dataReader["ServiceQuoteFile"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceQuote.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceQuote.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ReferenceId"] != DBNull.Value) { oEServiceQuote.ReferenceId = (string)dataReader["ReferenceId"]; }
                            if (dataReader["QuotationStatusId"] != DBNull.Value) { oEServiceQuote.QuotationStatusId = (int)dataReader["QuotationStatusId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceQuote.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["QuotationStatus"] != DBNull.Value) { oEServiceQuote.QuotationStatus = (string)dataReader["QuotationStatus"]; }


                            if (dataReader["OpId"] != DBNull.Value) { oEServiceQuote.OpId = (int)dataReader["OpId"]; }
                            oEServiceQuote.QouteDetails = getAllQuotationDetailse(oEServiceQuote.ServiceQuoteId);
                            users.Add(oEServiceQuote);
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
        public List<EServiceQuote> getAllCompanyServiceQuoteDate(int companyid, string startDate, string endDate)
        {
            List<EServiceQuote> users = new List<EServiceQuote>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select QAS.QuotationStatusId,QAS.CreatedDate,QS.QuotationStatus,B.BranchName,CO.Name CompanyName,SQ.*,SR.* from ServiceQuotes SQ ");
                    sQuery.Append(" left join Services SR on SR.ServiceId=SQ.ServiceId ");
                    sQuery.Append(" inner  join Branchs b on b.branchId=SQ.BranchId  ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=B.compnayId ");
                    sQuery.Append("  inner join QuotationAndStatus QAS on QAS.ServiceQuoteId=SQ.ServiceQuoteId ");
                    sQuery.Append("  inner join QuotationStatus QS on QS.QuotationStatusId=QAS.QuotationStatusId ");
                    sQuery.AppendFormat(" where CO.CompanyID={0} and QAS.EndDate is null SQ.Enddate is null and SQ.ServiceQuoteDate between '{1}' and '{2}' order by SQ.ServiceQuoteId desc", companyid, startDate, endDate);
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceQuote oEServiceQuote = new EServiceQuote();
                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceQuote.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceQuoteDate"] != DBNull.Value) { oEServiceQuote.ServiceQuoteDate = dataReader["ServiceQuoteDate"].ToString(); }
                            if (dataReader["ServiceQuoteFile"] != DBNull.Value) { oEServiceQuote.ServiceQuoteFile = (string)dataReader["ServiceQuoteFile"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceQuote.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["ReferenceId"] != DBNull.Value) { oEServiceQuote.ReferenceId = (string)dataReader["ReferenceId"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceQuote.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["QuotationStatusId"] != DBNull.Value) { oEServiceQuote.QuotationStatusId = (int)dataReader["QuotationStatusId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceQuote.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["QuotationStatus"] != DBNull.Value) { oEServiceQuote.QuotationStatus = (string)dataReader["QuotationStatus"]; }

                            if (dataReader["OpId"] != DBNull.Value) { oEServiceQuote.OpId = (int)dataReader["OpId"]; }
                            oEServiceQuote.QouteDetails = getAllQuotationDetailse(oEServiceQuote.ServiceQuoteId);
                            users.Add(oEServiceQuote);
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

        public EServiceQuote getSingleServiceQuote(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EServiceQuote oEServiceQuote = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select QAS.QuotationStatusId,QAS.CreatedDate,QS.QuotationStatus,B.BranchName,CO.Name CompanyName,CO.CompanyID,b.branchId,SQ.*,SR.* from ServiceQuotes SQ ");
                    sQuery.Append(" left join Services SR on SR.ServiceId=SQ.ServiceId ");
                    sQuery.Append(" inner  join Branchs b on b.branchId=SQ.BranchId  ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=B.compnayId ");

                    sQuery.Append("  inner join QuotationAndStatus QAS on QAS.ServiceQuoteId=SQ.ServiceQuoteId ");
                    sQuery.Append("  inner join QuotationStatus QS on QS.QuotationStatusId=QAS.QuotationStatusId ");

                    sQuery.AppendFormat(" where SQ.ServiceQuoteId={0} and QAS.EndDate is null", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEServiceQuote = new EServiceQuote();

                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceQuote.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceQuoteDate"] != DBNull.Value) { oEServiceQuote.ServiceQuoteDate = dataReader["ServiceQuoteDate"].ToString(); }
                            if (dataReader["ServiceQuoteFile"] != DBNull.Value) { oEServiceQuote.ServiceQuoteFile = (string)dataReader["ServiceQuoteFile"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceQuote.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceQuote.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ReferenceId"] != DBNull.Value) { oEServiceQuote.ReferenceId = (string)dataReader["ReferenceId"]; }
                            if (dataReader["CompanyID"] != DBNull.Value) { oEServiceQuote.CompanyId = (int)dataReader["CompanyID"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oEServiceQuote.BranchId = (int)dataReader["branchId"]; }
                            if (dataReader["QuotationStatusId"] != DBNull.Value) { oEServiceQuote.QuotationStatusId = (int)dataReader["QuotationStatusId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceQuote.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["QuotationStatus"] != DBNull.Value) { oEServiceQuote.QuotationStatus = (string)dataReader["QuotationStatus"]; }

                            if (dataReader["OpId"] != DBNull.Value) { oEServiceQuote.OpId = (int)dataReader["OpId"]; }
                            oEServiceQuote.QouteDetails = getAllQuotationDetailse(oEServiceQuote.ServiceQuoteId);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEServiceQuote;
        }

        public async Task<EServiceQuote> addServiceQuote(EServiceQuote newServiceQuote)
        {

            newServiceQuote.ServiceQuoteDate = DateTime.UtcNow.ToString();
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ServiceQuotes.AddAsync(newServiceQuote);
                await context.SaveChangesAsync();
            }

            return newServiceQuote;
        }
        public async Task<EServiceQuote> updateServiceQuote(EServiceQuote ServiceQuote)
        {


            if (ServiceQuote == null)
            {
                throw new DomainValidationFundException("Validation : The ServiceQuote is not found, make sure you are updating the correct ServiceQuote");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ServiceQuotes.Attach(ServiceQuote);
                context.Entry(ServiceQuote).Property(x => x.BranchId).IsModified = true;
                context.Entry(ServiceQuote).Property(x => x.ServiceId).IsModified = true;
                context.Entry(ServiceQuote).Property(x => x.ReferenceId).IsModified = true;
                context.Entry(ServiceQuote).Property(x => x.ServiceQuoteFile).IsModified = true;


                await context.SaveChangesAsync();
            }

            insertQuoteStatus(ServiceQuote.ServiceQuoteId, ServiceQuote.QuotationStatusId);

            return ServiceQuote;
        }
        public async Task<EServiceQuote> deleteServiceQuote(int Id)
        {
            EServiceQuote eServiceQuote = new EServiceQuote();
            eServiceQuote = getSingleServiceQuote(Id);
            if (eServiceQuote == null)
            {
                throw new DomainValidationFundException("Validation : The ServiceQuote is not found, make sure you are deleting the correct ServiceQuote");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ServiceQuotes.Remove(eServiceQuote);
                await context.SaveChangesAsync();

            }

            return eServiceQuote;
        }


        public async Task<EServiceQuote> removeServiceQuote(int id)
        {
            EServiceQuote eServiceQuote = new EServiceQuote();


            eServiceQuote = getSingleServiceQuote(id);
            eServiceQuote.EndDate =  DateTime.UtcNow;

            if (eServiceQuote == null)
            {
                throw new DomainValidationFundException("Validation : The ServiceQuote is not found, make sure you are removing the correct ServiceQuote");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ServiceQuotes.Attach(eServiceQuote);
                context.Entry(eServiceQuote).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eServiceQuote;
        }


        public int gettotalServiceQuoteCount()
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" select count(*)as ServiceQuoteCount from ServiceQuotes where EndDate is null ");
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public int getCompanytotalServiceQuoteCount(int companyId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" Select count(*) from ServiceQuotes SQ ");
                sQuery.Append(" inner join Services SR on SR.ServiceId=SQ.ServiceId ");
                sQuery.Append(" inner Join Branchs B on B.branchId=SR.BranchId");
                sQuery.AppendFormat(" where b.compnayId={0} and SQ.Enddate is null  ", companyId);
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public async Task<EQuotationDetails> addQuotationDetails(EQuotationDetails newQuotationDetails)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.quotationDetails.AddAsync(newQuotationDetails);
                await context.SaveChangesAsync();
            }

            return newQuotationDetails;
        }
        public async Task<EQuotationDetails> updateQuotationDetails(EQuotationDetails quotationDetails)
        {
            if (quotationDetails == null)
            {
                throw new DomainValidationFundException("Validation : The ServiceQuote is not found, make sure you are updating the correct ServiceQuote");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.quotationDetails.Attach(quotationDetails);
                context.Entry(quotationDetails).Property(x => x.QuotationPrice).IsModified = true;
                context.Entry(quotationDetails).Property(x => x.MaterialId).IsModified = true;
                context.Entry(quotationDetails).Property(x => x.Qty).IsModified = true;
                context.Entry(quotationDetails).Property(x => x.Description).IsModified = true;


                await context.SaveChangesAsync();
            }

            return quotationDetails;
        }

        public List<EQuotationDetails> getAllQuotationDetailse(int ServiceQuoteId)
        {
            List<EQuotationDetails> users = new List<EQuotationDetails>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select *,M.MaterialName,Q.MaterialId from QuotationDetails Q ");
                    sQuery.Append(" inner join Materials M on M.MaterialId=Q.MaterialId ");
                    sQuery.AppendFormat(" where  Q.ServiceQuoteId={0}", ServiceQuoteId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EQuotationDetails oEServiceQuote = new EQuotationDetails();
                            if (dataReader["QuotationDetailsId"] != DBNull.Value) { oEServiceQuote.QuotationDetailsId = (int)dataReader["QuotationDetailsId"]; }
                            if (dataReader["ServiceQuoteID"] != DBNull.Value) { oEServiceQuote.ServiceQuoteId = (int)dataReader["ServiceQuoteID"]; }
                            if (dataReader["MaterialId"] != DBNull.Value) { oEServiceQuote.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["QuotationPrice"] != DBNull.Value) { oEServiceQuote.QuotationPrice = (decimal)dataReader["QuotationPrice"]; }
                            if (dataReader["Qty"] != DBNull.Value) { oEServiceQuote.Qty = (int)dataReader["Qty"]; }
                            if (dataReader["Description"] != DBNull.Value) { oEServiceQuote.Description = (string)dataReader["Description"]; }

                            if (dataReader["MaterialName"] != DBNull.Value) { oEServiceQuote.MaterialName = (string)dataReader["MaterialName"]; }

                            users.Add(oEServiceQuote);
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



        // delete all quote details
        public bool deleteAllQuoteDetails(int ServiceQuoteId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" delete QuotationDetails where ServiceQuoteId={0} ", ServiceQuoteId);



                command.CommandText = sQuery.ToString();
                // for select only to retrive value ExecuteScalar()
                return (command.ExecuteNonQuery() > 0);

            }

            return result;
        }

        public bool insertBuldQuoteMaterials(int ServiceQuoteId, List<EQuotationDetails> lQuoteDetials,int OpId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" insert into QuotationDetails values ");


                for (int i = 0; i < lQuoteDetials.Count; i++)
                {

                    if (lQuoteDetials[i].QuotationPrice == null)
                    {
                        lQuoteDetials[i].QuotationPrice = 0;
                    }
                    if (lQuoteDetials[i].Qty == null)
                    {
                        lQuoteDetials[i].Qty = 0;
                    }
                    if (i == lQuoteDetials.Count - 1)
                    {
                        sQuery.AppendFormat(" ( {0}, {1},{2},{3},'{4}',{5}) ",ServiceQuoteId, lQuoteDetials[i].MaterialId, lQuoteDetials[i].QuotationPrice, lQuoteDetials[i].Qty, lQuoteDetials[i].Description, OpId);

                    }
                    else
                    {
                        sQuery.AppendFormat(" ( {0}, {1},{2},{3},'{4}',{5}), ", ServiceQuoteId, lQuoteDetials[i].MaterialId, lQuoteDetials[i].QuotationPrice, lQuoteDetials[i].Qty, lQuoteDetials[i].Description, OpId);

                    }

                }

                command.CommandText = sQuery.ToString();
                // for select only to retrive value ExecuteScalar()
                return (command.ExecuteNonQuery() > 0);

            }

            return result;
        }

        public bool EndQuoteStatus(int ServiceQuoteId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" update  QuotationAndStatus set EndDate='{0}' where ServiceQuoteId={1} and EndDate is null ", DateTime.UtcNow, ServiceQuoteId);
                command.CommandText = sQuery.ToString();
                // for select only to retrive value ExecuteScalar()
                return (command.ExecuteNonQuery() > 0);

            }

            return result;
        }

        public bool insertQuoteStatus(int ServiceQuoteId, int QuotationStatusId)
        {

            EndQuoteStatus(ServiceQuoteId);
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" INSERT INTO QuotationAndStatus(ServiceQuoteId,QuotationStatusId,CreatedDate)VALUES({0},{1},'{2}') ", ServiceQuoteId, QuotationStatusId, DateTime.UtcNow);
                command.CommandText = sQuery.ToString();
                // for select only to retrive value ExecuteScalar()
                return (command.ExecuteNonQuery() > 0);

            }

            return result;
        }
    }

}

