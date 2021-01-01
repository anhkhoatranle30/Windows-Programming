using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class InfoDAO
    {
    }
    class InfoDAOSQLServer
    {
        public static INFO GetRandom()
        {
            Random _rng = new Random();
            var db = new WP_Project3_CakeShopAppEntities();
            var infoList = db.INFOes.ToList();
            int lastInfoID = infoList.Last().InfoID;
            var randomInfo = infoList[_rng.Next(1, lastInfoID + 1)];
            return randomInfo;
        }
    }
}
