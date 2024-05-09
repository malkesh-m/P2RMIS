CREATE TABLE [dbo].[PeerReviewDocument]
(
	[PeerReviewDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [ClientProgramId] INT NULL, 
    [FiscalYear] VARCHAR(8) NULL, 
    [PeerReviewDocumentTypeId] INT NOT NULL, 
    [PeerReviewContentTypeId] INT NOT NULL, 
    [TrainingCategoryId] INT NULL, 
    [Heading] VARCHAR(100) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [ContentUrl] VARCHAR(500) NULL, 
    [ContentFileLocation] VARCHAR(500) NULL, 
	[FileType] varchar(10) NULL,
    [ArchivedFlag] BIT NOT NULL DEFAULT 0, 
    [ArchiveDate] DATETIME2(0) NULL, 
    [ArchivedBy] INT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PeerReviewDocument_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_PeerReviewDocument_ClientProgram] FOREIGN KEY ([ClientProgramId]) REFERENCES [ClientProgram]([ClientProgramId]), 
    CONSTRAINT [FK_PeerReviewDocument_PeerReviewDocumentType] FOREIGN KEY ([PeerReviewDocumentTypeId]) REFERENCES [PeerReviewDocumentType]([PeerReviewDocumentTypeId]), 
    CONSTRAINT [FK_PeerReviewDocument_PeerReviewContentType] FOREIGN KEY ([PeerReviewContentTypeId]) REFERENCES [PeerReviewContentType]([PeerReviewContentTypeId]), 
    CONSTRAINT [FK_PeerReviewDocument_TrainingCategory] FOREIGN KEY ([TrainingCategoryId]) REFERENCES [TrainingCategory]([TrainingCategoryId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a non-application peer review document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system client/tenant',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program.  Null represents all.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ClientProgramId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a year program offering. Null represents all years.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = 'FiscalYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review file content type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewContentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training category associated with the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'TrainingCategoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label under which document displays',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'Heading'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description explaining purpose of document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Location for external web based documents or videos',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ContentUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Location for files uploaded to system',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ContentFileLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the document has been archived',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ArchivedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the document was archived',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = 'ArchiveDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'UserId of user responsible for archiving the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocument',
    @level2type = N'COLUMN',
    @level2name = N'ArchivedBy'