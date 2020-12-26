<Query Kind="Statements">
  <Connection>
    <ID>7e1ed734-9f6b-4f6d-87b4-c20c9442d78f</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-3FC04S3\SQLEXPRESS</Server>
    <Database>WP_Project3_CakeShopApp</Database>
  </Connection>
</Query>

var RevByCakeIDquery = ORDER_DETAILs.GroupJoin(CAKEs,
								o => o.CakeID,
								c => c.CakeID,
								(o, gc) => new { o, gc }
								)
							.SelectMany(r => r.gc.DefaultIfEmpty(),
										(r, gc) => new { r.o.CakeID, Money = gc.Price * r.o.Quantity })
							.GroupBy(r => r.CakeID)
							.Select(r => new { CakeID = r.Key, Money = r.Sum(rev => rev.Money) });
var RevByCatIDquery = CAKEs.Join(RevByCakeIDquery,
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
						CATEGORies,
						q => q.CatID,
						c => c.CatID,
						(q, gr) => new {q, gr})
						.SelectMany(r => r.gr.DefaultIfEmpty(),
									(r, gr) => new {CatName = gr.CatName,
													Money = r.q.Money});
RevByCakeIDquery.Dump();
RevByCatIDquery.Dump();
RevByCatNameQuery.Dump();