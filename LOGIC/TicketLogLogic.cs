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
    public class TicketLogLogic
    {
        DticketLog dticketLog = new DticketLog();
        public async Task<List<EticketLog>> getAllticketLog(EUser logeduser)
        {

            List<EticketLog> ticketLog = new List<EticketLog>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                ticketLog = dticketLog.getAllticketLog();
            }
            
            return ticketLog;
        }
        public async Task<List<EticketLog>> getticketLog(int ticketId)
        {

            List<EticketLog> ticketLog = dticketLog.getticketLog(ticketId);

            return ticketLog;
        }


        public async Task<EticketLog> getticketLogById(int id)
        {

            EticketLog ticketLog = dticketLog.getSingleticketLog(id);

            return ticketLog;
        }
        public async Task<Boolean> addticketLog(EticketLog newticketLog)
        {

            var resul = await dticketLog.addticketLog(newticketLog);
            if (resul.ticketLogId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateticketLog(EticketLog ticketLog)
        {

            var resul = await dticketLog.updateticketLog(ticketLog);
            if (resul != null && resul.ticketLogId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteticketLog(int Id)
        {

            var resul = await dticketLog.deleteticketLog(Id);
            if (resul != null && resul.ticketLogId > 0)
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
