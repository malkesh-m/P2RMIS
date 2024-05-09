CREATE TABLE [dbo].[MechanismTemplate]
(
	[MechanismTemplateId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[ProgramMechanismId] INT NOT NULL,
	[ReviewStatusId] INT NULL,
	[ReviewStageId] INT NOT NULL,
	[SummaryDocumentId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MechanismTemplate_ReviewStatus] FOREIGN KEY ([ReviewStatusId]) REFERENCES [ReviewStatus]([ReviewStatusId]), 
    --Once NOT NULLs FIXED CONSTRAINT [UN_MechanismTemplate_ProgramMechanismId_ReviewStageId_ReviewStatusId] UNIQUE ([ProgramMechanismId],[ReviewStageId],[ReviewStatusId]), 
    CONSTRAINT [FK_MechanismTemplate_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_MechanismTemplate_ReviewStage] FOREIGN KEY ([ReviewStageId]) REFERENCES [ReviewStage]([ReviewStageId]), 
    CONSTRAINT [FK_MechanismTemplate_SummaryDocument] FOREIGN KEY ([SummaryDocumentId]) REFERENCES [SummaryDocument]([SummaryDocumentId]) 
)

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a mechanism template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplate',
    @level2type = N'COLUMN',
    @level2name = N'MechanismTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an award mechansim offering',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review status',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStatusId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MechanismTemplate] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the file used to generate the summary document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplate',
    @level2type = N'COLUMN',
    @level2name = N'SummaryDocumentId'
GO


CREATE INDEX [IX_MechanismTemplate_ProgramMechanismId_ReviewStageId] ON [dbo].[MechanismTemplate] ([ProgramMechanismId],[ReviewStageId]) WHERE (DeletedFlag = 0)
