USE [hair_salon_test]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 2/26/2016 4:09:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clients](
	[name] [varchar](255) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stylists]    Script Date: 2/26/2016 4:09:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stylists](
	[name] [varchar](255) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([name], [id], [stylist_id]) VALUES (N'client1', 182, 206)
INSERT [dbo].[clients] ([name], [id], [stylist_id]) VALUES (N'client1', 183, 209)
INSERT [dbo].[clients] ([name], [id], [stylist_id]) VALUES (N'jim', 184, 209)
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([name], [id]) VALUES (N'Bob', 209)
INSERT [dbo].[stylists] ([name], [id]) VALUES (N'hey', 210)
INSERT [dbo].[stylists] ([name], [id]) VALUES (N'Jill', 205)
INSERT [dbo].[stylists] ([name], [id]) VALUES (N'Alex', 206)
SET IDENTITY_INSERT [dbo].[stylists] OFF
