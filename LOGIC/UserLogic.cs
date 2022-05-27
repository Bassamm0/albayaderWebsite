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
        public async Task<Boolean> AddUserWithBranch(EUser newUser)
        {

            //validate the username and email already existed

            var resul = await _DUser.addUser(newUser);
            if (resul.UserId > 0)
            {
                // insert user and branch
                EUserAndBranch _eUserAndBranch = new EUserAndBranch();
                DUserAndBranch _dUserAndBranch = new DUserAndBranch();

                _eUserAndBranch.UserId = resul.UserId;
                _eUserAndBranch.BranchId = resul.BranchId;
               var branchRes= await _dUserAndBranch.addUserAndBranch(_eUserAndBranch);


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
        public async Task<Boolean> updateUserwithbranch(EUser user)
        {

            var resul = await _DUser.updateUser(user);
            if (resul != null && resul.UserId > 0)
            {
                // insert user and branch
                EUserAndBranch _eUserAndBranch = new EUserAndBranch();
                DUserAndBranch _dUserAndBranch = new DUserAndBranch();
                _eUserAndBranch.UserAndBranchId = resul.UserAndBranchId;
                _eUserAndBranch.UserId = resul.UserId;
                _eUserAndBranch.BranchId = resul.BranchId;
                var branchRes = await _dUserAndBranch.updateUserAndBranch(_eUserAndBranch);

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
        public async Task<Boolean> changePassword(int id, string oldPassword,string password)
        {

            // validate the old password

            var user = await _DUser.getSingleUserBypasswordAndIdl(oldPassword, id);
            if (user == null)
            {
                throw new DomainValidationFundException("Invalid Old Password.");
             }
            if(password == oldPassword)
            {
                throw new DomainValidationFundException("You can't use the same old password.");
            }
            if (!IsValidPassword(password))
            {
                 throw new DomainValidationFundException("The provided password didn't meet the minimum required complexity.");
            }
            var resul = await _DUser.changePassword(id,password);
            if (resul != null && resul.UserId > 0)
            {
                return true;
                // send email
            }
            else
            {
                return false;
            }

        }
        public async Task<Boolean> recoverPassword( string password,string token)
        {


            ERecoverPassword eRecoverPassword = _DUser.getTokenDetails(token);
            if (eRecoverPassword == null)
            {
                throw new DomainNotFundException("Token not correct");

            }

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


        public async Task<Boolean> validateToken(string token)
        {


            ERecoverPassword eRecoverPassword = _DUser.getTokenDetails(token);
            if(eRecoverPassword == null)
            {
                throw new DomainNotFundException("Token not correct");

            }
            DateTime tokenDate = eRecoverPassword.generatedDate;

            TimeSpan ts = DateTime.Now - tokenDate;

           
            if (ts.TotalMinutes > 15)
            {
                throw new DomainExpiredException("Token already expired");
            }

           

           
                return true;
          
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
        public async Task<List<EPositions>> getPostions()
        {

            List<EPositions> positions = _DUser.getPostions();

            return positions;
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = @"(?=^.{8,30}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;&quot;:;'?/&gt;.&lt;,]).*$";
            Regex currencyRegex = new Regex(pattern);
            return currencyRegex.IsMatch(password);
        }
    }
}
