using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Functions
{
    public class UserFunctions: IUser
    {

        
        public async Task<EUser> addUser(EUser newUser)
        {
            
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
            }

            return newUser;
        }
        public async Task<List<EUser>> getAllUsers()
        {
            List <EUser> users = new List<EUser>();
            using (var context =new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                users = await context.Users.ToListAsync();

            }
            return users;
        }

        public List<EUser> getAllUsersForAuth()
        {
            List<EUser> users = new List<EUser>();
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                users =  context.Users.ToList();
            }
            return users;
        }
        public async Task<EUser> addNewUser(EUser newUser)
        {

            DUser OEDUser = new DUser();
            EUser newAddedUser = await OEDUser.addUser(newUser);



            return newAddedUser;
        }

    }
}