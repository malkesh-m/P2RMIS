CREATE TABLE [dbo].[CommunicationLogRecipient]
(
	[CommunicationLogRecipientId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [CommunicationLogId] INT NOT NULL, 
    [CommunicationLogRecipientTypeId] INT NOT NULL, 
    [PanelUserAssignmentId] INT NOT NULL, 
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_CommunicationLogRecipient_CommunicationLog] FOREIGN KEY ([CommunicationLogId]) REFERENCES [CommunicationLog]([CommunicationLogId]), 
    CONSTRAINT [FK_CommunicationLogRecipient_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]), 
    CONSTRAINT [FK_CommunicationLogRecipient_CommunicationLogRecipientType] FOREIGN KEY ([CommunicationLogRecipientTypeId]) REFERENCES [CommunicationLogRecipientType]([CommunicationLogRecipientTypeId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The unique identifer for a recipient/creator of the email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipient',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogRecipientId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a communication email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipient',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the type of recipient that person is on the email',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipient',
    @level2type = N'COLUMN',
    @level2name = N'CommunicationLogRecipientTypeId'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user assignment to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CommunicationLogRecipient',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
