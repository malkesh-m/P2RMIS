CREATE TABLE [dbo].[CommunicationLog]
(
	[CommunicationLogId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [SessionPanelId] INT NOT NULL, 
    [Subject] VARCHAR(300) NOT NULL, 
    [Message] VARCHAR(MAX) NOT NULL, 
    [BCC] VARCHAR(500) NULL, 
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_CommunicationLog_SessionPanel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a communication email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The subject of the email message',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'Subject'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The body of the email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'Message'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The BCC text of the email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLog',
    @level2type = N'COLUMN',
    @level2name = N'BCC'
GO

CREATE INDEX [IX_CommunicationLog_SessionPanelId] ON [dbo].[CommunicationLog] ([SessionPanelId]) WHERE [DeletedFlag] = 0
