CREATE TABLE [dbo].[ProgramMeeting]
(
	[ProgramMeetingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [ClientMeetingId] INT NOT NULL,
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramMeeting_ClientMeeting] FOREIGN KEY (ClientMeetingId) REFERENCES ClientMeeting(ClientMeetingId), 
    CONSTRAINT [FK_ProgramMeeting_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES ProgramYear(ProgramYearId)
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program assignment to a meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeeting',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMeetingId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program fiscal year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeeting',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMeeting',
    @level2type = N'COLUMN',
    @level2name = N'ClientMeetingId'
GO

CREATE UNIQUE INDEX [IX_ProgramMeeting_ProgramYearId_ClientMeetingId] ON [dbo].[ProgramMeeting] ([ProgramYearId],[ClientMeetingId],[DeletedDate])
