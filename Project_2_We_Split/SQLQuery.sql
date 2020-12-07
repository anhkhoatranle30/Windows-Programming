go
create database WP_Project2_WeSplit
go
use WP_Project2_WeSplit
go

create table MEMBER
(
	MemberID int identity(1,1) primary key,
	MemberName nvarchar(50)
)

create table TRIP
(
	TripID int identity(1, 1) primary key,
	TripName nvarchar(50),
	Status int
)

create table TRIPIMAGES
(
	TripImageID int identity(1,1) primary key,
	TripID int,
	Path varchar(50),
	foreign key (TripID) references TRIP(TripID)
)

create table LOCATION 
(
	LocationID int identity(1,1) primary key,
	TripID int,
	LocationName nvarchar(50),
	foreign key (TripID) references TRIP(TripID)
)

create table MEMBERSPERTRIP 
(
	TripID int,
	MemberID int,
	primary key (TripID, MemberID),
	foreign key(TripID) references TRIP(TripID),
	foreign key (MemberID) references MEMBER(MemberID)
)

create table MEMBERCOST 
(
	CostID int identity(1,1) primary key,
	MemberID int,
	TripID int,
	CostName nvarchar(50),
	Cost int,
	foreign key (TripID, MemberID) references MEMBERSPERTRIP(TripID, MemberID)
)

insert into TRIP(TripName, Status) values(N'Du lịch Phú Yên', '2')
insert into TRIP(TripName, Status) values(N'Mộc Châu - Tà Xùa', '0')
insert into TRIP(TripName, Status) values(N'Côn Đảo', '0')
insert into TRIP(TripName, Status) values(N'Mùa thu ở Trung Quốc', '2')
insert into TRIP(TripName, Status) values(N'Mẫu Sơn - Lạng Sơn', '0')

insert into MEMBER(MemberName) values(N'Nguyễn Tuấn Khải')
insert into MEMBER(MemberName) values(N'Tô Phương Thanh')
insert into MEMBER(MemberName) values(N'Tô Việt Thắng')
insert into MEMBER(MemberName) values(N'Vũ Hoàng Lâm')
insert into MEMBER(MemberName) values(N'Bùi Tuyết Hân')

