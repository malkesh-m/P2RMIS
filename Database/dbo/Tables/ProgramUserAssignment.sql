CREATE TABLE [dbo].[ProgramUserAssignment]
(
	[ProgramUserAssignmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NOT NULL, 
	[LegacyParticipantId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramUserAssignment_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_ProgramUserAssignment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [UN_ProgramUserAssignment_ProgramYearId_UserId_ClientParticipationId] UNIQUE ([ProgramYearId], [UserId], [ClientParticipantTypeId], [DeletedDate]), 
    CONSTRAINT [FK_ProgramUserAssignment_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user program assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ProgramUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a yearly instance of a program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = N'COLUMN',
    @level2name = 'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a participation type for a program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a participation instance from legacy system',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = N'COLUMN',
    @level2name = N'LegacyParticipantId'
GO

GO

CREATE INDEX [IX_ProgramUserAssignment_UserId_ProgramYearId] ON [dbo].[ProgramUserAssignment] ([UserId], [ProgramYearId])

GO

CREATE INDEX [IX_ProgramUserAssignment_ProgramYearId] ON [dbo].[ProgramUserAssignment] ([ProgramYearId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User assignments to a review program',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramUserAssignment',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ProgramUserAssignment] TO [web-p2rmis]
    AS [dbo];