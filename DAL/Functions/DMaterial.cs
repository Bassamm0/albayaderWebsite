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
    public class DMaterial
    {
        public List<EMaterials> getMaterials()
        {
            List<EMaterials> lMaterial = new List<EMaterials>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Materials C ");
                    sQuery.Append(" where C.EndDate is null order by C.MaterialName");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EMaterials oEMaterials = new EMaterials();
                            if (dataReader["MaterialId"] != DBNull.Value) { oEMaterials.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MaterialName"] != DBNull.Value) { oEMaterials.MaterialName = (string)dataReader["MaterialName"]; }
                            if (dataReader["Price"] != DBNull.Value) { oEMaterials.Price = (decimal)dataReader["Price"]; }
                            if (dataReader["Description"] != DBNull.Value) { oEMaterials.Description = (string)dataReader["Description"]; }

                            lMaterial.Add(oEMaterials);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lMaterial;
        }


        public List<EMaterials> getAllCompanyMaterials(int companyid)
        {
            List<EMaterials> lMaterial = new List<EMaterials>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Materials C ");
                    sQuery.AppendFormat(" where C.MaterialId={0} and C.EndDate is null order by C.MaterialName", companyid);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EMaterials oEMaterials = new EMaterials();
                            if (dataReader["MaterialId"] != DBNull.Value) { oEMaterials.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MaterialName"] != DBNull.Value) { oEMaterials.MaterialName = (string)dataReader["MaterialName"]; }
                            if (dataReader["Price"] != DBNull.Value) { oEMaterials.Price = (decimal)dataReader["Price"]; }
                            if (dataReader["Description"] != DBNull.Value) { oEMaterials.Description = (string)dataReader["Description"]; }


                            lMaterial.Add(oEMaterials);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lMaterial;
        }

        public EMaterials getSingleMaterial(int Id)
        {


            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EMaterials oEMaterials = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from Materials C ");
                    sQuery.AppendFormat(" where C.MaterialId ={0} ", Id);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEMaterials = new EMaterials();

                            if (dataReader["MaterialId"] != DBNull.Value) { oEMaterials.MaterialId = (int)dataReader["MaterialId"]; }
                            if (dataReader["MaterialName"] != DBNull.Value) { oEMaterials.MaterialName = (string)dataReader["MaterialName"]; }
                            if (dataReader["Price"] != DBNull.Value) { oEMaterials.Price = (decimal)dataReader["Price"]; }
                            if (dataReader["Description"] != DBNull.Value) { oEMaterials.Description = (string)dataReader["Description"]; }


                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEMaterials;
        }

        public async Task<EMaterials> addMaterial(EMaterials newMaterial)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.materials.AddAsync(newMaterial);
                await context.SaveChangesAsync();
            }

            return newMaterial;
        }
        public async Task<EMaterials> updateMaterial(EMaterials Material)
        {
            EMaterials eMaterial = new EMaterials();
            eMaterial = getSingleMaterial(Material.MaterialId);
            if (eMaterial == null)
            {
                throw new DomainValidationFundException("Validation : The Material is not found, make sure you are updating the correct Material");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.materials.Attach(Material);
                context.Entry(Material).Property(x => x.MaterialId).IsModified = true;
                context.Entry(Material).Property(x => x.Price).IsModified = true;
                 context.Entry(Material).Property(x => x.MaterialName).IsModified = true;
                 context.Entry(Material).Property(x => x.Description).IsModified = true;

                await context.SaveChangesAsync();
            }

            return Material;
        }
        public async Task<EMaterials> deleteMaterial(int Id)
        {
            EMaterials eMaterial = new EMaterials();
            eMaterial = getSingleMaterial(Id);
            if (eMaterial == null)
            {
                throw new DomainValidationFundException("Validation : The Material is not found, make sure you are deleting the correct Material");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.materials.Remove(eMaterial);
                await context.SaveChangesAsync();

            }

            return eMaterial;
        }


        public async Task<EMaterials> removeMaterial(int id)
        {
            EMaterials eMaterial = new EMaterials();


            eMaterial = getSingleMaterial(id);
            eMaterial.EndDate = DateTime.Now;

            if (eMaterial == null)
            {
                throw new DomainValidationFundException("Validation : The Material is not found, make sure you are removing the correct Material");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.materials.Attach(eMaterial);
                context.Entry(eMaterial).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eMaterial;
        }

    }
}
