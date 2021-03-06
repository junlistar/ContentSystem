USE [FruitOrder]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserInfoId] int identity(1000,1),
	[Fans_id] [nvarchar](50) NOT NULL,
	[Fans_weixin_openid] [nvarchar](255) NULL,
	[NickName] [nvarchar](50) NULL,
	[Avatar] [ntext] NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Fans_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfig]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfig](
	[SystemConfigId] int identity(1000,1),
	[Title] [nvarchar](50) NULL,
	[Val] [nvarchar](255) NULL,
	[Remarks] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysAccount]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SysAccount](
	[SysAccountId] [int] IDENTITY(1000,1) NOT NULL,
	[Account] [varchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[Password] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_SysAccount] PRIMARY KEY CLUSTERED 
(
	[SysAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] int identity(1000,1),
	[Tid] [nvarchar](50) NOT NULL,
	[Oid] [int] NOT NULL,
	[Outer_sku_id] [nvarchar](50) NULL,
	[Outer_item_id] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[sku_id] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[Total_fee] [decimal](18, 2) NULL,
	[Num] [int] NULL,
	[Wx_no] [nvarchar](50) NULL,
	[Taboo] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] int identity(1000,1),
	[Title] [nvarchar](255) NULL,
	[Tid] [nvarchar](50) NOT NULL,
	[Total_fee] [decimal](18, 2) NULL,
	[Pic_thumb_path] [ntext] NULL,
	[Status_str] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[Fans_weixin_openid] [nvarchar](255) NULL,
	[Payment] [decimal](18, 2) NULL,
	[Pay_time] [datetime] NULL,
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
	[Start_send] [nvarchar](50) NOT NULL,
	[End_send] [nvarchar](50) NOT NULL,
	[Send_day] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarInfo]    Script Date: 05/12/2018 21:37:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CalendarInfo](
	[CalendarInfoId] [int] IDENTITY(1000,1) NOT NULL,
	[Day] [varchar](50) NULL,
	[Status] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[SendInfo]
(
	[SendInfoId] [int] identity(1000,1),
	[Tid] [Nvarchar](50) not null,
	[Send_time] [Nvarchar](50) not null,
	[Is_send] [int] default(1),
	[Send_num] [int] default(1),
) 
go