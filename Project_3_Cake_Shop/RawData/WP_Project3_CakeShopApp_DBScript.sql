USE [master]
GO
/****** Object:  Database [WP_Project3_CakeShopApp]    Script Date: 1/1/2021 11:03:35 AM ******/
CREATE DATABASE [WP_Project3_CakeShopApp] ON  PRIMARY 
( NAME = N'WP_Project3_CakeShopApp', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\WP_Project3_CakeShopApp.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WP_Project3_CakeShopApp_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\WP_Project3_CakeShopApp_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WP_Project3_CakeShopApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET  MULTI_USER 
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET DB_CHAINING OFF 
GO
USE [WP_Project3_CakeShopApp]
GO
/****** Object:  Table [dbo].[CAKE]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAKE](
	[CakeID] [int] NOT NULL,
	[CakeName] [nvarchar](100) NULL,
	[Description] [nvarchar](1500) NULL,
	[Price] [int] NULL,
	[Image] [nvarchar](50) NULL,
	[CategoryID] [int] NULL,
 CONSTRAINT [PK_CAKE] PRIMARY KEY CLUSTERED 
(
	[CakeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CATEGORY]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY](
	[CatID] [int] NOT NULL,
	[CatName] [nvarchar](100) NULL,
 CONSTRAINT [PK_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INFO]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INFO](
	[InfoID] [int] NOT NULL,
	[FullDes] [nvarchar](650) NULL,
 CONSTRAINT [PK_INFO] PRIMARY KEY CLUSTERED 
(
	[InfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDER_DETAIL]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDER_DETAIL](
	[OrderID] [int] NOT NULL,
	[CakeID] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ORDER_DETAIL] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[CakeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDERS](
	[OrderID] [int] NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[HomeAddress] [nvarchar](100) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[PaymentType] [int] NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT]    Script Date: 1/1/2021 11:03:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT](
	[PaymentType] [int] NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[IsShipping] [int] NULL,
 CONSTRAINT [PK_PAYMENT] PRIMARY KEY CLUSTERED 
(
	[PaymentType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (1, N'Angel Pony Cake', N'Tùy Chọn Cốt Bánh:
▪️ Banana Walnut Cake
▪️ Carrot Cake
▪️ Chocolate Cake
▪️ Red Velvet Cake
🎂 Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất.
✏️ Quý khách vui lòng chú thích thêm lời nhắn để viết lên đế bánh.', 750000, N'main.jpeg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (2, N'Baby Dino Cake', N'Tùy Chọn Cốt Bánh:
▪️ Banana Walnut Cake
▪️ Carrot Cake
▪️ Chocolate Cake
▪️ Red Velvet Cake
🎂 Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất.
✏️ Quý khách vui lòng chú thích thêm lời nhắn để viết lên đế bánh.', 690000, N'main.png', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (3, N'Baby Shark #1', N'Tùy Chọn Cốt Bánh:
▪️ Banana Walnut Cake
▪️ Carrot Cake
▪️ Chocolate Cake
▪️ Red Velvet Cake
🎂 Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất.
✏️ Quý khách vui lòng chú thích thêm lời nhắn để viết lên đế bánh.', 680000, N'main.jpg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (4, N'Baby Shark #2', N'Tùy Chọn Cốt Bánh:
▪️ Banana Walnut Cake
▪️ Carrot Cake
▪️ Chocolate Cake
▪️ Red Velvet Cake
🎂 Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất.
✏️ Quý khách vui lòng chú thích thêm lời nhắn để viết lên đế bánh.', 680000, N'main.jpg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (5, N'Banana Walnuts Birthday Cake', N'Thành phần: Chuối, bột mì, hạt óc chó, trứng, sữa, dầu, đường, phô mai…
✅ 3 Tầng Bánh Kem Chuối Hạt Óc Chó
✅ Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất
✏️ Quý khách vui lòng chú thích thêm lời nhắn chúc mừng để đính kèm trên lá cờ sinh nhật', 590000, N'main.jpg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (6, N'Carrot Birthday Cake', N' Thành phần: Cà rốt, bột mì, hạt óc chó, trứng, sữa, dầu, đường, phô mai…
✅ 3 Tầng Bánh Kem Cà Rốt
✅ Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất
✏️ Quý khách vui lòng chú thích thêm lời nhắn chúc mừng để đính kèm trên lá cờ sinh nhật', 590000, N'main.jpeg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (7, N'Cinderella', N'Tùy Chọn Cốt Bánh:
▪️ Banana Walnut Cake
▪️ Carrot Cake
▪️ Chocolate Cake
▪️ Red Velvet Cake
🎂 Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất.
✏️ Quý khách vui lòng chú thích thêm lời nhắn để viết lên đế bánh.', 680000, N'main.jpg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (8, N'Colorful Candy Cake', N'Bánh Sinh Nhật Kẹo Sắc Màu
✅ Thành phần: Bột mì, dầu, đường, trứng, sữa, chocolate, bột ca cao. Được phủ một lớp kem cheese màu xanh mint và kẹo trang trí ở quanh đáy bánh.
✅ 3 Tầng Bánh Kem Tùy Chọn: Chocolate, Red Velvet, Chuối Hạt Óc Chó, Cà Rốt
✅ Đường kính: 16 cm
❗️ Vui lòng đặt trước 24h để được phục vụ tốt nhất
✏️ Quý khách vui lòng chú thích thêm lời nhắn chúc mừng để viết lên đế bánh kem', 570000, N'main.jpg', 1)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (9, N'Diet Whole Wheat Mix+', N'Bánh Ăn Kiêng Nguyên Cám Mix+

Dòng bánh ăn kiêng mới nhất từ La Vita, bao gồm bột nguyên cám, bột mì lúa mạch đen, các loại hạt óc chó, hạnh nhân, nho khô,… cho một hương vị ngon miệng nhưng vẫn giúp hỗ trợ ăn kiêng, giảm cân hiệu quả.
Trọng lượng: 300 gram
Hình thức: Ổ dài chia thành 10-12 lát', 89000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (10, N'Whole Wheat 12 Grain Bread', N'Bánh 12 Hạt Nguyên Cám
Trọng lượng: 400 gram
Hình dáng: Ổ dài, chia thành 12 – 13 lát
Đặt trước: 1 ngày để được phục vụ tốt nhất', 60000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (11, N'Classic English Muffins (6 Pack)', N'Món bánh cổ điển của người Anh, thích hợp để ăn sáng cùng món trứng Benedict', 86000, N'main.jpg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (12, N'Combo Bánh Ăn Kiêng', N'Thông tin combo Bánh Ăn Kiêng gồm:
🍞 Nordlander Bread – Bánh Lúa Mạch Đen Đức. Làm từ bột lúa mạch, ít calories, nhiều hạt ngũ cốc dinh dưỡng nên Norlander là chiếc bánh chuyên biệt dành cho chế độ ăn kiêng hiệu quả.
✔️Trọng lượng: 400gr
✔️Calories ước tính là 230 – 250cal/100gr
🍞 Dark Rye Bread – Bánh Mì Đen Đan Mạch. Bánh mềm, thơm, ít calories, phù hợp cho ăn kiêng.
✔️Trọng lượng: 300gr
✔️Calories ước tính là 259 – 280cal/100gr
🥯 Rye Caraway Bagel – Bánh Mì Đen Vòng ngon nức tiếng. Bánh làm từ bột mì đen, dạng hình đồng xu, có vị thơm từ hạt Thì Là Ba Tư.
✔️Quy cách: 3 chiếc/ túi, 300gr/túi
✔️Calories ước tính là 259 – 280cal/100gr
🍞 High Fiber Bread – Bánh Mì Chất xơ với ngũ cốc nguyên hạt. Là bánh ngũ cốc dành cho ăn kiêng với hàm lượng chất xơ cao, ít calories và giúp no lâu.
✔️Trọng lượng: 360gr
✔️Calories ước tính là 202 – 225cal/100gr
———————————-
‼️Lưu ý:
✅ Combo sẽ được làm tươi từ xưởng sản xuất, địa chỉ 73 Thảo Điền, Quận 2, HCM. Nên cần đặt trước 1 ngày trong khung giờ từ 7h00 – 21h00. Giao bánh hôm sau từ 9h – 17h, từ T2 – T7 hàng tuần.', 269000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (13, N'Combo Eat Clean & Detox', N'Combo Eat Clean & Detox bao gồm 4 dòng bánh sau:
🍞 Takesumi Whole Wheat Bread – Bánh Mì Tinh Than Tre Nhật Bản. Bánh được làm từ bột tinh than tre hoạt tính Bona Takesumi, bánh gồm có: Bột Tinh Than Tre Nhật Bản, bột mì nguyên cám, bột mè đen. Tỉ lệ nguyên liệu dinh dưỡng, bổ trợ tốt cho quá thải độc tố, làm sạch ruột và ăn kiêng hiệu quả.
✔️Trọng lượng: 400gr
✔️Calories ước tính là 290 – 310 cal/100gr
🥯 Takesumi Burger Buns Whole Wheat – Burger Tinh Than Tre Nhật Bản Nguyên Cám. Bánh được làm từ bột than tre hoạt tính Bona Takesumi được nghiền từ loại tre Mosontake quý hiếm của Nhật Bản, có độ tinh khiết cao và chứa các khoáng chất như kali, canxi, photpho, sắt… Ngoài ra, tre Mosontake có đặc tính xốp, giúp bột Takesumi có khả năng hút khuẩn và loại bỏ chất độc hại tuyệt vời.
✔️ Trọng lượng: 100 gram
✔️ Số lượng: Túi 3 cái
✔️ Calories ước tính là 310 – 330 cal/100gr
🍞 Dark Rye Bread – Bánh Mì Đen Đan Mạch. Bánh mềm, thơm, ít calories, phù hợp cho ăn kiêng.
✔️Trọng lượng: 300gr
✔️Calories ước tính là 259 – 280 cal/100gr
🍞 Nordlander Bread – Bánh Lúa Mạch Đen Đức. Làm từ bột lúa mạch, ít calories, nhiều hạt ngũ cốc dinh dưỡng nên Norlander là chiếc bánh chuyên biệt dành cho chế độ ăn kiêng hiệu quả.
✔️Trọng lượng: 400gr
✔️Calories ước tính là 230 – 250 cal/100gr
*** Lưu ý ***
✅ Combo sẽ được làm tươi từ xưởng sản xuất, địa chỉ 73 Thảo Điền, Quận 2, HCM. Nên cần đặt trước 1 ngày trong khung giờ từ 7h00 – 21h00. Giao bánh hôm sau từ 9h – 17h, từ T2 – T7 hàng tuần.', 275000, N'main.jpg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (14, N'Dark Rye With Caraway Bread', N'Bánh Lúa Mạch Đen Đan Mạch
Được làm từ bột mì đen kết hợp với gia vị thảo dược caraway với nhiều lợi ích dinh dưỡng, bánh mì đen Đan Mạch vốn là một trong những dòng bánh ăn kiêng, hỗ trợ giảm cân tuyệt vời của La Vita Bakery.
Trọng lượng: 300 grams
Hình dáng: Ổ tròn, chia thành 10 lát
Calories ước tính: 320 – 350 calories/100 grams
Đặt trước: 1 ngày để được phục vụ tốt nhất', 60000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (15, N'High Fiber Bread', N'Bánh Mì Giàu Chất Xơ
High Fiber được làm từ bột mì thô giàu chất xơ của Đức. Với hàm lượng chất xơ cao, nhiều hạt dinh dưỡng, ít calories và giúp no lâu nên bánh đặc biệt thích hợp cho người ăn kiêng
Trọng lượng: 360 grams
Hình dáng: Ổ dài, chia thành 12 lát
Calories ước tính: 320 – 350 calories/100 grams
Đặt trước: 1 ngày để được phục vụ tốt nhất', 75000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (16, N'Honey Whole Wheat Bread', N'Bánh Mỳ Mật Ong Nguyên Cám là dòng bánh mỳ nguyên cám chứa nhiều chất dinh dưỡng quý từ mật ong và vỏ cám lúa mỳ. Bánh đặc biệt mềm dai, thơm ngon nên các em bé cực kỳ yêu thích.
Trọng lượng: 400gram
Hình dáng: Ổ dài, chia thành 12 – 13 lát
Đặt trước: 1 ngày để được phục vụ tốt nhất

Cách bảo quản:
– Dùng bánh trong 3 ngày nếu bảo quản kín điều kiện nhiệt độ phòng 18 độ C.
– Dùng bánh trong 1 tháng nếu bảo quản ngăn đông lạnh.', 56000, N'main.jpeg', 2)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (17, N'Cinnamon Cranberry Bagels (3 Pack)', N'Bánh Mỳ Vòng Nam Việt Quất Khô
Chất bánh mềm, thơm mùi quế, có vị ngọt nhẹ từ trái Nam Việt Quất
Trọng lượng: 120 gram/cái
Hình dáng: Ổ tròn hình donut
Số lượng: Túi 3 cái
Đặt trước: 1 ngày để được phục vụ tốt nhất', 81000, N'main.jpeg', 3)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (18, N'MultiGrain Bagels (3 Pack)', N'Bánh Mỳ Vòng Nhiều Loại Hạt
Trọng lượng: 120 gram/cái
Hình dáng: Ổ tròn hình donut
Số lượng: Túi 6 cái
Đặt trước: 1 ngày để được phục vụ tốt nhất', 66000, N'main.jpeg', 3)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (19, N'Pesto Parmesan Bagels (3 Pack)', N'Bánh Mỳ Vòng Thảo Dược
Chất bánh mềm, có vị nguyệt quế tây, phô mai parmesan thơm ngậy
Trọng lượng: 120 gram/cái
Hình dáng: Ổ tròn hình donut
Số lượng: Túi 3 cái
Đặt trước: 1 ngày để được phục vụ tốt nhất', 81000, N'main.jpeg', 3)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (20, N'Rye Caraway Bagels (3 Pack)', N'Bánh Mỳ Vòng Lúa Mạch Đen
Rye Bagel được làm từ bột mì đen, thêm bột mì protein cao và hạt thì là ba tư nổi tiếng. Rye Bagel là phần bánh dinh dưỡng, tốt cho sức khỏe và giúp ích cho quá trình ăn kiêng hiệu quả.
Trọng lượng: 100 gram/cái
Hình dáng: Ổ tròn hình donut
Số lượng: Túi 3 cái
Calories ước tính: 320 – 350 calories/100 grams
Đặt trước: 1 ngày để được phục vụ tốt nhất', 81000, N'main.jpeg', 3)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (21, N'Whole Wheat Multiseed Bagels (3 Pack)', N'Bánh Mỳ Vòng Ngũ Cốc Nguyên Cám
Chất bánh mềm, thơm mùi ngũ cốc, dinh dưỡng từ các loại hạt như: Hạt Lanh, yến mạch, hạt dưa, hạt bí…
Trọng lượng: 120 gram/cái
Hình dáng: Ổ tròn hình donut
Số lượng: Túi 3 cái
Đặt trước: 1 ngày để được phục vụ tốt nhất', 81000, N'main.jpg', 3)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (22, N'Set 20/10 Cupcakes', N'Set Cupcake Ngày Phụ Nữ Việt Nam 20/10
🧁 4 Bánh Cupcake được trang trí theme 20/10, được đóng gói bằng hộp mica như hình', 200000, N'main.jpeg', 4)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (23, N'Set-Of-Four Pink Cupcake', N'Set 4 cái cupcakes chủ đề Valentine

Quà tặng đáng yêu cho mùa Valentine lãng mạn bên người ấy.', 200000, N'main.jpeg', 4)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (24, N'Set-Of-Four Red Love Cupcake', N'Set 4 cái cupcakes chủ đề Valentine

Quà tặng đáng yêu cho mùa Valentine lãng mạn bên người thân yêu nhất!', 200000, N'main.jpeg', 4)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (25, N'Chocolate Cupcake', N'Cupcake với vị Chocolate cổ điển điểm tô với những trái tim nhỏ sẽ giúp bạn có một món tráng miệng tuyệt vời.', 40000, N'main.jpg', 4)
INSERT [dbo].[CAKE] ([CakeID], [CakeName], [Description], [Price], [Image], [CategoryID]) VALUES (26, N'Red Velvet Cupcake', N'Những chiếc cupcake nhỏ xinh sẽ giúp bạn và người ấy gắn kết nhau hơn.', 40000, N'main.jpg', 4)
GO
INSERT [dbo].[CATEGORY] ([CatID], [CatName]) VALUES (-1, N'Tất cả | All')
INSERT [dbo].[CATEGORY] ([CatID], [CatName]) VALUES (1, N'Bánh kem | Cake')
INSERT [dbo].[CATEGORY] ([CatID], [CatName]) VALUES (2, N'Bánh mì | Bread')
INSERT [dbo].[CATEGORY] ([CatID], [CatName]) VALUES (3, N'Bánh mì vòng | Bagels')
INSERT [dbo].[CATEGORY] ([CatID], [CatName]) VALUES (4, N'Bánh nướng nhỏ | Cupcake')
GO
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (1, N'Bò ở đây không phải là danh từ mà là động từ. Theo cách giải thích của nhiều người truyền lại là trong quá trình ủ bột với men, bột sẽ nở ra, tự động "bò" lên vành tô nên mới có tên gọi độc đáo như vậy.')
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (2, N'Bánh bông lan :
Loại bánh mền xốp là thơm phưng phức không có gì quá xa lạ đối với chúng ta. Nhưng ít người biết được là loại bánh này có xuất xứ từ Pháp. Trước đây bánh thường được pha thêm hương vani, vani được chiết hương từ một loại phong lan nên người Việt gọi là bánh bông lan.
Còn tại sao lại là bông lan chứ không phải hoa lan? Có lẽ bởi loại bánh được người Pháp phổ biến với người miền Nam trước, người miền Nam hay gọi hoa là bông nên cái tên gắn với cách gọi của người miền Nam đã trở thành tên chung cho bánh.')
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (3, N'Baumkuchen  :
Baumkuchen là một chiếc bánh đặc biệt cả về hình dáng lẫn tên gọi. "Baum" trong tiếng Đức có nghĩa là "cái cây", và chiếc bánh có hình vòng tròn, có lỗ ở giữa, tượng trưng cho những vân gỗ của cây.
Công thức làm bánh Baumkuchen lần đầu tiên xuất hiện trong quyển sách nấu ăn Ein Neues Kochbuch của Marx Rumpolt, là quyển sách nấu ăn dành cho đầu bếp chuyên nghiệp đầu tiên được xuất bản. Marx Rumpolt trước đó đã làm đầu bếp ở Hungary và Bohemia, và chuyện kể rằng bánh Baumkuchen có xuất xứ là chiếc bánh được làm trong các đám cưới ở Hungary, và là "con cháu" của bánh ống khói Kürtőskalács, chiếc bánh truyền thống của Hungary.')
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (4, N'Schwarzwälder :
Bánh Schwarzwälder "thứ thiệt" bao gồm nhiều lớp bánh bông lan chocolate xen giữa các lớp kem tươi trộn với anh đào. Bánh sau đó được phủ một lớp kem tươi lên trên, rồi trang trí bằng quả anh đào đen và chocolate bào vụn. Rượu brandy anh đào là một nguyên liệu bắt buộc khi làm bánh Schwarzwälder, nếu không có nguyên liệu này, chiếc bánh sẽ không được phép mang tên Schwarzwälder Kirschtorte khi bán.')
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (5, N'Bienenstich :
Để lý giải cho cái tên Bienenstich hài hước, có một truyền thuyết kể lại rằng, một chú ong đã bị hấp dẫn bởi chiếc bánh và bay vào đốt người thợ khi ông đang làm bánh, từ đó, chiếc bánh này được mang cái tên "bánh ong đốt". Chiếc bánh này được xem như một trong số những chiếc bánh cổ điển nhất của mảng ẩm thực ngọt Đức.')
INSERT [dbo].[INFO] ([InfoID], [FullDes]) VALUES (6, N'Devil Food Cake :
Devil Food Cake được ví như chiếc bánh “bộ đôi hoàn hảo” với Angle Food Cake đầy thú vị và hài hước trong thế giới bánh ngọt. Nếu như món bánh đồng hành mang màu trắng ngà tinh khiết thì món bánh “ác quỷ” lại được làm với tông màu đen pha chút đỏ thẫm đầy chủ ý. Bánh có kết cấu nhiều lớp chocolate xếp chồng lên nhau, và phần nhân kem tươi trắng ngần hay xốt chocolate tan chảy. Với hương vị đậm đà từ chocolate qua gam màu đen đỏ đã tạo nên sự tương phản đặc biệt với món bánh “thiên thần” trắng như bông.')
GO
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (1, 1, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (1, 4, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (1, 12, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (2, 20, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (2, 25, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (3, 2, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (4, 3, 5)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (4, 9, 5)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (4, 25, 5)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (5, 5, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (5, 7, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (6, 13, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (6, 15, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (6, 22, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (7, 2, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (8, 12, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (8, 26, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (9, 6, 20)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (9, 19, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (10, 22, 10)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (11, 2, 2)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (11, 3, 2)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (12, 3, 2)
INSERT [dbo].[ORDER_DETAIL] ([OrderID], [CakeID], [Quantity]) VALUES (12, 4, 2)
GO
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (1, N'Đỗ Ðông Nguyên', N'84937426460', N'410 Nguyen Dinh Chieu, Ward 4, Dist.3, Ho Chi Minh', CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (2, N'Huỳnh Hải Quân', N'84918188890', N'164/22 Le Dinh Tham Str., Ho Chi Minh', CAST(N'2020-02-02T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (3, N'Lưu Kim Phú', N'84918414666', N'5/9 Ap Tay Vinh Phu Thuan An Binh Duong, Ho Chi Minh', CAST(N'2020-03-03T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (4, N'Mạch Thiện Luân', N'84918770546', N'67 / 140 Bui Dinh Tuy Street, Ward12, Binh Thanh Dist, Ho Chi Minh', CAST(N'2020-04-04T00:00:00.0000000' AS DateTime2), 3)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (5, N'Lê Tấn Lợi', N'84919955975', N'376 / 24 Nguyen Dinh Chieu Street. District 3, Ho Chi Minh', CAST(N'2020-05-05T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (6, N'Nguyễn Thảo Uyên', N'84903944056', N'10/1A Tan Quy St., Ho Chi Minh', CAST(N'2020-06-06T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (7, N'Dương Vân Trinh', N'84392279355', N'65/18B Nguyen Văn Luong, Ward 10, Dist.6', CAST(N'2020-07-07T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (8, N'La Hoa Thiên', N'84121730312', N'482/3 Le Quang Dinh Street, Ward 11', CAST(N'2020-08-08T00:00:00.0000000' AS DateTime2), 3)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (9, N'Nghiêm Mai Thanh', N'84121459565', N' 56/26 Thich Quang Duc Street, Ward 5', CAST(N'2020-09-09T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (10, N'Đỗ Trường Thành', N'84121890743', N'57 Nguyen Hue Street, Ben Nghe Ward, District 1', CAST(N'2020-10-10T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (11, N'test', N'02301892309', N'211/140 something', CAST(N'2020-12-30T16:57:49.2207344' AS DateTime2), 2)
INSERT [dbo].[ORDERS] ([OrderID], [CustomerName], [PhoneNumber], [HomeAddress], [CreatedAt], [PaymentType]) VALUES (12, N'adfadf', N'12312313', N'adfadf', CAST(N'2020-12-30T17:00:14.1903959' AS DateTime2), 2)
GO
INSERT [dbo].[PAYMENT] ([PaymentType], [PaymentMethod], [IsShipping]) VALUES (0, N'Mua tại cửa hàng - Tiền mặt', 0)
INSERT [dbo].[PAYMENT] ([PaymentType], [PaymentMethod], [IsShipping]) VALUES (1, N'Mua tại cửa hàng - Thẻ', 0)
INSERT [dbo].[PAYMENT] ([PaymentType], [PaymentMethod], [IsShipping]) VALUES (2, N'Giao tận nơi - tiền mặt', 1)
INSERT [dbo].[PAYMENT] ([PaymentType], [PaymentMethod], [IsShipping]) VALUES (3, N'Giao tận nơi - thẻ', 1)
GO
ALTER TABLE [dbo].[CAKE]  WITH CHECK ADD  CONSTRAINT [FK_CAKE_CATEGORY] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[CATEGORY] ([CatID])
GO
ALTER TABLE [dbo].[CAKE] CHECK CONSTRAINT [FK_CAKE_CATEGORY]
GO
ALTER TABLE [dbo].[ORDER_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_DETAIL_CAKE] FOREIGN KEY([CakeID])
REFERENCES [dbo].[CAKE] ([CakeID])
GO
ALTER TABLE [dbo].[ORDER_DETAIL] CHECK CONSTRAINT [FK_ORDER_DETAIL_CAKE]
GO
ALTER TABLE [dbo].[ORDER_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_DETAIL_ORDERS] FOREIGN KEY([OrderID])
REFERENCES [dbo].[ORDERS] ([OrderID])
GO
ALTER TABLE [dbo].[ORDER_DETAIL] CHECK CONSTRAINT [FK_ORDER_DETAIL_ORDERS]
GO
ALTER TABLE [dbo].[ORDERS]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_PAYMENT] FOREIGN KEY([PaymentType])
REFERENCES [dbo].[PAYMENT] ([PaymentType])
GO
ALTER TABLE [dbo].[ORDERS] CHECK CONSTRAINT [FK_ORDERS_PAYMENT]
GO
USE [master]
GO
ALTER DATABASE [WP_Project3_CakeShopApp] SET  READ_WRITE 
GO
