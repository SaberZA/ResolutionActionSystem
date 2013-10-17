USE [ResolutionActionSystem]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingType]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingType](
	[MeetingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingTypeName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MeetingType] PRIMARY KEY CLUSTERED 
(
	[MeetingTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingItemStatusLu]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingItemStatusLu](
	[MeetingItemStatusLuId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingItemStatusDesc] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MeetingItemStatusLu] PRIMARY KEY CLUSTERED 
(
	[MeetingItemStatusLuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[MeetingId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingNumber] [int] NOT NULL,
	[MeetingDate] [datetime] NOT NULL,
	[MeetingType_MeetingTypeId] [int] NULL,
	[PreviousMeeting_MeetingId] [int] NULL,
 CONSTRAINT [PK_dbo.Meeting] PRIMARY KEY CLUSTERED 
(
	[MeetingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_MeetingType_MeetingTypeId] ON [dbo].[Meeting] 
(
	[MeetingType_MeetingTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PreviousMeeting_MeetingId] ON [dbo].[Meeting] 
(
	[PreviousMeeting_MeetingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingItem]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingItem](
	[MeetingItemId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingItemDesc] [nvarchar](max) NOT NULL,
	[MeetingItemDueDate] [datetime] NOT NULL,
	[PersonResponsible_PersonId] [int] NULL,
 CONSTRAINT [PK_dbo.MeetingItem] PRIMARY KEY CLUSTERED 
(
	[MeetingItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PersonResponsible_PersonId] ON [dbo].[MeetingItem] 
(
	[PersonResponsible_PersonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingItemStatus]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingItemStatus](
	[MeetingItemStatusId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingItemStatusDate] [datetime] NOT NULL,
	[MeetingItemStatusLu_MeetingItemStatusLuId] [int] NULL,
	[Meeting_MeetingId] [int] NULL,
	[MeetingItem_MeetingItemId] [int] NULL,
 CONSTRAINT [PK_dbo.MeetingItemStatus] PRIMARY KEY CLUSTERED 
(
	[MeetingItemStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Meeting_MeetingId] ON [dbo].[MeetingItemStatus] 
(
	[Meeting_MeetingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_MeetingItem_MeetingItemId] ON [dbo].[MeetingItemStatus] 
(
	[MeetingItem_MeetingItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_MeetingItemStatusLu_MeetingItemStatusLuId] ON [dbo].[MeetingItemStatus] 
(
	[MeetingItemStatusLu_MeetingItemStatusLuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingAction]    Script Date: 10/17/2013 10:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingAction](
	[MeetingActionId] [int] IDENTITY(1,1) NOT NULL,
	[ActionDescription] [nvarchar](max) NOT NULL,
	[MeetingItemStatus_MeetingItemStatusId] [int] NULL,
	[PersonResponsible_PersonId] [int] NULL,
 CONSTRAINT [PK_dbo.MeetingAction] PRIMARY KEY CLUSTERED 
(
	[MeetingActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_MeetingItemStatus_MeetingItemStatusId] ON [dbo].[MeetingAction] 
(
	[MeetingItemStatus_MeetingItemStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PersonResponsible_PersonId] ON [dbo].[MeetingAction] 
(
	[PersonResponsible_PersonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_dbo.Meeting_dbo.Meeting_PreviousMeeting_MeetingId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[Meeting]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Meeting_dbo.Meeting_PreviousMeeting_MeetingId] FOREIGN KEY([PreviousMeeting_MeetingId])
REFERENCES [dbo].[Meeting] ([MeetingId])
GO
ALTER TABLE [dbo].[Meeting] CHECK CONSTRAINT [FK_dbo.Meeting_dbo.Meeting_PreviousMeeting_MeetingId]
GO
/****** Object:  ForeignKey [FK_dbo.Meeting_dbo.MeetingType_MeetingType_MeetingTypeId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[Meeting]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Meeting_dbo.MeetingType_MeetingType_MeetingTypeId] FOREIGN KEY([MeetingType_MeetingTypeId])
REFERENCES [dbo].[MeetingType] ([MeetingTypeId])
GO
ALTER TABLE [dbo].[Meeting] CHECK CONSTRAINT [FK_dbo.Meeting_dbo.MeetingType_MeetingType_MeetingTypeId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingItem_dbo.Person_PersonResponsible_PersonId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingItem]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingItem_dbo.Person_PersonResponsible_PersonId] FOREIGN KEY([PersonResponsible_PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[MeetingItem] CHECK CONSTRAINT [FK_dbo.MeetingItem_dbo.Person_PersonResponsible_PersonId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingItemStatus_dbo.Meeting_Meeting_MeetingId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingItemStatus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.Meeting_Meeting_MeetingId] FOREIGN KEY([Meeting_MeetingId])
REFERENCES [dbo].[Meeting] ([MeetingId])
GO
ALTER TABLE [dbo].[MeetingItemStatus] CHECK CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.Meeting_Meeting_MeetingId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingItemStatus_dbo.MeetingItem_MeetingItem_MeetingItemId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingItemStatus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.MeetingItem_MeetingItem_MeetingItemId] FOREIGN KEY([MeetingItem_MeetingItemId])
REFERENCES [dbo].[MeetingItem] ([MeetingItemId])
GO
ALTER TABLE [dbo].[MeetingItemStatus] CHECK CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.MeetingItem_MeetingItem_MeetingItemId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingItemStatus_dbo.MeetingItemStatusLu_MeetingItemStatusLu_MeetingItemStatusLuId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingItemStatus]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.MeetingItemStatusLu_MeetingItemStatusLu_MeetingItemStatusLuId] FOREIGN KEY([MeetingItemStatusLu_MeetingItemStatusLuId])
REFERENCES [dbo].[MeetingItemStatusLu] ([MeetingItemStatusLuId])
GO
ALTER TABLE [dbo].[MeetingItemStatus] CHECK CONSTRAINT [FK_dbo.MeetingItemStatus_dbo.MeetingItemStatusLu_MeetingItemStatusLu_MeetingItemStatusLuId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingAction_dbo.MeetingItemStatus_MeetingItemStatus_MeetingItemStatusId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingAction_dbo.MeetingItemStatus_MeetingItemStatus_MeetingItemStatusId] FOREIGN KEY([MeetingItemStatus_MeetingItemStatusId])
REFERENCES [dbo].[MeetingItemStatus] ([MeetingItemStatusId])
GO
ALTER TABLE [dbo].[MeetingAction] CHECK CONSTRAINT [FK_dbo.MeetingAction_dbo.MeetingItemStatus_MeetingItemStatus_MeetingItemStatusId]
GO
/****** Object:  ForeignKey [FK_dbo.MeetingAction_dbo.Person_PersonResponsible_PersonId]    Script Date: 10/17/2013 10:44:29 ******/
ALTER TABLE [dbo].[MeetingAction]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MeetingAction_dbo.Person_PersonResponsible_PersonId] FOREIGN KEY([PersonResponsible_PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[MeetingAction] CHECK CONSTRAINT [FK_dbo.MeetingAction_dbo.Person_PersonResponsible_PersonId]
GO
