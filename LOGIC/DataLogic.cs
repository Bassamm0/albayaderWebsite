using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Functions;
using Entity;

namespace LOGIC
{
    public class DataLogic
    {

        DData ddata = new DData();
        public async Task<List<EEquipments>> getEquipments()
        {

            List<EEquipments> equipments = ddata.getEquipments();

            return equipments;
        }

        public async Task<List<EMaterials>> getmaterials()
        {

            List<EMaterials> materials = ddata.getmaterials();

            return materials;
        }

    }

}
