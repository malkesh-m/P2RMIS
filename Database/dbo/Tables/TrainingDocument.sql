CREATE TABLE [dbo].[TrainingDocument]
(
	[TrainingDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
	[TrainingCategoryId] INT NOT NULL,
    [DocumentName] VARCHAR(200) NOT NULL, 
    [DocumentDescription] VARCHAR(1000) NULL, 
    [FileLocation] VARCHAR(500) NOT NULL, 
    [ExternalAddressFlag] BIT NOT NULL DEFAULT 0,
	[ActiveFlag] BIT NOT NULL DEFAULT 1,
	[LegacyTrId] INT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_TrainingDocument_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_TrainingDocument_TrainingCategory] FOREIGN KEY ([TrainingCategoryId]) REFERENCES [TrainingCategory]([TrainingCategoryId]), 
)

GO

CREATE INDEX [IX_TrainingDocument_ProgramYearId] ON [dbo].[TrainingDocument] ([ProgramYearId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'TrainingDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program fiscal year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display name for a document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text describing the purpose of a document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The path or url (if external) of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'FileLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'True if the document is stored on a server other than P2RMIS; otherwise false',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'ExternalAddressFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contains document (files) used for training purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'TrainingCategoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'True if the document is active and should be displayed to reviewers; otherwise false',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a training document for mapping purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'LegacyTrId'
	GO
	GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[TrainingDocument] TO [web-p2rmis]
    AS [dbo];
GO