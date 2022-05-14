using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserConstants
    {
        public static List<EUser> Users = new List<EUser>()
        {
            new EUser() { Username = "jason_admin", Email = "jason.admin@email.com", Password = "MyPass_w0rd", FirstName = "Jason", Lastname = "Bryant", Role = "Administrator" },
            new EUser() { Username = "elyse_seller", Email = "elyse.seller@email.com", Password = "MyPass_w0rd", FirstName = "Elyse", Lastname = "Lambert", Role = "Seller" },
        };
    }
}
