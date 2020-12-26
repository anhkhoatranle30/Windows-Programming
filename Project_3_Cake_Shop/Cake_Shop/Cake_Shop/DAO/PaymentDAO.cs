using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class PaymentDAO
    {
    }
    class PaymentDAOSQLServer
    {
        public static BindingList<PAYMENT> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<PAYMENT>(db.PAYMENTs.ToList());
            return result;
        }
    }
}
