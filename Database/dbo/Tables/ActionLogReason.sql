CREATE TABLE [dbo].[ActionLogReason]
(
	[ActionLogReasonId] INT NOT NULL PRIMARY KEY, 
    [Reason] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an action log reason',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ActionLogReason',
    @level2type = N'COLUMN',
    @level2name = N'ActionLogReasonId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Action reason',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ActionLogReason',
    @level2type = N'COLUMN',
    @level2name = N'Reason'