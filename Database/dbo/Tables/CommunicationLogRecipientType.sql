CREATE TABLE [dbo].[CommunicationLogRecipientType]
(
	[CommunicationLogRecipientTypeId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [RecipientType] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for recipient type for an email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipientType',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogRecipientTypeId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The recipient type for an email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipientType',
    @level2type = N'COLUMN',
    @level2name = N'RecipientType'