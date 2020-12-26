﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class CakeDAO
    {
    }
    class CakeDAOSQLServer
    {
        public static BindingList<CAKE> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<CAKE>(db.CAKEs.ToList());
            return result;
        }
        public static CAKE GetByID(int cakeID)
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = db.CAKEs.Find(cakeID);
            return result;
        }
    }
}