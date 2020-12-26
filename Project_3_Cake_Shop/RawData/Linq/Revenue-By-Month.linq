<Query Kind="Statements">
  <Connection>
    <ID>7e1ed734-9f6b-4f6d-87b4-c20c9442d78f</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-3FC04S3\SQLEXPRESS</Server>
    <Database>WP_Project3_CakeShopApp</Database>
  </Connection>
</Query>

var RevByOrderID = ORDER_DETAILs.Join(CAKEs,
								od => od.CakeID,
								c => c.CakeID,
								(od, c) => new {OrderID = od.OrderID, Money = od.Quantity * c.Price})
							.GroupBy(r => r.OrderID)
							.Select(r => new 
							{
								OrderID = r.Key,
								Money = r.Sum(i => i.Money)
							});
var RevByMonthsQuery = RevByOrderID.Join(ORDERS,
								q => q.OrderID,
								o => o.OrderID,
								(q, o) => new 
										{
											Month = o.CreatedAt,
											Money = q.Money
										});
RevByOrderID.Dump();
RevByMonthsQuery.Dump();