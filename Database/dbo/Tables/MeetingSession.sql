CREATE TABLE [dbo].[MeetingSession]
(
	[MeetingSessionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LegacySessionId] VARCHAR(10) NULL, 
    [ClientMeetingId] INT NOT NULL, 
    [SessionAbbreviation] VARCHAR(20) NOT NULL, 
    [SessionDescription] VARCHAR(100) NOT NULL, 
    [StartDate] datetime2(0) NULL, 
    [EndDate] datetime2(0) NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MeetingSession_Meeting] FOREIGN KEY ([ClientMeetingId]) REFERENCES [ClientMeeting]([ClientMeetingId]), 
    CONSTRAINT [CK_MeetingSession_EndDateGreaterStartDate] CHECK (EndDate >= StartDate)
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel session within a meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'MeetingSessionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = 'ClientMeetingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Shortened internal name of a session',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'SessionAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name of a session',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'SessionDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date session begins',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date session ends',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO

CREATE INDEX [IX_MeetingSession_StartDate_EndDate] ON [dbo].[MeetingSession] ([StartDate], [EndDate])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier of a session for mapping',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = N'COLUMN',
    @level2name = N'LegacySessionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Session component of a review meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingSession',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MeetingSession] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_MeetingSession_SessionAbbreviation] ON [dbo].[MeetingSession] ([SessionAbbreviation])

GO

CREATE NONCLUSTERED INDEX [IX_MeetingSession_DeletedFlag_MeetingSessionId_ClientMeetingId] ON [dbo].[MeetingSession]
(
	[DeletedFlag] ASC,
	[MeetingSessionId] ASC,
	[ClientMeetingId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
