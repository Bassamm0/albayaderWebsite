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
                    sQuery.Append(" SELECT COUNT(serviceId) ");
                    sQuery.Append(" from services where ServiceTypeId =1 and EndDate is null and StatusId=5 ");
                    sQuery.Append(" ) AS Preventive, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(serviceId) ");
                    sQuery.Append(" from services where ServiceTypeId =2 and EndDate is null and StatusId=5 ");
                    sQuery.Append(" ) AS Corrective, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(serviceId) ");
                    sQuery.Append(" from services where ServiceTypeId =3 and EndDate is null and StatusId=5 ");
                    sQuery.Append(" ) AS Other ");


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
                            OEDashboard.preventMonth = getMonthServiceBytypeAndYear(1, year);
                            OEDashboard.correctiveMonth = getMonthServiceBytypeAndYear(2, year);
                            OEDashboard.allServiceMonth = getMonthServiceBytypeAndYear(0, year);

                            OEDashboard.preventiveBranch = getServiceBranch(1);
                            OEDashboard.correctiveBranch = getServiceBranch(2);

                            OEDashboard.allServiceBranch = getServiceBranch(0);

                            OEDashboard.branchCount = odbranch.gettotalBranchCount();




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
                    sQuery.Append(" SELECT    COUNT(*) as ServiceCount,( ");
                    sQuery.Append(" Select Convert(char(3),DateName( month , DateAdd( month , MONTH(CompletionDate) , 0 ) - 1 ), 0) )as Mon");
                    sQuery.Append(" FROM      services  ");
                    if(type != 0)
                    {
                        sQuery.AppendFormat(" WHERE     YEAR(CompletionDate) = '{0}' and StatusId=5 and ServiceTypeId={1} and EndDate is null ", year, type);
                    }
                    else
                    {
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
                    sQuery.Append(" SELECT COUNT(SR.serviceId)");
                    sQuery.Append(" FROM services SR ");
                    sQuery.Append(" inner join Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.AppendFormat(" where SR.StatusId=5 and SR.ServiceTypeId=1 and SR.EndDate is null and CO.CompanyID={0} ",companyId);
                    sQuery.Append(" ) AS Preventive, ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" SELECT COUNT(serviceId) ");
                    sQuery.Append(" from services where ServiceTypeId =2 and EndDate is null and StatusId=5 ");
                    sQuery.Append(" ) AS Corrective ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            OEDashboard = new EDashboard();

                            if (dataReader["Preventive"] != DBNull.Value) { OEDashboard.preventiveCount = (int)dataReader["Preventive"]; }
                            if (dataReader["Corrective"] != DBNull.Value) { OEDashboard.correctiveCount = (int)dataReader["Corrective"]; }
                            OEDashboard.preventMonth = getMonthServiceBytypeAndYearCompany(1, year, companyId);
                            OEDashboard.correctiveMonth = getMonthServiceBytypeAndYearCompany(2, year, companyId);
                            OEDashboard.allServiceMonth = getMonthServiceBytypeAndYearCompany(0, year, companyId);


                            OEDashboard.preventiveBranch = getServiceBranchCompany(1, companyId);
                            OEDashboard.correctiveBranch = getServiceBranchCompany(2, companyId);
                            OEDashboard.allServiceBranch = getServiceBranchCompany(0, companyId);

                            OEDashboard.branchCount = odbranch.getCompanytotalBranchCount(companyId);





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


        public List<ServicePerBranch> getServiceBranch(int type)
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
                        sQuery.AppendFormat(" where SR.EndDate is null and SR.StatusId=5 ");
                    }
                    else
                    {
                        sQuery.AppendFormat(" where SR.ServiceTypeId ={0} and SR.EndDate is null and SR.StatusId=5 ", type);
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
        public List<ServicePerBranch> getServiceBranchCompany(int type,int companyId)
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
                        sQuery.AppendFormat(" where SR.EndDate is null and SR.StatusId=5 and BR.compnayId={0}",companyId);
                    }
                    else
                    {
                        sQuery.AppendFormat(" where SR.ServiceTypeId ={0} and SR.EndDate is null and SR.StatusId=5  and BR.compnayId={1} ", type,companyId);
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

    }
}
