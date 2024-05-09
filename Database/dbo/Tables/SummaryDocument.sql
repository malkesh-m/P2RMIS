CREATE TABLE [dbo].[SummaryDocument]
(
	[SummaryDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DocumentFile] VARCHAR(50) NOT NULL, 
    [DocumentName] VARCHAR(50) NOT NULL, 
    [DocumentDescription] VARCHAR(500) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a summary document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryDocument',
    @level2type = N'COLUMN',
    @level2name = N'SummaryDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'File name of the SSRS report used to generate the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentFile'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SummaryDocument',
    @level2type = N'COLUMN',
    @level2name = N'DocumentDescription'