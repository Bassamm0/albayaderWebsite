using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL.Functions;

namespace LOGIC
{
    public class EmailLogic
    {

        public async Task<Boolean> sendEmail()
        {
            DEmail _demail=new DEmail();
            var resul = await _demail.sendEmail();

         return resul;
        }
    }
}
