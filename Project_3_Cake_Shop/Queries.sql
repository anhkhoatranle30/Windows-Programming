/*doanh thu theo thang trong nam*/
select MONTH(o.CreatedAt) as Months, sum(od.Quantity * c.Price) as Number
from ORDER_DETAIL od, ORDERS o, CAKE c
where od.OrderID = o.OrderID and od.CakeID = c.CakeID and YEAR(o.CreatedAt) = YEAR(GETDATE()) - 1
group by MONTH(o.CreatedAt)
--doanh thu theo banh cua nam nay
select c.CakeID, c.CakeName, sum(od.Quantity * c.Price)
from ORDER_DETAIL od, ORDERS o, CAKE c
where od.OrderID = o.OrderID and od.CakeID = c.CakeID and YEAR(o.CreatedAt) = YEAR(GETDATE()) - 1
group by c.CakeID, c.CakeName