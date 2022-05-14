using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUser
    {

        Task<EUser> addUser(EUser newUser);
        Task<List<EUser>> getAllUsers();
        Task<EUser> addNewUser(EUser newUser);




    }
}
