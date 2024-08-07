USE [master]
GO
/****** Object:  Database [FoodOrderDb]    Script Date: 30/07/2024 16:43:37 ******/
CREATE DATABASE [FoodOrderDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FoodOrderDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FoodOrderDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FoodOrderDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FoodOrderDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FoodOrderDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodOrderDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodOrderDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FoodOrderDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FoodOrderDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FoodOrderDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FoodOrderDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [FoodOrderDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FoodOrderDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FoodOrderDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FoodOrderDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FoodOrderDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FoodOrderDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FoodOrderDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FoodOrderDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FoodOrderDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FoodOrderDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FoodOrderDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FoodOrderDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FoodOrderDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FoodOrderDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FoodOrderDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FoodOrderDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FoodOrderDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FoodOrderDb] SET RECOVERY FULL 
GO
ALTER DATABASE [FoodOrderDb] SET  MULTI_USER 
GO
ALTER DATABASE [FoodOrderDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FoodOrderDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FoodOrderDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FoodOrderDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FoodOrderDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FoodOrderDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FoodOrderDb', N'ON'
GO
ALTER DATABASE [FoodOrderDb] SET QUERY_STORE = OFF
GO
USE [FoodOrderDb]
GO
/****** Object:  User [app1]    Script Date: 30/07/2024 16:43:38 ******/
CREATE USER [app1] FOR LOGIN [app1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [app1]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 30/07/2024 16:43:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[FoodId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NULL,
	[Price] [decimal](18, 0) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 30/07/2024 16:43:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[IsPaid] [int] NULL,
	[CustomerName] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 30/07/2024 16:43:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NULL,
	[FoodId] [int] NULL,
	[Qty] [int] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Pecel Ayam', CAST(25000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Nasi Goreng', CAST(26000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'Mie Goreng', CAST(25000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'Kwetiau Goreng', CAST(25000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'Bihun Goreng', CAST(25000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'Teh Manis Hangat', CAST(5000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'Teh Tawar Hangat', CAST(5000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'Es Teh Manis', CAST(5000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'Es Lemon Tea', CAST(5000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'Lemon Tea Hangat', CAST(5000 AS Decimal(18, 0)), N'Admin', CAST(N'2024-07-29T14:00:37.837' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
USE [master]
GO
ALTER DATABASE [FoodOrderDb] SET  READ_WRITE 
GO
