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
                    sQuery.Append(" inner join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
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
                    sQuery.Append("select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.* from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
                    sQuery.AppendFormat(" where SR.StatusId={0} ",StatusId);
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
                    sQuery.Append("select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName,UT.PictureFileName, ");
                    sQuery.Append(" BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.*,STF.StatusAfterName  from services SR ");
                    sQuery.Append(" inner join  Branchs BR on BR.branchId=SR.BranchId ");
                    sQuery.Append(" inner join Companies CO on CO.CompanyID=BR.compnayId ");
                    sQuery.Append(" inner join Users U on U.UserId=SR.CreatedBy ");
                    sQuery.Append(" inner join Users UT on UT.UserId=SR.TechnicianId ");
                    sQuery.Append(" inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId ");
                    sQuery.Append(" inner join StatusAfter STF on STF.StatusAfterId=SR.StatusAfterId ");
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
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEServiceModel.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Remark"] != DBNull.Value) { oEServiceModel.Remark = (string)dataReader["Remark"]; }
                            if (dataReader["StatusAfterId"] != DBNull.Value) { oEServiceModel.StatusAfterId = (int)dataReader["StatusAfterId"]; }
                            if (dataReader["StatusAfterName"] != DBNull.Value) { oEServiceModel.StatusAfterName = (string)dataReader["StatusAfterName"]; }
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

                    sQuery.Append("select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName, ");
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
            newService.StatusAfterId = null;

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
                context.Entry(eServices).Property(x => x.StatusAfterId).IsModified = true;

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
                context.Services.Attach(eServices);
                context.Entry(eServices).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eServices;
        }

        public async Task<EServices> updateStatus(int serviceId, int statusId,string remark,int statusAfterId)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(serviceId);
            eServices.StatusId = statusId;
            eServices.Remark=remark;
            eServices.StatusAfterId=statusAfterId;

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

                await context.SaveChangesAsync();
            }

            return eServices;
        }


        public async Task<EServices> clientSignature(int serviceId, string SupervisourSignature, string SupervisourName, string SupervisourFeedback)
        {
            EServices eServices = new EServices();


            eServices = getSingleServiceOnly(serviceId);
            eServices.StatusId = 5;
            eServices.SupervisourSignature = SupervisourSignature;
            eServices.SupervisourName = SupervisourName;
            eServices.SupervisourFeedback = SupervisourFeedback;
            eServices.CompletionDate = DateTime.Now;
       

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
                context.Entry(eServices).Property(x => x.CompletionDate).IsModified = true;

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




    }
}
