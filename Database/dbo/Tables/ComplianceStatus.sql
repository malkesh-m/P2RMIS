CREATE TABLE [dbo].[ComplianceStatus]
(
	[ComplianceStatusId] INT NOT NULL PRIMARY KEY, 
    [ComplianceStatusLabel] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label describing a compliance status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ComplianceStatus',
    @level2type = N'COLUMN',
    @level2name = N'ComplianceStatusLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a compliance status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ComplianceStatus',
    @level2type = N'COLUMN',
    @level2name = N'ComplianceStatusId'