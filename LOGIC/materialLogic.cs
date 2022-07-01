using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;



namespace LOGIC
{
    public class materialLogic
    {
        DMaterial dMaterials = new DMaterial();
        public async Task<List<EMaterials>> getMaterials()
        {

            List<EMaterials> Materials = dMaterials.getMaterials();

            return Materials;
        }
        public async Task<List<EMaterials>> getAllCompanyMaterials(int companyid)
        {

            List<EMaterials> Materials = dMaterials.getAllCompanyMaterials(companyid);

            return Materials;
        }
        public async Task<EMaterials> getMaterialById(int id)
        {

            EMaterials Material = dMaterials.getSingleMaterial(id);

            return Material;
        }
        public async Task<EMaterials> addMaterial(EMaterials newMaterial)
        {

            var resul = await dMaterials.addMaterial(newMaterial);
            if (resul.MaterialId > 0)
            {
                return resul;
            }
            else
            {
                return resul;
            }

        }
        public async Task<Boolean> updateMaterial(EMaterials Material)
        {

            var resul = await dMaterials.updateMaterial(Material);
            if (resul != null && resul.MaterialId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteMaterial(int Id)
        {

            var resul = await dMaterials.deleteMaterial(Id);
            if (resul != null && resul.MaterialId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> removeMaterial(int id)
        {

            var resul = await dMaterials.removeMaterial(id);
            if (resul != null && resul.MaterialId > 0)
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
