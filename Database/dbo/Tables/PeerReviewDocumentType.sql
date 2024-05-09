CREATE TABLE [dbo].[PeerReviewDocumentType]
(
	[PeerReviewDocumentTypeId] INT NOT NULL PRIMARY KEY, 
    [DocumentType] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Peer review document type identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentType',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewDocumentTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for the document type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentType',
    @level2type = N'COLUMN',
    @level2name = N'DocumentType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the document type displays in dropdowns',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewDocumentType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'