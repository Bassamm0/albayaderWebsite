using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class CalenderEventsLogic
    {
        DCalenderEvents dCalenderEvents = new DCalenderEvents();
        public async Task<List<ECalenderEvents>> getAllCalenderEvents(EUser logeduser)
        {

            List<ECalenderEvents> CalenderEvents = new List<ECalenderEvents>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager"))
            {
                CalenderEvents = dCalenderEvents.getAllCalenderEvents();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                CalenderEvents = dCalenderEvents.getAllBranchCalenderEvents(logeduser.BranchId);
            }


            return CalenderEvents;
        }


        public async Task<List<ECalenderEvents>> getAllCalenderEventsForDisplay(EUser logeduser)
        {

            List<ECalenderEvents> CalenderEvents = new List<ECalenderEvents>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                CalenderEvents = dCalenderEvents.getAllCalenderEvents();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                CalenderEvents = dCalenderEvents.getAllBranchCalenderEvents(logeduser.CompanyId);
            }
            else 
            {
                //CalenderEvents = dCalenderEvents.getUserCalenderEvents(logeduser.UserId);
            }

            return CalenderEvents;
        }
        public async Task<List<ECalenderEvents>> getAllCompanyCalenderEvents(int CalenderEventId)
        {

            List<ECalenderEvents> CalenderEvents = dCalenderEvents.getAllBranchCalenderEvents(CalenderEventId);

            return CalenderEvents;
        }
        public async Task<ECalenderEvents> getCalenderEventById(int id)
        {

            ECalenderEvents CalenderEvent = dCalenderEvents.getSingleCalenderEvent(id);

            return CalenderEvent;
        }
        public async Task<Boolean> addCalenderEvent(ECalenderEvents newCalenderEvent)
        {

            var resul = await dCalenderEvents.addCalenderEvent(newCalenderEvent);
            if (resul.EventId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateCalenderEvent(ECalenderEvents CalenderEvent)
        {

            var resul = await dCalenderEvents.updateCalenderEvent(CalenderEvent);
            if (resul != null && resul.EventId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> deleteCalenderEvent(int Id)
        {

            var resul = await dCalenderEvents.deleteCalenderEvent(Id);
            if (resul != null && resul.EventId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> removeCalenderEvent(int id)
        {

            var resul = await dCalenderEvents.removeCalenderEvent(id);
            if (resul != null && resul.EventId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> completeCalenderEvent(int id)
        {

            var resul = await dCalenderEvents.completeCalenderEvent(id);
            if (resul != null && resul.EventId > 0)
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
