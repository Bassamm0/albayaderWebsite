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
    public class DCalenderEvents
    {

        public List<ECalenderEvents> getAllCalenderEvents()
        {
            List<ECalenderEvents> users = new List<ECalenderEvents>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("  Select ET.*,C.*,(U.FirstName + ' '+U.Lastname) as TechnicainName,B.BranchName from CalenderEvents C  ");
                    sQuery.Append("   inner join eventType ET on ET.eventTypeId=C.eventTypeId ");
                    sQuery.Append("  left join users U on C.TechnicanId=U.UserId ");
                    sQuery.Append("  left join Branchs B on B.branchId=C.branchId  ");
                    sQuery.Append(" where C.EndDate is null order by C.eventStartDate");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECalenderEvents oECalenderEvents = new ECalenderEvents();
                            if (dataReader["EventId"] != DBNull.Value) { oECalenderEvents.EventId = (int)dataReader["EventId"]; }
                            if (dataReader["statusId"] != DBNull.Value) { oECalenderEvents.statusId = (int)dataReader["statusId"]; }
                            if (dataReader["title"] != DBNull.Value) { oECalenderEvents.title = (string)dataReader["title"]; }
                            if (dataReader["eventStartDate"] != DBNull.Value) { oECalenderEvents.eventStartDate = (string)dataReader["eventStartDate"]; }
                            if (dataReader["eventEndDate"] != DBNull.Value) { oECalenderEvents.eventEndDate = (string)dataReader["eventEndDate"]; }
                            if (dataReader["allDay"] != DBNull.Value) { oECalenderEvents.allDay = (bool)dataReader["allDay"]; }
                            if (dataReader["url"] != DBNull.Value) { oECalenderEvents.url = (string)dataReader["url"]; }
                            if (dataReader["TechnicanId"] != DBNull.Value) { oECalenderEvents.TechnicanId = (int)dataReader["TechnicanId"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oECalenderEvents.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["eventTypeId"] != DBNull.Value) { oECalenderEvents.eventTypeId = (int)dataReader["eventTypeId"]; }
                            if (dataReader["description"] != DBNull.Value) { oECalenderEvents.description = (string)dataReader["description"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oECalenderEvents.branchId = (int)dataReader["branchId"]; }
                            if (dataReader["TechnicainName"] != DBNull.Value) { oECalenderEvents.TechnicainName = (string)dataReader["TechnicainName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oECalenderEvents.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["eventType"] != DBNull.Value) { oECalenderEvents.eventType = (string)dataReader["eventType"]; }

                            users.Add(oECalenderEvents);
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


        public List<ECalenderEvents> getAllBranchCalenderEvents(int compnayId)
        {
            List<ECalenderEvents> users = new List<ECalenderEvents>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();

                    sQuery.Append("  Select ET.*,C.*,(U.FirstName + ' '+U.Lastname) as TechnicainName,B.BranchName from CalenderEvents C  ");
                    sQuery.Append("   inner join eventType ET on ET.eventTypeId=C.eventTypeId ");
                    sQuery.Append("  left join users U on C.TechnicanId=U.UserId ");
                    sQuery.Append("  left join Branchs B on B.branchId=C.branchId  ");
                    sQuery.AppendFormat(" where B.compnayId={0}  or C.eventTypeId=3   and C.EndDate is null order by C.eventStartDate", compnayId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECalenderEvents oECalenderEvents = new ECalenderEvents();
                            if (dataReader["EventId"] != DBNull.Value) { oECalenderEvents.EventId = (int)dataReader["EventId"]; }
                            if (dataReader["title"] != DBNull.Value) { oECalenderEvents.title = (string)dataReader["title"]; }
                            if (dataReader["eventStartDate"] != DBNull.Value) { oECalenderEvents.eventStartDate = (string)dataReader["eventStartDate"]; }
                            if (dataReader["eventEndDate"] != DBNull.Value) { oECalenderEvents.eventEndDate = (string)dataReader["eventEndDate"]; }
                            if (dataReader["allDay"] != DBNull.Value) { oECalenderEvents.allDay = (bool)dataReader["allDay"]; }
                            if (dataReader["url"] != DBNull.Value) { oECalenderEvents.url = (string)dataReader["url"]; }
                            if (dataReader["TechnicanId"] != DBNull.Value) { oECalenderEvents.TechnicanId = (int)dataReader["TechnicanId"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oECalenderEvents.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["eventTypeId"] != DBNull.Value) { oECalenderEvents.eventTypeId = (int)dataReader["eventTypeId"]; }
                            if (dataReader["description"] != DBNull.Value) { oECalenderEvents.description = (string)dataReader["description"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oECalenderEvents.branchId = (int)dataReader["branchId"]; }
                            if (dataReader["TechnicainName"] != DBNull.Value) { oECalenderEvents.TechnicainName = (string)dataReader["TechnicainName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oECalenderEvents.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["eventType"] != DBNull.Value) { oECalenderEvents.eventType = (string)dataReader["eventType"]; }
                            if (dataReader["statusId"] != DBNull.Value) { oECalenderEvents.statusId = (int)dataReader["statusId"]; }


                            users.Add(oECalenderEvents);
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
        public List<ECalenderEvents> getUserCalenderEvents(int UserId)
        {
            List<ECalenderEvents> users = new List<ECalenderEvents>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();

                    sQuery.Append("  Select ET.*,C.*,(U.FirstName + ' '+U.Lastname) as TechnicainName,B.BranchName from CalenderEvents C  ");
                    sQuery.Append("   inner join eventType ET on ET.eventTypeId=C.eventTypeId ");
                    sQuery.Append("  left join users U on C.TechnicanId=U.UserId ");
                    sQuery.Append("  left join Branchs B on B.branchId=C.branchId  ");
                    
                    sQuery.AppendFormat(" where U.UserId={0}  or C.eventTypeId=3 or C.eventTypeId=4  and C.EndDate is null order by C.eventStartDate", UserId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECalenderEvents oECalenderEvents = new ECalenderEvents();
                            if (dataReader["EventId"] != DBNull.Value) { oECalenderEvents.EventId = (int)dataReader["EventId"]; }
                            if (dataReader["title"] != DBNull.Value) { oECalenderEvents.title = (string)dataReader["title"]; }
                            if (dataReader["eventStartDate"] != DBNull.Value) { oECalenderEvents.eventStartDate = (string)dataReader["eventStartDate"]; }
                            if (dataReader["eventEndDate"] != DBNull.Value) { oECalenderEvents.eventEndDate = (string)dataReader["eventEndDate"]; }
                            if (dataReader["allDay"] != DBNull.Value) { oECalenderEvents.allDay = (bool)dataReader["allDay"]; }
                            if (dataReader["url"] != DBNull.Value) { oECalenderEvents.url = (string)dataReader["url"]; }
                            if (dataReader["TechnicanId"] != DBNull.Value) { oECalenderEvents.TechnicanId = (int)dataReader["TechnicanId"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oECalenderEvents.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["eventTypeId"] != DBNull.Value) { oECalenderEvents.eventTypeId = (int)dataReader["eventTypeId"]; }
                            if (dataReader["description"] != DBNull.Value) { oECalenderEvents.description = (string)dataReader["description"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oECalenderEvents.branchId = (int)dataReader["branchId"]; }
                            if (dataReader["TechnicainName"] != DBNull.Value) { oECalenderEvents.TechnicainName = (string)dataReader["TechnicainName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oECalenderEvents.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["eventType"] != DBNull.Value) { oECalenderEvents.eventType = (string)dataReader["eventType"]; }
                            if (dataReader["statusId"] != DBNull.Value) { oECalenderEvents.statusId = (int)dataReader["statusId"]; }


                            users.Add(oECalenderEvents);
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

        public ECalenderEvents getSingleCalenderEvent(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            ECalenderEvents oECalenderEvents = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("  Select B.compnayId,ET.*,C.*,(U.FirstName + ' '+U.Lastname) as TechnicainName,B.BranchName from CalenderEvents C  ");
                    sQuery.Append("   inner join eventType ET on ET.eventTypeId=C.eventTypeId ");
                    sQuery.Append("  left join users U on C.TechnicanId=U.UserId ");
                    sQuery.Append("  left join Branchs B on B.branchId=C.branchId  ");
                    sQuery.AppendFormat(" where C.EventId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oECalenderEvents = new ECalenderEvents();
                            if (dataReader["EventId"] != DBNull.Value) { oECalenderEvents.EventId = (int)dataReader["EventId"]; }
                            if (dataReader["title"] != DBNull.Value) { oECalenderEvents.title = (string)dataReader["title"]; }
                            if (dataReader["eventStartDate"] != DBNull.Value) { oECalenderEvents.eventStartDate = (string)dataReader["eventStartDate"]; }
                            if (dataReader["eventEndDate"] != DBNull.Value) { oECalenderEvents.eventEndDate = (string)dataReader["eventEndDate"]; }
                            if (dataReader["allDay"] != DBNull.Value) { oECalenderEvents.allDay = (bool)dataReader["allDay"]; }
                            if (dataReader["url"] != DBNull.Value) { oECalenderEvents.url = (string)dataReader["url"]; }
                            if (dataReader["TechnicanId"] != DBNull.Value) { oECalenderEvents.TechnicanId = (int)dataReader["TechnicanId"]; }
                            if (dataReader["createdBy"] != DBNull.Value) { oECalenderEvents.createdBy = (int)dataReader["createdBy"]; }
                            if (dataReader["eventTypeId"] != DBNull.Value) { oECalenderEvents.eventTypeId = (int)dataReader["eventTypeId"]; }
                            if (dataReader["description"] != DBNull.Value) { oECalenderEvents.description = (string)dataReader["description"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oECalenderEvents.branchId = (int)dataReader["branchId"]; }
                            if (dataReader["TechnicainName"] != DBNull.Value) { oECalenderEvents.TechnicainName = (string)dataReader["TechnicainName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oECalenderEvents.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["eventType"] != DBNull.Value) { oECalenderEvents.eventType = (string)dataReader["eventType"]; }
                            if (dataReader["compnayId"] != DBNull.Value) { oECalenderEvents.compnayId = (int)dataReader["compnayId"]; }
                            if (dataReader["statusId"] != DBNull.Value) { oECalenderEvents.statusId = (int)dataReader["statusId"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oECalenderEvents;
        }

        public async Task<ECalenderEvents> addCalenderEvent(ECalenderEvents newCalenderEvent)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.CalenderEvents.AddAsync(newCalenderEvent);
                await context.SaveChangesAsync();
            }

            return newCalenderEvent;
        }
        public async Task<ECalenderEvents> updateCalenderEvent(ECalenderEvents CalenderEvent)
        {

            if (CalenderEvent == null)
            {
                throw new DomainValidationFundException("Validation : The CalenderEvent is not found, make sure you are updating the correct CalenderEvent");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.CalenderEvents.Attach(CalenderEvent);
                context.Entry(CalenderEvent).Property(x => x.title).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.eventStartDate).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.eventEndDate).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.allDay).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.url).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.TechnicanId).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.eventTypeId).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.description).IsModified = true;
                context.Entry(CalenderEvent).Property(x => x.branchId).IsModified = true;

                await context.SaveChangesAsync();
            }

            return CalenderEvent;
        }
        public async Task<ECalenderEvents> deleteCalenderEvent(int Id)
        {
            ECalenderEvents eCalenderEvent = new ECalenderEvents();
            eCalenderEvent = getSingleCalenderEvent(Id);
            if (eCalenderEvent == null)
            {
                throw new DomainValidationFundException("Validation : The CalenderEvent is not found, make sure you are deleting the correct CalenderEvent");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.CalenderEvents.Remove(eCalenderEvent);
                await context.SaveChangesAsync();

            }

            return eCalenderEvent;
        }
      
        public async Task<ECalenderEvents> removeCalenderEvent(int id)
        {
            ECalenderEvents eCalenderEvent = new ECalenderEvents();


            eCalenderEvent = getSingleCalenderEvent(id);
            eCalenderEvent.EndDate = DateTime.UtcNow;

            if (eCalenderEvent == null)
            {
                throw new DomainValidationFundException("Validation : The CalenderEvent is not found, make sure you are removing the correct CalenderEvent");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.CalenderEvents.Attach(eCalenderEvent);
                context.Entry(eCalenderEvent).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eCalenderEvent;
        }


        public async Task<ECalenderEvents> completeCalenderEvent(int id)
        {
            ECalenderEvents eCalenderEvent = new ECalenderEvents();


            eCalenderEvent = getSingleCalenderEvent(id);
            eCalenderEvent.statusId =5;

            if (eCalenderEvent == null)
            {
                throw new DomainValidationFundException("Validation : The CalenderEvent is not found, make sure you are removing the correct CalenderEvent");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.CalenderEvents.Attach(eCalenderEvent);
                context.Entry(eCalenderEvent).Property(x => x.statusId).IsModified = true;
                await context.SaveChangesAsync();

            }

            return eCalenderEvent;
        }

    }
}
