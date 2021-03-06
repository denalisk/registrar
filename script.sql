USE [registrar]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 2/28/2017 4:45:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[number] [varchar](10) NULL,
	[dept_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[departments]    Script Date: 2/28/2017 4:45:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 2/28/2017 4:45:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[enrollment_date] [varchar](255) NULL,
	[dept_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students_courses]    Script Date: 2/28/2017 4:45:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students_courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NULL,
	[course_id] [int] NULL,
	[grade] [varchar](10) NULL
) ON [PRIMARY]

GO
