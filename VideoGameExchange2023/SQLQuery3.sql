USE [master]
GO
/****** Object:  Database [VideoGameExchange2023]    Script Date: 16-08-23 01:30:42 ******/
CREATE DATABASE [VideoGameExchange2023]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VideoGameExchange2023', FILENAME = N'C:\Users\jkten\VideoGameExchange2023.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VideoGameExchange2023_log', FILENAME = N'C:\Users\jkten\VideoGameExchange2023_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VideoGameExchange2023] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VideoGameExchange2023].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VideoGameExchange2023] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET ARITHABORT OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VideoGameExchange2023] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VideoGameExchange2023] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VideoGameExchange2023] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VideoGameExchange2023] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VideoGameExchange2023] SET  MULTI_USER 
GO
ALTER DATABASE [VideoGameExchange2023] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VideoGameExchange2023] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VideoGameExchange2023] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VideoGameExchange2023] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VideoGameExchange2023] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VideoGameExchange2023] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VideoGameExchange2023] SET QUERY_STORE = OFF
GO
USE [VideoGameExchange2023]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[username] [nchar](10) NOT NULL,
	[password] [nchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookdate] [date] NOT NULL,
	[player] [varchar](50) NOT NULL,
	[game] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Console]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Console](
	[consolename] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Console] PRIMARY KEY CLUSTERED 
(
	[consolename] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Copy]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Copy](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[videogame] [varchar](50) NOT NULL,
	[owner] [varchar](50) NOT NULL,
	[available] [bit] NOT NULL,
 CONSTRAINT [PK_Copy] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loan]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[startdate] [date] NOT NULL,
	[enddate] [date] NOT NULL,
	[ongoing] [bit] NOT NULL,
	[copy] [int] NOT NULL,
	[lender] [varchar](50) NOT NULL,
	[borrower] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[pseudo] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[credit] [int] NOT NULL,
	[registrationdate] [date] NOT NULL,
	[dateofbirth] [date] NOT NULL,
	[birthdaygiftgivenyear] [int] NOT NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[pseudo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videogames]    Script Date: 16-08-23 01:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videogames](
	[name] [varchar](50) NOT NULL,
	[creditcost] [int] NOT NULL,
	[console] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Videogames] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Admin] ([username], [password]) VALUES (N'adm       ', N'1234      ')
GO
INSERT [dbo].[Admin] ([username], [password]) VALUES (N'admin     ', N'admin     ')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'Nintendo')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'PS1')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'PS2')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'PS3')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'Switch')
GO
INSERT [dbo].[Console] ([consolename]) VALUES (N'Xbox')
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'AN', N'Anton', N'Anton', 12, CAST(N'2023-07-20' AS Date), CAST(N'2009-03-01' AS Date), 2023)
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'BB', N'Buba', N'Buba', 12, CAST(N'2023-07-20' AS Date), CAST(N'1999-06-01' AS Date), 2023)
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'EF', N'Enny', N'Frans', 21, CAST(N'2023-07-18' AS Date), CAST(N'1990-07-26' AS Date), 2023)
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'LL', N'Lola', N'Lola', 10, CAST(N'2023-07-22' AS Date), CAST(N'2003-07-22' AS Date), 0)
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'MJ', N'Marie', N'Marie', 1, CAST(N'2023-07-20' AS Date), CAST(N'2000-06-01' AS Date), 2023)
GO
INSERT [dbo].[Player] ([pseudo], [username], [password], [credit], [registrationdate], [dateofbirth], [birthdaygiftgivenyear]) VALUES (N'MK', N'Mimi', N'Mimi', 20, CAST(N'2023-07-20' AS Date), CAST(N'1980-01-01' AS Date), 2023)
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'Driver', 1, N'PS1')
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'FIFA', 3, N'Xbox')
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'Fortnite', 1, N'Xbox')
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'Ico', 1, N'PS2')
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'Mario Bros', 3, N'Nintendo')
GO
INSERT [dbo].[Videogames] ([name], [creditcost], [console]) VALUES (N'Zelda', 2, N'Xbox')
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Book_Player] FOREIGN KEY([player])
REFERENCES [dbo].[Player] ([pseudo])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Book_Player]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Book_Videogames] FOREIGN KEY([game])
REFERENCES [dbo].[Videogames] ([name])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Book_Videogames]
GO
ALTER TABLE [dbo].[Copy]  WITH CHECK ADD  CONSTRAINT [FK_Copy_Player] FOREIGN KEY([owner])
REFERENCES [dbo].[Player] ([pseudo])
GO
ALTER TABLE [dbo].[Copy] CHECK CONSTRAINT [FK_Copy_Player]
GO
ALTER TABLE [dbo].[Copy]  WITH CHECK ADD  CONSTRAINT [FK_Copy_Videogames] FOREIGN KEY([videogame])
REFERENCES [dbo].[Videogames] ([name])
GO
ALTER TABLE [dbo].[Copy] CHECK CONSTRAINT [FK_Copy_Videogames]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Copy] FOREIGN KEY([copy])
REFERENCES [dbo].[Copy] ([id])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Copy]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Player_borrower] FOREIGN KEY([borrower])
REFERENCES [dbo].[Player] ([pseudo])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Player_borrower]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Player_lender] FOREIGN KEY([lender])
REFERENCES [dbo].[Player] ([pseudo])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Player_lender]
GO
ALTER TABLE [dbo].[Videogames]  WITH CHECK ADD  CONSTRAINT [FK_Videogames_Console] FOREIGN KEY([console])
REFERENCES [dbo].[Console] ([consolename])
GO
ALTER TABLE [dbo].[Videogames] CHECK CONSTRAINT [FK_Videogames_Console]
GO
USE [master]
GO
ALTER DATABASE [VideoGameExchange2023] SET  READ_WRITE 
GO
