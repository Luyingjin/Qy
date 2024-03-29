USE [master]
GO
/****** Object:  Database [MP_WeChat]    Script Date: 03/03/2015 13:33:42 ******/
CREATE DATABASE [MP_WeChat] ON  PRIMARY 
( NAME = N'MP_WeChat', FILENAME = N'D:\Data\MP\MP_WeChat.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MP_WeChat_log', FILENAME = N'D:\Data\MP\MP_WeChat_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MP_WeChat] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MP_WeChat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MP_WeChat] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MP_WeChat] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MP_WeChat] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MP_WeChat] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MP_WeChat] SET ARITHABORT OFF
GO
ALTER DATABASE [MP_WeChat] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MP_WeChat] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MP_WeChat] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MP_WeChat] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MP_WeChat] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MP_WeChat] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MP_WeChat] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MP_WeChat] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MP_WeChat] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MP_WeChat] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MP_WeChat] SET  DISABLE_BROKER
GO
ALTER DATABASE [MP_WeChat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MP_WeChat] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MP_WeChat] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MP_WeChat] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MP_WeChat] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MP_WeChat] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MP_WeChat] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MP_WeChat] SET  READ_WRITE
GO
ALTER DATABASE [MP_WeChat] SET RECOVERY FULL
GO
ALTER DATABASE [MP_WeChat] SET  MULTI_USER
GO
ALTER DATABASE [MP_WeChat] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MP_WeChat] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'MP_WeChat', N'ON'
GO
USE [MP_WeChat]
GO
/****** Object:  Table [dbo].[Wx_MpAccount]    Script Date: 03/03/2015 13:33:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wx_MpAccount](
	[ID] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Remark] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NULL,
	[AppID] [nvarchar](50) NULL,
	[AppSecret] [nvarchar](50) NULL,
	[AccessToken] [nvarchar](50) NULL,
	[ExpireTime] [datetime] NULL,
	[CreateUserID] [nvarchar](50) NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyUserID] [nvarchar](50) NULL,
	[ModifyUser] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Wx_MpAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公众号名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公众号描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公众号类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'令牌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'AccessToken'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'ModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'ModifyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wx_MpAccount', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
