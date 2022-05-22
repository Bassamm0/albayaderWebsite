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
    public class BranchLogic
    {
        DBranchs dBranchs = new DBranchs();
        public async Task<List<EBranchs>> getAllBranchs()
        {

            List<EBranchs> Branchs = dBranchs.getAllBranchs();

            return Branchs;
        }
        public async Task<List<EBranchs>> getAllCompanyBranchs(int companyid)
        {

            List<EBranchs> Branchs = dBranchs.getAllCompanyBranchs(companyid);

            return Branchs;
        }
        public async Task<EBranchs> getBranchById(int id)
        {

            EBranchs Branch = dBranchs.getSingleBranch(id);

            return Branch;
        }
        public async Task<Boolean> addBranch(EBranchs newBranch)
        {

            var resul = await dBranchs.addBranch(newBranch);
            if (resul.BranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateBranch(EBranchs Branch)
        {

            var resul = await dBranchs.updateBranch(Branch);
            if (resul != null && resul.BranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteBranch(int Id)
        {

            var resul = await dBranchs.deleteBranch(Id);
            if (resul != null && resul.BranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> removeBranch(int id)
        {

            var resul = await dBranchs.removeBranch(id);
            if (resul != null && resul.BranchId > 0)
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
