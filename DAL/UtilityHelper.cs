using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Entity;
//using System.Net;
//using System.Net.Mail;

namespace DAL
{
    public class UtilityHelper
    {


        AppConfiguration AppConfig=new AppConfiguration();
        public async Task<bool>  SendEmailAsync( string to, string subject, string body)
        {
            string smtpClient = AppConfig.smtpClient;
            string emailFrom = AppConfig.emailFrom;
            string pwd = AppConfig.ePassword;

            //string smtpClient = "smtp.gmail.com";
            //string emailFrom = "bassam.mhisen@gmail.com";
            //string pwd = "Bassam@123";


            InternetAddressList list = new InternetAddressList();
            list.Add(new MailboxAddress("",to));
          

            bool isSent = false;
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Albayader Service Support", emailFrom));
                email.Sender = MailboxAddress.Parse(emailFrom);
                email.To.AddRange(list);
                email.Subject = subject;
                var builder = new BodyBuilder();

                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
            
                smtp.Connect(smtpClient, 587, SecureSocketOptions.None);
                smtp.Authenticate(emailFrom, pwd);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                isSent = true;
            }
            catch (Exception ex)
            {
                isSent = false;

            }
            return isSent;
        }

        public async void SendCompleteEmailAsyncToClient(List<EUser> toList, string subject, string body)
        {
            string smtpClient = AppConfig.smtpClient;
            string emailFrom = AppConfig.emailFrom;
            string pwd = AppConfig.ePassword;

            InternetAddressList list = new InternetAddressList();
           foreach(EUser user in toList)
            {
                list.Add(new MailboxAddress(user.FirstName + ' ' +user.Lastname, user.Email));

            }
           
            bool isSent = false;
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Albayader Service Support", emailFrom));
                email.Sender = MailboxAddress.Parse(emailFrom);
                email.To.AddRange(list);
                email.Subject = subject;
                var builder = new BodyBuilder();

                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();

                smtp.Connect(smtpClient, 587, SecureSocketOptions.None);
                smtp.Authenticate(emailFrom, pwd);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                isSent = true;
            }
            catch (Exception ex)
            {
                isSent = false;

            }
           
        }

        public async void SendCompleteEmailAsyncToAdmin(List<EUser> toList, string subject, string body)
        {
            string smtpClient = AppConfig.smtpClient;
            string emailFrom = AppConfig.emailFrom;
            string pwd = AppConfig.ePassword;

            InternetAddressList list = new InternetAddressList();
            foreach (EUser user in toList)
            {
                list.Add(new MailboxAddress(user.FirstName + ' ' + user.Lastname, user.Email));

            }

            bool isSent = false;
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Albayader Service Support", emailFrom));
                email.Sender = MailboxAddress.Parse(emailFrom);
                email.To.AddRange(list);
                email.Subject = subject;
                var builder = new BodyBuilder();

                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();

                smtp.Connect(smtpClient, 587, SecureSocketOptions.None);
                smtp.Authenticate(emailFrom, pwd);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                isSent = true;
            }
            catch (Exception ex)
            {
                isSent = false;

            }
           
        }

        public async void SendEmail(List<EUser> toList, string subject, string body)
        {
            string smtpClient = AppConfig.smtpClient;
            string emailFrom = AppConfig.emailFrom;
            string pwd = AppConfig.ePassword;

            InternetAddressList list = new InternetAddressList();
            foreach (EUser user in toList)
            {
                list.Add(new MailboxAddress(user.FirstName + " " + user.Lastname, user.Email));

            }

            bool isSent = false;
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailFrom);
                email.To.AddRange(list);
                email.Subject = subject;
                var builder = new BodyBuilder();
                email.From.Add(new MailboxAddress("Albayader Service Support", emailFrom));
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                smtp.Connect(smtpClient, 587, SecureSocketOptions.None);
                smtp.Authenticate(emailFrom, pwd);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                isSent = true;
            }
            catch (Exception ex)
            {
                isSent = false;

            }

        }
        public async Task SendEmailAs(string from, string to, string subject, string body)
        {


            //SmtpClient client = new SmtpClient("mail.albayader-me.com");
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("bassam@albayader-me.com", "e02e6cTl8");
            //MailMessage message = new MailMessage(from, to, subject, body);

            //message.IsBodyHtml = true;

            //try
            //{
            //    await client.SendMailAsync(message);
            //}
            //catch (Exception e)
            //{

            //}


        }


    }
}
