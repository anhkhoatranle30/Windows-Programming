<Query Kind="Statements">
  <Connection>
    <ID>7e1ed734-9f6b-4f6d-87b4-c20c9442d78f</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-3FC04S3\SQLEXPRESS</Server>
    <Database>WP_Project3_CakeShopApp</Database>
  </Connection>
</Query>

var query = ORDER_DETAILs.GroupJoin(CAKEs,
								o => o.CakeID,
								c => c.CakeID,
								(o, gc) => new {o, gc}
								)
							.SelectMany(r => r.gc.DefaultIfEmpty(),
										(r, gc) => new {r.o.CakeID, Money = gc.Price * r.o.Quantity})
							.GroupBy(r => r.CakeID)
							.Select(r => new {CakeID = r.Key, Money = r.Sum(rev => rev.Money)});
query.Dump();