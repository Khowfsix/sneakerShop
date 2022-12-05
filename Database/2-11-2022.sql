USE [sneakerShop]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

--================================================================================================================================================================================================================================================================
USE [sneakerShop]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cart](
	[cartId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [nvarchar](128) NOT NULL,
	[buyDate] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[cartId] [int] NOT NULL,
	[productId] [int] NOT NULL,
	[quantity] [int] NULL,
	[unitPrice] [float] NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC,
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](200) NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[imagesProduct]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[imagesProduct](
	[productId] [int] NOT NULL,
	[images] [varchar](255) NOT NULL,
 CONSTRAINT [PK_imagesProduce] PRIMARY KEY CLUSTERED 
(
	[productId] ASC,
	[images] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [nvarchar](128) NOT NULL,
	[cartID] [int] NOT NULL,
	[orderDate] [date] NULL,
	[status] [tinyint] NULL,
	[shipping] [tinyint] NULL,
	[totalPay] [bigint] NULL,
	[paymentType] [int] NULL,
	[address] [nvarchar](500) NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paymentType]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paymentType](
	[paymentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[paymentTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_pmType] PRIMARY KEY CLUSTERED 
(
	[paymentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [nvarchar](200) NULL,
	[categoryId] [int] NULL,
	[description] [nvarchar](500) NULL,
	[price] [float] NULL,
	[amount] [int] NULL,
	[status] [tinyint] NULL,
	[createDate] [date] NULL,
	[updateDate] [date] NULL,
	[sex] [nvarchar](10) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipment](
	[shipmentID] [int] IDENTITY(1,1) NOT NULL,
	[shipperID] [nvarchar](128) NOT NULL,
	[orderID] [int] NOT NULL,
 CONSTRAINT [PK_shipment] PRIMARY KEY CLUSTERED 
(
	[shipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 10/24/2022 5:17:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[stockID] [int] IDENTITY(1,1) NOT NULL,
	[productId] [int] NOT NULL,
	[size] [int] NOT NULL,
	[inStock] [int] NOT NULL,
	[lastUpdate] [date] NULL,
 CONSTRAINT [PK_ProductInStock] PRIMARY KEY CLUSTERED 
(
	[stockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
--/****** Object:  Table [dbo].[UserRoles]    Script Date: 10/24/2022 5:17:32 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[UserRoles](
--	[roleId] [int] IDENTITY(1,1) NOT NULL,
--	[roleName] [nvarchar](50) NULL,
-- CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
--(
--	[roleId] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]
--GO
--/****** Object:  Table [dbo].[Users]    Script Date: 10/24/2022 5:17:32 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Users](
--	[userId] [int] IDENTITY(1,1) NOT NULL,
--	[username] [nvarchar](50) NULL,
--	[email] [nvarchar](100) NULL,
--	[fullname] [nvarchar](50) NULL,
--	[password] [nvarchar](50) NULL,
--	[images] [nvarchar](500) NULL,
--	[phone] [nvarchar](20) NULL,
--	[status] [int] NULL,
--	[roleId] [int] NULL,
--	[defaultAddress] [nvarchar](500) NULL,
--	[paypalNumber] [varchar](50) NULL,
-- CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
--(
--	[userId] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]
--GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

--INSERT [dbo].[Cart] ([cartId], [userId], [buyDate], [status]) VALUES (1, 2, CAST(N'2022-10-14T00:00:00.000' AS DateTime), 1)
--INSERT [dbo].[Cart] ([cartId], [userId], [buyDate], [status]) VALUES (2, 5, CAST(N'2022-10-16T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
--INSERT [dbo].[CartItem] ([cartId], [productId], [quantity], [unitPrice]) VALUES (1, 1, 2, 1480000)
--INSERT [dbo].[CartItem] ([cartId], [productId], [quantity], [unitPrice]) VALUES (1, 2, 5, 2980000)
--INSERT [dbo].[CartItem] ([cartId], [productId], [quantity], [unitPrice]) VALUES (1, 31, 10, 25000000)
--INSERT [dbo].[CartItem] ([cartId], [productId], [quantity], [unitPrice]) VALUES (2, 1, 20, 1480000)
--GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (1, N'Adidas', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (2, N'Nike', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (3, N'MLB', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (4, N'Puma', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (5, N'Skechers', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (6, N'Reebok', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (7, N'Converse', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (8, N'Vans', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (9, N'New Balance', 1)
INSERT [dbo].[Category] ([categoryId], [categoryName], [status]) VALUES (10, N'Balenciaga', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (1, N'1f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (2, N'2f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (3, N'3g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (4, N'4h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (5, N'5h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (6, N'6h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (7, N'7a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (7, N'7b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (7, N'7c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (7, N'7d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (8, N'8f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (9, N'9f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (10, N'10h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (11, N'11f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (12, N'12h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (14, N'14a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (14, N'14b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (14, N'14c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (14, N'14d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (14, N'14e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (15, N'15a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (15, N'15b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (15, N'15c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (15, N'15d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (15, N'15e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (16, N'16a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (16, N'16b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (16, N'16c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (16, N'16d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17f.jpg')
GO
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17g.jph')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (17, N'17h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (18, N'18h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (19, N'19h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (20, N'20h.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (21, N'21a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (21, N'21b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (21, N'21c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (21, N'21d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (21, N'21e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (22, N'22f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (23, N'23a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (23, N'23b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (23, N'23c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (23, N'23d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (23, N'23e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (24, N'24a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (24, N'24b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (24, N'24c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (24, N'24d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (25, N'25a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (25, N'25b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (25, N'25c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (26, N'26g.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (27, N'27a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (27, N'27b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (27, N'27c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (27, N'27d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (27, N'27e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (28, N'28f.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (29, N'29a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (29, N'29b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (29, N'29c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (29, N'29d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (30, N'30a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (30, N'30b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (30, N'30c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (30, N'30d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (31, N'31a.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (31, N'31b.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (31, N'31c.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (31, N'31d.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (31, N'31e.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (32, N'demo.jpg')
INSERT [dbo].[imagesProduct] ([productId], [images]) VALUES (33, N'demo.jpg')
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

--INSERT [dbo].[Order] ([orderID], [userID], [cartID], [orderDate], [status], [shipping], [totalPay], [paymentType], [address]) VALUES (1, 2, 1, CAST(N'2022-10-14' AS Date), 0, NULL, 100000, 1, N'10 Han Thuyen, Binh Tho, Thu Duc')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[paymentType] ON 

INSERT [dbo].[paymentType] ([paymentTypeID], [paymentTypeName]) VALUES (1, N'Thanh toán khi nhận hàng')
INSERT [dbo].[paymentType] ([paymentTypeID], [paymentTypeName]) VALUES (2, N'Chuyển khoảng ngân hàng')
INSERT [dbo].[paymentType] ([paymentTypeID], [paymentTypeName]) VALUES (3, N'Internet Banking')
INSERT [dbo].[paymentType] ([paymentTypeID], [paymentTypeName]) VALUES (4, N'Ví điện tử')
SET IDENTITY_INSERT [dbo].[paymentType] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (1, N'Giày Adidas Response Super 3.0', 1, N'Giày Adidas Response Super 3.0', 1480000, 2, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (2, N'Giày Adidas Ultraboost 5.0 DNA GV8750', 1, N'Giày Adidas Ultraboost 5.0 DNA GV8750', 2980000, 5, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (3, N'Giày Adidas Ultraboost 5.0 DNA GV8746', 1, N'Giày Adidas Ultraboost 5.0 DNA GV8746', 2980000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (4, N'Giày Nike Air Zoom Pegasus 39 "Summit White" Dh4072-501', 2, N'Giày Nike Air Zoom Pegasus 39 "Summit White" Dh4072-501', 2650000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (5, N'Giày Nike Air Zoom Pegasus 39 "Iris Whisper" Dh4072-500', 2, N'Giày Nike Air Zoom Pegasus 39 "Iris Whisper" Dh4072-500', 2650000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (6, N'Giày Nike Air Zoom Pegasus 39 "Cargo Khaki" Dh4071-300', 2, N'Giày Nike Air Zoom Pegasus 39 "Cargo Khaki" Dh4071-300', 2650000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (7, N'Giày MLB Chunky Liner Boston Red Sox 3ASXCA12N', 3, N'Giày MLB Chunky Liner Boston Red Sox 3ASXCA12N', 3350000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (8, N'Giày MLB Playball Mule Monogram New York Yankees 3AMUM212N-50BGD', 3, N'Giày MLB Playball Mule Monogram New York Yankees 3AMUM212N-50BGD', 1770000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (9, N'Giày MLB Chunky Liner High New York Yankees White Black 3ASXCB12N-50IVS', 3, N'Giày MLB Chunky Liner High New York Yankees White Black 3ASXCB12N-50IVS', 3350000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (10, N'Giày Nữ Puma Deviate Nitro 194453-13', 4, N'Giày Nữ Puma Deviate Nitro 194453-13', 3999000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nữ')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (11, N'Giày Nam Puma Blaze Of Glory 383532-01', 4, N'Giày Nam Puma Blaze Of Glory 383532-01', 3499000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (12, N'Giày Nam Puma Voyage Nitro 195504-08', 4, N'Giày Nam Puma Voyage Nitro 195504-08', 3499000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (14, N'Giày Nam Skechers 246001-BURG', 5, N'Giày Nam Skechers 246001-BURG', 2472000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (15, N'Giày Nam Skechers 246001-GYBK', 5, N'Giày Nam Skechers 246001-GYBK', 2472000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (16, N'Giày Nam Skechers One Piece Casual Monster 894037', 5, N'Giày Nam Skechers One Piece Casual Monster 894037', 2152000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (17, N'Giày Nam Reebok Club C Revenge Vintage FW4862', 6, N'Giày Nam Reebok Club C Revenge Vintage FW4862', 2796000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (18, N'Giày Reebok Zig GX0504', 6, N'Giày Reebok Zig GX0504', 2796000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (19, N'Giày Nữ Reebok Zig GX6239', 6, N'Giày Nữ Reebok Zig GX6239', 2796000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nữ')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (20, N'Giày Converse Run Star Hi ‘Black’ 166800C', 7, N'Giày Converse Run Star Hi ‘Black’ 166800C', 4490000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (21, N'Giày nam Converse Chuck 70 Low ‘Flames’ 165029C', 7, N'Giày nam Converse Chuck 70 Low ‘Flames’ 165029C', 2290000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (22, N'Giày Converse UNT1TL3D High GS ‘Not A Chuck – University Blue’ 272575C', 7, N'Giày Converse UNT1TL3D High GS ‘Not A Chuck – University Blue’ 272575C', 1790000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (23, N'Vans Slip on Pro Checkerboard Black White', 8, N'Vans Slip on Pro Checkerboard Black White', 1470000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (24, N'Vans Style 36 Marshmallow Red VN', 8, N'Vans Style 36 Marshmallow Red VN', 1650000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (25, N'Vans Slip On Classic Checkerboard Golden Brown', 8, N'Vans Slip On Classic Checkerboard Golden Brown', 1160000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (26, N'Giày Nam New Balance Fresh Foam X 880V12', 9, N'Giày Nam New Balance Fresh Foam X 880V12', 2306500, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (27, N'Giày Nam New Balance Fuelcell Rc Elite V2 Racing', 9, N'Giày Nam New Balance Fuelcell Rc Elite V2 Racing', 2196500, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (28, N'Giày Nam New Balance 57/40 Classic Lifestyle', 9, N'Giày Nam New Balance 57/40 Classic Lifestyle', 1676500, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Nam')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (29, N'Giày Balenciaga Track Trainer Black Red 542023-W1GB6-1002', 10, N'Giày Balenciaga Track Trainer Black Red 542023-W1GB6-1002', 24800000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (30, N'Giày Balenciaga Wmns Speed Trainer ''Pink'' 593698 W0682 5961', 10, N'Giày Balenciaga Wmns Speed Trainer ''Pink'' 593698 W0682 5961', 19000000, 0, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (31, N'Giày Balenciaga Triple S White Black Red 2019 524037 W09E1 9000', 10, N'Giày Balenciaga Triple S White Black Red 2019 524037 W09E1 9000', 25000000, 10, 1, CAST(N'2022-10-14' AS Date), CAST(N'2022-10-14' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (32, N'Giay Demo1', 10, N'Giay Demo', 10, 0, 1, CAST(N'2022-10-16' AS Date), CAST(N'2022-10-16' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (33, N'Giay Demo2', 10, N'Giay Demo', 10, 0, 1, CAST(N'2022-10-16' AS Date), CAST(N'2022-10-16' AS Date), N'Unisex')
INSERT [dbo].[Product] ([productId], [productName], [categoryId], [description], [price], [amount], [status], [createDate], [updateDate], [sex]) VALUES (34, N'Giay Demo3', 10, N'Giay Demo', 10, 0, 1, CAST(N'2022-10-17' AS Date), CAST(N'2022-10-17' AS Date), N'Unisex')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Shipment] ON 

--INSERT [dbo].[Shipment] ([shipmentID], [shipperID], [orderID]) VALUES (1, 4, 1)
SET IDENTITY_INSERT [dbo].[Shipment] OFF
GO

--SET IDENTITY_INSERT [dbo].[Stock] ON 
GO
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 37, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 38, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 39, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 40, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 41, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (1, 42, 98, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 37, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 38, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 39, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 40, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 41, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (2, 42, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 37, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 38, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 39, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 40, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 41, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (3, 42, 100, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 37, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 38, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 39, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 40, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 41, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (4, 42, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 37, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 38, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 39, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 40, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 41, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (5, 42, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 37, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 38, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 39, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 40, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 41, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (6, 42, 95, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 37, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 38, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 39, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 40, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 41, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (7, 42, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 37, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 38, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 39, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 40, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 41, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (8, 42, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 37, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 38, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 39, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 40, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 41, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (9, 42, 90, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 34, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 35, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 36, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 37, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 38, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (10, 39, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 39, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 40, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 41, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 42, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 43, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (11, 44, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 39, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 40, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 41, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 42, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 43, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (12, 44, 85, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 39, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 40, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 41, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 42, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 43, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (14, 44, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 39, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 40, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 41, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 42, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 43, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (15, 44, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 39, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 40, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 41, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 42, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 43, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (16, 44, 80, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 39, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 40, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 41, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 42, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 43, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (17, 44, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 37, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 38, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 39, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 40, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 41, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (18, 42, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 34, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 35, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 36, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 37, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 38, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (19, 39, 75, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 37, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 38, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 39, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 40, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 41, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (20, 42, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 39, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 40, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 41, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 42, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 43, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (21, 44, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 37, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 38, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 39, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 40, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 41, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (22, 42, 70, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 37, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 38, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 39, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 40, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 41, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (23, 42, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 37, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 38, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 39, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 40, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 41, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (24, 42, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 37, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 38, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 39, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 40, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 41, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (25, 42, 65, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 39, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 40, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 41, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 42, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 43, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (26, 44, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 39, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 40, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 41, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 42, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 43, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (27, 44, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 39, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 40, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 41, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 42, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 43, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (28, 44, 60, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 37, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 38, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 39, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 40, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 41, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (29, 42, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 37, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 38, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 39, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 40, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 41, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (30, 42, 55, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 37, 45, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 38, 45, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 39, 45, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 40, 45, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 41, 45, CAST(N'2022-10-15' AS Date))
INSERT [dbo].[Stock] ([productId], [size], [inStock], [lastUpdate]) VALUES (31, 42, 45, CAST(N'2022-10-15' AS Date))
GO

INSERT INTO [dbo].[AspNetRoles]
VALUES
(
    1, 'Admin'
),
(
    2, 'User'
)
GO

SET IDENTITY_INSERT [dbo].[Stock] OFF 
GO
--SET IDENTITY_INSERT [dbo].[UserRoles] ON 

--INSERT [dbo].[UserRoles] ([roleId], [roleName]) VALUES (1, N'Admin')
--INSERT [dbo].[UserRoles] ([roleId], [roleName]) VALUES (2, N'Seller')
--INSERT [dbo].[UserRoles] ([roleId], [roleName]) VALUES (3, N'Customer')
--INSERT [dbo].[UserRoles] ([roleId], [roleName]) VALUES (4, N'Shipper')
--SET IDENTITY_INSERT [dbo].[UserRoles] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Users] ON 

--INSERT [dbo].[Users] ([userId], [username], [email], [fullname], [password], [images], [phone], [status], [roleId], [defaultAddress], [paypalNumber]) VALUES (1, N'Admin1', N'admin1@gmail.com', N'admin', N'admin123', N'admin.jpg', N'1111111111', 1, 1, N'1 Vo Van Ngan', NULL)
--INSERT [dbo].[Users] ([userId], [username], [email], [fullname], [password], [images], [phone], [status], [roleId], [defaultAddress], [paypalNumber]) VALUES (2, N'Customer1', N'customer1@gmail.com', N'customer1', N'customer123', N'user.jpg', N'3333333333', 1, 3, N'2 Le Van Chi', NULL)
--INSERT [dbo].[Users] ([userId], [username], [email], [fullname], [password], [images], [phone], [status], [roleId], [defaultAddress], [paypalNumber]) VALUES (3, N'Seller1', N'seller1@gmail.com', N'seller1', N'seller123', N'user.jpg', N'2222222222', 1, 2, N'3 Le Van Viet', NULL)
--INSERT [dbo].[Users] ([userId], [username], [email], [fullname], [password], [images], [phone], [status], [roleId], [defaultAddress], [paypalNumber]) VALUES (4, N'Shipper1', N'shipper1@gmail.com', N'shipper1', N'shipper1', N'user.jpg', N'4444444444', 1, 4, N'4 Dang Van Bi', NULL)
--INSERT [dbo].[Users] ([userId], [username], [email], [fullname], [password], [images], [phone], [status], [roleId], [defaultAddress], [paypalNumber]) VALUES (5, N'Customer2', N'customer2@gmail.com', N'customer2', N'customer123', N'user.jpg', N'3333333333', 1, 3, N'5 Thong Nhat', NULL)
--SET IDENTITY_INSERT [dbo].[Users] OFF
--GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[CartItem] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [createDate]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT (getdate()) FOR [lastUpdate]
GO
--ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [status]
--GO
--ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [roleId]
--GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_cartUsers] FOREIGN KEY([userId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_cartUsers]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_cartitemCart] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([cartId])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_cartitemCart]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_cartitemProduct] FOREIGN KEY([productId])
REFERENCES [dbo].[Stock] ([stockID])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_cartitemProduct]
GO
ALTER TABLE [dbo].[imagesProduct]  WITH CHECK ADD  CONSTRAINT [FK_images] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([productId])
GO
ALTER TABLE [dbo].[imagesProduct] CHECK CONSTRAINT [FK_images]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_orderCart] FOREIGN KEY([cartID])
REFERENCES [dbo].[Cart] ([cartId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_orderCart]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_orderPaymentType] FOREIGN KEY([paymentType])
REFERENCES [dbo].[paymentType] ([paymentTypeID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_orderPaymentType]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_orderUser] FOREIGN KEY([userID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_orderUser]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([categoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_category]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_shipOrder] FOREIGN KEY([orderID])
REFERENCES [dbo].[Order] ([orderID])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_shipOrder]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_shipper] FOREIGN KEY([shipperID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_shipper]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_stock] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([productId])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_stock]
GO
--ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_userrole] FOREIGN KEY([roleId])
--REFERENCES [dbo].[UserRoles] ([roleId])
--GO
--ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_userrole]
--GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD CHECK  (([inStock]>=(0)))
GO