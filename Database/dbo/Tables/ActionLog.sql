CREATE TABLE [dbo].[ActionLog]
(
	[ActionLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ActionLogReasonId] INT NOT NULL, 
    [Message] VARCHAR(8000) NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [FK_ActionLog_ActionLogReason] FOREIGN KEY ([ActionLogReasonId]) REFERENCES [ActionLogReason]([ActionLogReasonId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an action log entry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ActionLog',
    @level2type = N'COLUMN',
    @level2name = N'ActionLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an action description.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ActionLog',
    @level2type = N'COLUMN',
    @level2name = N'ActionLogReasonId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text logged',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ActionLog',
    @level2type = N'COLUMN',
    @level2name = N'Message'