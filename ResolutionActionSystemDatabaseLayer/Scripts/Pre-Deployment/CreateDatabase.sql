USE [master]
GO

/****** Object:  Database [ResolutionActionSystem]    Script Date: 10/17/2013 10:41:54 ******/
CREATE DATABASE [ResolutionActionSystem] 

GO

ALTER DATABASE [ResolutionActionSystem] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ResolutionActionSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ResolutionActionSystem] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET ARITHABORT OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [ResolutionActionSystem] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [ResolutionActionSystem] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ResolutionActionSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ResolutionActionSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET  ENABLE_BROKER 
GO

ALTER DATABASE [ResolutionActionSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ResolutionActionSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ResolutionActionSystem] SET  READ_WRITE 
GO

ALTER DATABASE [ResolutionActionSystem] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ResolutionActionSystem] SET  MULTI_USER 
GO

ALTER DATABASE [ResolutionActionSystem] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ResolutionActionSystem] SET DB_CHAINING OFF 
GO


