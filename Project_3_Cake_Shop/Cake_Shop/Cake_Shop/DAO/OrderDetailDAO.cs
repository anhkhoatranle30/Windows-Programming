using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class OrderDetailDAO
    {
    }
    class OrderDetailSQLServer
    {
        public static BindingList<ORDER_DETAIL> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<ORDER_DETAIL>(db.ORDER_DETAIL.ToList());
            return result;
        }
    }
}
