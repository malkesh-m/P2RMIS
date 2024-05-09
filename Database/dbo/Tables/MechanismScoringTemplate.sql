CREATE TABLE [dbo].[MechanismScoringTemplate]
(
	[MechanismScoringTemplateId] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProgramMechanismId] INT NOT NULL, 
    [ScoringTemplateId] INT NOT NULL, 
    [CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MechanismScoringTemplate_ProgramMechanism] FOREIGN KEY ([ProgramMechanismId]) REFERENCES [ProgramMechanism]([ProgramMechanismId]), 
    CONSTRAINT [FK_MechanismScoringTemplate_ScoringTemplate] FOREIGN KEY ([ScoringTemplateId]) REFERENCES [ScoringTemplate]([ScoringTemplateId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring template mechanism association',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'MechanismScoringTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program mechanism offering',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramMechanismId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring template definition',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ScoringTemplateId'
GO

CREATE INDEX [IX_MechanismScoringTemplate_ProgramMechanismId_ScoringTemplateId] ON [dbo].[MechanismScoringTemplate] ([ProgramMechanismId],[ScoringTemplateId])
