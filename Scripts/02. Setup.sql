USE [MINI_DGPAY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE TYPE='U' AND NAME='BT_ACCOUNT')
BEGIN
	CREATE TABLE [dbo].[BT_ACCOUNT](
		[UserId]		[nvarchar](10) NOT NULL PRIMARY KEY,
		[UserName]		[nvarchar](80) NULL,
		[UserPass]		[nvarchar](10) NULL,
		[UserPin]		[int] NULL,
		[UserMobileNo]	[nvarchar](20) NULL,
		[UserBalance]	[decimal](25, 8) NULL,
		[UserStatus]	[int] NULL,
		[CreateDate]	[date] NULL,
		[CreateBy]		[nvarchar](10) NULL,
		[ModifyDate]	[date] NULL,
		[ModifyBy]		[nvarchar](10) NULL
	) ON [PRIMARY]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.objects WHERE TYPE='U' AND NAME='BT_TRANSACTION')
BEGIN
	CREATE TABLE [dbo].[BT_TRANSACTION](
		[TranId]		[nvarchar](10) NOT NULL PRIMARY KEY,
		[TranDate]		[date] NULL,
		[TranSender]	[nvarchar](20) NULL,
		[TranRecver]	[nvarchar](20) NULL,
		[TranAmount]	[decimal](25, 8) NULL,
		[TranRemk]		[nvarchar](200) NULL,
		[TranType]		[nvarchar](10) NULL,
		[TranStatus]	[int] NULL
	) ON [PRIMARY]
END
GO