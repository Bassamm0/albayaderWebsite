﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entity
{
    public class UserViewModel
    {

        public int userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Int32 authLevel { get; set; }
        public string title { get; set; }

       
        
        public string firstName { get; set; }
       
        public string lastName { get; set; }
       
         
       
        public string mobile { get; set; }

       
        public string telephone { get; set; }
        public string role { get; set; }

        public int nationality { get; set; }
        public int countryId { get; set; }

      
        public string city { get; set; }

        public string birthday { get; set; }


        public string pictureFileName { get; set; }

    }
}