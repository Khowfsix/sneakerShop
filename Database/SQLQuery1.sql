USE [master];
GO
-- Create the new database if it does not exist already
IF NOT EXISTS
(
    SELECT [databases].[name]
    FROM [sys].[databases]
    WHERE [databases].[name] = N'sneakerShop'
)
    CREATE DATABASE [sneakerShop];
GO

USE [sneakerShop];
GO

CREATE TABLE [dbo].[UserRoles]
(
    [roleId] [INT] IDENTITY(1, 1) NOT NULL,
    [roleName] [NVARCHAR](50) NULL,
    CONSTRAINT [PK_UserRoles]
        PRIMARY KEY CLUSTERED ([roleId] ASC)
) ON [PRIMARY];
GO

CREATE TABLE [dbo].[Users]
(
    [userId] [INT] IDENTITY(1, 1) NOT NULL,
    [userName] [NVARCHAR](50) NULL,
    [email] [NVARCHAR](100) NULL,
    [fullname] [NVARCHAR](50) NULL,
    [password] [NVARCHAR](50) NULL,
    [images] [NVARCHAR](500) NULL,
    [phone] [NVARCHAR](20) NULL,
    [status] [INT] NULL,
    [roleId] [INT] NULL,
	defaultAddress NVARCHAR(500) NULL,
	paypalNumber VARCHAR(50) NULL

    CONSTRAINT [PK_Users]
        PRIMARY KEY CLUSTERED ([userId] ASC)
);
GO

IF OBJECT_ID('[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
CREATE TABLE [dbo].[Category]
(
    [categoryId] [INT] IDENTITY(1, 1) NOT NULL,
    [categoryName] [NVARCHAR](200) NULL,
    [status] [INT] NULL,
    CONSTRAINT [PK_Category]
        PRIMARY KEY ([categoryId])
);
GO

IF OBJECT_ID('[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
CREATE TABLE [dbo].[Product]
(
    [productId] [INT] IDENTITY(1, 1) NOT NULL,
    [productName] [NVARCHAR](200) NULL,
    [categoryId] [INT] NULL,
    [description] [NVARCHAR](500) NULL,
    [price] [FLOAT] NULL,
	[amount] INT NULL, --số sản phẩm đã bán được
    [status] TINYINT NULL,
    [createDate] [DATE] NULL,
	updateDate DATE,

    CONSTRAINT [PK_Product]
        PRIMARY KEY CLUSTERED ([productId] ASC)
)
GO

CREATE TABLE Stock
(
	[productId] [INT] NOT NULL,
	inStock INT NOT NULL,
	lastUpdate DATE NULL,

	CONSTRAINT [PK_ProductInStock]
        PRIMARY KEY CLUSTERED ([productId] ASC)
)

CREATE TABLE imagesProduct
(
	[productId] [INT] NOT NULL,
	[images] [NVARCHAR](500) NOT NULL,

	CONSTRAINT [PK_imagesProduce] PRIMARY KEY([productId],[images])
)
GO

IF OBJECT_ID('[dbo].[Cart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cart];
GO
CREATE TABLE [dbo].[Cart]
(
    [cartId] [NVARCHAR](50) NOT NULL,
    [userId] [INT] NULL,
    [buyDate] [DATETIME] NULL,
    [status] [INT] NULL,
    CONSTRAINT [PK_cart]
        PRIMARY KEY ([cartId])
);
GO