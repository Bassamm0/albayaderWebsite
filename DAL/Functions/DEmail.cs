using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Functions
{
    public class DEmail
    {
        public async Task<Boolean> sendEmail()
        { 
            EUser eUser = new EUser();
            eUser.FirstName = "Bassam";
            eUser.Lastname = "mhisen";
           eUser.Email = "mohaisen_bassam@hotmail.com";

            // ***** send email your password been changed.
            StringBuilder body = new StringBuilder();
            body.AppendFormat("Hello {0}", eUser.FirstName + ' ' + eUser.Lastname);
            body.AppendLine("Your password changed successfuly  ");


            body.AppendLine("");
            body.AppendLine("Regards ");
            body.AppendLine("Al Bayader Team ");

            string subject = "Password Recovered AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result = await utilityHelper.SendEmailAsync(eUser.Email, subject, body.ToString());

            return result;
        }
        public async Task<Boolean> sendEmailOld()
        {
            EUser eUser = new EUser();
            eUser.FirstName = "Bassam";
            eUser.Lastname = "mhisen";
            eUser.Email = "bassam_m_99@yahoo.com";

            // ***** send email your password been changed.
            StringBuilder body = new StringBuilder();
            body.AppendFormat("Hello {0}", eUser.FirstName + ' ' + eUser.Lastname);
            body.AppendLine("Your password changed successfuly  ");


            body.AppendLine("");
            body.AppendLine("Regards ");
            body.AppendLine("Al Bayader Team ");

            string subject = "Password Recovered AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result = await utilityHelper.SendEmailAsync(eUser.Email, subject, body.ToString());

            return result;
        }

    }
}
