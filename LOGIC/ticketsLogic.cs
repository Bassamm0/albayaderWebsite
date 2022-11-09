using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;
using System.Net.Http;
using Azure.Core;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LOGIC
{
    public class ticketLogic
    {
        Dtickets dtickets = new Dtickets();
        DUser dUser = new DUser();
        List<EUser> ousers = new List<EUser>();
        UtilityHelper utilityHelper = new UtilityHelper();


        private static string Header = @"<div style=""background-color:rgb(236, 236, 236); margin: 0px; font-family: 'Courier New', Courier, monospace; "">  
<div style=""width: 800px; margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px; text-align: center; "">
<img src=""http://albayader-me.com/wp-content/uploads/2021/11/logo-albyader.png"" />
</div>
<div style=""width: 800px; margin-left: auto; margin-right: auto; background-color: white; padding: 30px;border: 1px solid rgb(217, 217, 217); "">";

        private static string Footer = @"</div>
<div style=""width: 800px;margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px;font-size: 12px "">
<div>
<div>Regards </div>
<div>Al Bayader Team</div>
<div>OUR CLIENT IS OUR PARTNER</div>
</div>
</div>
</div>";
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
        public async Task<List<EticketViews>> getAllOpenticketsview()
        {

            List<EticketViews> tickets = new List<EticketViews>();
            
             tickets = dtickets.getAllOpenticketsview();
            

            return tickets;
        }

        public async Task<List<EticketViews>> getAllOpenticketsDate(EUser logeduser,string startDate,string endDate)
        {

            List<EticketViews> tickets = new List<EticketViews>();
            if (logeduser.CompanyTypeId == 1 && (logeduser.UserRole.ToLower() == "administrator" || logeduser.UserRole.ToLower() == "manager" || logeduser.UserRole.ToLower() == "technicion"))
            {
                tickets = dtickets.getAllOpenticketsDate(startDate,  endDate);
            }
            else if (logeduser.CompanyTypeId != 1 && logeduser.UserRole.ToLower() == "client manager")
            {
                tickets = dtickets.getAllCompanyOpenticketsDate(logeduser.CompanyId, startDate, endDate);
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
        public async Task<EticketViewsDetails> getticketById(int ticketId)
        {

            EticketViewsDetails ticket = dtickets.getSingleticket(ticketId);

            return ticket;
        }
        public async Task<Etickets> addticket(Etickets newticket,EUser Creator)
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

               
               
                ousers.Add(Creator);

                string emailclientBody=buildEmailbody(resul, Creator, "Client");

                string emailadmintBody = buildEmailbody(resul, Creator, "admin");

                Thread T1 = new Thread(delegate ()
                {
                   
                 utilityHelper.SendEmail(ousers, "[#" + resul.ticketId + "] " + resul.subject, emailclientBody);

                });
                T1.Start();
               
                ousers = dUser.getAllAdminAndManagerAndSupport();

                //  send email


                Thread T2 = new Thread(delegate ()
                {
                    utilityHelper.SendEmail(ousers, "New Ticket [#" + resul.ticketId + "] " + resul.subject, emailadmintBody);

                });
                T2.Start();


                // notfication

                return resul;
            }
            else
            {
                return resul;
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
        public async Task<Boolean> insertNewStatus(EticketAndStatus ticketAndStatus)
        {

            var resul = await dtickets.insertNewStatus(ticketAndStatus);
            if (resul.ticketId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> assginTicketuser(EticketAndUser ticketAndUser, EUser logeduser)
        {

            var resul = await dtickets.assginTicketuser(ticketAndUser);
            if (resul.ticketId > 0)
            {
                return true;

                //send email
                //DUser dUser = new DUser();
                //List<EUser> ousers = new List<EUser>();
                 
                EUser oAssigneduser = new EUser();
                oAssigneduser = dUser.getSingleUser(ticketAndUser.AssginUserId);
                ousers.Add(oAssigneduser);

                string createAssignBoday;
                string emailclientBody = buildAssignedEmailbody(resul.ticketId, oAssigneduser, "Client");

                string emailadmintBody = buildAssignedEmailbody(resul.ticketId, oAssigneduser, "admin");

                Thread T1 = new Thread(delegate ()
                {

                    utilityHelper.SendEmail(ousers, "[#" + resul.ticketId + "] " + "has been assigned to you", emailclientBody);

                });
                T1.Start();

                ousers = dUser.getAllAdminAndManagerAndSupport();

                //  send email


                Thread T2 = new Thread(delegate ()
                {
                    utilityHelper.SendEmail(ousers, "New Ticket [#" + resul.ticketId + "] " + "has been assigned to support", emailadmintBody);

                });
                T2.Start();
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> createTicketService(EticketAndService ticketAndService)
        {

          
            DService dService = new DService();
           EServices eService=new EServices();
           eService.BranchId = ticketAndService.branchId;
            eService.TechnicianId = ticketAndService.TechnicianId;
            eService.CreatedBy = ticketAndService.userId;
            eService.StatusId = 1;

            eService.CreatedDate = DateTime.Now;
            eService.ServiceTypeId = 2;

            var resul = await dService.addService(eService);
            if (resul.ServiceId > 0)
            {
                ticketAndService.ServiceId = resul.ServiceId;
                var resul1 = await dtickets.createTicketService(ticketAndService);


                // send email


                EUser oAssigneduser = new EUser();
                oAssigneduser = dUser.getSingleUser(ticketAndService.TechnicianId);
                ousers.Add(oAssigneduser);

                string createAssignBoday;
                string emailclientBody = buildCreatServiceEmailbody(resul.ServiceId, ticketAndService.ticketId, oAssigneduser, "Client");

                string emailadmintBody = buildCreatServiceEmailbody(resul.ServiceId,ticketAndService.ticketId, oAssigneduser, "admin");

                Thread T1 = new Thread(delegate ()
                {

                    utilityHelper.SendEmail(ousers, "A new service [#" + resul.ServiceId + "] for the ticket " + "[#" + ticketAndService.ticketId + "] has been created under you", emailclientBody);

                });
                T1.Start();

                ousers = dUser.getAllAdminAndManagerAndSupport();

                //  send email


                Thread T2 = new Thread(delegate ()
                {
                    utilityHelper.SendEmail(ousers, "A new service [#" + resul.ServiceId + "] for the ticket " + "[#" + ticketAndService.ticketId + "] has been created under " + oAssigneduser.FirstName + ' ' +oAssigneduser.Lastname, emailadmintBody);


                });
                T2.Start();

                if (resul1.ticketAndServiceId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
           
        }

        private string buildEmailbody(Etickets ticket, EUser Creator, string to)
        {

            StringBuilder Clientbody = new StringBuilder();

            Clientbody.Append(Header);

            if (to == "admin")
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Al Bayader  Admin and Support </p>");
                Clientbody.AppendFormat("<div>New ticket has been received, one of the staff members need to review it and assign it to the right support.<p> Listed below are details of this ticket</p>.</div> ");

            }
            else
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear {0} {1}   </p>", Creator.FirstName,Creator.Lastname);
                Clientbody.AppendFormat("<div>Your ticket has been received, one of the staff members will review it and reply accordingly.<p> Listed below are details of this ticket</p>.</div> ");

            }

            Clientbody.Append("<ul style=\"list-style:none ;\" >");
            Clientbody.AppendFormat("<li>Ticket ID:{0}</li>",ticket.ticketId);
            Clientbody.AppendFormat("<li>Subject:{0}</li>",ticket.subject);
            Clientbody.AppendFormat("<li>Category:{0}</li>", getCategoryName(ticket.ticketCategoryId));
            Clientbody.AppendFormat("<li>Severity:{0}</li>", getServirityName(ticket.severityId));
            Clientbody.AppendFormat("<li>{0}</li>",ticket.ticketDetails);
             Clientbody.Append("</ul>");

            Clientbody.Append(Footer);




            return Clientbody.ToString();

        }


        private string buildAssignedEmailbody(int ticketid, EUser Assigned, string to)
        {

            StringBuilder Clientbody = new StringBuilder();

            Clientbody.Append(Header);
            if (to == "admin")
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Al Bayader  Admin and Support </p>");
                Clientbody.AppendFormat("<div>The ticket with Ref:[#{0}] has been assigned to {1} {2}.</div> ",ticketid, Assigned.FirstName, Assigned.Lastname);

            }
            else
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear {0} {1}   </p>", Assigned.FirstName, Assigned.Lastname);
                Clientbody.AppendFormat("<div>The ticket with Ref:[#{0}] has been assigned to you, please review it and take the necessary action.</div> ", ticketid);

            }




            Clientbody.Append(Footer);



            return Clientbody.ToString();

        }

        private string buildCreatServiceEmailbody(int serviceId,int ticketid, EUser Assigned, string to)
        {

            StringBuilder Clientbody = new StringBuilder();

            Clientbody.Append(Header);
            if (to == "admin")
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Al Bayader  Admin and Support </p>");
                Clientbody.AppendFormat("<div>A new Service [#{0}] for the ticket with Ref:[#{1}] has been created and assigned to {2} {3}.</div> ", serviceId, ticketid, Assigned.FirstName, Assigned.Lastname);

            }
            else
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear {0} {1}   </p>", Assigned.FirstName, Assigned.Lastname);
                Clientbody.AppendFormat("<div>>A new Service [#{0}] for the ticket with Ref:[#{1}] has been assigned to you, please review it and take the necessary action.</div> ", serviceId, ticketid);

            }



            Clientbody.Append(Footer);


            return Clientbody.ToString();

        }

        private string getServirityName(int id)
        {
            string name = "Low";
            switch (id)
            {
                case 1:
                    name = "Low";
                    break;
                    case 2:
                    break;
                    name = "Medium";
                    case 3:
                    name = "High";
                    break;
                    case 4:
                    name= "Critical";
                    break;
                    default: name = "Low";
                    break;



            }




            return name;
        }
        private string getCategoryName(int id)
        {
            string name = "Kitchen and Bakery Section";
            switch (id)
            {
                case 1:
                    name = "Kitchen and Bakery Section";
                    break;
                case 2:
                    break;
                    name = "Boiler";
                case 3:
                    name = "Other";
                    break;
               
                default:
                    name = "Other";
                    break;



            }




            return name;
        }

   

     


    }
}
