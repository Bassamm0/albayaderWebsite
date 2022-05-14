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
    public class UserAndBranchLogic
    {
        DUserAndBranch dUserAndBranch = new DUserAndBranch();
        public async Task<List<EUserAndBranch>> getAllUserAndBranch()
        {

            List<EUserAndBranch> UserAndBranch = dUserAndBranch.getAllUserAndBranch();

            return UserAndBranch;
        }
        public async Task<EUserAndBranch> getUserAndBranchById(int id)
        {

            EUserAndBranch Branch = dUserAndBranch.getSingleUserAndBranch(id);

            return Branch;
        }
        public async Task<Boolean> addUserAndBranch(EUserAndBranch newBranch)
        {


            var resul = await dUserAndBranch.addUserAndBranch(newBranch);
            if (resul.UserAndBranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateUserAndBranch(EUserAndBranch Branch)
        {

            var resul = await dUserAndBranch.updateUserAndBranch(Branch);
            if (resul != null && resul.UserAndBranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteUserAndBranchh(int Id)
        {

            var resul = await dUserAndBranch.deleteUserAndBranch(Id);
            if (resul != null && resul.UserAndBranchId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> removeUserAndBranch(int id)
        {

            var resul = await dUserAndBranch.removeUserAndBranch(id);
            if (resul != null && resul.UserAndBranchId > 0)
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
