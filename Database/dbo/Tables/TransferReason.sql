CREATE TABLE [dbo].[TransferReason]
(
	[TransferReasonId] INT NOT NULL PRIMARY KEY, 
    [Reason] VARCHAR(250) NOT NULL, 
    [TransferType] VARCHAR(20) NOT NULL,
	[SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a transfer reason',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TransferReason',
    @level2type = N'COLUMN',
    @level2name = 'TransferReasonId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The reason an application was transferred',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TransferReason',
    @level2type = N'COLUMN',
    @level2name = N'Reason'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of transfer this reason relates to',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TransferReason',
    @level2type = N'COLUMN',
    @level2name = N'TransferType'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display order of the description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TransferReason',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
