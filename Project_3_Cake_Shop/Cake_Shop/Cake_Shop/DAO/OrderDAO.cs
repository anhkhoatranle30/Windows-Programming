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
    class OrderDAOSQLServer
    {
        public static BindingList<ORDER> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<ORDER>(db.ORDERS.ToList());
            return result;
        }
        /// <summary>
        /// Thêm một order vào db
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>OrderID vừa mới thêm</returns>
        public static int Add(ORDER newOrder)
        {
            var db = new WP_Project3_CakeShopAppEntities();
            int lastOrderID = db.ORDERS.ToList().Last().OrderID;
            if(newOrder.OrderID != lastOrderID + 1)
            {
                newOrder.OrderID = lastOrderID + 1;
            }
            db.ORDERS.Add(newOrder);
            db.SaveChanges();

            db = new WP_Project3_CakeShopAppEntities();
            int addedOrderID = db.ORDERS.ToList().Last().OrderID;
            return addedOrderID;
        }
    }
}
