using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class DashboardLogic
    {
        DDashbaord oDDashBoard = new DDashbaord();
        public async Task<EDashboard> getDashboardData(string year,EUser logeduser)
        {
            EDashboard oEDashbaord = new EDashboard();

            if(logeduser.CompanyTypeId==1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager"))
            {
                oEDashbaord = oDDashBoard.getDashboardData(year);
            }
            else if(logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                oEDashbaord = oDDashBoard.getDashboardDataUser(year,logeduser.CompanyId);
            }

            return oEDashbaord;

        }


    }
}
