using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using Microsoft.EntityFrameworkCore;

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
                    sQuery.Append(" Select * from Equipments ");
               

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
