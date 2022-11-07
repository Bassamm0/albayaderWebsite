using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;
using System.Transactions;
using MimeKit;

namespace LOGIC
{
    public class TicketLogLogic
    {
        DticketLog dticketLog = new DticketLog();
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
        public async Task<EticketLog> addticketLog(EticketLog newticketLog,EUser replyUser)
        {

            var resul = await dticketLog.addticketLog(newticketLog);
            EUser ticketOwner = new EUser();

            Dtickets dTicket = new Dtickets()
;
            int CreatorId = dTicket.getTicketCreatorId(newticketLog.ticketId);

            EUser euser = new EUser();
            euser = dUser.getSingleUser(CreatorId);
            if (resul.ticketLogId > 0)
            {

                //send email
                string status = dticketLog.getTicketstatus(resul.ticketId);

                ousers.Add(euser);

                string emailclientBody = buildEmailbody(resul, replyUser, "Client",status);

                string emailadmintBody = buildEmailbody(resul, replyUser, "admin", status);
               

                Thread T1 = new Thread(delegate ()
                {

                    utilityHelper.SendEmail(ousers, "[#" + resul.ticketId + "] Reply" , emailclientBody);

                });
                T1.Start();

                ousers = dUser.getAllAdminAndManagerAndSupport();

                //  send email


                Thread T2 = new Thread(delegate ()
                {
                    utilityHelper.SendEmail(ousers, "New Ticket [#" + resul.ticketId + "] Reply" , emailadmintBody);

                });
                T2.Start();

            }
            return resul;
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
        private string buildEmailbody(EticketLog ticketLog, EUser Creator, string to,string status)
        {

            StringBuilder Clientbody = new StringBuilder();

            Clientbody.Append(Header);
            if (to == "admin")
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Al Bayader  Admin and Support </p>");
                Clientbody.AppendFormat("<div>the ticket has been updated, with below details.</div> ");

            }
            else
            {
                Clientbody.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear {0} {1}   </p>", Creator.FirstName, Creator.Lastname);
                Clientbody.AppendFormat("<div>Your ticket has been updated, with below details.</div> ");

            }

            Clientbody.Append("<ul style=\"list-style:none ;\" >");
            Clientbody.AppendFormat("<li>Ticket ID:{0}</li>", ticketLog.ticketId);
            Clientbody.AppendFormat("<li>Status:{0}</li>", status);
             Clientbody.AppendFormat("<li>{0}</li>", ticketLog.Message);
            Clientbody.Append("</ul>");



            Clientbody.Append(Footer);



            return Clientbody.ToString();

        }

    }
}
