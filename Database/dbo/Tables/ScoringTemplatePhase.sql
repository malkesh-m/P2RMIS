CREATE TABLE [dbo].[ScoringTemplatePhase]
(
	[ScoringTemplatePhaseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ScoringTemplateId] INT NOT NULL,
	[StepTypeId] INT NOT NULL, 
	[StepOrder] INT NOT NULL,
	[OverallClientScoringId] INT NOT NULL,
	[CriteriaClientScoringId] INT NOT NULL, 
    CONSTRAINT [FK_ScoringTemplatePhase_ScoringTemplateId] FOREIGN KEY ([ScoringTemplateId]) REFERENCES [ScoringTemplate]([ScoringTemplateId]), 
    CONSTRAINT [FK_ScoringTemplatePhase_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId]), 
    CONSTRAINT [FK_ScoringTemplatePhase_OverallClientScoringScale] FOREIGN KEY ([OverallClientScoringId]) REFERENCES [ClientScoringScale]([ClientScoringId]), 
    CONSTRAINT [FK_ScoringTemplatePhase_CriteriaClientScoringScale] FOREIGN KEY ([CriteriaClientScoringId]) REFERENCES [ClientScoringScale]([ClientScoringId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring template phase',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'ScoringTemplatePhaseId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring template definition',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'ScoringTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Workflow step stype identifier for associated phase',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'StepTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the phase occurs',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'StepOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client scoring scale identifier for overall criteria',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'OverallClientScoringId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client scoring scale identifier for non-overall scored criteria',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplatePhase',
    @level2type = N'COLUMN',
    @level2name = N'CriteriaClientScoringId'
GO
