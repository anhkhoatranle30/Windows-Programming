using Cake_Shop.Business;
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
        public static void Add(ORDER_DETAIL newOD)
        {
            var db = new WP_Project3_CakeShopAppEntities();
            db.ORDER_DETAIL.Add(newOD);
            db.SaveChanges();
        }
        public static void Add(BindingList<CartItem> cartList, int orderID)
        {
            var db = new WP_Project3_CakeShopAppEntities();
            foreach(var item in cartList)
            {
                db.ORDER_DETAIL.Add(new ORDER_DETAIL()
                {
                    OrderID = orderID,
                    CakeID = item.CakeItem.CakeID,
                    Quantity = item.Quantity
                });
            }
            db.SaveChanges();
        }
    }
}
