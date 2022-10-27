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
    public class DticketLog
    {
        public List<EticketLog> getAllticketLog()
        {
            List<EticketLog> users = new List<EticketLog>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select TL.*,CONCAT(U.FirstName,' ',U.Lastname) as UserFullName from ticketLog TL");
                    sQuery.Append(" inner join Users U on U.UserId=TL.UserId  ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketLog oEticketLog = new EticketLog();
                            if (dataReader["ticketLogId"] != DBNull.Value) { oEticketLog.ticketLogId = (int)dataReader["ticketLogId"]; }
                            if (dataReader["ticketId"] != DBNull.Value) { oEticketLog.ticketId = (int)dataReader["ticketId"]; }
                            if (dataReader["Message"] != DBNull.Value) { oEticketLog.Message = (string)dataReader["Message"]; }
                            if (dataReader["CreationDate"] != DBNull.Value) { oEticketLog.CreationDate = (DateTime)dataReader["CreationDate"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEticketLog.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["UserFullName"] != DBNull.Value) { oEticketLog.UserFullName = (string)dataReader["UserFullName"]; }
                           

                            users.Add(oEticketLog);
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


        public List<EticketLog> getticketLog(int ticketId)
        {
            List<EticketLog> users = new List<EticketLog>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select TL.*,CONCAT(U.FirstName,' ',U.Lastname) as UserFullName from ticketLog TL");
                    sQuery.Append(" inner join Users U on U.UserId=TL.UserId  ");
                   
                    sQuery.AppendFormat(" where TL.ticketId={0}", ticketId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketLog oEticketLog = new EticketLog();
                            if (dataReader["ticketLogId"] != DBNull.Value) { oEticketLog.ticketLogId = (int)dataReader["ticketLogId"]; }
                            if (dataReader["ticketId"] != DBNull.Value) { oEticketLog.ticketId = (int)dataReader["ticketId"]; }
                            if (dataReader["Message"] != DBNull.Value) { oEticketLog.Message = (string)dataReader["Message"]; }
                            if (dataReader["CreationDate"] != DBNull.Value) { oEticketLog.CreationDate = (DateTime)dataReader["CreationDate"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEticketLog.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["UserFullName"] != DBNull.Value) { oEticketLog.UserFullName = (string)dataReader["UserFullName"]; }

                            oEticketLog.lticketLogFiles = getAllTicketLogFile(oEticketLog.ticketLogId);
                            users.Add(oEticketLog);
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

        public EticketLog getSingleticketLog(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EticketLog oEticketLog = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select TL.*,CONCAT(U.FirstName,' ',U.Lastname) as UserFullName from ticketLog TL");
                    sQuery.Append(" inner join Users U on U.UserId=TL.UserId  ");
                    sQuery.AppendFormat(" where TL.ticketLogId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEticketLog = new EticketLog();

                            if (dataReader["ticketLogId"] != DBNull.Value) { oEticketLog.ticketLogId = (int)dataReader["ticketLogId"]; }
                            if (dataReader["ticketId"] != DBNull.Value) { oEticketLog.ticketId = (int)dataReader["ticketId"]; }
                            if (dataReader["Message"] != DBNull.Value) { oEticketLog.Message = (string)dataReader["Message"]; }
                            if (dataReader["CreationDate"] != DBNull.Value) { oEticketLog.CreationDate = (DateTime)dataReader["CreationDate"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEticketLog.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["UserFullName"] != DBNull.Value) { oEticketLog.UserFullName = (string)dataReader["UserFullName"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEticketLog;
        }

        public async Task<EticketLog> addticketLog(EticketLog newticketLog)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ticketLog.AddAsync(newticketLog);
                await context.SaveChangesAsync();
            }

            return newticketLog;
        }
        public async Task<EticketLog> updateticketLog(EticketLog ticketLog)
        {

            ticketLog.CreationDate = DateTime.UtcNow;
            if (ticketLog == null)
            {
                throw new DomainValidationFundException("Validation : The ticketLog is not found, make sure you are updating the correct ticketLog");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ticketLog.Attach(ticketLog);
                context.Entry(ticketLog).Property(x => x.ticketId).IsModified = true;
                context.Entry(ticketLog).Property(x => x.Message).IsModified = true;
                context.Entry(ticketLog).Property(x => x.CreationDate).IsModified = true;
                context.Entry(ticketLog).Property(x => x.UserId).IsModified = true;


                await context.SaveChangesAsync();
            }

            return ticketLog;
        }
        public async Task<EticketLog> deleteticketLog(int Id)
        {
            EticketLog eticketLog = new EticketLog();
            eticketLog = getSingleticketLog(Id);
            if (eticketLog == null)
            {
                throw new DomainValidationFundException("Validation : The ticketLog is not found, make sure you are deleting the correct ticketLog");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.ticketLog.Remove(eticketLog);
                await context.SaveChangesAsync();

            }

            return eticketLog;
        }


        public List<EticketLogFiles> getAllTicketLogFile(int ticketlogIdId)
        {
            List<EticketLogFiles> users = new List<EticketLogFiles>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from ticketLogFiles TF ");
                    sQuery.AppendFormat("where TF.ticketLogId={0}", ticketlogIdId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EticketLogFiles oEticketFiles = new EticketLogFiles();
                            if (dataReader["ticketLogFileId"] != DBNull.Value) { oEticketFiles.ticketLogFileId = (int)dataReader["ticketLogFileId"]; }
                            if (dataReader["ticketLogId"] != DBNull.Value) { oEticketFiles.ticketLogId = (int)dataReader["ticketLogId"]; }
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


    }
}
