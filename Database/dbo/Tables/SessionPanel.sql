CREATE TABLE [dbo].[SessionPanel]
(
	[SessionPanelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LegacyPanelId] INT NULL DEFAULT next value for seq_SessionPanel_LegacyPanelId, 
    [MeetingSessionId] INT NULL, 
    [PanelAbbreviation] VARCHAR(20) NOT NULL, 
    [PanelName] VARCHAR(250) NOT NULL, 
    [StartDate] datetime2(0) NULL, 
    [EndDate] datetime2(0) NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_SessionPanel_MeetingSession] FOREIGN KEY ([MeetingSessionId]) REFERENCES [MeetingSession]([MeetingSessionId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a panel for mapping',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'LegacyPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting''s session',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'MeetingSessionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for a panel name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'PanelAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name of a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'PanelName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date panel begins',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date panel ends',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Review panel used to group user''s and applications',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SessionPanel',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[SessionPanel] TO [web-p2rmis]
    AS [dbo];
GO

CREATE NONCLUSTERED INDEX [IX_SessionPanel_SessionPanelId_DeletedFlag] ON [dbo].[SessionPanel]
(
	[SessionPanelId] ASC,
	[DeletedFlag] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_SessionPanel_DeletedFlag_SessionPanelId_MeetingSessionId] ON [dbo].[SessionPanel]
(
	[DeletedFlag] ASC,
	[SessionPanelId] ASC,
	[MeetingSessionId] ASC
)
INCLUDE ( 	[PanelAbbreviation],
	[PanelName],
	[StartDate],
	[EndDate]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
