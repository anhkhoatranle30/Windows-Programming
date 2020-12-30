using System;
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
        public static BindingList<CAKE> GetAllByCatID(int catID)
        {
            if(catID == -1)
            {
                return GetAll();
            }
            var db = new WP_Project3_CakeShopAppEntities();
            var cakeList = db.CAKEs.ToList();
            var query = cakeList
                                .Where(c => c.CategoryID == catID)
                                .ToList();
            var result = new BindingList<CAKE>(query);
            return result;
        }
        public static string ImagesFolder(int cakeID)
        {
            var result = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\Cakes\\" + cakeID.ToString();
            return result;
        }
        public static int Add(CAKE cake)
        {
            var db = new WP_Project3_CakeShopAppEntities();
            int nextCakeID = db.CAKEs.ToList().Last().CakeID + 1;
            if (cake.CakeID != nextCakeID)
            {
                cake.CakeID = nextCakeID;
            }
            db.CAKEs.Add(cake);
            db.SaveChanges();

            return nextCakeID;
        }
    }
}
