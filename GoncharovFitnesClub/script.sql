USE [master]
GO
/****** Object:  Database [FitnesClub]    Script Date: 16.02.2023 17:08:58 ******/
CREATE DATABASE [FitnesClub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FitnesClub', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FitnesClub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FitnesClub_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FitnesClub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FitnesClub] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FitnesClub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FitnesClub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FitnesClub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FitnesClub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FitnesClub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FitnesClub] SET ARITHABORT OFF 
GO
ALTER DATABASE [FitnesClub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FitnesClub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FitnesClub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FitnesClub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FitnesClub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FitnesClub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FitnesClub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FitnesClub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FitnesClub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FitnesClub] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FitnesClub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FitnesClub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FitnesClub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FitnesClub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FitnesClub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FitnesClub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FitnesClub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FitnesClub] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FitnesClub] SET  MULTI_USER 
GO
ALTER DATABASE [FitnesClub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FitnesClub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FitnesClub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FitnesClub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FitnesClub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FitnesClub] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FitnesClub] SET QUERY_STORE = OFF
GO
USE [FitnesClub]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](30) NULL,
	[PhoneNum] [nvarchar](17) NOT NULL,
	[Email] [nvarchar](30) NULL,
	[DateOfReg] [date] NOT NULL,
	[DateOfEnd] [date] NULL,
	[SubscriptionID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coach]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach](
	[CoachID] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](30) NULL,
	[PhoneNum] [nvarchar](17) NOT NULL,
	[Email] [nvarchar](30) NULL,
	[SpecialityID] [int] NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED 
