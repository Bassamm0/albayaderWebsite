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
    public class DServiceDetials
    {

        public List<EServiceDetails> getAllServiceDetails(int ServiceId)
        {
            List<EServiceDetails> services = new List<EServiceDetails>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" order by SR.CreatedDate desc ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EServiceDetails oEServiceModel = new EServiceDetails();
                           
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oEServiceModel.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                            if (dataReader["EquipmentId"] != DBNull.Value) { oEServiceModel.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["SerialNo"] != DBNull.Value) { oEServiceModel.SerialNo = (string)dataReader["Elect"]; }
                            if (dataReader["Elect"] != DBNull.Value) { oEServiceModel.Elect = (bool)dataReader["Elect"]; }
                            if (dataReader["Moving"] != DBNull.Value) { oEServiceModel.Moving = (bool)dataReader["Moving"]; }
                            if (dataReader["Bearings"] != DBNull.Value) { oEServiceModel.Bearings = (bool)dataReader["Bearings"]; }
                            if (dataReader["Bells"] != DBNull.Value) { oEServiceModel.Bells = (bool)dataReader["Bells"]; }
                            if (dataReader["Motor"] != DBNull.Value) { oEServiceModel.Motor = (bool)dataReader["Motor"]; }
                            if (dataReader["Heater"] != DBNull.Value) { oEServiceModel.Heater = (bool)dataReader["Heater"]; }
                            if (dataReader["ControlBoard"] != DBNull.Value) { oEServiceModel.ControlBoard = (bool)dataReader["ControlBoard"]; }
                            if (dataReader["Compressor"] != DBNull.Value) { oEServiceModel.Compressor = (bool)dataReader["Compressor"]; }
                            if (dataReader["TmpControl"] != DBNull.Value) { oEServiceModel.TmpControl = (bool)dataReader["TmpControl"]; }


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

        public EServiceDetails getSingleServiceDetails(int serviceDetailsId)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();

            EServiceDetails oEServiceModel = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();

                    sQuery.Append(" select * from servicedetails");

                    sQuery.AppendFormat(" where serviceDetailsId={0} ", serviceDetailsId);


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            oEServiceModel = new EServiceDetails();
                            if (dataReader["ServiceId"] != DBNull.Value) { oEServiceModel.ServiceId = (int)dataReader["ServiceId"]; }
                            if (dataReader["ServiceDetailId"] != DBNull.Value) { oEServiceModel.ServiceDetailId = (int)dataReader["ServiceDetailId"]; }
                            if (dataReader["EquipmentId"] != DBNull.Value) { oEServiceModel.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["SerialNo"] != DBNull.Value) { oEServiceModel.SerialNo = (string)dataReader["Elect"]; }
                            if (dataReader["Elect"] != DBNull.Value) { oEServiceModel.Elect = (bool)dataReader["Elect"]; }
                            if (dataReader["Moving"] != DBNull.Value) { oEServiceModel.Moving = (bool)dataReader["Moving"]; }
                            if (dataReader["Bearings"] != DBNull.Value) { oEServiceModel.Bearings = (bool)dataReader["Bearings"]; }
                            if (dataReader["Bells"] != DBNull.Value) { oEServiceModel.Bells = (bool)dataReader["Bells"]; }
                            if (dataReader["Motor"] != DBNull.Value) { oEServiceModel.Motor = (bool)dataReader["Motor"]; }
                            if (dataReader["Heater"] != DBNull.Value) { oEServiceModel.Heater = (bool)dataReader["Heater"]; }
                            if (dataReader["ControlBoard"] != DBNull.Value) { oEServiceModel.ControlBoard = (bool)dataReader["ControlBoard"]; }
                            if (dataReader["Compressor"] != DBNull.Value) { oEServiceModel.Compressor = (bool)dataReader["Compressor"]; }
                            if (dataReader["TmpControl"] != DBNull.Value) { oEServiceModel.TmpControl = (bool)dataReader["TmpControl"]; }
                           


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

        public async Task<EServiceDetails> addServiceDetails(EServiceDetails newServiceDetails)
        {

       
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ServiceDetails.AddAsync(newServiceDetails);
                await context.SaveChangesAsync();
            }

            return newServiceDetails;
        }

        public async Task<EServiceDetails> updateServiceDetails(EServiceDetails ServiceDetails)
        {
           
   
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                
                context.ServiceDetails.Attach(ServiceDetails);
                context.Entry(ServiceDetails).Property(x => x.ServiceId).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.EquipmentId).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.SerialNo).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Elect).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Moving).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Bearings).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Bells).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Motor).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Heater).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.Compressor).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.ControlBoard).IsModified = true;
                context.Entry(ServiceDetails).Property(x => x.TmpControl).IsModified = true;
            
                await context.SaveChangesAsync();
            }

            return ServiceDetails;
        }
    }
}
