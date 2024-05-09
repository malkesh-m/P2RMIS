CREATE TABLE [dbo].[CommunicationLogAttachment]
(
	[CommunicationLogAttachmentId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [CommunicationLogId] INT NOT NULL, 
    [AttachmentFileName] VARCHAR(100) NOT NULL, 
    [AttachmentLocation] VARCHAR(100) NOT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_CommunicationLogAttachment_CommunicationLog] FOREIGN KEY ([CommunicationLogId]) REFERENCES [CommunicationLog]([CommunicationLogId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The unique identifier of an attachment in a email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogAttachment',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogAttachmentId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a communication email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogAttachment',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The filename of the attachment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogAttachment',
    @level2type = N'COLUMN',
    @level2name = N'AttachmentFileName'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The location of the attachment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogAttachment',
    @level2type = N'COLUMN',
    @level2name = N'AttachmentLocation'