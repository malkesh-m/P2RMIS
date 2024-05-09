CREATE TABLE [dbo].[ProgramMechanismSummaryStatement]
(
	[ProgramMechanismSummaryStatementId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProgramMechanismId] INT NOT NULL,
	[ReviewStatusId] INT NOT NULL,
	[TemplateLocation] VARCHAR(100) NULL,
	[StoredProcedureName] VARCHAR(50) NULL, 
    [ClientSummaryTemplateId] INT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramMechanismSummaryStatement_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_ProgramMechanismSummaryStatement_ReviewStatus] FOREIGN KEY ([ReviewStatusId]) REFERENCES [ReviewStatus]([ReviewStatusId]), 
    CONSTRAINT [FK_ProgramMechanismSummaryStatement_ClientSummaryTemplateId] FOREIGN KEY ([ClientSummaryTemplateId]) REFERENCES [ClientSummaryTemplate]([ClientSummaryTemplateId]) 
)

GO

CREATE INDEX [IX_ProgramMechanismSummaryStatement_ProgramMechanismId_ReviewStatusId] ON [dbo].[ProgramMechanismSummaryStatement] ([ProgramMechanismId],[ReviewStatusId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program mechanism summary statement assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismSummaryStatementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program mechanism',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application review status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Obsolete - See ClientSummaryTemplate - File location where the summary statement template is stored',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'TemplateLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Obsolete - See ClientSummaryTemplate - Stored procedure that will be executed to populate the template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'StoredProcedureName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client-specific summary statement template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramMechanismSummaryStatement',
    @level2type = N'COLUMN',
    @level2name = N'ClientSummaryTemplateId'