USE [master]
GO
/****** Object:  Database [StudentLab05]    Script Date: 07/10/2023 7:24:50 AM ******/
CREATE DATABASE [StudentLab05]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudentLab05', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StudentLab05.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StudentLab05_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StudentLab05_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentLab05].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentLab05] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StudentLab05] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StudentLab05] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StudentLab05] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StudentLab05] SET ARITHABORT OFF 
GO
ALTER DATABASE [StudentLab05] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StudentLab05] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StudentLab05] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StudentLab05] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StudentLab05] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StudentLab05] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StudentLab05] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StudentLab05] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StudentLab05] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StudentLab05] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StudentLab05] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StudentLab05] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StudentLab05] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StudentLab05] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StudentLab05] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StudentLab05] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StudentLab05] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StudentLab05] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StudentLab05] SET  MULTI_USER 
GO
ALTER DATABASE [StudentLab05] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StudentLab05] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StudentLab05] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StudentLab05] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StudentLab05] SET DELAYED_DURABILITY = DISABLED 
GO
USE [StudentLab05]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 07/10/2023 7:24:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[FacultyID] [int] NOT NULL,
	[FacultyName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[FacultyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 07/10/2023 7:24:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[FacultyID] [int] NOT NULL,
	[MajorID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
(
	[FacultyID] ASC,
	[MajorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 07/10/2023 7:24:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [nvarchar](10) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[AverageScore] [float] NOT NULL,
	[FacultyID] [int] NULL,
	[MajorID] [int] NULL,
	[Avatar] [nvarchar](255) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName]) VALUES (1, N'Công Nghệ Thông Tin')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName]) VALUES (2, N'Ngôn Ngữ Anh')
INSERT [dbo].[Faculty] ([FacultyID], [FacultyName]) VALUES (3, N'Quản Trị Kinh Doanh')
GO
INSERT [dbo].[Major] ([FacultyID], [MajorID], [Name]) VALUES (1, 1, N'Công Nghệ Phần Mềm')
INSERT [dbo].[Major] ([FacultyID], [MajorID], [Name]) VALUES (1, 2, N'Hệ Thống Thông Tin')
GO
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID], [MajorID], [Avatar]) VALUES (N'1', N'1', 1, 1, NULL, N'1.jpg')
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID], [MajorID], [Avatar]) VALUES (N'2', N'2', 2, 2, NULL, N'23zinp5c3.vyy.jpg')
INSERT [dbo].[Student] ([StudentID], [FullName], [AverageScore], [FacultyID], [MajorID], [Avatar]) VALUES (N'3', N'3', 3, 3, NULL, N'3.jpg')
GO
ALTER TABLE [dbo].[Major]  WITH CHECK ADD  CONSTRAINT [FK_Major_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([FacultyID])
GO
ALTER TABLE [dbo].[Major] CHECK CONSTRAINT [FK_Major_Faculty]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([FacultyID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Faculty]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Major] FOREIGN KEY([FacultyID], [MajorID])
REFERENCES [dbo].[Major] ([FacultyID], [MajorID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Major]
GO
USE [master]
GO
ALTER DATABASE [StudentLab05] SET  READ_WRITE 
GO
