using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class CategoryDAO
    {

    }
    class CategoryDAOSQLServer
    {
        public static BindingList<CATEGORY> GetAll()
        {
            var db = new WP_Project3_CakeShopAppEntities();
            var result = new BindingList<CATEGORY>(db.CATEGORies.ToList());
            return result;
        }
    }
}
