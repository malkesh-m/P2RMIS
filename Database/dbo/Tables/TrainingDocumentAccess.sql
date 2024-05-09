CREATE TABLE [dbo].[TrainingDocumentAccess]
(
	[TrainingDocumentAccessId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TrainingDocumentId] INT NOT NULL, 
    [MeetingTypeId] INT NOT NULL, 
    [ClientParticipantTypeId] INT NOT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_TrainingDocumentAccess_TrainingDocument] FOREIGN KEY ([TrainingDocumentId]) REFERENCES [TrainingDocument]([TrainingDocumentId]), 
    CONSTRAINT [FK_TrainingDocumentAccess_MeetingType] FOREIGN KEY ([MeetingTypeId]) REFERENCES [MeetingType]([MeetingTypeId]), 
    CONSTRAINT [FK_TrainingDocumentAccess_ClientParticipantType] FOREIGN KEY ([ClientParticipantTypeId]) REFERENCES [ClientParticipantType]([ClientParticipantTypeId]), 
)

GO

CREATE INDEX [IX_TrainingDocumentAccess_TrainingDocumentId_MeetingTypeId_ClientParticipantTypeId] ON [dbo].[TrainingDocumentAccess] ([TrainingDocumentId],[MeetingTypeId],[ClientParticipantTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a meeting type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client participant type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'ClientParticipantTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'TrainingDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training document access member',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocumentAccess',
    @level2type = N'COLUMN',
    @level2name = N'TrainingDocumentAccessId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contains access membership for training documents',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocumentAccess',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[TrainingDocumentAccess] TO [web-p2rmis]
    AS [dbo];