USE [master]
GO
/****** Object:  Database [Comics]    Script Date: 10/16/2020 11:53:09 PM ******/
CREATE DATABASE [Comics]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Comics', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Comics.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Comics_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Comics_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Comics] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Comics].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Comics] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Comics] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Comics] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Comics] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Comics] SET ARITHABORT OFF 
GO
ALTER DATABASE [Comics] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Comics] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Comics] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Comics] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Comics] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Comics] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Comics] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Comics] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Comics] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Comics] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Comics] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Comics] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Comics] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Comics] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Comics] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Comics] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Comics] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Comics] SET RECOVERY FULL 
GO
ALTER DATABASE [Comics] SET  MULTI_USER 
GO
ALTER DATABASE [Comics] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Comics] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Comics] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Comics] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Comics] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Comics', N'ON'
GO
ALTER DATABASE [Comics] SET QUERY_STORE = OFF
GO
USE [Comics]
GO
/****** Object:  User [test]    Script Date: 10/16/2020 11:53:09 PM ******/
CREATE USER [test] FOR LOGIN [test] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Character]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Character](
	[Id] [int] NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](500) NULL,
	[Modified] [datetime2](7) NULL,
	[ResourceURI] [varchar](200) NULL,
	[ThumbnailPath] [varchar](200) NULL,
	[ThumbnailExtension] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacterUrl]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacterUrl](
	[Id] [int] NOT NULL,
	[CharacterId] [int] NOT NULL,
	[Type] [varchar](100) NULL,
	[Url] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comic]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comic](
	[Id] [int] NOT NULL,
	[DigitalId] [int] NULL,
	[Title] [varchar](150) NULL,
	[IssueNumber] [int] NULL,
	[Description] [varchar](200) NULL,
	[VariantDescription] [varchar](100) NULL,
	[Modified] [datetime] NULL,
	[Isbn] [varchar](40) NULL,
	[Upc] [varchar](100) NULL,
	[DiamondCode] [varchar](20) NULL,
	[Ean] [varchar](10) NULL,
	[Issn] [varchar](10) NULL,
	[Format] [varchar](10) NULL,
	[PageCount] [int] NULL,
	[ResourceURI] [varchar](200) NULL,
	[SerieId] [int] NOT NULL,
	[ThumbnailPath] [varchar](200) NULL,
	[ThumbnailExtension] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicCharacter]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicCharacter](
	[IdComic] [int] NOT NULL,
	[IdCharacter] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdComic] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicCollectedIssue]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicCollectedIssue](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicCollection]    Script Date: 10/16/2020 11:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicCollection](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicDate]    Script Date: 10/16/2020 11:53:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicDate](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicImage]    Script Date: 10/16/2020 11:53:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicImage](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicPrice]    Script Date: 10/16/2020 11:53:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicPrice](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicUrl]    Script Date: 10/16/2020 11:53:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicUrl](
	[Id] [int] NOT NULL,
	[ComicId] [int] NOT NULL,
	[Type] [varchar](100) NULL,
	[Url] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComicVariant]    Script Date: 10/16/2020 11:53:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComicVariant](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Comics] SET  READ_WRITE 
GO


-- insert

INSERT INTO [CHARACTER] (ID, [NAME], [DESCRIPTION], MODIFIED, ResourceURI, ThumbnailPath, ThumbnailExtension)
VALUES (1009157, 'Spider-Girl (Anya Corazon)', NULL, convert(datetime,'2013-12-17T16:00:02'), 
'http://gateway.marvel.com/v1/public/characters/1009157', 'http://i.annihil.us/u/prod/marvel/i/mg/a/10/528d369de3e4f', 'jpg')

INSERT INTO [CHARACTER] (ID, [NAME], [DESCRIPTION], MODIFIED, ResourceURI, ThumbnailPath, ThumbnailExtension)
VALUES (1011347, 'Spider-Ham (Larval Earth)', 'As Spider-Ham Peter faced such nefarious foes as Ductor Doom, Bull-Frog, Raven the Hunter, Hogzilla, the Buzzard and the King-Pig!',
convert(datetime,'2015-03-26T13:33:09'),
'http://gateway.marvel.com/v1/public/characters/1011347',
'http://i.annihil.us/u/prod/marvel/i/mg/b/40/image_not_available', 'jpg')

INSERT INTO [CHARACTER] (ID, [NAME], [DESCRIPTION], MODIFIED, ResourceURI, ThumbnailPath, ThumbnailExtension)
VALUES (1009610, 'Spider-Man', 
'Bitten by a radioactive spider, high school student Peter Parker gained the speed, strength and powers of a spider. Adopting the name Spider-Man, Peter hoped to start a career using his new abilities. ' +
'Taught that with great power comes great responsibility, Spidey has vowed to use his powers to help people.',
convert(datetime,'2020-07-21T10:30:10'),
'http://gateway.marvel.com/v1/public/characters/1009610',
'http://i.annihil.us/u/prod/marvel/i/mg/3/50/526548a343e4b', 'jpg')
