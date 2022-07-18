USE [master]
GO
/****** Object:  Database [SaeedNADb]    Script Date: 13/03/1401 17:29:55 ******/
CREATE DATABASE [SaeedNADb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SaeedNADb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\SaeedNADb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SaeedNADb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\SaeedNADb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SaeedNADb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SaeedNADb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SaeedNADb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SaeedNADb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SaeedNADb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SaeedNADb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SaeedNADb] SET ARITHABORT OFF 
GO
ALTER DATABASE [SaeedNADb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SaeedNADb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SaeedNADb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SaeedNADb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SaeedNADb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SaeedNADb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SaeedNADb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SaeedNADb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SaeedNADb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SaeedNADb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SaeedNADb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SaeedNADb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SaeedNADb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SaeedNADb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SaeedNADb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SaeedNADb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SaeedNADb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SaeedNADb] SET RECOVERY FULL 
GO
ALTER DATABASE [SaeedNADb] SET  MULTI_USER 
GO
ALTER DATABASE [SaeedNADb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SaeedNADb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SaeedNADb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SaeedNADb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SaeedNADb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SaeedNADb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SaeedNADb', N'ON'
GO
ALTER DATABASE [SaeedNADb] SET QUERY_STORE = OFF
GO
USE [SaeedNADb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[PostId] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Mobile] [nvarchar](15) NULL,
	[Subject] [nvarchar](64) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counters]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counters](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Number] [nvarchar](20) NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Counters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Histories]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Histories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[WorkPlace] [nvarchar](30) NOT NULL,
	[Date] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Histories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MyServices]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyServices](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Text] [nvarchar](200) NOT NULL,
	[Icon] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MyServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalInfos]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalInfos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Birthday] [nvarchar](15) NULL,
	[Mobile] [nvarchar](20) NULL,
	[AboutMe] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Age] [nvarchar](64) NULL,
	[Language] [nvarchar](250) NULL,
	[Nationality] [nvarchar](200) NULL,
	[ResumeImage] [nvarchar](max) NULL,
	[AvatarImage] [nvarchar](max) NULL,
	[ResumeFile] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PersonalInfos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portfolios]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolios](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](150) NULL,
	[Tags] [nvarchar](max) NULL,
	[ProjectCustomer] [nvarchar](max) NULL,
	[ProjectAddress] [nvarchar](max) NULL,
	[ProjectLanguage] [nvarchar](max) NULL,
	[State] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Portfolios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](150) NULL,
	[Description] [nvarchar](max) NULL,
	[Tags] [nvarchar](max) NULL,
	[Visit] [int] NOT NULL,
	[State] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seos]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MetaTags] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[Canonical] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[Publisher] [nvarchar](max) NULL,
	[GoogleAnalytics] [nvarchar](max) NULL,
	[SiteMap] [nvarchar](max) NULL,
	[RobotsTxt] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Seos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SiteLogo] [nvarchar](max) NULL,
	[SiteFavIcon] [nvarchar](max) NULL,
	[SiteUrl] [nvarchar](max) NULL,
	[SiteTitle] [nvarchar](max) NULL,
	[SiteMode] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Progress] [nvarchar](3) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialMediaes]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialMediaes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MediaIcon] [nvarchar](64) NOT NULL,
	[MediaLink] [nvarchar](200) NOT NULL,
	[MediaName] [nvarchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SocialMediaes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13/03/1401 17:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[ForgotToken] [nvarchar](64) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220101193428_InitDb', N'5.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220104203807_AddUserAndContactUsTable', N'5.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220106140719_AddReadFieldToContactUsTable', N'5.0.11')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password], [ForgotToken], [IsDelete], [CreateDate], [LastUpdateDate]) VALUES (1, N'saeeddda', N'noori.saeed1375@gmail.com', N'06-6F-EC-3B-13-2B-C5-59-D5-24-7D-D9-D9-0C-B5-0B', N'', 0, CAST(N'2022-01-05T00:09:11.1364064' AS DateTime2), CAST(N'2022-01-05T00:09:11.1364064' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Portfolios_CategoryId]    Script Date: 13/03/1401 17:29:56 ******/
CREATE NONCLUSTERED INDEX [IX_Portfolios_CategoryId] ON [dbo].[Portfolios]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Posts_CategoryId]    Script Date: 13/03/1401 17:29:56 ******/
CREATE NONCLUSTERED INDEX [IX_Posts_CategoryId] ON [dbo].[Posts]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactUs] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsRead]
GO
ALTER TABLE [dbo].[Portfolios]  WITH CHECK ADD  CONSTRAINT [FK_Portfolios_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Portfolios] CHECK CONSTRAINT [FK_Portfolios_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Categories_CategoryId]
GO
USE [master]
GO
ALTER DATABASE [SaeedNADb] SET  READ_WRITE 
GO
