USE [master]
GO
/****** Object:  Database [UniversityManagement]    Script Date: 06/03/2024 10:44:05 AM ******/
CREATE DATABASE [UniversityManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UniversityManagement', FILENAME = N'D:\SQL\Saved Database\UniversityManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UniversityManagement_log', FILENAME = N'D:\SQL\Saved Database\UniversityManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [UniversityManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UniversityManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UniversityManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UniversityManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UniversityManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UniversityManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UniversityManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [UniversityManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [UniversityManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UniversityManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UniversityManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UniversityManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UniversityManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UniversityManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UniversityManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UniversityManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UniversityManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UniversityManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UniversityManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UniversityManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UniversityManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UniversityManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UniversityManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UniversityManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UniversityManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UniversityManagement] SET  MULTI_USER 
GO
ALTER DATABASE [UniversityManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UniversityManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UniversityManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UniversityManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UniversityManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UniversityManagement] SET QUERY_STORE = OFF
GO
USE [UniversityManagement]
GO
/****** Object:  Table [dbo].[AnnouncementCategory]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnnouncementCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Announcements]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[AnnouncementID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Content] [text] NULL,
	[AuthorID] [int] NULL,
	[PublishedDate] [date] NULL,
	[CategoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AnnouncementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HolidaySchedule]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HolidaySchedule](
	[HolidayID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[HolidayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingSchedule]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingSchedule](
	[MeetingID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[MeetingDate] [date] NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategory]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[PostImage] [varchar](255) NOT NULL,
	[Content] [text] NULL,
	[AuthorID] [int] NULL,
	[PublishedDate] [date] NULL,
	[CategoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeachingSchedule]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeachingSchedule](
	[TeachingID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherID] [int] NULL,
	[SubjectID] [int] NULL,
	[ClassID] [int] NULL,
	[TeachingDate] [date] NULL,
	[DayOfWeek] [varchar](20) NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TeachingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06/03/2024 10:44:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[FullName] [varchar](100) NULL,
	[Phone] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AnnouncementCategory] ON 

INSERT [dbo].[AnnouncementCategory] ([CategoryID], [Name]) VALUES (1, N'Nghien Cuu')
INSERT [dbo].[AnnouncementCategory] ([CategoryID], [Name]) VALUES (2, N'SangTao')
SET IDENTITY_INSERT [dbo].[AnnouncementCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Announcements] ON 

INSERT [dbo].[Announcements] ([AnnouncementID], [Title], [Content], [AuthorID], [PublishedDate], [CategoryID]) VALUES (1, N'Announcement of doctoral thesis defense information of graduate student Hoang Thi Thu', N'University of Science and Technology - University of Da Nang announces information about doctoral thesis defense of graduate students as follows:  - Full name of PhD student: Hoang Thi Thu  - Thesis title: Research on some factors affecting the quality of frozen dough and orientation for application in bread production.  - Code: 9540101  - Date of protection: June 22, 2024  The entire content and summary of the thesis can be downloaded here.', 2, CAST(N'2023-03-03' AS Date), 1)
INSERT [dbo].[Announcements] ([AnnouncementID], [Title], [Content], [AuthorID], [PublishedDate], [CategoryID]) VALUES (2, N'CongNgheSo', N'Khoa CNS duoc thanh lap voi mong muon phat trien sang tao doi moi tot hon trong nhung nam toi', 2, CAST(N'2024-06-01' AS Date), 2)
SET IDENTITY_INSERT [dbo].[Announcements] OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([ClassID], [ClassName]) VALUES (1, N'DSTT123')
INSERT [dbo].[Classes] ([ClassID], [ClassName]) VALUES (2, N'NMLT321')
INSERT [dbo].[Classes] ([ClassID], [ClassName]) VALUES (5, N'19T1')
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[HolidaySchedule] ON 

INSERT [dbo].[HolidaySchedule] ([HolidayID], [Title], [StartDate], [EndDate]) VALUES (2, N'Nghi le', CAST(N'2023-04-01' AS Date), CAST(N'2023-05-04' AS Date))
INSERT [dbo].[HolidaySchedule] ([HolidayID], [Title], [StartDate], [EndDate]) VALUES (4, N'NghiHe', CAST(N'2024-06-30' AS Date), CAST(N'2024-07-31' AS Date))
SET IDENTITY_INSERT [dbo].[HolidaySchedule] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingSchedule] ON 

INSERT [dbo].[MeetingSchedule] ([MeetingID], [Title], [Description], [MeetingDate], [StartTime], [EndTime]) VALUES (1, N'Meeting', N'Picnic', CAST(N'2024-05-05' AS Date), CAST(N'07:00:00' AS Time), CAST(N'19:00:00' AS Time))
INSERT [dbo].[MeetingSchedule] ([MeetingID], [Title], [Description], [MeetingDate], [StartTime], [EndTime]) VALUES (3, N'CallOnline', N'DayHe', CAST(N'2024-06-01' AS Date), CAST(N'08:30:00' AS Time), CAST(N'10:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[MeetingSchedule] OFF
GO
SET IDENTITY_INSERT [dbo].[PostCategory] ON 

INSERT [dbo].[PostCategory] ([CategoryID], [Name]) VALUES (1, N'Khoa Hoc Cong Nghe')
INSERT [dbo].[PostCategory] ([CategoryID], [Name]) VALUES (2, N'STEM - TIN TUC')
INSERT [dbo].[PostCategory] ([CategoryID], [Name]) VALUES (3, N'KhoaCNS')
SET IDENTITY_INSERT [dbo].[PostCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([PostID], [Title], [PostImage], [Content], [AuthorID], [PublishedDate], [CategoryID]) VALUES (1, N'Innovation and Technology X.0 Contest', N'post1.jpg', N'International conference The IEEE International Conference on Connected Innovation and Technology X.O (ICCITX.O) & The Smart Healthcare International Conference (SHeIC) - IEEE ICCITX.O & SHeIC 2024 with the theme of the intersection of Healthcare fields Health, Technology and Public Policy. The conference was organized by cooperation from: Université de Technologie de Troyes, Troyes, France; École Nationale Supérieure D''arts Et Métiers, Rabat Morocco; Hôpital Européen, Marseille, France and Polytechnic University, University of Danang (UD). The Conference sessions took place in parallel in all 3 countries through two reporting methods: face-to-face and online. International scientists, including lecturers and students from the University of Danang, participated in reporting at the 3-day conference (from April 22-24, 2024).', 2, CAST(N'2023-03-03' AS Date), 1)
INSERT [dbo].[Posts] ([PostID], [Title], [PostImage], [Content], [AuthorID], [PublishedDate], [CategoryID]) VALUES (2, N'Competition "STEM innovation serving smart cities', N'post2.jpg', N'On April 26, 2024, at the Polytechnic University, the University of Danang (UD), the contest "STEM INNOVATION FOR SMART CITY - SI4SC" took place attracting participation. of 29 teams from 15 High Schools in Da Nang city, Quang Nam province and Quang Ngai province. The topics belong to 04 field groups: Artificial Intelligence (AI) - Information Technology, Mechanics - Automation, Construction Engineering and Chemistry - Environment.', 2, CAST(N'2023-05-03' AS Date), 2)
INSERT [dbo].[Posts] ([PostID], [Title], [PostImage], [Content], [AuthorID], [PublishedDate], [CategoryID]) VALUES (4, N'CNS', N'post2.jpg', N'Khoa CNS la khoa chuyen ve Cong Nghe', 1, CAST(N'2024-06-03' AS Date), 2)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 

INSERT [dbo].[Subjects] ([SubjectID], [SubjectName]) VALUES (1, N'Toan')
INSERT [dbo].[Subjects] ([SubjectID], [SubjectName]) VALUES (2, N'C#')
INSERT [dbo].[Subjects] ([SubjectID], [SubjectName]) VALUES (3, N'Java2')
INSERT [dbo].[Subjects] ([SubjectID], [SubjectName]) VALUES (4, N'Android')
INSERT [dbo].[Subjects] ([SubjectID], [SubjectName]) VALUES (5, N'R')
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
SET IDENTITY_INSERT [dbo].[TeachingSchedule] ON 

INSERT [dbo].[TeachingSchedule] ([TeachingID], [TeacherID], [SubjectID], [ClassID], [TeachingDate], [DayOfWeek], [StartTime], [EndTime]) VALUES (1, 1, 1, 1, CAST(N'2024-03-03' AS Date), N'3', CAST(N'12:30:00' AS Time), CAST(N'19:00:00' AS Time))
INSERT [dbo].[TeachingSchedule] ([TeachingID], [TeacherID], [SubjectID], [ClassID], [TeachingDate], [DayOfWeek], [StartTime], [EndTime]) VALUES (3, 2, 3, 2, CAST(N'2024-06-05' AS Date), N'4', CAST(N'07:00:00' AS Time), CAST(N'11:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[TeachingSchedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [Phone], [Role]) VALUES (1, N'admin', N'1', N'Adminmin', N'123', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [Phone], [Role]) VALUES (2, N'teacher1', N'1', N'Hoang Anh', N'123', N'Teacher')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [Phone], [Role]) VALUES (3, N'teacher2', N'1', N'Teacher', N'1234', N'Teacher')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [Phone], [Role]) VALUES (7, N'teacher3', N'1', N'Dung', N'1234', N'Teacher')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E4F86E33CB]    Script Date: 06/03/2024 10:44:06 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Announcements]  WITH CHECK ADD FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Announcements]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[AnnouncementCategory] ([CategoryID])
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[PostCategory] ([CategoryID])
GO
ALTER TABLE [dbo].[TeachingSchedule]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ClassID])
GO
ALTER TABLE [dbo].[TeachingSchedule]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([SubjectID])
GO
ALTER TABLE [dbo].[TeachingSchedule]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Users] ([UserID])
GO
USE [master]
GO
ALTER DATABASE [UniversityManagement] SET  READ_WRITE 
GO
