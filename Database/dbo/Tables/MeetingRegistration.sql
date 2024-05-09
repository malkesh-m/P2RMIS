CREATE TABLE [dbo].[MeetingRegistration]
(
	[MeetingRegistrationId] INT NOT NULL PRIMARY KEY IDENTITY,
	[PanelUserAssignmentId] INT NULL, 
	[SessionUserAssignmentId] INT NULL,
    [RegistrationSubmittedFlag] BIT NOT NULL DEFAULT 0, 
    [RegistrationSubmittedDate] DATETIME2(0) NULL, 
	[LegacyMrId] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MeetingRegistration_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]), 
    CONSTRAINT [CK_MeetingRegistration_ID_Exists] CHECK (PanelUserAssignmentId IS NOT NULL OR SessionUserAssignmentId IS NOT NULL), 
    CONSTRAINT [FK_MeetingRegistration_SessionUserAssignment] FOREIGN KEY ([SessionUserAssignmentId]) REFERENCES [SessionUserAssignment]([SessionUserAssignmentId]), 
)

GO

CREATE INDEX [IX_MeetingRegistration_PanelUserAssignment] ON [dbo].[MeetingRegistration] ([PanelUserAssignmentId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistration',
    @level2type = N'COLUMN',
    @level2name = N'MeetingRegistrationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel user assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistration',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether registration has been submitted to meeting/travel for approval',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistration',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationSubmittedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date registration was submitted',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistration',
    @level2type = N'COLUMN',
    @level2name = N'RegistrationSubmittedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a meeting registration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MeetingRegistration',
    @level2type = N'COLUMN',
    @level2name = N'LegacyMrId'
	GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MeetingRegistration] TO [web-p2rmis]
    AS [dbo];
GO