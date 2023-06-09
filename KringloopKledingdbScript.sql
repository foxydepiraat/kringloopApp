USE [master]
GO
/****** Object:  Database [kringloopAfhaling]    Script Date: 6-6-2023 10:55:33 ******/
CREATE DATABASE [kringloopAfhaling]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'kringloopAfhaling', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\kringloopAfhaling.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'kringloopAfhaling_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\kringloopAfhaling_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [kringloopAfhaling] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [kringloopAfhaling].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [kringloopAfhaling] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET ARITHABORT OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [kringloopAfhaling] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [kringloopAfhaling] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET  DISABLE_BROKER 
GO
ALTER DATABASE [kringloopAfhaling] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [kringloopAfhaling] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [kringloopAfhaling] SET  MULTI_USER 
GO
ALTER DATABASE [kringloopAfhaling] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [kringloopAfhaling] SET DB_CHAINING OFF 
GO
ALTER DATABASE [kringloopAfhaling] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [kringloopAfhaling] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [kringloopAfhaling] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [kringloopAfhaling] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [kringloopAfhaling] SET QUERY_STORE = OFF
GO
USE [kringloopAfhaling]
GO
/****** Object:  Table [dbo].[afhaling]    Script Date: 6-6-2023 10:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[afhaling](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[datum] [date] NULL,
	[gezinslid_id] [int] NULL,
 CONSTRAINT [PK_afhaling] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gezin]    Script Date: 6-6-2023 10:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gezin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[kringloopKaartnummer] [varchar](6) NULL,
	[achternaam] [varchar](50) NULL,
	[Woonplaats] [varchar](50) NULL,
	[actief] [int] NULL,
	[reden] [varchar](50) NULL,
 CONSTRAINT [PK_gezin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gezinslid]    Script Date: 6-6-2023 10:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gezinslid](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[voornaam] [varchar](50) NULL,
	[geboortejaar] [varchar](4) NULL,
	[gezin_id] [int] NULL,
	[actief] [int] NULL,
 CONSTRAINT [PK_gezinslid] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[residence]    Script Date: 6-6-2023 10:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[residence](
	[id] [int] NULL,
	[residence] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_afhaling]    Script Date: 6-6-2023 10:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_afhaling] ON [dbo].[afhaling]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_gezinslid]    Script Date: 6-6-2023 10:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_gezinslid] ON [dbo].[gezinslid]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [kringloopAfhaling] SET  READ_WRITE 
GO
