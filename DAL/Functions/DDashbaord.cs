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
    public class DDashbaord
    {
        DBranchs odbranch = new DBranchs();
        // admin
        public  EDashboard  getDashboardData(string year)
        {

            
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EDashboard OEDashboard = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT  ( ");
                    sQuery.Append(" SELECT COUNT(distinct SR.serviceId) from services SR ");
                    sQuery.Append("  inner join ServiceDetails SD on SD.ServiceId=SR.ServiceId ");
                    sQuery.AppendFormat("   where ServiceTypeId =1 and SR.EndDate is null and StatusId=5 and  YEAR(CompletionDate) ='{0}' ",year);
                    sQuery.Append(" ) AS Preventive, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(SR.serviceId) from services SR ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and SR.EndDate is null and StatusId=5 and  YEAR(CompletionDate) ='{0}' ", year);
                  
                    sQuery.Append(" ) AS Corrective, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(serviceId) ");
                    sQuery.AppendFormat(" from services where ServiceTypeId =3 and EndDate is null and StatusId=5 and  YEAR(CompletionDate) ='{0}' ", year);
                    sQuery.Append(" ) AS Other, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and  YEAR(CompletionDate) ='{0}' and CSD.AMCTypeId=1 ", year);
                    sQuery.Append(" ) AS correctiveAMCCount, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and  YEAR(CompletionDate) ='{0}' and CSD.AMCTypeId=2 ", year);
                    sQuery.Append(" ) AS correctiveNoneAMCCount ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            OEDashboard = new EDashboard();

                            if (dataReader["Preventive"] != DBNull.Value) { OEDashboard.preventiveCount = (int)dataReader["Preventive"]; }
                            if (dataReader["Corrective"] != DBNull.Value) { OEDashboard.correctiveCount = (int)dataReader["Corrective"]; }
                            if (dataReader["Other"] != DBNull.Value) { OEDashboard.otherCount = (int)dataReader["Other"]; }
                            if (dataReader["correctiveAMCCount"] != DBNull.Value) { OEDashboard.correctiveAMCCount = (int)dataReader["correctiveAMCCount"]; }
                            if (dataReader["correctiveNoneAMCCount"] != DBNull.Value) { OEDashboard.correctiveNoneAMCCount = (int)dataReader["correctiveNoneAMCCount"]; }

                            OEDashboard.preventMonth = getMonthServiceBytypeAndYear(1, year);
                            OEDashboard.correctiveMonth = getMonthServiceBytypeAndYear(2, year);
                            OEDashboard.correctiveMonthAMC = getMonthCorrectiveServiceBytypeAndYearAMCType(2, year,1);
                            OEDashboard.correctiveMonthNoneAMC = getMonthCorrectiveServiceBytypeAndYearAMCType(2, year,2);


                           
                           

                            
                            OEDashboard.allServiceMonth = getMonthServiceBytypeAndYear(0, year);

                            OEDashboard.preventiveBranch = getServiceBranch(1, year);
                            OEDashboard.correctiveBranch = getServiceBranch(2, year);

                            OEDashboard.allServiceBranch = getServiceBranch(0, year);

                            OEDashboard.branchCount = odbranch.gettotalBranchCount();
                            OEDashboard.lsservicePerMonthVist = getMonthServiceByVisitTypeAndYear(year);
                            OEDashboard.lsservicePerBranchVisit = getServiceByVisitTypeAndBranch(year);



                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return OEDashboard;
        }


        // user
        public List<ServicePerMonth> getMonthServiceBytypeAndYear(int type,string year)
        {
            List<ServicePerMonth> lServicePerMonth = new List<ServicePerMonth>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                
                    if (type == 1)
                    {
                        

                        sQuery.Append(" SELECT    COUNT(distinct SR.ServiceId) as ServiceCount,( ");
                        sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                        sQuery.Append(" FROM services SR ");
                        sQuery.Append(" inner join ServiceDetails SD on SD.ServiceId=SR.ServiceId ");
                        sQuery.AppendFormat(" WHERE     YEAR(CompletionDate) = '{0}' and StatusId=5 and ServiceTypeId={1} and SR.EndDate is null ", year, type);

                    }
                    else if (type == 2)
                    {
                        sQuery.Append(" SELECT    COUNT(distinct SR.ServiceId) as ServiceCount,( ");
                        sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                        sQuery.Append(" FROM services SR ");
                        sQuery.Append(" inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId  ");

                        sQuery.AppendFormat(" WHERE     YEAR(CompletionDate) = '{0}' and StatusId=5 and ServiceTypeId={1} and SR.EndDate is null ", year, type);

                    }
                    else if (type == 0)
                    {
                        sQuery.Append(" SELECT    COUNT(*) as ServiceCount,( ");
                        sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                        sQuery.Append(" FROM      services  ");
                        sQuery.AppendFormat(" WHERE     YEAR(CompletionDate) = '{0}' and StatusId=5  and EndDate is null ", year);
                    }
                    sQuery.Append(" GROUP BY  MONTH(CompletionDate) ");
                    sQuery.Append(" Order by MONTH(CompletionDate) ");
               

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonth oServicePerMonth = new ServicePerMonth();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }



        public List<ServicePerMonth> getMonthCorrectiveServiceBytypeAndYearAMCType(int type, string year,int amcType)
        {
            List<ServicePerMonth> lServicePerMonth = new List<ServicePerMonth>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT    COUNT(*) as ServiceCount,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                    sQuery.Append(" FROM      services  s");
                    sQuery.Append(" inner join CorrectiveServiceDetails CSD on CSD.ServiceId=s.ServiceId ");

                    sQuery.AppendFormat(" WHERE YEAR(CompletionDate) = '{0}' and StatusId=5 and s.ServiceTypeId={1} and s.EndDate is null and CSD.AMCTypeId={2} ", year, type,amcType);
                   
                    sQuery.Append(" GROUP BY  MONTH(CompletionDate) ");
                    sQuery.Append(" Order by MONTH(CompletionDate) ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonth oServicePerMonth = new ServicePerMonth();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }


        public EDashboard getDashboardDataUser(string year,int companyId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EDashboard OEDashboard = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT  ( ");

                    sQuery.Append(" SELECT COUNT(distinct SR.serviceId)");
                    sQuery.Append(" FROM services SR ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join ServiceDetails SD on SD.ServiceId=SR.ServiceId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.ServiceTypeId=1 and SR.EndDate is null and CO.CompanyID={0} and  YEAR(CompletionDate) ='{1}' ", companyId, year);

                    sQuery.Append(" ) AS Preventive, ");

                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(SR.serviceId)");
                    sQuery.Append(" FROM services SR ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.ServiceTypeId=2 and SR.EndDate is null and CO.CompanyID={0} and  YEAR(CompletionDate) ='{1}' ", companyId, year);
                    sQuery.Append(" ) AS Corrective, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and CO.CompanyID={0} and  YEAR(CompletionDate) ='{1}' and CSD.AMCTypeId=1 ", companyId, year);
                    sQuery.Append(" ) AS correctiveAMCCount, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and CO.CompanyID={0} and  YEAR(CompletionDate) ='{1}' and CSD.AMCTypeId=2 ", companyId, year);
                    sQuery.Append(" ) AS correctiveNoneAMCCount ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            OEDashboard = new EDashboard();

                            if (dataReader["Preventive"] != DBNull.Value) { OEDashboard.preventiveCount = (int)dataReader["Preventive"]; }
                            if (dataReader["Corrective"] != DBNull.Value) { OEDashboard.correctiveCount = (int)dataReader["Corrective"]; }
                            if (dataReader["correctiveAMCCount"] != DBNull.Value) { OEDashboard.correctiveAMCCount = (int)dataReader["correctiveAMCCount"]; }
                            if (dataReader["correctiveNoneAMCCount"] != DBNull.Value) { OEDashboard.correctiveNoneAMCCount = (int)dataReader["correctiveNoneAMCCount"]; }

                            OEDashboard.preventMonth = getMonthServiceBytypeAndYearCompany(1, year, companyId);
                            OEDashboard.correctiveMonth = getMonthServiceBytypeAndYearCompany(2, year, companyId);
                            OEDashboard.allServiceMonth = getMonthServiceBytypeAndYearCompany(0, year, companyId);

                            OEDashboard.correctiveMonthAMC = getMonthServiceBytypeAndYearCompanyAMC(2, year, companyId, 1);
                            OEDashboard.correctiveMonthNoneAMC = getMonthServiceBytypeAndYearCompanyAMC(2, year, companyId, 2);

                            OEDashboard.preventiveBranch = getServiceBranchCompany(1, companyId,year);
                            OEDashboard.correctiveBranch = getServiceBranchCompany(2, companyId, year);
                            OEDashboard.allServiceBranch = getServiceBranchCompany(0, companyId, year);

                            OEDashboard.branchCount = odbranch.getCompanytotalBranchCount(companyId);

                            OEDashboard.lsservicePerMonthVist = getMonthServiceByVisitTypeAndYearCompany(year,companyId);
                            OEDashboard.lsservicePerBranchVisit = getServiceByVisitTypeAndBranchCompany(companyId, year);




                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return OEDashboard;
        }


        public EDashboard getDashboardDataUserBranch(string year, int companyId,int BranchId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EDashboard OEDashboard = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT  ( ");

                    sQuery.Append(" SELECT COUNT(distinct SR.serviceId)");
                    sQuery.Append(" FROM services SR ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join ServiceDetails SD on SD.ServiceId=SR.ServiceId ");

                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.ServiceTypeId=1 and SR.EndDate is null and BR.branchId={0} and  YEAR(CompletionDate) ='{1}' ", BranchId, year);
                    sQuery.Append(" ) AS Preventive, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(SR.serviceId)");
                    sQuery.Append(" FROM services SR ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.ServiceTypeId=2 and SR.EndDate is null and BR.branchId={0} and  YEAR(CompletionDate) ='{1}' ", BranchId, year);
                    sQuery.Append(" ) AS Corrective, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and BR.branchId={0}  and  YEAR(CompletionDate) ='{1}' and CSD.AMCTypeId=1 ", BranchId, year);
                    sQuery.Append(" ) AS correctiveAMCCount, ");

                    sQuery.Append(" ( ");
                    sQuery.Append("  SELECT COUNT(sr.serviceId)  from services sr ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append("  inner join CorrectiveServiceDetails CSD on CSD.ServiceId=sr.ServiceId ");
                    sQuery.AppendFormat(" where ServiceTypeId =2 and sr.EndDate is null and StatusId=5 and BR.branchId={0}  and  YEAR(CompletionDate) ='{1}' and CSD.AMCTypeId=2 ",BranchId, year);
                    sQuery.Append(" ) AS correctiveNoneAMCCount ");
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            OEDashboard = new EDashboard();

                            if (dataReader["Preventive"] != DBNull.Value) { OEDashboard.preventiveCount = (int)dataReader["Preventive"]; }
                            if (dataReader["Corrective"] != DBNull.Value) { OEDashboard.correctiveCount = (int)dataReader["Corrective"]; }
                            if (dataReader["correctiveAMCCount"] != DBNull.Value) { OEDashboard.correctiveAMCCount = (int)dataReader["correctiveAMCCount"]; }
                            if (dataReader["correctiveNoneAMCCount"] != DBNull.Value) { OEDashboard.correctiveNoneAMCCount = (int)dataReader["correctiveNoneAMCCount"]; }

                            OEDashboard.preventMonth = getMonthServiceBytypeAndYearCompany(1, year, companyId);
                            OEDashboard.correctiveMonth = getMonthServiceBytypeAndYearCompany(2, year, companyId);
                            OEDashboard.allServiceMonth = getMonthServiceBytypeAndYearCompany(0, year, companyId);

                            OEDashboard.correctiveMonthAMC = getMonthServiceBytypeAndYearCompanyAMC(2, year, companyId, 1);
                            OEDashboard.correctiveMonthNoneAMC = getMonthServiceBytypeAndYearCompanyAMC(2, year, companyId, 2);

                            OEDashboard.preventiveBranch = getServiceBranchCompany(1, companyId, year);
                            OEDashboard.correctiveBranch = getServiceBranchCompany(2, companyId, year);
                            OEDashboard.allServiceBranch = getServiceBranchCompany(0, companyId, year);

                            //OEDashboard.branchCount = odbranch.getCompanytotalBranchCount(companyId);

                            //OEDashboard.lsservicePerMonthVist = getMonthServiceByVisitTypeAndYearCompany(year, companyId);
                            //OEDashboard.lsservicePerBranchVisit = getServiceByVisitTypeAndBranchCompany(companyId, year);




                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return OEDashboard;
        }
        public List<ServicePerMonth> getMonthServiceBytypeAndYearCompany(int type, string year,int companyid)
        {
            List<ServicePerMonth> lServicePerMonth = new List<ServicePerMonth>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT    COUNT(*) as ServiceCount,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    if (type != 0)
                    {
                        sQuery.AppendFormat(" WHERE YEAR(SR.CompletionDate) = '{0}' and SR.StatusId=5 and SR.ServiceTypeId={1} and SR.EndDate is null and CO.CompanyID={2} ", year, type,companyid);
                    }
                    else
                    {
                        sQuery.AppendFormat(" WHERE YEAR(SR.CompletionDate) = '{0}' and SR.StatusId=5  and SR.EndDate is null and CO.CompanyID={1} ", year, companyid);

                    }
                    sQuery.Append(" GROUP BY  MONTH(CompletionDate) ");
                    sQuery.Append(" Order by MONTH(CompletionDate) ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonth oServicePerMonth = new ServicePerMonth();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }


        public List<ServicePerMonth> getMonthServiceBytypeAndYearCompanyAMC(int type, string year, int companyid,int AMCType)
        {
            List<ServicePerMonth> lServicePerMonth = new List<ServicePerMonth>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT    COUNT(*) as ServiceCount,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join CorrectiveServiceDetails CSD on CSD.ServiceId=SR.ServiceId ");

            
                    sQuery.AppendFormat(" WHERE YEAR(SR.CompletionDate) = '{0}' and SR.StatusId=5 and SR.ServiceTypeId={1} and SR.EndDate is null and CO.CompanyID={2} and CSD.AMCTypeId={3} ", year, type, companyid,AMCType);
                   
                    sQuery.Append(" GROUP BY  MONTH(CompletionDate) ");
                    sQuery.Append(" Order by MONTH(CompletionDate) ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonth oServicePerMonth = new ServicePerMonth();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }


        public List<ServicePerBranch> getServiceBranch(int type,string year)
        {
            List<ServicePerBranch> lServicePerMonth = new List<ServicePerBranch>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT COUNT(SR.serviceId)AS ServiceCount,BR.BranchName ");
                    sQuery.Append(" from services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                     if (type == 0)
                    {
                        sQuery.AppendFormat(" where SR.EndDate is null and SR.StatusId=5 and YEAR(SR.CompletionDate) ='{0}' ", year);
                    }
                    else
                    {
                        sQuery.AppendFormat(" where SR.ServiceTypeId ={0} and SR.EndDate is null and SR.StatusId=5 and YEAR(SR.CompletionDate) ='{1}' ", type, year);
                    }
                    sQuery.Append("  Group by BR.BranchName ");
                  


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerBranch oServicePerMonth = new ServicePerBranch();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["branchName"] != DBNull.Value) { oServicePerMonth.branchName = (string)dataReader["branchName"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }
        public List<ServicePerBranch> getServiceBranchCompany(int type,int companyId,string year)
        {
            List<ServicePerBranch> lServicePerMonth = new List<ServicePerBranch>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT COUNT(SR.serviceId)AS ServiceCount,BR.BranchName ");
                    sQuery.Append(" from services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                    if (type == 0)
                    {
                        sQuery.AppendFormat(" where SR.EndDate is null and SR.StatusId=5 and BR.compnayId={0}  and YEAR(SR.CompletionDate) = '{1}'", companyId,year);
                    }
                    else
                    {
                        sQuery.AppendFormat(" where SR.ServiceTypeId ={0} and SR.EndDate is null and SR.StatusId=5  and BR.compnayId={1} and YEAR(SR.CompletionDate) = '{2}' ", type,companyId,year);
                    }
                    sQuery.Append("  Group by BR.BranchName ");



                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerBranch oServicePerMonth = new ServicePerBranch();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["branchName"] != DBNull.Value) { oServicePerMonth.branchName = (string)dataReader["branchName"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }



        public List<ServicePerMonthVist> getMonthServiceByVisitTypeAndYear(string year)
        {
            List<ServicePerMonthVist> lServicePerMonth = new List<ServicePerMonthVist>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT COUNT(*) as ServiceCount,SV.VistTypeName,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(SR.CompletionDate) , 0 ) - 1 ), 0) )as Mon ");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                    sQuery.Append(" inner join SiteVistType SV on SV.SiteVistTypeId=SR.SiteVistTypeId  ");
                    sQuery.AppendFormat(" WHERE YEAR(SR.CompletionDate) = '{0}' and SR.StatusId=5 and SR.ServiceTypeId =2 and SR.EndDate is null ", year);               
                    sQuery.Append(" GROUP BY  MONTH(SR.CompletionDate), SV.VistTypeName ");
                    sQuery.Append(" Order by MONTH(SR.CompletionDate) ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonthVist oServicePerMonth = new ServicePerMonthVist();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }
                             if (dataReader["VistTypeName"] != DBNull.Value) { oServicePerMonth.VistTypeName = (string)dataReader["VistTypeName"]; }
 
                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }

        public List<ServicePerMonthVist> getMonthServiceByVisitTypeAndYearCompany(string year,int companyId)
        {
            List<ServicePerMonthVist> lServicePerMonth = new List<ServicePerMonthVist>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT COUNT(*) as ServiceCount,SV.VistTypeName,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(SR.CompletionDate) , 0 ) - 1 ), 0) )as Mon ");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                    sQuery.Append(" inner join SiteVistType SV on SV.SiteVistTypeId=SR.SiteVistTypeId  ");
                    sQuery.AppendFormat(" WHERE YEAR(SR.CompletionDate) = '{0}' and SR.StatusId=5 and SR.ServiceTypeId =2 and SR.EndDate is null and  BR.compnayId={1} ", year,companyId);
                    sQuery.Append(" GROUP BY  MONTH(SR.CompletionDate), SV.VistTypeName ");
                    sQuery.Append(" Order by MONTH(SR.CompletionDate) ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerMonthVist oServicePerMonth = new ServicePerMonthVist();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["Mon"] != DBNull.Value) { oServicePerMonth.monthName = (string)dataReader["Mon"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oServicePerMonth.VistTypeName = (string)dataReader["VistTypeName"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }
        public List<ServicePerBranchVisit> getServiceByVisitTypeAndBranch(string year)
        {
            List<ServicePerBranchVisit> lServicePerMonth = new List<ServicePerBranchVisit>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT COUNT(serviceId)AS ServiceCount,BR.BranchName,BR.branchId,SV.VistTypeName ");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                    sQuery.Append(" inner join SiteVistType SV on SV.SiteVistTypeId=SR.SiteVistTypeId  ");
                    sQuery.AppendFormat("  where SR.ServiceTypeId =2 and SR.EndDate is null and SR.StatusId=5 and YEAR(SR.CompletionDate) = '{0}' ",year); 
                    sQuery.Append(" Group by SV.VistTypeName,BR.BranchName,BR.branchId ");
 
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerBranchVisit oServicePerMonth = new ServicePerBranchVisit();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                             if (dataReader["BranchName"] != DBNull.Value) { oServicePerMonth.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oServicePerMonth.VistTypeName = (string)dataReader["VistTypeName"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oServicePerMonth.branchId = (int)dataReader["branchId"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }

        public List<ServicePerBranchVisit> getServiceByVisitTypeAndBranchCompany(int companyId,string year)
        {
            List<ServicePerBranchVisit> lServicePerMonth = new List<ServicePerBranchVisit>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" SELECT COUNT(serviceId)AS ServiceCount,BR.BranchName,BR.branchId,SV.VistTypeName ");
                    sQuery.Append(" FROM services SR  ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId  ");
                    sQuery.Append(" inner join SiteVistType SV on SV.SiteVistTypeId=SR.SiteVistTypeId  ");
                    sQuery.AppendFormat("  where SR.ServiceTypeId =2 and SR.EndDate is null and SR.StatusId=5 and BR.compnayId={0}  and YEAR(SR.CompletionDate) = '{1}' ", companyId,year);
                    sQuery.Append(" Group by SV.VistTypeName,BR.BranchName,BR.branchId ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ServicePerBranchVisit oServicePerMonth = new ServicePerBranchVisit();
                            if (dataReader["ServiceCount"] != DBNull.Value) { oServicePerMonth.value = (int)dataReader["ServiceCount"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oServicePerMonth.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["VistTypeName"] != DBNull.Value) { oServicePerMonth.VistTypeName = (string)dataReader["VistTypeName"]; }
                            if (dataReader["branchId"] != DBNull.Value) { oServicePerMonth.branchId = (int)dataReader["branchId"]; }

                            lServicePerMonth.Add(oServicePerMonth);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lServicePerMonth;
        }
    }
}
