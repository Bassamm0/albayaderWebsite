using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;
using static DAL.DALException;
using System.Text.RegularExpressions;

namespace LOGIC.UserLogic
{
    public class UserLogic
    {
        private UserFunctions _userFunction =new UserFunctions();
        DUser _DUser = new DUser();
        //With Iuser interface
        //public async Task<Boolean> addUser(string username, string emailAdress, string password, int authLevelId)
        //{

        //        var resul= await _user.addUser(username, emailAdress, password, authLevelId);
        //        if (resul.Id > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }


        //}

        public  EUser getLoginUser(string email,string password)
        {


            EUser user = _DUser.getLoginUser(email,password);
            if(user == null)
            {
                EErrorLogin errorLogin = new EErrorLogin();
                errorLogin.Email = email;
                errorLogin.Password = password;
                errorLogin.ErrorDate=DateTime.Now;
                _DUser.addErrorLogin(errorLogin);


            }else
            {
                ELoginLog eLoginLog = new ELoginLog();
                eLoginLog.LoginDate = DateTime.Now;
                eLoginLog.UserId = user.UserId;
                _DUser.addLoginLog(eLoginLog);
            }

            return user;
        }
        public async Task<List<EUser>> getCompanyUsers(int companyId)
        {
             List<EUser> users = _DUser.getCompanyUsers(companyId);

            return users;
        }
        public async Task<List<EUser>> getBranchUsers(int branchId)
        {
            List<EUser> users = _DUser.getBranchUsers(branchId);

            return users;
        }
        public async Task<List<EUser>> getAllUsers()
        {
            List<EUser> users = _DUser.getAllUsers();

            return users;
        }
        public async Task<EUser> getUserById(int id)
        {
             EUser user = _DUser.getSingleUser(id);

            return user;
        }
        public List<EUser> getAllUsersForAuth()
        {
            List<EUser> users = _userFunction.getAllUsersForAuth();

            return users;
        }
        public List<EUser> getAllUsersForAuthNew()
        {
             List<EUser> users = _DUser.getAllUsers();

            return users;
        }

        public async Task<Boolean> addUser(EUser newUser)
        {
            
            //validate the username and email already existed
            
                var resul = await _DUser.addUser(newUser);
                if (resul.UserId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        public async Task<Boolean> updateUser(EUser user)
        {
           
                var resul = await _DUser.updateUser(user);
                if (resul!=null && resul.UserId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
        }
        public async Task<Boolean> deleteUser(int Id)
        {

            var resul = await _DUser.deleteUser(Id);
            if (resul != null && resul.UserId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> removUser(int id)
        {

            var resul = await _DUser.removeUser(id);
            if (resul != null && resul.UserId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> changePassword(int id, string password)
        {
            if (!IsValidPassword(password))
            {
                 throw new DomainValidationFundException("The provided password didn't meet the minimum required complexity.");
            }
            var resul = await _DUser.changePassword(id,password);
            if (resul != null && resul.UserId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> recoverPassword( string password,string token)
        {


            ERecoverPassword eRecoverPassword = _DUser.getTokenDetails(token);
            DateTime tokenDate = eRecoverPassword.generatedDate;
 
            TimeSpan ts = DateTime.Now - tokenDate;

            if (tokenDate == null)
            {
                throw new DomainNotFundException("Token not correct");
            }
            if(ts.TotalMinutes > 15)
            {
                throw new DomainExpiredException("Token already expired");
            }

            if (!IsValidPassword(password))
            {
                throw new DomainValidationFundException("The provided password didn't meet the minimum required complexity.");
            }

            var resul = await _DUser.recoverPassword(eRecoverPassword.UserId, password);
            if (resul != null && resul.UserId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> forgetpassword( string email)
        {

         
            // if email exist

           
            var resul = await _DUser.forgetpassword( email);
            if (resul != null && resul.UserId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsValidPassword(string password)
        {
            string pattern = @"(?=^.{8,30}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;&quot;:;'?/&gt;.&lt;,]).*$";
            Regex currencyRegex = new Regex(pattern);
            return currencyRegex.IsMatch(password);
        }
    }
}
