CREATE TABLE [dbo].[PeerReviewContentType]
(
	[PeerReviewContentTypeId] INT NOT NULL PRIMARY KEY, 
    [ContentType] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [DefaultFlag] BIT NOT NULL DEFAULT 0, 
    [AccessMethod] VARCHAR(20) NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Method in which the content is accessed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewContentType',
    @level2type = N'COLUMN',
    @level2name = N'AccessMethod'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the content type is default for a new document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewContentType',
    @level2type = N'COLUMN',
    @level2name = N'DefaultFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the content type displays in dropdowns',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewContentType',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label for the content type to display to user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewContentType',
    @level2type = N'COLUMN',
    @level2name = N'ContentType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a peer review content type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PeerReviewContentType',
    @level2type = N'COLUMN',
    @level2name = N'PeerReviewContentTypeId'