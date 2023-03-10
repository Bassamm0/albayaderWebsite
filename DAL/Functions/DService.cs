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
    public class DService
    {
        public List<EServiceModel> getAllService()
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,STF.StatusAfterName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.Append(" where SR.EndDate is null order by SR.CreatedDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);
                           
                                services.Add(oEServiceModel);
                        

                           
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }


        
        public List<EServiceModel> getAllServiceByStatus(int StatusId)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName, STF.StatusAfterName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId={0} ",StatusId);
                    sQuery.Append(" and  SR.EndDate is null order by SR.CreatedDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                           if (oEServiceModel.ServiceTypeId == 1)
                            {
                                oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);
                            }
                            else
                            {
                                oEServiceModel.CorrectiveServiceDetails = getAllCorrectiveServiceDetails(oEServiceModel.ServiceId);
                            }
                           
                            if (oEServiceModel.ServiceDetails != null && oEServiceModel.ServiceDetails.Count>0)
                            {
                                services.Add(oEServiceModel);
                            }
                            if (StatusId != 1)
                            {
                                if (oEServiceModel.CorrectiveServiceDetails != null && oEServiceModel.CorrectiveServiceDetails.Count > 0)
                                {
                                    services.Add(oEServiceModel);
                                }
                            }
                            else
                            {
                                services.Add(oEServiceModel);

                            }
                            


                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }
        public List<EServiceModel> getAllServiceByStatusCompany(int StatusId,int companyId)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName, STF.StatusAfterName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId={0} and CO.CompanyId={1} ", StatusId,companyId);
                    sQuery.Append("  and SR.EndDate is null order by SR.CreatedDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            // oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }

        public List<EServiceModel> getAllCompletedService()
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select  CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5" );
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                         
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }

                            //if (oEServiceModel.ServiceTypeId == 1)
                            //{
                            //    oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);

                            //}
                            //else
                            //{
                            //    oEServiceModel.CorrectiveServiceDetails = getAllCorrectiveServiceDetails(oEServiceModel.ServiceId);
                            //}

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }
        public List<EServiceModel> getServiceReport()
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select  CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,CO.CompanyId,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5");
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }

                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEServiceModel.CompanyId = (int)dataReader["CompanyId"]; }

                            //if (oEServiceModel.ServiceTypeId == 1)
                            //{
                            //    oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);

                            //}
                            //else
                            //{
                            //    oEServiceModel.CorrectiveServiceDetails = getAllCorrectiveServiceDetails(oEServiceModel.ServiceId);
                            //}

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }

        public List<EServiceModel> getAllCompletedServiceBranch(int BranchId)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,CO.CompanyId,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and BR.branchId={0} ",BranchId);
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }
                            // oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEServiceModel.CompanyId = (int)dataReader["CompanyId"]; }

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }
        public List<EServiceModel> getAllCompletedServiceDate(string startDate,string endDate)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,CO.CompanyId,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.CompletionDate between '{0}' and '{1}'", startDate, endDate);
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }
                            // oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEServiceModel.CompanyId = (int)dataReader["CompanyId"]; }

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }
        public List<EServiceModel> getAllCompletedServiceCompany(int companyId)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and CO.CompanyID={0}", companyId);
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                             if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }


                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }

                            //if (oEServiceModel.ServiceTypeId == 1)
                            //{
                            //    oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);

                            //}
                            //else
                            //{
                            //    oEServiceModel.CorrectiveServiceDetails=getAllCorrectiveServiceDetails(oEServiceModel.ServiceId);
                            //}

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }

        public List<EServiceModel> getAllCompletedServiceCompanydate(int companyId,string startDate,String endDate)
        {
            List<EServiceModel> services = new List<EServiceModel>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,CO.CompanyId,ST.ServiceTypeName, STF.StatusAfterName ,STV.VistTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType STV on STV.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and CO.CompanyID={0} and SR.CompletionDate between '{1}' and '{2}'", companyId, startDate, endDate);
                    sQuery.Append("  and SR.EndDate is null order by SR.CompletionDate desc ");
                
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceModel oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }

                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                            // oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);
                            if (dataReader["CompanyId"] != DBNull.Value) { oEServiceModel.CompanyId = (int)dataReader["CompanyId"]; }

                            services.Add(oEServiceModel);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return services;
        }

        public EServiceModel getSingleService(int ServiceId)
        {

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();

            EServiceModel oEServiceModel = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName,UT.PictureFileName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.*,STF.StatusAfterName  from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.ServiceId={0} ",ServiceId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            
                            oEServiceModel = new EServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate =dataReader["CompletionDate"].ToString(); }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEServiceModel.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SupervisourName"] != DBNull.Value) { oEServiceModel.SupervisourName = (string)dataReader["SupervisourName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["SupervisourSignature"] != DBNull.Value) { oEServiceModel.SupervisourSignature = (string)dataReader["SupervisourSignature"]; }
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                            if (dataReader["Recommendation"] != DBNull.Value) { oEServiceModel.recomendation = (string)dataReader["Recommendation"]; }
                            if (dataReader["serviceRender"] != DBNull.Value) { oEServiceModel.serviceRender = (string)dataReader["serviceRender"]; }

                            oEServiceModel.ServiceDetails = getAllServiceDetails(oEServiceModel.ServiceId);                          
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEServiceModel;
        }

       
        public ECorrectiveServiceModel getCorrectiveSingleService(int ServiceId)
        {

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();

            ECorrectiveServiceModel oEServiceModel = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName,UT.PictureFileName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SVT.*,SR.*,STF.StatusAfterName  from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join SiteVistType SVT on SVT.SiteVistTypeId=SR.SiteVistTypeId ");
                    sQuery.Append(" left join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.ServiceId={0} ", ServiceId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            oEServiceModel = new ECorrectiveServiceModel();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = (DateTime)dataReader["CompletionDate"]; }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEServiceModel.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
                            if (dataReader["SiteVistTypeId"] != DBNull.Value) { oEServiceModel.SiteVistTypeId = (int)dataReader["SiteVistTypeId"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oEServiceModel.VistTypeName = (string)dataReader["VistTypeName"]; }
                            if (dataReader["SupervisourName"] != DBNull.Value) { oEServiceModel.SupervisourName = (string)dataReader["SupervisourName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["SupervisourSignature"] != DBNull.Value) { oEServiceModel.SupervisourSignature = (string)dataReader["SupervisourSignature"]; }
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }
                            if (dataReader["Recommendation"] != DBNull.Value) { oEServiceModel.Recommendation = (string)dataReader["Recommendation"]; }
                            if (dataReader["RootOfCause"] != DBNull.Value) { oEServiceModel.rootOfCause = (string)dataReader["RootOfCause"]; }

                            oEServiceModel.ServiceDetails = getAllCorrectiveServiceDetails(oEServiceModel.ServiceId);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEServiceModel;
        }
        public EServices getSingleServiceOnly(int ServiceId)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();

            EServices oEServiceModel = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();

                    sQuery.Append("select CONCAT(U.FirstName,' ',U.Lastname) as CreaterName ,CONCAT(UT.FirstName,' ',UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.AppendFormat(" where SR.ServiceId={0} ", ServiceId);
               
                    
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            oEServiceModel = new EServices();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceTypeId"] != DBNull.Value) { oEServiceModel.ServiceTypeId = (int)dataReader["ServiceTypeId"]; }
                            if (dataReader["StatusId"] != DBNull.Value) { oEServiceModel.StatusId = (int)dataReader["StatusId"]; }
                            if (dataReader["TechnicianId"] != DBNull.Value) { oEServiceModel.TechnicianId = (int)dataReader["TechnicianId"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEServiceModel.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["CreatedDate"] != DBNull.Value) { oEServiceModel.CreatedDate = (DateTime)dataReader["CreatedDate"]; }
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = (DateTime)dataReader["CompletionDate"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["SupervisourName"] != DBNull.Value) { oEServiceModel.SupervisourName = (string)dataReader["SupervisourName"]; }
                            if (dataReader["SupervisourFeedback"] != DBNull.Value) { oEServiceModel.SupervisourFeedback = (string)dataReader["SupervisourFeedback"]; }
                            if (dataReader["SupervisourSignature"] != DBNull.Value) { oEServiceModel.SupervisourSignature = (string)dataReader["SupervisourSignature"]; }
                            if (dataReader["SupervisourMobile"] != DBNull.Value) { oEServiceModel.SupervisourMobile = (string)dataReader["SupervisourMobile"]; }
                            if (dataReader["SupervisourDesignation"] != DBNull.Value) { oEServiceModel.SupervisourDesignation = (string)dataReader["SupervisourDesignation"]; }


                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEServiceModel;
        }


        public async Task<EServices> addService(EServices newService)
        {

            newService.EndDate = null;
            newService.CompletionDate = null;
            newService.Remark = null;
            newService.SupervisourName = null;
            newService.SupervisourSignature = null;
            newService.SupervisourFeedback = null;
            newService.StatusAfterId = 4;
            //newService.SiteVistTypeId = 1;
            newService.CreatedDate=DateTime.UtcNow;

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Services.AddAsync(newService);
                await context.SaveChangesAsync();
            }

            return newService;
        }

        public async Task<EServices> updateService(EServices Service)
        {
            EServices eServices = new EServices();
             eServices = getSingleServiceOnly(Service.ServiceId);
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Services.Attach(Service);
                context.Entry(eServices).Property(x => x.ServiceId).IsModified = true;
                context.Entry(eServices).Property(x => x.StatusId).IsModified = true;
                context.Entry(eServices).Property(x => x.TechnicianId).IsModified = true;
                context.Entry(eServices).Property(x => x.ServiceTypeId).IsModified = true;
                context.Entry(eServices).Property(x => x.Remark).IsModified = true;
                context.Entry(eServices).Property(x => x.Recommendation).IsModified = true;
                context.Entry(eServices).Property(x => x.serviceRender).IsModified = true;
                context.Entry(eServices).Property(x => x.StatusAfterId).IsModified = true;

                await context.SaveChangesAsync();
            }

            return Service;
        }

        public async Task<EServices> removeService(int id)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(id);
            eServices.EndDate = DateTime.UtcNow;

            if (eServices == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Services.Attach(eServices);
                context.Entry(eServices).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eServices;
        }

        public async Task<EServices> updateStatus(int serviceId, int statusId,string remark,int statusAfterId,int siteVistTypeId, string Recommendation, string serviceRender,string rootOfCause)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(serviceId);
            eServices.StatusId = statusId;
            eServices.Remark=remark;
            eServices.StatusAfterId=statusAfterId;
            eServices.SiteVistTypeId= siteVistTypeId;
            eServices.Recommendation= Recommendation;
            eServices.serviceRender= serviceRender;
            eServices.rootOfCause = rootOfCause;

            if (eServices == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Services.Attach(eServices);
                context.Entry(eServices).Property(x => x.StatusId).IsModified = true;
                context.Entry(eServices).Property(x => x.Remark).IsModified = true;
                context.Entry(eServices).Property(x => x.StatusAfterId).IsModified = true;
                context.Entry(eServices).Property(x => x.SiteVistTypeId).IsModified = true;
                context.Entry(eServices).Property(x => x.Recommendation).IsModified = true;
                context.Entry(eServices).Property(x => x.serviceRender).IsModified = true;
                context.Entry(eServices).Property(x => x.rootOfCause).IsModified = true;

                await context.SaveChangesAsync();
            }
            
          
            //complete
            if (eServices.StatusId == 5)
            {
              
            }


            return eServices;
        }


        public async Task<EServices> clientSignature(int serviceId, string SupervisourSignature, 
            string SupervisourName, string SupervisourFeedback, string SupervisourMobile, string SupervisourDesignation)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(serviceId);
            eServices.StatusId = 5;
            eServices.SupervisourSignature = SupervisourSignature;
            eServices.SupervisourName = SupervisourName;
            eServices.SupervisourFeedback = SupervisourFeedback;
            eServices.SupervisourMobile = SupervisourMobile;
            eServices.SupervisourDesignation = SupervisourDesignation;
            eServices.CompletionDate = DateTime.UtcNow;

            if (eServices == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Services.Attach(eServices);
                context.Entry(eServices).Property(x => x.StatusId).IsModified = true;
                context.Entry(eServices).Property(x => x.SupervisourSignature).IsModified = true;
                context.Entry(eServices).Property(x => x.SupervisourName).IsModified = true;
                context.Entry(eServices).Property(x => x.SupervisourFeedback).IsModified = true;
                context.Entry(eServices).Property(x => x.SupervisourMobile).IsModified = true;
                context.Entry(eServices).Property(x => x.SupervisourDesignation).IsModified = true;
                context.Entry(eServices).Property(x => x.CompletionDate).IsModified = true;

                await context.SaveChangesAsync();
            }
            //send email
            DUser dUser = new DUser();
            List<EUser> ousers = new List<EUser>();
            ousers = dUser.getAllAdminAndManager();

            List<EUser> ousersManger = new List<EUser>();
            ousersManger = dUser.getAllCompanyManager(eServices.ServiceId);

            EServiceModel _serviceModel = new EServiceModel();
            ECorrectiveServiceModel _ecorrectiveServiceModel = new ECorrectiveServiceModel();


            string subject = "Service Completed";
            if (eServices.ServiceTypeId == 1)
            {
                _serviceModel = getSingleService(eServices.ServiceId);
                subject = "Preventive Service Completed at "+ eServices.BranchName + " by Albayader Team";
            }
            else
            {
              
                _ecorrectiveServiceModel = getCorrectiveSingleService(eServices.ServiceId);
                subject = "Corrective Service Completed at " + eServices.BranchName + " by Albayader Team";
            }

        
            UtilityHelper utilityHelper = new UtilityHelper();


            string body = buildEmailboday(eServices, _serviceModel, _ecorrectiveServiceModel, "client");
            string adminboday = buildEmailboday(eServices, _serviceModel, _ecorrectiveServiceModel, "admin");

            bool result = false;
            Thread T1 = new Thread(delegate ()
            {
                if (ousersManger != null)
            {
                utilityHelper.SendCompleteEmailAsyncToClient(ousersManger, subject, body.ToString());

            }

            });

            T1.Start();


            Thread T2 = new Thread(delegate ()
            {
                utilityHelper.SendCompleteEmailAsyncToAdmin(ousers, subject, adminboday.ToString());

            });

            T2.Start();



            return eServices;
        }

        private string buildEmailboday(EServices eServices,EServiceModel _serviceModel, ECorrectiveServiceModel _ecorrectiveServiceModel,string to)
        {

            StringBuilder Clientbody = new StringBuilder();

            Clientbody.Append(" <div style=\"background-color:rgb(236, 236, 236); margin: 0px; font-family: 'Courier New', Courier, monospace; \">  ");
            Clientbody.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px; text-align: center; \">");
            Clientbody.Append("<img src=\"http://albayader-me.com/wp-content/uploads/2021/11/logo-albyader.png\" />");
            Clientbody.Append("</div>");
            Clientbody.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: white; padding: 30px;border: 1px solid rgb(217, 217, 217); \">");
            Clientbody.Append("");
            Clientbody.Append("");
            if (to == "admin")
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Al Bayader  Admin </p>");
                Clientbody.AppendFormat("<div>Service for {0} in  branch <span style=\"font - weight: 400;\">{1}</span> has be completed successfully</div> ", eServices.CompanyName, eServices.BranchName);

            }
            else
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear {0}  Manager </p>", eServices.CompanyName);
                Clientbody.AppendFormat("<div>Service in your branch <span style=\"font - weight: 400;\">{0}</span> has be completed successfully</div> ", eServices.BranchName);

            }



            Clientbody.AppendFormat("<p>Service Refrence number :{0}</p>", eServices.ServiceId);
            Clientbody.AppendLine("<p style=\"font-weight:bold; \">Details as below</p>");

            if (eServices.ServiceTypeId == 1)
            {
                foreach (EServiceDetails servicedetails in _serviceModel.ServiceDetails)
                {
                    Clientbody.AppendFormat("<p> Equipment Name:{0}</p>", servicedetails.EquipmentName);
                    Clientbody.AppendFormat("<p> Serial No:{0}</p>", servicedetails.SerialNo);
                    Clientbody.AppendFormat(" <p style=\"font-weight: bold; \">Parts Checked</p>");
                    Clientbody.AppendLine("<ul>");
                    if (servicedetails.Elect)
                        Clientbody.AppendFormat("<li>Elect Parts</li>");
                    if (servicedetails.Moving)
                        Clientbody.AppendFormat(" <li>Moving Parts</li>");
                    if (servicedetails.Bearings)
                        Clientbody.AppendFormat("<li> Bearings</li>");
                    if (servicedetails.Bells)
                        Clientbody.AppendFormat("<li>Belts</li>");
                    if (servicedetails.Motor)
                        Clientbody.AppendFormat(" <li>Motor</li>");
                    if (servicedetails.Heater)
                        Clientbody.AppendFormat("<li> EHeater</li>");
                    if (servicedetails.SafetySwitch)
                        Clientbody.AppendFormat(" <li>Safety Switch</li>");
                    if (servicedetails.ControlBoard)
                        Clientbody.AppendFormat(" <li>Control Board</li>");
                    if (servicedetails.Compressor)
                        Clientbody.AppendFormat(" <li>Compressor</li>");
                    if (servicedetails.TmpControl)
                        Clientbody.AppendFormat("<li> Tmp. Control</li>");
                    Clientbody.AppendFormat(" </ul>");
                    Clientbody.AppendFormat(" <p style=\"font-weight:bold; \">Used Materials</p>");
                    Clientbody.Append("<ul>");
                    foreach (EMaterialsUsed materuse in servicedetails.MaterialsUsed)
                    {

                        Clientbody.AppendFormat("<li> {0}</li>", materuse.MateriaUsedlName);
                    }
                    Clientbody.Append("</ul>");

                    Clientbody.AppendFormat(" <p style=\"font-weight:bold; \">Required Materials</p>");
                    Clientbody.Append("<ul>");
                    foreach (ERequiredMaterials mrateruse in servicedetails.requiredMaterials)
                    {
                        Clientbody.AppendFormat("<li> {0}</li>", mrateruse.RequireMaterialName);
                    }
                    Clientbody.Append("</ul>");
                }


                Clientbody.AppendFormat("<p>Remarks: {0}</p>", _serviceModel.Remark);
                Clientbody.AppendFormat("<p>Status After Service: {0}</p>", _serviceModel.StatusAfterName);
                Clientbody.AppendFormat("<p>Supervisor Name: {0}</p>", _serviceModel.SupervisourName);
                Clientbody.AppendFormat("<p>Supervisor Feedback: {0}</p>", _serviceModel.SupervisourFeedback);


                Clientbody.AppendFormat(" <p>For more details about the service please <a href=\"{0}\"> Login to the service portal</a></p> ", "http://services.albayader-me.com");
               

            }
            else
            {


                Clientbody.AppendFormat("<p> Vist type:{0}</p>", _ecorrectiveServiceModel.VistTypeName);
                foreach (ECorrectiveServiceDetails servicedetails in _ecorrectiveServiceModel.ServiceDetails)
                {
                    Clientbody.AppendFormat(" <p>Equipment Name :{0}</p>", servicedetails.EquipmentName);
                    Clientbody.AppendFormat(" Serial No:", servicedetails.SerialNo);

                    Clientbody.AppendFormat("<p style=\"font-weight:bold; \">Request Details</p>");
                    Clientbody.AppendFormat("<p>Reported by: {0}</p>", servicedetails.ReportedBy);
                    Clientbody.AppendFormat("<p>Problem Reported: {0}</p>", servicedetails.ProblemReported);
                    Clientbody.AppendFormat("<p>Condition: {0}</p>", servicedetails.ConditionName);



                    Clientbody.AppendFormat(" <p style=\"font-weight:bold; \">Used Materials</p>");
                    Clientbody.Append("<ul>");
                    foreach (EMaterialsUsed materuse in servicedetails.MaterialsUsed)
                    {

                        Clientbody.AppendFormat("<li> {0}</li>", materuse.MateriaUsedlName);
                    }
                    Clientbody.Append("</ul>");
                    Clientbody.AppendFormat("<p>Service Render: {0}</p>", servicedetails.ServiceRendered);

                }

                Clientbody.AppendFormat("<p>Remarks: {0}</p>", _ecorrectiveServiceModel.Remark);
                Clientbody.AppendFormat("<p>Status After Service: {0}</p>", _ecorrectiveServiceModel.StatusAfterName);
                Clientbody.AppendFormat("<p>Supervisor Name: {0}</p>", _serviceModel.SupervisourName);
                Clientbody.AppendFormat("<p>Supervisor Feedback: {0}</p>", _serviceModel.SupervisourFeedback);


                Clientbody.AppendFormat("<p> For more details about the service please <a href=\"{0}\" >Login to the service portal</a></p> ", "http://www.albayader-me.com");

            }

            Clientbody.Append("</div>");
            Clientbody.AppendLine("<div style=\"width: 800px;margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px;font-size: 12px \">");
            Clientbody.Append(" <div>");
        
           
            Clientbody.AppendLine(" <div>Regards </div>");
            Clientbody.AppendLine("<div>Al Bayader Team</div>");
            Clientbody.AppendLine("<div>OUR CLIENT IS OUR PARTNER</div>");
            Clientbody.Append("</div>");
            Clientbody.Append("</div>");
            Clientbody.Append("</div>");



            return Clientbody.ToString();

        }
        public List<EServiceDetails> getAllServiceDetails(int serviceId)
        {
            List<EServiceDetails> serviceDetails = new List<EServiceDetails>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("  select EQ.Name EquipmentName,* from ServiceDetails  SD  ");
                    sQuery.AppendFormat(" inner join Equipments EQ on EQ.EquipmentId=SD.EquipmentId ");
                    sQuery.AppendFormat("  where SD.ServiceId={0} and SD.EndDate is null", serviceId);
                   
                    

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceDetails oEServiceDetails = new EServiceDetails();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceDetails.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oEServiceDetails.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                            if (dataReader["EquipmentId"] != DBNull.Value) { oEServiceDetails.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["Elect"] != DBNull.Value) { oEServiceDetails.Elect = (bool)dataReader["Elect"]; }
                            if (dataReader["Moving"] != DBNull.Value) { oEServiceDetails.Moving = (bool)dataReader["Moving"]; }
                            if (dataReader["Bearings"] != DBNull.Value) { oEServiceDetails.Bearings = (bool)dataReader["Bearings"]; }
                            if (dataReader["Bells"] != DBNull.Value) { oEServiceDetails.Bells = (bool)dataReader["Bells"]; }
                            if (dataReader["Motor"] != DBNull.Value) { oEServiceDetails.Motor = (bool)dataReader["Motor"]; }
                            if (dataReader["Heater"] != DBNull.Value) { oEServiceDetails.Heater = (bool)dataReader["Heater"]; }
                            if (dataReader["ControlBoard"] != DBNull.Value) { oEServiceDetails.ControlBoard = (bool)dataReader["ControlBoard"]; }
                            if (dataReader["Compressor"] != DBNull.Value) { oEServiceDetails.Compressor = (bool)dataReader["Compressor"]; }
                            if (dataReader["TmpControl"] != DBNull.Value) { oEServiceDetails.TmpControl = (bool)dataReader["TmpControl"]; }
                            if (dataReader["SafetySwitch"] != DBNull.Value) { oEServiceDetails.SafetySwitch = (bool)dataReader["SafetySwitch"]; }
                            if (dataReader["SerialNo"] != DBNull.Value) { oEServiceDetails.SerialNo = (string)dataReader["SerialNo"]; }
                            if (dataReader["EquipmentName"] != DBNull.Value) { oEServiceDetails.EquipmentName = (string)dataReader["EquipmentName"]; }
                            oEServiceDetails.MaterialsUsed = getMaterialsUsed(oEServiceDetails.ServiceDetailId);
                            oEServiceDetails.requiredMaterials = getRequiredMaterials(oEServiceDetails.ServiceDetailId);
                            oEServiceDetails.servicePictures = getServicePictures(oEServiceDetails.ServiceDetailId);
                            serviceDetails.Add(oEServiceDetails);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return serviceDetails;
        }

        public List<ECorrectiveServiceDetails> getAllCorrectiveServiceDetails(int serviceId)
        {
            List<ECorrectiveServiceDetails> serviceDetails = new List<ECorrectiveServiceDetails>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat(" select *,EQ.Name EquipmentName from CorrectiveServiceDetails CS  ");
                    sQuery.AppendFormat(" inner join Equipments EQ on EQ.EquipmentId=CS.EquipmentId ");
                    sQuery.AppendFormat("  inner join Condition CO on CO.ConditionId=CS.ConditionId");
                    sQuery.AppendFormat("  inner join EquipmentType EQT on EQT.EquipmentTypeId=CS.EquipmentTypeId");
                    sQuery.AppendFormat("  where CS.ServiceId={0} and CS.EndDate is null", serviceId);



                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ECorrectiveServiceDetails oEServiceDetails = new ECorrectiveServiceDetails();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceDetails.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["CorrectiveServiceDetailsId"] != DBNull.Value) { oEServiceDetails.CorrectiveServiceDetailsId = 
                                    (int)dataReader["CorrectiveServiceDetailsId"]; }
                            if (dataReader["EquipmentId"] != DBNull.Value) { oEServiceDetails.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["ConditionId"] != DBNull.Value) { oEServiceDetails.ConditionId = (int)dataReader["ConditionId"]; }
                            if (dataReader["EquipmentTypeId"] != DBNull.Value) { oEServiceDetails.EquipmentTypeId = (int)dataReader["EquipmentTypeId"]; }
                            if (dataReader["ReportedDate"] != DBNull.Value) { oEServiceDetails.ReportedDate = dataReader["ReportedDate"].ToString(); }
                            if (dataReader["ReportedBy"] != DBNull.Value) { oEServiceDetails.ReportedBy = (string)dataReader["ReportedBy"]; }
                            if (dataReader["Model"] != DBNull.Value) { oEServiceDetails.Model = (string)dataReader["Model"]; }
                            if (dataReader["ServiceRendered"] != DBNull.Value) { oEServiceDetails.ServiceRendered = (string)dataReader["ServiceRendered"]; }
                            if (dataReader["SerialNo"] != DBNull.Value) { oEServiceDetails.SerialNo = (string)dataReader["SerialNo"]; }
                            if (dataReader["ProblemReported"] != DBNull.Value) { oEServiceDetails.ProblemReported = (string)dataReader["ProblemReported"]; }
                            if (dataReader["EquipmentName"] != DBNull.Value) { oEServiceDetails.EquipmentName = (string)dataReader["EquipmentName"]; }
                            if (dataReader["ConditionName"] != DBNull.Value) { oEServiceDetails.ConditionName = (string)dataReader["ConditionName"]; }
                            if (dataReader["SerialNo"] != DBNull.Value) { oEServiceDetails.SerialNo = (string)dataReader["SerialNo"]; }
                            if (dataReader["EquipmentTypeName"] != DBNull.Value) { oEServiceDetails.EquipmentTypeName = (string)dataReader["EquipmentTypeName"]; }
                            if (dataReader["AMCTypeId"] != DBNull.Value) { oEServiceDetails.AMCTypeId = (int)dataReader["AMCTypeId"]; }
                            oEServiceDetails.MaterialsUsed = getCorrectMaterialsUsed(oEServiceDetails.CorrectiveServiceDetailsId);
                             oEServiceDetails.servicePictures = getCorrectiveServicePictures(oEServiceDetails.CorrectiveServiceDetailsId);
                            serviceDetails.Add(oEServiceDetails);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return serviceDetails;
        }








        public List<EMaterialsUsed> getMaterialsUsed(int serviceDetailsId)
        {
            List<EMaterialsUsed> materialsUsed = new List<EMaterialsUsed>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select *,M.MaterialName MateriaUsedlName from MaterUsed MTU  ");
                    sQuery.Append(" inner join Materials M on M.MaterialId=MTU.MaterialId  ");
                    sQuery.AppendFormat(" where MTU.ServiceDetailId={0}  ", serviceDetailsId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EMaterialsUsed oEMaterialsUsed = new EMaterialsUsed();
                            if (dataReader["MaterialsUsedId"] != DBNull.Value) { oEMaterialsUsed.MaterialsUsedId = (int)dataReader["MaterialsUsedId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oEMaterialsUsed.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                            if (dataReader["MaterialId"] != DBNull.Value) { oEMaterialsUsed.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MateriaUsedlName"] != DBNull.Value) { oEMaterialsUsed.MateriaUsedlName = (string)dataReader["MateriaUsedlName"]; }


                            materialsUsed.Add(oEMaterialsUsed);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return materialsUsed;
        }
        public List<EMaterialsUsed> getCorrectMaterialsUsed(int CorrectiveServiceDetailsId)
        {
            List<EMaterialsUsed> materialsUsed = new List<EMaterialsUsed>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select *,M.MaterialName MateriaUsedlName from MaterUsed MTU  ");
                    sQuery.Append(" inner join Materials M on M.MaterialId=MTU.MaterialId  ");
                    sQuery.AppendFormat(" where MTU.CorrectiveServiceDetailsId={0}  ", CorrectiveServiceDetailsId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EMaterialsUsed oEMaterialsUsed = new EMaterialsUsed();
                            if (dataReader["MaterialsUsedId"] != DBNull.Value) { oEMaterialsUsed.MaterialsUsedId = (int)dataReader["MaterialsUsedId"]; }
                             if (dataReader["MaterialId"] != DBNull.Value) { oEMaterialsUsed.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MateriaUsedlName"] != DBNull.Value) { oEMaterialsUsed.MateriaUsedlName = (string)dataReader["MateriaUsedlName"]; }


                            materialsUsed.Add(oEMaterialsUsed);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return materialsUsed;
        }


        public List<ERequiredMaterials> getRequiredMaterials(int serviceDetailsId)
        {
            List<ERequiredMaterials> requiredMaterialsd = new List<ERequiredMaterials>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select *,M.MaterialName RequireMaterialName from RequiredMaterials RQM ");
                    sQuery.Append(" inner join Materials M on M.MaterialId=RQM.MaterialId ");
                    sQuery.AppendFormat(" where RQM.ServiceDetailId={0}  ", serviceDetailsId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ERequiredMaterials oERequiredMaterials = new ERequiredMaterials();
                            if (dataReader["RequiredMaterialId"] != DBNull.Value) { oERequiredMaterials.RequiredMaterialId = (int)dataReader["RequiredMaterialId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oERequiredMaterials.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                            if (dataReader["MaterialId"] != DBNull.Value) { oERequiredMaterials.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["RequireMaterialName"] != DBNull.Value) { oERequiredMaterials.RequireMaterialName = (string)dataReader["RequireMaterialName"]; }


                            requiredMaterialsd.Add(oERequiredMaterials);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return requiredMaterialsd;
        }


        public List<EServicePictures> getServicePictures(int serviceDetailsId)
        {
            List<EServicePictures> servicePictures = new List<EServicePictures>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from ServicePictures SP ");
                    sQuery.Append(" inner join PictureTypes PTY on PTY.PictureTypeId=SP.PictureTypeId ");
                    sQuery.AppendFormat(" where SP.ServiceDetailId={0}  ", serviceDetailsId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServicePictures oEServicePictures = new EServicePictures();
                            if (dataReader["PictureId"] != DBNull.Value) { oEServicePictures.PictureId = (int)dataReader["PictureId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oEServicePictures.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                             if (dataReader["FileName"] != DBNull.Value) { oEServicePictures.FileName = (string)dataReader["FileName"]; }
                            if (dataReader["PictureTypeId"] != DBNull.Value) { oEServicePictures.PictureTypeId = (int)dataReader["PictureTypeId"]; }


                            servicePictures.Add(oEServicePictures);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return servicePictures;
        }

        public List<EServicePictures> getCorrectiveServicePictures(int CorrectiveServiceDetailsId)
        {
            List<EServicePictures> servicePictures = new List<EServicePictures>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from ServicePictures SP ");
                    sQuery.Append(" inner join PictureTypes PTY on PTY.PictureTypeId=SP.PictureTypeId ");
                    sQuery.AppendFormat(" where SP.CorrectiveServiceDetailsId={0}  ", CorrectiveServiceDetailsId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServicePictures oEServicePictures = new EServicePictures();
                            if (dataReader["PictureId"] != DBNull.Value) { oEServicePictures.PictureId = (int)dataReader["PictureId"]; }
                            if (dataReader["CorrectiveServiceDetailsId"] != DBNull.Value) { oEServicePictures.CorrectiveServiceDetailsId = (int)dataReader["CorrectiveServiceDetailsId"]; }
                            if (dataReader["FileName"] != DBNull.Value) { oEServicePictures.FileName = (string)dataReader["FileName"]; }
                            if (dataReader["PictureTypeId"] != DBNull.Value) { oEServicePictures.PictureTypeId = (int)dataReader["PictureTypeId"]; }


                            servicePictures.Add(oEServicePictures);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return servicePictures;
        }


        public bool updateServiceDate(int serviceId, string  newDate)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;

            try
            {
            conn.Open();

            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("UPDATE Services SET ");
                sQuery.AppendFormat(" CompletionDate = DATEADD(day, DATEDIFF(day, CompletionDate,'{0}'), CompletionDate), ", newDate);
                sQuery.AppendFormat(" CreatedDate = DATEADD(day, DATEDIFF(day, CreatedDate, '{0}'), CreatedDate) ", newDate);
                sQuery.AppendFormat(" where ServiceId={0}", serviceId);
                command.CommandText = sQuery.ToString();
                // for select only to retrive value ExecuteScalar()
                return (command.ExecuteNonQuery() > 0);

            }
            }
            finally
            {
                conn.Close();
            }

           

            return result;
        }


        public bool updateBranch(int serviceId, int branchId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            bool result = false;

            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("UPDATE Services SET ");
                     sQuery.AppendFormat(" BranchId ={0}", branchId);
                    sQuery.AppendFormat(" where ServiceId={0}", serviceId);
                    command.CommandText = sQuery.ToString();
                    // for select only to retrive value ExecuteScalar()
                    return (command.ExecuteNonQuery() > 0);

                }
            }
            finally
            {
                conn.Close();
            }



            return result;
        }

        // check status data change for auto status update


      
    }
}
