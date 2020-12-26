using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class OrderDAO
    {
    }
    class OrderSQLServer
    {
        public static BindingList<ORDER> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<ORDER>(db.ORDERS.ToList());
            return result;
        }

    }
}
