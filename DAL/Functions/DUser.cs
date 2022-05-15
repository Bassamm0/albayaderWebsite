﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static DAL.DALException;

namespace DAL.Functions
{
    public class DUser 
    {
        Encrption m_oEncrption = new Encrption();

        public  List<EUser> getAllUsers()
        {
            List<EUser> users = new List<EUser>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
             try
            {
                 conn.Open();
                using (var command = conn.CreateCommand())
                {
                    
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select * from users where isDeleted={0}",0);
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader =  command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EUser oEUsers = new EUser();
                            if (dataReader["UserId"] != DBNull.Value) { oEUsers.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["Nationality"] != DBNull.Value) { oEUsers.Nationality = (int)dataReader["Nationality"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oEUsers.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["PositionId"] != DBNull.Value) { oEUsers.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Title"] != DBNull.Value) { oEUsers.Title = (string)dataReader["Title"]; }
                            if (dataReader["Username"] != DBNull.Value) { oEUsers.Username = (string)dataReader["Username"]; }
                            if (dataReader["Password"] != DBNull.Value) { oEUsers.Password = m_oEncrption.Decrypt((string)dataReader["Password"]); }
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Role"] != DBNull.Value) { oEUsers.Role = (string)dataReader["Role"]; }
                            users.Add(oEUsers);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return users;
        }
        public EUser getSingleUser(int Id)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EUser oEUsers = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select * from users where UserId ={0} ", Id);
                   
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEUsers = new EUser();

                            if (dataReader["UserId"] != DBNull.Value) { oEUsers.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["Nationality"] != DBNull.Value) { oEUsers.Nationality = (int)dataReader["Nationality"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oEUsers.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["PositionId"] != DBNull.Value) { oEUsers.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Title"] != DBNull.Value) { oEUsers.Title = (string)dataReader["Title"]; }
                            if (dataReader["Username"] != DBNull.Value) { oEUsers.Username = (string)dataReader["Username"]; }
                            if (dataReader["Password"] != DBNull.Value) { oEUsers.Password = m_oEncrption.Decrypt((string)dataReader["Password"]); }
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Role"] != DBNull.Value) { oEUsers.Role = (string)dataReader["Role"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEUsers;
        }
        public EUser getLoginUser(string email, string password)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EUser oEUsers = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select * from users where email='{0}' and password ='{1}' and IsDeleted=0 ", email, m_oEncrption.Encrypt(password));

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEUsers = new EUser();

                            if (dataReader["UserId"] != DBNull.Value) { oEUsers.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["Nationality"] != DBNull.Value) { oEUsers.Nationality = (int)dataReader["Nationality"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oEUsers.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["PositionId"] != DBNull.Value) { oEUsers.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Title"] != DBNull.Value) { oEUsers.Title = (string)dataReader["Title"]; }
                            if (dataReader["Username"] != DBNull.Value) { oEUsers.Username = (string)dataReader["Username"]; }
                            if (dataReader["Password"] != DBNull.Value) { oEUsers.Password = m_oEncrption.Decrypt((string)dataReader["Password"]); }
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Role"] != DBNull.Value) { oEUsers.Role = (string)dataReader["Role"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEUsers;
        }
        public EUser getSingleUserByEmail(string email)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EUser oEUsers = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select * from users where email ='{0}'  and IsDeleted=0 ", email);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEUsers = new EUser();

                            if (dataReader["UserId"] != DBNull.Value) { oEUsers.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["Nationality"] != DBNull.Value) { oEUsers.Nationality = (int)dataReader["Nationality"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oEUsers.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["PositionId"] != DBNull.Value) { oEUsers.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Title"] != DBNull.Value) { oEUsers.Title = (string)dataReader["Title"]; }
                            if (dataReader["Username"] != DBNull.Value) { oEUsers.Username = (string)dataReader["Username"]; }
                            if (dataReader["Password"] != DBNull.Value) { oEUsers.Password = m_oEncrption.Decrypt((string)dataReader["Password"]); }
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Role"] != DBNull.Value) { oEUsers.Role = (string)dataReader["Role"]; }

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oEUsers;
        }
        public async Task<EUser> addUser(EUser newUser)
        {
            newUser.Password = m_oEncrption.Encrypt(newUser.Password);

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
            }

            return newUser;
        }

       
        public async Task<EUser> updateUser(EUser user)
        {
            EUser eUser = new EUser();
            eUser = getSingleUser(user.UserId);
            if (eUser == null)
            {
               throw new DomainValidationFundException("Validation : The user is not found, make sure you are updating the correct user");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Users.Attach(user);
                context.Entry(user).Property(x => x.Title).IsModified = true;
                context.Entry(user).Property(x => x.FirstName).IsModified = true;
                context.Entry(user).Property(x => x.Lastname).IsModified = true;
                context.Entry(user).Property(x => x.Mobile).IsModified = true;
                context.Entry(user).Property(x => x.Telephone).IsModified = true;
                context.Entry(user).Property(x => x.Role).IsModified = true;
                context.Entry(user).Property(x => x.AuthLevelRefId).IsModified = true;
                context.Entry(user).Property(x => x.Birthday).IsModified = true;
                context.Entry(user).Property(x => x.PictureFileName).IsModified = true;
                context.Entry(user).Property(x => x.Nationality).IsModified = true;
                context.Entry(user).Property(x => x.CountryId).IsModified = true;
                context.Entry(user).Property(x => x.PositionId).IsModified = true;
                context.Entry(user).Property(x => x.City).IsModified = true;
                await context.SaveChangesAsync();
            }

            return user;
        }
        public async Task<EUser> deleteUser(int Id)
        {
            EUser eUser = new EUser();
            eUser = getSingleUser(Id);
            if (eUser == null)
            {
                throw new DomainValidationFundException("Validation : The user is not found, make sure you are deleting the correct user");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                    context.Users.Remove(eUser);
                    await context.SaveChangesAsync();

            }

            return eUser;
        }

        public async Task<EUser> removeUser(int id)
        {
            EUser eUser = new EUser();
            

            eUser = getSingleUser(id);
            eUser.EndDate = DateTime.Now;

            if (eUser == null)
            {
                throw new DomainValidationFundException("Validation : The user is not found, make sure you are removing the correct user");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Users.Attach(eUser);
                context.Entry(eUser).Property(x => x.EndDate).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eUser;
        }

        public async Task<EUser> changePassword(int id,string password)
        {
            EUser eUser = new EUser();


            eUser = getSingleUser(id);

            eUser.Password = m_oEncrption.Encrypt(password);

            if (eUser == null)
            {
                throw new DomainValidationFundException("Validation : Something went wrong!");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Users.Attach(eUser);
                context.Entry(eUser).Property(x => x.Password).IsModified = true;

                await context.SaveChangesAsync();
            }

            return eUser;
        }
        


        public DateTime getTokenDate(string token)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            DateTime result;
            conn.Open();
            using (var command = conn.CreateCommand())
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.AppendFormat("select generatedDate from recoverpassword where token='{0}' ", token);
                command.CommandText = sQuery.ToString();
                 result = (DateTime)command.ExecuteScalar();
            }
          
            return result;
        }


        // get token details for recover password
        public ERecoverPassword getTokenDetails(string token)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            ERecoverPassword oERecoverPassword = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select * from recoverpassword where token='{0}' ", token);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oERecoverPassword = new ERecoverPassword();

                            if (dataReader["recoverPasswordId"] != DBNull.Value) { oERecoverPassword.recoverPasswordId = (int)dataReader["recoverPasswordId"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oERecoverPassword.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["generatedDate"] != DBNull.Value) { oERecoverPassword.generatedDate = (DateTime)dataReader["generatedDate"]; }
                            if (dataReader["token"] != DBNull.Value) { oERecoverPassword.token = (string)dataReader["token"]; }
                        

                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return oERecoverPassword;
        }
        public async Task<EUser> recoverPassword(int id,string password)
        {

            EUser eUser = new EUser();
            

            eUser = getSingleUser(id);
            eUser.Password = m_oEncrption.Encrypt(password); ;

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Users.Attach(eUser);
                context.Entry(eUser).Property(x => x.Password).IsModified = true;
               
                await context.SaveChangesAsync();
            }

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
            result = await utilityHelper.SendEmailAsync("Bassam@albayader-me.com", eUser.Email, subject, body.ToString());

            if (!result)
            {
                throw new DomainInternalException("Email can't be sent, plesae try again later");
            }

            //*****
            return eUser;
        }
        public async Task<EUser> forgetpassword( string email)
        {
            EUser eUser = new EUser();

            eUser = getSingleUserByEmail(email);
            if (eUser == null)
            {
                throw new DomainValidationFundException("Validation : The email doesn't exist");
            }
            // insert token
            ERecoverPassword eRecoverPassword = new ERecoverPassword();
            eRecoverPassword.generatedDate = DateTime.Now;
            eRecoverPassword.token=Guid.NewGuid().ToString();
            eRecoverPassword.UserId = eUser.UserId;

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.RecoverPassword.AddAsync(eRecoverPassword);
                await context.SaveChangesAsync();
            }
            // send email with token

            StringBuilder body= new StringBuilder();
            body.AppendFormat("Hello {0} you requested to change your password",eUser.FirstName + ' '+ eUser.Lastname);
            body.AppendLine("To change your password please click the link below ");
            body.AppendLine("");
            body.AppendFormat("<a href='http://www.albayader-me.com?token={0}'>Change Password</a>", eRecoverPassword.token);
            body.AppendLine("if you didn't requested to change the password please ignore the email");
            body.AppendLine("Regards ");
            body.AppendLine("Al Bayader Team ");

            string subject = "forget password from AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result =  await utilityHelper.SendEmailAsync("Bassam@albayader-me.com",email,subject,body.ToString());

            if (!result)
            {
                throw new DomainInternalException("Email can't be sent, plesae try again later");
            }
            return eUser;
        }


        // login log
        public async Task<ELoginLog> addLoginLog(ELoginLog loginLog)
        {
            
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.LoginLog.AddAsync(loginLog);
                await context.SaveChangesAsync();
            }

            return loginLog;
        }

        public async Task<EErrorLogin> addErrorLogin(EErrorLogin errorLogin)
        {

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.ErrorLogin.AddAsync(errorLogin);
                await context.SaveChangesAsync();
            }

            return errorLogin;
        }



    }
}