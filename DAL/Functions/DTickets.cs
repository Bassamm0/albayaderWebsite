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
    public class Dtickets
    {
        public List<EticketViews> getAlltickets()
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.Append(" order by t.creationDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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

        public List<EticketViews> getAllOpentickets()
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.Append(" where most_recent_status.ticketStatusId not in(7) order by t.creationDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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
        public List<EticketViews> getAllOpenticketsview()
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select top(18) t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.Append(" where most_recent_status.ticketStatusId not in(7) order by t.creationDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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

        public List<EticketViews> getAllOpenticketsDate(string startDate,string endDate)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where most_recent_status.ticketStatusId not in(7) and t.creationdate >= '{0}' and t.creationdate <= '{1}' ",startDate,endDate);
                    sQuery.Append(" order by t.creationDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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

        public List<EticketViews> getAllClosedtickets()
        {
            List<EticketViews> users = new List<EticketViews>();
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.Append(" where most_recent_status.ticketStatusId=7 order by t.creationDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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



        public List<EticketViews> getAllCompanytickets(int companyid)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} order by t.creationDate desc", companyid);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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

        public List<EticketViews> getAllCompanyOpentickets(int companyid)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} and most_recent_status.ticketStatusId not in(7) order by t.creationDate desc", companyid);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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
        public List<EticketViews> getUserOpentickets(int companyid,int userId)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} and most_recent_status.ticketStatusId not in(7)  and t.createdBy={1} order by t.creationDate desc", companyid,userId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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
        public List<EticketViews> getAllCompanyOpenticketsDate(int companyid,string startDate,string endDate)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} and most_recent_status.ticketStatusId not in(7) and t.creationdate >= '{1}' and t.creationdate <= '{2}'", companyid, startDate, endDate);
                    sQuery.Append(" order by t.creationDate desc ");
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEticketViews.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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
        public List<EticketViews> getAllCompanyClosedtickets(int companyid)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} and most_recent_status.ticketStatusId=7 order by t.creationDate desc", companyid);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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

        public List<EticketViews> getUserClosedtickets(int companyid,int userId)
        {
            List<EticketViews> users = new List<EticketViews>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,TAS.ServiceId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join ( ");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.Append(" left join ticketAndService TAS on TAS.ticketId=t.ticketId ");
                    sQuery.AppendFormat(" where C.CompanyID={0} and most_recent_status.ticketStatusId=7  and t.createdBy={1} order by t.creationDate desc", companyid,userId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketViews oEticketViews = new EticketViews();
                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["serviceId"] != DBNull.Value) { oEticketViews.serviceId = (int)dataReader["serviceId"]; }

                            users.Add(oEticketViews);
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
        public EticketViewsDetails getSingleticket(int ticketId)
        {
            DticketLog dtlog = new DticketLog();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EticketViewsDetails oEticketViews = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select t.*,B.compnayId,CONCAT(AU.FirstName,' ',AU.Lastname) as AssignedUser,most_recent_status.StatusDate,most_recent_status.ticketStatusId,ts.StatusName,U.FirstName,CONCAT(U.FirstName,' ',U.Lastname) as CreatorName,U.PictureFileName,B.BranchName,C.Name CompanyName,C.CompanyID from tickets t join (");
                    sQuery.Append(" select * from ticketAndStatus ");
                    sQuery.Append("  where ticketAndStatusId in ( ");
                    sQuery.Append("  select max(ticketAndStatusId) from ticketAndStatus group by ticketid ");
                    sQuery.Append("   ) ");
                    sQuery.Append(" ) as most_recent_status ");
                    sQuery.Append(" on t.ticketid = most_recent_status.ticketid ");
                    sQuery.Append(" inner join ticketCategory TC on TC.ticketCategoryId=t.ticketCategoryId");
                    sQuery.Append(" inner join ticketStatus TS on TS.ticketStatusId=most_recent_status.ticketStatusId ");
                    sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" inner join Companies C on C.CompanyID=B.compnayId ");
                    sQuery.Append(" left join");
                    sQuery.Append(" (   ");
                    sQuery.Append(" select * from ticketAndUser ");
                    sQuery.Append("   where assginDate in (");
                    sQuery.Append(" select max(assginDate) from ticketAndUser group by ticketid");
                    sQuery.Append("  )");
                    sQuery.Append("  ) as most_recent_assign ");
                    sQuery.Append("  on most_recent_assign.ticketId=t.ticketId ");
                    sQuery.Append(" left Join users AU on AU.UserId=most_recent_assign.AssginUserId ");
                    sQuery.AppendFormat(" where t.ticketId={0} ", ticketId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEticketViews = new EticketViewsDetails();

                            if (dataReader["ticketID"] != DBNull.Value) { oEticketViews.ticketId = (int)dataReader["ticketID"]; }
                            if (dataReader["subject"] != DBNull.Value) { oEticketViews.subject = (string)dataReader["subject"]; }
                            if (dataReader["ticketDetails"] != DBNull.Value) { oEticketViews.ticketDetails = (string)dataReader["ticketDetails"]; }
                            if (dataReader["creationDate"] != DBNull.Value) { oEticketViews.creationDate = (DateTime)dataReader["creationDate"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oEticketViews.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["severityId"] != DBNull.Value) { oEticketViews.severityId = (int)dataReader["severityId"]; }
                            if (dataReader["ticketCategoryId"] != DBNull.Value) { oEticketViews.ticketCategoryId = (int)dataReader["ticketCategoryId"]; }
                            if (dataReader["StatusDate"] != DBNull.Value) { oEticketViews.StatusDate = (DateTime)dataReader["StatusDate"]; }
                            if (dataReader["CreatorName"] != DBNull.Value) { oEticketViews.CreatorName = (string)dataReader["CreatorName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEticketViews.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEticketViews.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusName"] != DBNull.Value) { oEticketViews.StatusName = (string)dataReader["StatusName"]; }
                            if (dataReader["AssignedUser"] != DBNull.Value) { oEticketViews.AssignedUser = (string)dataReader["AssignedUser"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEticketViews.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["ticketStatusId"] != DBNull.Value) { oEticketViews.ticketStatusId = (int)dataReader["ticketStatusId"]; }
                            oEticketViews.lticketfile = getAllTicketFile(ticketId);
                            oEticketViews.lticketLog= dtlog.getticketLog(ticketId);

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEticketViews;
        }

        public async Task<Etickets> addticket(Etickets newticket)
        {
            newticket.creationDate = DateTime.UtcNow;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.tickets.AddAsync(newticket);
                await context.SaveChangesAsync();
            }

            return newticket;
        }
        public async Task<Etickets> updateticket(Etickets ticket)
        {
           
            if (ticket == null)
            {
                throw new DomainValidationFundException("Validation : The ticket is not found, make sure you are updating the correct ticket");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.tickets.Attach(ticket);
                context.Entry(ticket).Property(x => x.subject).IsModified = true;
                context.Entry(ticket).Property(x => x.ticketDetails).IsModified = true;
   
               
                context.Entry(ticket).Property(x => x.severityId).IsModified = true;
                context.Entry(ticket).Property(x => x.ticketCategoryId).IsModified = true;

                await context.SaveChangesAsync();
            }

            return ticket;
        }
       

       

        public int gettotalticketCount()
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat(" select count(*)as ticketCount from tickets ");
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public int getCompanytotalticketCount(int companyId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" select count(*)as ticketCount from tickets t ");
                sQuery.Append(" inner join Users U on U.UserId=t.createdBy ");
                sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId");
                 sQuery.AppendFormat(" where b.CompanyID={0} ", companyId);
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

        public async Task<EticketAndStatus> insertNewStatus(EticketAndStatus newticketStatus)
        {
            newticketStatus.StatusDate = DateTime.UtcNow;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ticketAndStatus.AddAsync(newticketStatus);
                await context.SaveChangesAsync();
            }
            return newticketStatus;
        }

        public List<EticketFiles> getAllTicketFile(int ticketId)
        {
            List<EticketFiles> users = new List<EticketFiles>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from ticketFiles TF ");
                    sQuery.AppendFormat(" where TF.ticketId={0}  and endDate is null", ticketId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketFiles oEticketFiles = new EticketFiles();
                            if (dataReader["ticketFileId"] != DBNull.Value) { oEticketFiles.ticketFileId = (int)dataReader["ticketFileId"]; }
                            if (dataReader["ticketId"] != DBNull.Value) { oEticketFiles.ticketId = (int)dataReader["ticketId"]; }
                            if (dataReader["fileName"] != DBNull.Value) { oEticketFiles.fileName = (string)dataReader["fileName"]; }
                          


                            users.Add(oEticketFiles);
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


        public async Task<EticketAndUser> assginTicketuser(EticketAndUser ticketAndUser)
        {
            ticketAndUser.assginDate = DateTime.UtcNow;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ticketAndUser.AddAsync(ticketAndUser);
                await context.SaveChangesAsync();
            }

            return ticketAndUser;
        }

        public async Task<EticketAndService> createTicketService(EticketAndService ticketAndService)
        {
            ticketAndService.CreationDate = DateTime.UtcNow;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ticketAndService.AddAsync(ticketAndService);
                await context.SaveChangesAsync();
            }

            return ticketAndService;
        }
        public int getTicketCreatorId(int ticketId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            int result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat("select createdBy from tickets where ticketId={0} ", ticketId);
                command.CommandText = sQuery.ToString();
                result = (int)command.ExecuteScalar();
            }

            return result;
        }

    }
}
