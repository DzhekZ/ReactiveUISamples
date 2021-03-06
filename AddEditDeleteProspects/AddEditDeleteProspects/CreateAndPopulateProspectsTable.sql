CREATE DATABASE Tests
GO

USE [Tests]
GO
/****** Object:  Table [dbo].[Prospects]    Script Date: 02/28/2012 18:51:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prospects](
	[ID] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [char](2) NOT NULL,
	[ZIP] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Prospects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Prospects_ID]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_ID]  DEFAULT (newid()) FOR [ID]
GO
/****** Object:  Default [DF_Prospects_FirstName]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_FirstName]  DEFAULT ('') FOR [FirstName]
GO
/****** Object:  Default [DF_Prospects_LastName]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_LastName]  DEFAULT ('') FOR [LastName]
GO
/****** Object:  Default [DF_Prospects_Address]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_Address]  DEFAULT ('') FOR [Address]
GO
/****** Object:  Default [DF_Prospects_City]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_City]  DEFAULT ('') FOR [City]
GO
/****** Object:  Default [DF_Prospects_State]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_State]  DEFAULT ('') FOR [State]
GO
/****** Object:  Default [DF_Prospects_ZIP]    Script Date: 02/28/2012 18:51:09 ******/
ALTER TABLE [dbo].[Prospects] ADD  CONSTRAINT [DF_Prospects_ZIP]  DEFAULT ('') FOR [ZIP]
GO

INSERT INTO Prospects VALUES ( 'Les',    'Pinter',     '34616 Highway 190', 'Springville', 'CA', '93265' )
INSERT INTO Prospects VALUES ( 'Sam',    'Schulman',   '1232 Nasa Road 1',  'Webster',     'TX', '77342' )
INSERT INTO Prospects VALUES ( 'Oren',   'Springer',   '22202 Westview',    'Houston',     'TX', '77024' )
INSERT INTO Prospects VALUES ( 'Venita', 'Cunningham', '1420 Columbia',     'Houston',     'TX', '77002' )
