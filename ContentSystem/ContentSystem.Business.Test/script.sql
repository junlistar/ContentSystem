USE [master]
GO
/****** Object:  Database [FruitOrder]    Script Date: 05/10/2018 16:11:04 ******/
CREATE DATABASE [FruitOrder] ON  PRIMARY 
( NAME = N'FruitOrder', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FruitOrder.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FruitOrder_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FruitOrder_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FruitOrder] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FruitOrder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FruitOrder] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FruitOrder] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FruitOrder] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FruitOrder] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FruitOrder] SET ARITHABORT OFF
GO
ALTER DATABASE [FruitOrder] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FruitOrder] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FruitOrder] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FruitOrder] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FruitOrder] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FruitOrder] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FruitOrder] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FruitOrder] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FruitOrder] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FruitOrder] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FruitOrder] SET  DISABLE_BROKER
GO
ALTER DATABASE [FruitOrder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FruitOrder] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FruitOrder] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FruitOrder] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FruitOrder] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FruitOrder] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FruitOrder] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FruitOrder] SET  READ_WRITE
GO
ALTER DATABASE [FruitOrder] SET RECOVERY FULL
GO
ALTER DATABASE [FruitOrder] SET  MULTI_USER
GO
ALTER DATABASE [FruitOrder] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FruitOrder] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'FruitOrder', N'ON'
GO
USE [FruitOrder]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 05/10/2018 16:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Fans_id] [int] NULL,
	[Fans_weixin_openid] [nvarchar](255) NULL,
	[NickName] [nvarchar](50) NULL,
	[Avatar] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 05/10/2018 16:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Tid] [nvarchar](50) NOT NULL,
	[Oid] [int] NULL,
	[Outer_sku_id] [nvarchar](50) NULL,
	[Outer_item_id] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[sku_id] int not null,
	[item_id] int not null,
	[Price] [decimal](18, 2) NULL,
	[Total_fee] [decimal](18, 2) NULL,
	[Num] [int] NULL,
	[Wx_no] [nvarchar](50) null,
	[Taboo] [nvarchar](50) null,
	
	
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 05/10/2018 16:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Title] [nvarchar](255) NULL,
	[Tid] [nvarchar](50) NOT NULL,
	[Total_fee] [decimal](18, 2) NULL,
	[Pic_thumb_path] [ntext] NULL,
	[Status_str] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[Fans_weixin_openid] [nvarchar](255) NULL,
	[Payment] [decimal](18, 2) NULL,
	[Pay_time] [nvarchar](50) NULL,
	[Remarks] [ntext] NULL,
	[Buyer_message] [ntext] NULL,
	[Shipping_type] [nvarchar](50) NULL,
	[Receiver_state] [nvarchar](50) NULL,
	[Receiver_city] [nvarchar](50) NULL,
	[Receiver_district] [nvarchar](50) NULL,
	[Receiver_address] [nvarchar](255) NULL,
	[Receiver_mobile] [nvarchar](50) NULL,
	[Fetcher_name] [nvarchar](50) NULL,
	[Fetcher_mobile] [nvarchar](50) NULL,
	[Fetch_time] [nvarchar](50) NULL,
	[Shop_id] [int] NULL,
	[Shop_name] [nvarchar](50) NULL,
	[Shop_mobile] [nvarchar](50) NULL,
	[Shop_state] [nvarchar](50) NULL,
	[Shop_city] [nvarchar](50) NULL,
	[Shop_district] [nvarchar](50) NULL,
	[Shop_address] [nvarchar](255) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 05/10/2018 16:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfig](
	[Title] [nvarchar](50) NULL,
	[Val] [nvarchar](255) NULL,
	[Remarks] [nvarchar](50) NULL
) ON [PRIMARY]
GO
