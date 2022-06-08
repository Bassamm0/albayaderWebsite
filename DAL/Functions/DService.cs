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
                            EServiceModel oEServiceModel = new EServiceModel();
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
                    sQuery.Append("select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
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
                            if (dataReader["CompletionDate"] != DBNull.Value) { oEServiceModel.CompletionDate = (DateTime)dataReader["CompletionDate"]; }
                            if (dataReader["CreaterName"] != DBNull.Value) { oEServiceModel.CreaterName = (string)dataReader["CreaterName"]; }
                            if (dataReader["TechnicianName"] != DBNull.Value) { oEServiceModel.TechnicianName = (string)dataReader["TechnicianName"]; }
                            if (dataReader["ServiceTypeName"] != DBNull.Value) { oEServiceModel.ServiceTypeName = (string)dataReader["ServiceTypeName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEServiceModel.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEServiceModel.CompanyName = (string)dataReader["CompanyName"]; }
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
            
                    sQuery.AppendFormat(" select SR.* from services SR where SR.EndDate is null and SR.ServiceId={0}",ServiceId);
                    
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
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Service.AddAsync(newService);
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
                context.Service.Attach(Service);
                context.Entry(eServices).Property(x => x.ServiceId).IsModified = true;
                context.Entry(eServices).Property(x => x.StatusId).IsModified = true;
                context.Entry(eServices).Property(x => x.TechnicianId).IsModified = true;
                context.Entry(eServices).Property(x => x.ServiceTypeId).IsModified = true;
               

                await context.SaveChangesAsync();
            }

            return Service;
        }

        public async Task<EServices> removeService(int id)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(id);
            eServices.EndDate = DateTime.Now;

            if (eServices == null)
            {
                throw new DomainValidationFundException("Validation : The Branch is not found, make sure you are removing the correct Branch");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Service.Attach(eServices);
                context.Entry(eServices).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eServices;
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
                    sQuery.AppendFormat(" select * from ServiceDetails SD where SD.ServiceId={0} ", serviceId);
                   
                    

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
                    sQuery.Append(" select * from MaterUsed MTU  ");
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
                            if (dataReader["MaterialName"] != DBNull.Value) { oEMaterialsUsed.MaterialName = (string)dataReader["MaterialName"]; }


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
                    sQuery.Append(" select * from RequiredMaterials RQM ");
                    sQuery.Append(" inner join Materials MT on MT.MaterialId=RQM.MaterialId ");
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
                            if (dataReader["MaterialName"] != DBNull.Value) { oERequiredMaterials.MaterialName = (string)dataReader["MaterialName"]; }


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




    }
}
