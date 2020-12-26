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
			var category = db.CATEGORies.ToList();

			var RevByCakeIDquery = order_detail.GroupJoin(cake,
								o => o.CakeID,
								c => c.CakeID,
								(o, gc) => new { o, gc }
								)
							.SelectMany(r => r.gc.DefaultIfEmpty(),
										(r, gc) => new { r.o.CakeID, Money = gc.Price * r.o.Quantity })
							.GroupBy(r => r.CakeID)
							.Select(r => new { CakeID = r.Key, Money = r.Sum(rev => rev.Money) });
			var RevByCatIDquery = cake.Join(RevByCakeIDquery,
										c => c.CakeID,
										q => q.CakeID,
										(c, q) => new { CatID = c.CategoryID, Money = q.Money })
							.GroupBy(r => r.CatID)
							.Select(r => new
							{
								CatID = r.Key,
								Money = (int)r.Sum(i => i.Money)
							});
			var RevByCatNameQuery = RevByCatIDquery.GroupJoin(
									category,
									q => q.CatID,
									c => c.CatID,
									(q, gr) => new { q, gr })
									.SelectMany(r => r.gr.DefaultIfEmpty(),
												(r, gr) => new Revenue
												{
													Name = gr.CatName,
													Value = r.q.Money
												})
									.ToList();
			var result = new BindingList<Revenue>(RevByCatNameQuery);
			return result;
		}
		public static BindingList<Revenue> GetAllMonths()
        {
			var db = new WP_Project3_CakeShopAppEntities();
			var order = db.ORDERS.ToList();
			var order_detail = db.ORDER_DETAIL.ToList();
			var cake = db.CAKEs.ToList();

			var RevByOrderID = order_detail.Join(cake,
								od => od.CakeID,
								c => c.CakeID,
								(od, c) => new { OrderID = od.OrderID, Money = od.Quantity * c.Price })
							.GroupBy(r => r.OrderID)
							.Select(r => new
							{
								OrderID = r.Key,
								Money = r.Sum(i => i.Money)
							});
			var RevByMonthsQuery = RevByOrderID.Join(order,
											q => q.OrderID,
											o => o.OrderID,
											(q, o) => new Revenue
											{
												Name = "Tháng" + ((DateTime)o.CreatedAt).Month.ToString(),
												Value = (int)q.Money
											})
											.ToList();


			var result = new BindingList<Revenue>(RevByMonthsQuery);
			return result;
        }
    }
}
