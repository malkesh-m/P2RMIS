CREATE TABLE [dbo].[MechanismTemplateElementScoring]
(
	[MechanismTemplateScoringId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[MechanismTemplateElementId] INT NOT NULL,
    [ClientScoringId] INT NOT NULL, 
    [StepTypeId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MechanismTemplateElementScoring_ClientScoringScale] FOREIGN KEY ([ClientScoringId]) REFERENCES [ClientScoringScale]([ClientScoringId]), 
    CONSTRAINT [FK_MechanismTemplateElementScoring_MechanismTemplateElement] FOREIGN KEY ([MechanismTemplateElementId]) REFERENCES [MechanismTemplateElement]([MechanismTemplateElementId]), 
    CONSTRAINT [AK_MechanismTemplateElementScoring_MechanismTemplateElementId_StepTypeId] UNIQUE ([MechanismTemplateElementId], [StepTypeId], [DeletedDate]), 
    CONSTRAINT [FK_MechanismTemplateElementScoring_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId])
)

GO

CREATE INDEX [IX_MechanismTemplateElementScoring_ClientScoringId_StepName] ON [dbo].[MechanismTemplateElementScoring] ([ClientScoringId], [StepTypeId])

GO

CREATE INDEX [IX_MechanismTemplateElementScoring_MechanismTemplateId_ClientScoringId] ON [dbo].[MechanismTemplateElementScoring] ([MechanismTemplateScoringId], [ClientScoringId])

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MechanismTemplateElementScoring] TO [web-p2rmis]
    AS [dbo];
