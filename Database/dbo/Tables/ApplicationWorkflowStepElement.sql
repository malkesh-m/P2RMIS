CREATE TABLE [dbo].[ApplicationWorkflowStepElement]
(
	[ApplicationWorkflowStepElementId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ApplicationWorkflowStepId] INT NOT NULL, 
    [ApplicationTemplateElementId] INT NOT NULL, 
	[AccessLevelId] INT NOT NULL,
	[ClientScoringId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationWorkflowStepElement_ApplicationWorkflowStep] FOREIGN KEY ([ApplicationWorkflowStepId]) REFERENCES [ApplicationWorkflowStep]([ApplicationWorkflowStepId]), 
    CONSTRAINT [FK_ApplicationWorkflowStepElement_ApplicationTemplateElement] FOREIGN KEY ([ApplicationTemplateElementId]) REFERENCES [ApplicationTemplateElement]([ApplicationTemplateElementId]), 
    CONSTRAINT [FK_ApplicationWorkflowStepElement_AccessLevel] FOREIGN KEY ([AccessLevelId]) REFERENCES [AccessLevel]([AccessLevelId]), 
    CONSTRAINT [UN_ApplicationWorkflowStepElement_ApplicationWorkflowStepId_ApplicationTemplateElementId] UNIQUE (ApplicationWorkflowStepId,ApplicationTemplateElementId, [DeletedDate]), 
    CONSTRAINT [FK_ApplicationWorkflowStepElement_ClientScoringScale] FOREIGN KEY ([ClientScoringId]) REFERENCES [ClientScoringScale]([ClientScoringId]),

)

GO

CREATE CLUSTERED INDEX [IX_ApplicationWorkflowStepElement_ApplicationWorkflowStepId_ApplicationTemplateElementId] ON [dbo].[ApplicationWorkflowStepElement] ([ApplicationWorkflowStepId],[ApplicationTemplateElementId])
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStepElement] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_ApplicationWorkflowStepElement_ApplicationTemplateElementId] ON [dbo].[ApplicationWorkflowStepElement] ([ApplicationTemplateElementId],[DeletedFlag]) INCLUDE ([ApplicationWorkflowStepElementId],[ApplicationWorkflowStepId],[ClientScoringId])
