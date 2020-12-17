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

create table STATUS(
	StatusID int primary key,
	StatusDisplayText nvarchar(50)
)

create table TRIP
(
	TripID int identity(1, 1) primary key,
	TripName nvarchar(50),
	TripDes nvarchar(1000),
	Status int foreign key references STATUS(StatusID)
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
	RecordID int identity(1,1) primary key,
	TripID int,
	MemberID int,
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
	foreign key (TripID) references TRIP(TripID),
	foreign key (MemberID) references MEMBER(MemberID)
)

insert into MEMBER(MemberName) values(N'Nguyễn Tuấn Khải')
insert into MEMBER(MemberName) values(N'Tô Phương Thanh')
insert into MEMBER(MemberName) values(N'Tô Việt Thắng')
insert into MEMBER(MemberName) values(N'Vũ Hoàng Lâm')
insert into MEMBER(MemberName) values(N'Bùi Tuyết Hân')

insert into STATUS(StatusID, StatusDisplayText) values ('0', N'Lên kế hoạch')
insert into STATUS(StatusID, StatusDisplayText) values ('1', N'Đang đi')
insert into STATUS(StatusID, StatusDisplayText) values ('2', N'Đã đi xong')


insert into TRIP(TripName, TripDes, Status) values
(N'Du lịch Phú Yên', N'Phú Yên đẹp tựa một cô thôn nữ, có nét chân chất, mộc mạc của làng quê Việt Nam lại ẩn chứa những nét nhẹ nhàng, thuần khiết, trong trẻo. Vẻ đẹp của Phú Yên đi cùng với những câu hát tuổi thơ “Bóng trăng trắng ngà, có cây đa to, có thằng Cuội già, ôm một mối mơ” trong bộ phim “Tôi thấy hoa vàng trên cỏ xanh” do đạo diễn Victor Vũ chắp tay thực hiện, đã làm lay động trái tim của biết bao nhiêu khán giả. Cũng chính bởi lẽ đó, du lịch Phú Yên đã trở thành một điểm đến lí tưởng trong lòng rất nhiều du khách!','2')
,(N'Mộc Châu - Tà Xùa', N'Những đồi chè xanh bạt ngàn phủ kín thung lũng, những mùa hoa ban, hoa mận nở trắng rừng, những cô gái Thái e ấp, dịu dàng với đôi má hây hây đỏ trong buổi chợ phiên, hay vẻ đẹp khó quên của Thác Dải Yếm mùa nước… Mộc Châu cứ nhẹ nhàng làm ngất ngây biết bao nhiêu người như thế khi đặt chân đến đây.','0')
,(N'Côn Đảo', N'Hồi bé, 2 chữ “Côn Đảo” trong mỗi chúng ta có lẽ gắn liền với 2 chữ “nhà tù” đứng trước nó - nơi “địa ngục trần gian” giam giữ và tra tấn các chiến sĩ cộng sản của ta. Ngày nay, dù ấn tượng này vẫn còn âm vang trong nhiều người, tuy nhiên hòn đảo này đã được bình chọn là di tích lịch sử quốc gia, đồng thời là thiên đường nghỉ dưỡng lý tưởng dành cho du khách thập phương. Ngoài những địa điểm thu hút khách du lịch, hãy cùng VTBay điểm qua 5 câu chuyện thú vị về Côn Đảo mà có thể bạn chưa biết, biết đâu bạn sẽ “đổ” liền và muốn ngay lập tức book vé đi Côn Đảo ấy chứ!','1')
,(N'Mùa thu ở Trung Quốc', N'Bạn đã đi du lịch Trung Quốc đất nước của nhiều danh lam thắng cảnh tươi đẹp cùng các kỳ quan thiên nhiên thế giới đã được công nhận bao giờ chưa? Hay có bất kỳ ý định nào cho chuyến chu du châu Á. Hãy cùng đến với những kinh nghiệm du lịch Trung Quốc để có một chuyến đi trọn vẹn nhất nhé!','2')
,(N'Mẫu Sơn - Lạng Sơn', N'Khi đến Lạng Sơn, ngoài cửa khẩu Tân Thanh, chợ Đông Kinh, là những thiên đường mua sắm, khách du lịch có thể lựa chọn nơi ngắm cảnh, thư giãn như Động Nhị Thanh; Bến Đá Kì Cùng; Hang động Chùa Tiên và Giếng Tiên; Khu Du lịch Mẫu Sơn; Khu danh thắng Hang Gió; Thành Nhà Mạc; Di tích Đoàn Thành Lạng Sơn, Ải Chi Lăng để tham quan.Trong số này, Mẫu Sơn là một địa chỉ hấp dẫn, không thể bỏ qua. Mùa hè thì nơi đây không khí mát mẻ, dễ chịu. Khi đông về thì có lúc Mẫu Sơn phủ lên mình màu trắng xóa của tuyết. Với một nước thuộc vùng nhiệt đới như Việt Nam, không phải ở bất kì nơi đâu cũng có hiện tượng thiên nhiên kì thú này.','0')


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

