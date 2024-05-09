CREATE TABLE [dbo].[ApplicationWorkflowSummaryStatement]
(
	[ApplicationWorkflowSummaryStatementId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ApplicationWorkflowId] INT NOT NULL,
	[DocumentFile] VARBINARY(MAX) NOT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationWorkflowSummaryStatement_ApplicationWorkflow] FOREIGN KEY ([ApplicationWorkflowId]) REFERENCES [ApplicationWorkflow]([ApplicationWorkflowId]), 
)

GO

CREATE INDEX [IX_ApplicationWorkflowSummaryStatement_ApplicationWorkflowId_DeletedFlag] ON [dbo].[ApplicationWorkflowSummaryStatement] ([ApplicationWorkflowId],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a working copy summary statement of an application workflow',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowSummaryStatementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application workflow',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Binary representation of a summary statement document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'DocumentFile'