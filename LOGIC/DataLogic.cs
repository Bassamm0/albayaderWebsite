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


       
       
        public async Task<EEquipments> getEquipmentById(int id)
        {

            EEquipments equipment = ddata.getSingleEquipment(id);

            return equipment;
        }
        public async Task<EEquipments> addEquipment(EEquipments newEquipment)
        {

            var resul = await ddata.addEquipment(newEquipment);
            if (resul.EquipmentId > 0)
            {
                return resul;
            }
            else
            {
                return resul;
            }

        }
        public async Task<Boolean> updateEquipment(EEquipments equipment)
        {

            var resul = await ddata.updateEquipment(equipment);
            if (resul != null && resul.EquipmentId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
       
        public async Task<Boolean> removeEquipmentl(int id)
        {

            var resul = await ddata.removeEquipment(id);
            if (resul != null && resul.EquipmentId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}
