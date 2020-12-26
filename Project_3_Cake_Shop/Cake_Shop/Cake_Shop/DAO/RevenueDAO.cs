using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake_Shop.DAO
{
    class RevenueDAO
    {
    }
    class RevenueDAOSQLServer
    {
        public static BindingList<Revenue> GetAllCategories()
        {
			var db = new WP_Project3_CakeShopAppEntities();
			var order_detail = db.ORDER_DETAIL.ToList();
			var cake = db.CAKEs.ToList();


			var query = order_detail.GroupJoin(cake,
								o => o.CakeID,
								c => c.CakeID,
								(o, gc) => new { o, gc }
								)
							.SelectMany(r => r.gc.DefaultIfEmpty(),
										(r, gc) => new { r.o.CakeID, Money = gc.Price * r.o.Quantity })
							.GroupBy(r => r.CakeID)
							.Select(r => new { CakeID = r.Key, Money = r.Sum(rev => rev.Money) });
			var query2 = cake.Join(query,
										c => c.CakeID,
										q => q.CakeID,
										(c, q) => new { CatID = c.CategoryID, Money = q.Money })
							.GroupBy(r => r.CatID)
							.Select(r => new Revenue
									{	
										Name = r.Key.ToString(), 
										Value = (int)r.Sum(i => i.Money) 
									})
							.ToList();
			var result = new BindingList<Revenue>(query2);
			return result;
		}
		public static BindingList<Revenue> GetAllMonths()
        {
			return new BindingList<Revenue>();
        }
    }
}
