using System;
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
                    sQuery.Append(" select AU.Authname UserRole, AU.Authname UserRole,* from users U  ");
                    sQuery.Append(" inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId  ");
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
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }
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

        public List<EUser> getCompanyUsers(int companyId)
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
                    sQuery.Append("select AU.Authname UserRole,CON.Name ResidentCountry,CONN.Name NationalityName, CO.Name CompanyName  ,* from users U ");
                    sQuery.Append("inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append("inner join Branchs BR on BR.branchId=UAB.BranchId ");
                    sQuery.Append("inner join Companies CO on CO.CompanyID = BR.compnayId ");
                    sQuery.Append("inner join Countries CON on CON.CountryId=U.CountryId ");
                    sQuery.Append("inner join Countries CONN on CONN.CountryId=U.Nationality ");
                    sQuery.Append("inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId ");

                    sQuery.AppendFormat("where U.IsDeleted = 0 and UAB.EndDate is null and CO.EndDate is null and CO.CompanyID={0}", companyId);
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                           
                            if (dataReader["Name"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["Name"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEUsers.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ResidentCountry"] != DBNull.Value) { oEUsers.ResidentContry = (string)dataReader["ResidentCountry"]; }
                            if (dataReader["NationalityName"] != DBNull.Value) { oEUsers.NationalityName = (string)dataReader["NationalityName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }

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
        public List<EUser> getAllTechnicain()
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
                    sQuery.Append(" select U.* from Users U ");
                    sQuery.Append(" inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UAB.BranchId ");
                    sQuery.Append(" where U.EndDate is null and U.AuthLevelRefId=2 and B.compnayId=2 ");
                 
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }

                         
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
        public List<EUser> getBranchUsers(int branchId)
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
                    sQuery.Append("select AU.Authname UserRole,CON.Name ResidentCountry,CONN.Name NationalityName, CO.Name CompanyName  ,* from users U ");
                    sQuery.Append("inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append("inner join Branchs BR on BR.branchId=UAB.BranchId ");
                    sQuery.Append("inner join Companies CO on CO.CompanyID = BR.compnayId ");
                    sQuery.Append("inner join Countries CON on CON.CountryId=U.CountryId ");
                    sQuery.Append("inner join Countries CONN on CONN.CountryId=U.Nationality ");
                    sQuery.Append("inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId ");

                    sQuery.AppendFormat("where U.IsDeleted = 0 and UAB.EndDate is null and CO.EndDate is null and BR.branchId={0}", branchId);
                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Name"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["Name"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEUsers.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ResidentCountry"] != DBNull.Value) { oEUsers.ResidentContry = (string)dataReader["ResidentCountry"]; }
                            if (dataReader["NationalityName"] != DBNull.Value) { oEUsers.NationalityName = (string)dataReader["NationalityName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["CompanyId"] != DBNull.Value) { oEUsers.CompanyId = (int)dataReader["CompanyId"]; }
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }
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
                    sQuery.Append("select AU.Authname UserRole,CON.Name ResidentCountry,CONN.Name NationalityName, CO.Name CompanyName ,* from users U ");
                    sQuery.Append("inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append("inner join Branchs BR on BR.branchId=UAB.BranchId ");
                    sQuery.Append("inner join Companies CO on CO.CompanyID = BR.compnayId ");
                    sQuery.Append("inner join Countries CON on CON.CountryId=U.CountryId ");
                    sQuery.Append("inner join Countries CONN on CONN.CountryId=U.Nationality ");
                    sQuery.Append("inner join Positions PO on PO.PositionId=U.PositionId ");
                    sQuery.Append("inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId ");

                    sQuery.AppendFormat("where U.UserId ={0} ", Id);
                   
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
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEUsers.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ResidentCountry"] != DBNull.Value) { oEUsers.ResidentContry = (string)dataReader["ResidentCountry"]; }
                            if (dataReader["NationalityName"] != DBNull.Value) { oEUsers.NationalityName = (string)dataReader["NationalityName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEUsers.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["UserAndBranchId"] != DBNull.Value) { oEUsers.UserAndBranchId = (int)dataReader["UserAndBranchId"]; }
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }


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
                    sQuery.Append("select BR.branchId, AU.Authname UserRole,CON.Name ResidentCountry,CONN.Name NationalityName, CO.Name CompanyName,CO.CompanyID,CO.CompanyTypeId ,* from users U ");
                    sQuery.Append("inner join UserAndBranch UAB on UAB.UserId=U.UserId ");
                    sQuery.Append("inner join Branchs BR on BR.branchId=UAB.BranchId ");
                    sQuery.Append("inner join Companies CO on CO.CompanyID = BR.compnayId ");
                    sQuery.Append("inner join Countries CON on CON.CountryId=U.CountryId ");
                    sQuery.Append("inner join Countries CONN on CONN.CountryId=U.Nationality ");
                    sQuery.Append("inner join Positions PO on PO.PositionId=U.PositionId ");
                    sQuery.Append("inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId ");

                    sQuery.AppendFormat(" where U.email='{0}' and U.password ='{1}' and U.IsDeleted=0 ", email, m_oEncrption.Encrypt(password));

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            oEUsers = new EUser();

                            if (dataReader["CompanyTypeId"] != DBNull.Value) { oEUsers.CompanyTypeId = (int)dataReader["CompanyTypeId"]; }
                            if (dataReader["UserId"] != DBNull.Value) { oEUsers.UserId = (int)dataReader["UserId"]; }
                            if (dataReader["Nationality"] != DBNull.Value) { oEUsers.Nationality = (int)dataReader["Nationality"]; }
                            if (dataReader["CountryId"] != DBNull.Value) { oEUsers.CountryId = (int)dataReader["CountryId"]; }
                            if (dataReader["PositionId"] != DBNull.Value) { oEUsers.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Title"] != DBNull.Value) { oEUsers.Title = (string)dataReader["Title"]; }
                            if (dataReader["Username"] != DBNull.Value) { oEUsers.Username = (string)dataReader["Username"]; }
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                            if (dataReader["Name"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["Name"]; }
                            if (dataReader["BranchName"] != DBNull.Value) { oEUsers.BranchName = (string)dataReader["BranchName"]; }
                            if (dataReader["ResidentCountry"] != DBNull.Value) { oEUsers.ResidentContry = (string)dataReader["ResidentCountry"]; }
                            if (dataReader["NationalityName"] != DBNull.Value) { oEUsers.NationalityName = (string)dataReader["NationalityName"]; }
                            if (dataReader["CompanyName"] != DBNull.Value) { oEUsers.CompanyName = (string)dataReader["CompanyName"]; }
                            if (dataReader["CompanyID"] != DBNull.Value) { oEUsers.CompanyId = (int)dataReader["CompanyID"]; }
                            if (dataReader["BranchId"] != DBNull.Value) { oEUsers.BranchId = (int)dataReader["BranchId"]; }
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }

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
                    sQuery.Append(" select AU.Authname UserRole,* from users U  ");
                    sQuery.Append(" inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId  ");
                    sQuery.AppendFormat("where U.email ='{0}'  and U.IsDeleted=0 and U.EndDate is null", email);

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
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }
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

        public async Task<EUser> getSingleUserBypasswordAndIdl(string password,int id)
        {

            string encPassword= m_oEncrption.Encrypt(password);
        
             var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            EUser oEUsers = null;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" select AU.Authname UserRole,* from users U  ");
                    sQuery.Append(" inner join AuthenticationLevels AU on AU.AuthId=U.AuthLevelRefId  ");
                    sQuery.AppendFormat("where U.Password ='{0}' and U.UserId={1} and U.IsDeleted=0 ", encPassword,id);

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
                            if (dataReader["UserRole"] != DBNull.Value) { oEUsers.UserRole = (string)dataReader["UserRole"]; }
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

        // get user by email

        public EUser getUserByEmail(string email)
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
                    sQuery.Append("select * from users U ");
               

                    sQuery.AppendFormat(" where U.email='{0}' ", email);

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
                            if (dataReader["FirstName"] != DBNull.Value) { oEUsers.FirstName = (string)dataReader["FirstName"]; }
                            if (dataReader["LastName"] != DBNull.Value) { oEUsers.Lastname = (string)dataReader["LastName"]; }
                            if (dataReader["City"] != DBNull.Value) { oEUsers.City = (string)dataReader["City"]; }
                            if (dataReader["Birthday"] != DBNull.Value) { oEUsers.Birthday = (DateTime)dataReader["Birthday"]; }
                            if (dataReader["Email"] != DBNull.Value) { oEUsers.Email = (string)dataReader["Email"]; }
                            if (dataReader["Mobile"] != DBNull.Value) { oEUsers.Mobile = (string)dataReader["Mobile"]; }
                            if (dataReader["Telephone"] != DBNull.Value) { oEUsers.Telephone = (string)dataReader["Telephone"]; }
                            if (dataReader["AuthLevelRefId"] != DBNull.Value) { oEUsers.AuthLevelRefId = (int)dataReader["AuthLevelRefId"]; }
                            if (dataReader["PictureFileName"] != DBNull.Value) { oEUsers.PictureFileName = (string)dataReader["PictureFileName"]; }
                     

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
            if (string.IsNullOrEmpty(newUser.PictureFileName))
            {
                newUser.PictureFileName = "";
            }
            newUser.EndDate = null;

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

        public async Task<EUser> updateProfile(EUser user)
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
                 context.Entry(user).Property(x => x.FirstName).IsModified = true;
                context.Entry(user).Property(x => x.Lastname).IsModified = true;
                context.Entry(user).Property(x => x.Mobile).IsModified = true;
                context.Entry(user).Property(x => x.Telephone).IsModified = true;
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
            eUser.EndDate =  DateTime.UtcNow;
            eUser.IsDeleted=true;

            if (eUser == null)
            {
                throw new DomainValidationFundException("Validation : The user is not found, make sure you are removing the correct user");
            }
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Users.Attach(eUser);
                context.Entry(eUser).Property(x => x.EndDate).IsModified = true;
                context.Entry(eUser).Property(x => x.IsDeleted).IsModified = true;

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
            // change email

            // ***** send email your password been changed.
            StringBuilder body = new StringBuilder();
            body.Append(" <div style=\"background-color:rgb(236, 236, 236); margin: 0px; font-family: 'Courier New', Courier, monospace; \">  ");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px; text-align: center; \">");
            body.Append("<img src=\"http://albayader-me.com/wp-content/uploads/2021/11/logo-albyader.png\" />");
            body.Append("</div>");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: white; padding: 30px;border: 1px solid rgb(217, 217, 217); \">");
            body.Append("");
            body.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear  {0} </p>", eUser.FirstName + ' ' + eUser.Lastname);

            body.AppendLine("<p>Your password changed successfuly  </p>");


            body.AppendLine(" <div>Regards </div>");
            body.AppendLine("<div>Al Bayader Team</div>");
            body.AppendLine("<div>OUR CLIENT IS OUR PARTNER</div>");
            body.Append("</div>");
            body.Append("</div>");
            body.Append("</div>");
            string subject = "Password Recovered AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result = await utilityHelper.SendEmailAsync( eUser.Email, subject, body.ToString());


            return eUser;
        }



        public string getUserEmail(string userId)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            string result;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select email from users where userid={0} ", userId);
                    command.CommandText = sQuery.ToString();
                    result = (string)command.ExecuteScalar();
                }
            }
            finally
            {
                conn.Close();
            }
           

            return result;
        }

        public DateTime getTokenDate(string token)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            DateTime result;
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.AppendFormat("select generatedDate from recoverpassword where token='{0}' ", token);
                    command.CommandText = sQuery.ToString();
                    result = (DateTime)command.ExecuteScalar();
                }


            }
            finally
            {
                conn.Close();
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
            body.Append(" <div style=\"background-color:rgb(236, 236, 236); margin: 0px; font-family: 'Courier New', Courier, monospace; \">  ");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px; text-align: center; \">");
            body.Append("<img src=\"http://albayader-me.com/wp-content/uploads/2021/11/logo-albyader.png\" />");
            body.Append("</div>");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: white; padding: 30px;border: 1px solid rgb(217, 217, 217); \">");
            body.Append("");
            body.AppendFormat("<p>Hello {0}</p>", eUser.FirstName + ' ' + eUser.Lastname);
            body.AppendLine("<p>Your password changed successfuly </p> ");


            body.AppendLine(" <div>Regards </div>");
            body.AppendLine("<div>Al Bayader Team</div>");
            body.AppendLine("<div>OUR CLIENT IS OUR PARTNER</div>");
            body.Append("</div>");
            body.Append("</div>");
            body.Append("</div>");
            string subject = "Password Recovered AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result = await utilityHelper.SendEmailAsync( eUser.Email, subject, body.ToString());

            if (!result)
            {
                throw new DomainInternalException("Password has been changed but Email can't be sent");
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
                throw new DomainValidationFundException("The email doesn't exist");
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
            body.Append(" <div style=\"background-color:rgb(236, 236, 236); margin: 0px; font-family: 'Courier New', Courier, monospace; \">  ");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: rgb(247 247 247); padding: 30px; text-align: center; \">");
            body.Append("<img src=\"http://albayader-me.com/wp-content/uploads/2021/11/logo-albyader.png\" />");
            body.Append("</div>");
            body.Append("<div style=\"width: 800px; margin-left: auto; margin-right: auto; background-color: white; padding: 30px;border: 1px solid rgb(217, 217, 217); \">");
            body.Append("");

            body.AppendFormat("<p style=\"font-weight:bold; font-size:22px; \">Dear  {0} </p>", eUser.FirstName + ' ' + eUser.Lastname);
            body.AppendFormat("<p>You requested to change your password</p>");
            body.AppendLine("<p>Please click on the following link  ");

            body.AppendFormat("<a href='http://service.albayader-me.com/recoverpasswprd?token={0}'>Change Password</a> to complete your request<p>", eRecoverPassword.token);
            body.AppendLine("<p>if you didn't requested to change the password please ignore this email</p>");

            body.AppendLine(" <div>Regards </div>");
            body.AppendLine("<div>Al Bayader Team</div>");
            body.AppendLine("<div>OUR CLIENT IS OUR PARTNER</div>");
            body.Append("</div>");
            body.Append("</div>");
            body.Append("</div>");

            string subject = "forget password from AL Bayader";
            UtilityHelper utilityHelper = new UtilityHelper();

            bool result = false;
            result =  await utilityHelper.SendEmailAsync(email,subject,body.ToString());

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

        public List<EPositions> getPostions()
        {
            List<EPositions> lpositions = new List<EPositions>();

            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append(" Select * from positions ");


                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            EPositions positions = new EPositions();
                            if (dataReader["PositionId"] != DBNull.Value) { positions.PositionId = (int)dataReader["PositionId"]; }
                            if (dataReader["Name"] != DBNull.Value) { positions.Name = (string)dataReader["Name"]; }



                            lpositions.Add(positions);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return lpositions;
        }


        public List<EUser> getAllAdminAndManager()
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
                    sQuery.Append(" select distinct U.* from Users U  ");
                    sQuery.Append(" inner join UserAndBranch UB on UB.UserId=U.UserId  ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UB.BranchId  ");
                    sQuery.Append(" where U.EndDate is null and U.AuthLevelRefId in (1,3)  and compnayId=2  ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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
        public List<EUser> getAllAdminAndManagerAndSupport()
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
                    sQuery.Append(" select distinct U.* from Users U  ");
                    sQuery.Append(" inner join UserAndBranch UB on UB.UserId=U.UserId  ");
                    sQuery.Append(" inner join Branchs B on B.branchId=UB.BranchId  ");
                    sQuery.Append(" where U.EndDate is null and U.AuthLevelRefId in (1,3,6)  and compnayId=2  ");

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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


        public List<EUser> getAllCompanyManager(int ServiceId)
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
                    sQuery.Append(" select U.* from Users U ");
                    sQuery.Append(" inner join UserAndBranch UB on UB.UserId=U.UserId ");
                    sQuery.Append(" inner join Branchs b on B.branchId=UB.BranchId  ");
                    sQuery.Append(" inner join Companies CO on Co.CompanyID=B.compnayId  ");
                    sQuery.Append(" where U.EndDate is null and U.AuthLevelRefId=4 and CO.CompanyID in  ");
                    sQuery.Append(" ( ");
                    sQuery.Append(" select co.CompanyID from Companies CO ");
                    sQuery.Append(" inner join Branchs B on B.compnayId=CO.CompanyID ");
                    sQuery.Append(" inner join Services SR on SR.BranchId=B.branchId ");
                
                    sQuery.AppendFormat(" where SR.ServiceId={0} ) ", ServiceId);

                    command.CommandText = sQuery.ToString();
                    DbDataReader dataReader = command.ExecuteReader();

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
    }
}


 