insert into TRIPIMAGES(TripID, Path) values('1', '1.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '2.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '3.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '4.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '5.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '6.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '7.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '8.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '9.jpg')
insert into TRIPIMAGES(TripID, Path) values('1', '10.jpg')
insert into TRIPIMAGES(TripID, Path) values('2', '1.jpg')
insert into TRIPIMAGES(TripID, Path) values('2', '2.jpg')
insert into TRIPIMAGES(TripID, Path) values('2', '3.jpg')
insert into TRIPIMAGES(TripID, Path) values('2', '4.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '1.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '2.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '3.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '4.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '5.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '6.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '7.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '8.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '9.jpg')
insert into TRIPIMAGES(TripID, Path) values('3', '10.jpg')
insert into TRIPIMAGES(TripID, Path) values('4', '1.jpg')
insert into TRIPIMAGES(TripID, Path) values('4', '2.jpg')
insert into TRIPIMAGES(TripID, Path) values('4', '3.jpg')
insert into TRIPIMAGES(TripID, Path) values('4', '4.jpg')
insert into TRIPIMAGES(TripID, Path) values('4', '5.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '1.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '2.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '3.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '4.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '5.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '6.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '7.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '8.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '9.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '10.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '11.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '12.jpg')
insert into TRIPIMAGES(TripID, Path) values('5', '13.jpg')

insert into LOCATION(TripID, LocationName) values('1', N'Phú Yên')
insert into LOCATION(TripID, LocationName) values('2', N'Hà Nội')
insert into LOCATION(TripID, LocationName) values('2', N'Thanh Sơn')
insert into LOCATION(TripID, LocationName) values('2', N'Phù Yên')
insert into LOCATION(TripID, LocationName) values('2', N'Bắc Yên')
insert into LOCATION(TripID, LocationName) values('2', N'Tà Xùa')
insert into LOCATION(TripID, LocationName) values('3', N'Côn Đảo')
insert into LOCATION(TripID, LocationName) values('4', N'Bắc Kinh')
insert into LOCATION(TripID, LocationName) values('4', N'Cam Túc')
insert into LOCATION(TripID, LocationName) values('4', N'Tây An')
insert into LOCATION(TripID, LocationName) values('4', N'Thành Đô')
insert into LOCATION(TripID, LocationName) values('5', N'Mẫu Sơn')
insert into LOCATION(TripID, LocationName) values('5', N'Núi Phặt Chỉ')
insert into LOCATION(TripID, LocationName) values('5', N'Suối Long Đầu')
insert into LOCATION(TripID, LocationName) values('5', N'Bản người Dao')

insert into MEMBERSPERTRIP(TripID, MemberID) values('1', '1')
insert into MEMBERSPERTRIP(TripID, MemberID) values('1', '2')
insert into MEMBERSPERTRIP(TripID, MemberID) values('1', '3')
insert into MEMBERSPERTRIP(TripID, MemberID) values('2', '1')
insert into MEMBERSPERTRIP(TripID, MemberID) values('2', '2')
insert into MEMBERSPERTRIP(TripID, MemberID) values('2', '3')
insert into MEMBERSPERTRIP(TripID, MemberID) values('2', '4')
insert into MEMBERSPERTRIP(TripID, MemberID) values('2', '5')
insert into MEMBERSPERTRIP(TripID, MemberID) values('3', '1')
insert into MEMBERSPERTRIP(TripID, MemberID) values('3', '2')
insert into MEMBERSPERTRIP(TripID, MemberID) values('4', '1')
insert into MEMBERSPERTRIP(TripID, MemberID) values('4', '4')
insert into MEMBERSPERTRIP(TripID, MemberID) values('4', '5')
insert into MEMBERSPERTRIP(TripID, MemberID) values('5', '3')
insert into MEMBERSPERTRIP(TripID, MemberID) values('5', '4')
insert into MEMBERSPERTRIP(TripID, MemberID) values('5', '5')

insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '1', N'Thuê khách sạn', '1700000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '1', N'Bè Nổi', '800000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '2', N'Thuê cano', '300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '2', N'Ăn uống', '1200000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '3', N'Vé xe', '450000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '3', N'Thuê xe máy', '1120000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('1', '3', N'Đồ cắm trại', '1300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '1', N'Thuê homestay', '3500000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '2', N'Cơm trưa', '750000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '2', N'Đổ xăng', '200000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '3', N'Kem đánh răng', '75000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '3', N'Dầu gội đầu', '60000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '3', N'Cơm tối', '800000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '3', N'Nước ngọt', '100000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '4', N'Bữa sáng', '650000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '5', N'Thuê lều', '200000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '5', N'Bia', '300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('2', '5', N'Đồ nướng', '350000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Vé xe', '320000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Tham quan', '480000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Thuê xe máy', '720000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Thuê khách sạn', '1400000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Dầu gội đầu', '20000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '1', N'Đổ xăng', '80000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '2', N'Hải sản', '300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '2', N'Ăn sáng trưa tối', '500000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('3', '2', N'Vé máy bay', '4400000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '1', N'Vé máy bay khứ hồi', '26946000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '4', N'Tour Tử Cấm Thành, Gubei', '3324000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '4', N'Vạn Lý Trường Thành', '1950000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '4', N'Taxi', '300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '4', N'Lạc Sơn Đại Phật', '846000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '5', N'Lăng Mộ Tần Thuỷ Hoàng', '1415000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '5', N'Ăn uống', '1200000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('4', '5', N'Thuê khách sạn', '4200000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '3', N'Taxi', '300000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '3', N'Thuê khách sạn', '1100000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '3', N'Lợn sữa quay', '850000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '3', N'Gà nướng mật ong', '150000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '4', N'Cá hồi Mẫu Sơn', '400000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '4', N'Rượu Mẫu Sơn', '100000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '5', N'Đổ Xăng', '50000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '5', N'Ăn sáng', '100000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '5', N'Ăn trưa', '100000')
insert into MEMBERCOST(TripID, MemberID, CostName, Cost) values('5', '5', N'Ăn tối', '123000')
