using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using Microsoft.EntityFrameworkCore;
using static DAL.DALException;

namespace DAL.Functions
{
    public class DData
    {
        public List<EEquipments> getEquipments()
        {
            List<EEquipments> Equipments = new List<EEquipments>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from Equipments where EndDate is null ");
               

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EEquipments oEEquipments = new EEquipments();
                            if (dataReader["EquipmentId"] != DBNull.Value) { oEEquipments.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["Name"] != DBNull.Value) { oEEquipments.Name = (string)dataReader["Name"]; }
                           

                            Equipments.Add(oEEquipments);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return Equipments;
        }



        public EEquipments getSingleEquipment(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EEquipments oEEquipments = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select * from Equipments ");
                    sQuery.AppendFormat(" where EquipmentId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEEquipments = new EEquipments();

                            if (dataReader["EquipmentId"] != DBNull.Value) { oEEquipments.EquipmentId = (int)dataReader["EquipmentId"]; }
                            if (dataReader["Name"] != DBNull.Value) { oEEquipments.Name = (string)dataReader["Name"]; }


                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEEquipments;
        }

        public async Task<EEquipments> updateEquipment(EEquipments _Equipment)
        {
            if (_Equipment == null)
            {
                throw new DomainValidationFundException("Validation : The Material is not found, make sure you are updating the correct Material");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Equipments.Attach(_Equipment);
                context.Entry(_Equipment).Property(x => x.Name).IsModified = true;

                await context.SaveChangesAsync();
            }

            return _Equipment;
        }

        public async Task<EEquipments> addEquipment(EEquipments newEquipment)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Equipments.AddAsync(newEquipment);
                await context.SaveChangesAsync();
            }

            return newEquipment;
        }

        public async Task<EEquipments> removeEquipment(int id)
        {
            EEquipments equipments = new EEquipments();


            equipments = getSingleEquipment(id);
            equipments.EndDate = DateTime.UtcNow;

            if (equipments == null)
            {
                throw new DomainValidationFundException("Validation : The Material is not found, make sure you are removing the correct Material");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Equipments.Attach(equipments);
                context.Entry(equipments).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return equipments;
        }
        public List<EMaterials> getmaterials()
        {
            List<EMaterials> materials = new List<EMaterials>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from materials ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EMaterials oEMaterials = new EMaterials();
                            if (dataReader["MaterialId"] != DBNull.Value) { oEMaterials.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MaterialName"] != DBNull.Value) { oEMaterials.MaterialName = (string)dataReader["MaterialName"]; }


                            materials.Add(oEMaterials);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return materials;
        }






    }
}
