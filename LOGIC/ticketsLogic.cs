﻿using System;
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
    public class ticketLogic
    {
        Dtickets dtickets = new Dtickets();
        public async Task<List<EticketViews>> getAlltickets(EUser logeduser)
        {

            List<EticketViews> tickets = new List<EticketViews>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                tickets = dtickets.getAlltickets();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                tickets = dtickets.getAllCompanytickets(logeduser.CompanyId);
            }
            
            return tickets;
        }
        public async Task<List<EticketViews>> getAllOpentickets(EUser logeduser)
        {

            List<EticketViews> tickets = new List<EticketViews>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                tickets = dtickets.getAllOpentickets();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                tickets = dtickets.getAllCompanyOpentickets(logeduser.CompanyId);
            }

            return tickets;
        }
        public async Task<List<EticketViews>> getAllClosedtickets(EUser logeduser)
        {

            List<EticketViews> tickets = new List<EticketViews>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                tickets = dtickets.getAllClosedtickets();
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                tickets = dtickets.getAllCompanyClosedtickets(logeduser.CompanyId);
            }

            return tickets;
        }
        public async Task<List<EticketViews>> getAllCompanytickets(int companyid)
        {

            List<EticketViews> tickets = dtickets.getAllCompanytickets(companyid);

            return tickets;
        }
        public async Task<EticketViews> getticketById(int id)
        {

            EticketViews ticket = dtickets.getSingleticket(id);

            return ticket;
        }
        public async Task<Boolean> addticket(Etickets newticket)
        {

            var resul = await dtickets.addticket(newticket);
            if (resul.ticketId > 0)
            {
                // create status as new 
                EticketAndStatus ticketAndStatus = new EticketAndStatus();
                ticketAndStatus.StatusDate = DateTime.UtcNow;
                ticketAndStatus.ticketId=resul.ticketId;
                ticketAndStatus.UserId = newticket.createdBy;
                ticketAndStatus.ticketStatusId = 1;
                dtickets.insertNewStatus(ticketAndStatus);

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateticket(Etickets ticket)
        {

            var resul = await dtickets.updateticket(ticket);
            if (resul != null && resul.ticketId > 0)
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