(
	[CoachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[NameRole] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speciality](
	[SpecialityID] [int] IDENTITY(1,1) NOT NULL,
	[NameSpeciality] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[SpecialityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[NameStatus] [nvarchar](30) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription](
	[SubscriptionID] [int] IDENTITY(1,1) NOT NULL,
	[NameSubscription] [nvarchar](90) NOT NULL,
	[SpecialityID] [int] NULL,
	[CoachID] [int] NULL,
	[AmountOfDays] [int] NOT NULL,
	[VisitDateID] [int] NULL,
	[TimeOfVisit] [nvarchar](17) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](9, 2) NULL,
 CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED 
(
	[SubscriptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitDate]    Script Date: 16.02.2023 17:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitDate](
	[VisitDateID] [int] IDENTITY(1,1) NOT NULL,
	[DayOfVisit] [nvarchar](90) NOT NULL,
 CONSTRAINT [PK_VisitDate] PRIMARY KEY CLUSTERED 
(
	[VisitDateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([ClientID], [Surname], [Name], [Patronymic], [PhoneNum], [Email], [DateOfReg], [DateOfEnd], [SubscriptionID], [StatusID]) VALUES (1, N'Гончаров', N'Александр', N'Дмитриевич', N'+7 916-866-38-19', N'gonharov.alex@ya.ru', CAST(N'2023-02-15' AS Date), CAST(N'2023-03-17' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Coach] ON 

INSERT [dbo].[Coach] ([CoachID], [Surname], [Name], [Patronymic], [PhoneNum], [Email], [SpecialityID], [Photo]) VALUES (2, N'Горчук', N'Наталья', N'Александровна', N'+7 044-908-27-86', N'Natalya@gmail.com', 1, NULL)
INSERT [dbo].[Coach] ([CoachID], [Surname], [Name], [Patronymic], [PhoneNum], [Email], [SpecialityID], [Photo]) VALUES (3, N'Годунов', N'Фёдор', NULL, N'+7 415-314-31-23', NULL, 2, NULL)
SET IDENTITY_INSERT [dbo].[Coach] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [NameRole]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([RoleID], [NameRole]) VALUES (2, N'Сотрудник')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([SpecialityID], [NameSpeciality]) VALUES (1, N'Тренер художественной гимнастики ')
INSERT [dbo].[Speciality] ([SpecialityID], [NameSpeciality]) VALUES (2, N'тренер тяжелой атлетики')
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusID], [NameStatus]) VALUES (1, N'В действии')
INSERT [dbo].[Status] ([StatusID], [NameStatus]) VALUES (2, N'Истёк')
INSERT [dbo].[Status] ([StatusID], [NameStatus]) VALUES (3, N'В ожидании')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Subscription] ON 

INSERT [dbo].[Subscription] ([SubscriptionID], [NameSubscription], [SpecialityID], [CoachID], [AmountOfDays], [VisitDateID], [TimeOfVisit], [Description], [Price]) VALUES (1, N'Стандартный месяц', NULL, NULL, 30, 1, N'9:00 - 21:30', N'Стандартный абонемент на месяц с доступом ко всем тренажёрам', CAST(2000.00 AS Decimal(9, 2)))
INSERT [dbo].[Subscription] ([SubscriptionID], [NameSubscription], [SpecialityID], [CoachID], [AmountOfDays], [VisitDateID], [TimeOfVisit], [Description], [Price]) VALUES (2, N'Стандарт год', NULL, NULL, 365, 1, N'9-00 - 21:30', N'Стандартная подписка, на год', CAST(16000.00 AS Decimal(9, 2)))
INSERT [dbo].[Subscription] ([SubscriptionID], [NameSubscription], [SpecialityID], [CoachID], [AmountOfDays], [VisitDateID], [TimeOfVisit], [Description], [Price]) VALUES (3, N'Тяжёлая атлетика', NULL, 3, 30, 4, N'15:00 - 17:00', N'Курсы тяжелой атлетики для тех кто хочет быстро сделать рельев тела', CAST(1500.00 AS Decimal(9, 2)))
SET IDENTITY_INSERT [dbo].[Subscription] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Login], [Password], [RoleID]) VALUES (1, N'Admin', N'123', 1)
INSERT [dbo].[User] ([UserID], [Login], [Password], [RoleID]) VALUES (2, N'Staff', N'321', 2)
INSERT [dbo].[User] ([UserID], [Login], [Password], [RoleID]) VALUES (3, N'Alex', N'699', 1)
INSERT [dbo].[User] ([UserID], [Login], [Password], [RoleID]) VALUES (6, N'Natalya@gmail.com', N'432fgh95342vf312', 2)
INSERT [dbo].[User] ([UserID], [Login], [Password], [RoleID]) VALUES (15, N'Popug@ya.ru', N'856745', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitDate] ON 

INSERT [dbo].[VisitDate] ([VisitDateID], [DayOfVisit]) VALUES (1, N'Все дни')
INSERT [dbo].[VisitDate] ([VisitDateID], [DayOfVisit]) VALUES (2, N'По выходным')
INSERT [dbo].[VisitDate] ([VisitDateID], [DayOfVisit]) VALUES (3, N'По будням')
INSERT [dbo].[VisitDate] ([VisitDateID], [DayOfVisit]) VALUES (4, N'Понедельник, среда, пятница')
SET IDENTITY_INSERT [dbo].[VisitDate] OFF
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Status]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Subscription] FOREIGN KEY([SubscriptionID])
REFERENCES [dbo].[Subscription] ([SubscriptionID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Subscription]
GO
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [FK_Coach_Speciality] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[Speciality] ([SpecialityID])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [FK_Coach_Speciality]
GO
ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Coach] FOREIGN KEY([CoachID])
REFERENCES [dbo].[Coach] ([CoachID])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_Subscription_Coach]
GO
ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Speciality] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[Speciality] ([SpecialityID])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_Subscription_Speciality]
GO
ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_VisitDate] FOREIGN KEY([VisitDateID])
REFERENCES [dbo].[VisitDate] ([VisitDateID])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_Subscription_VisitDate]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [FitnesClub] SET  READ_WRITE 
GO
