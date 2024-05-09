CREATE TABLE [dbo].[ApplicationWorkflowStep]
(
	[ApplicationWorkflowStepId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ApplicationWorkflowId] INT NOT NULL, 
	[StepTypeId] INT NOT NULL,
    [StepName] VARCHAR(50) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    [StepOrder] INT NOT NULL, 
	[Resolution] BIT NOT NULL DEFAULT 0,
    [ResolutionDate] datetime2(0) NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationWorkflowStep_ApplicationWorkflow] FOREIGN KEY ([ApplicationWorkflowId]) REFERENCES [ApplicationWorkflow]([ApplicationWorkflowId]), 
    CONSTRAINT [FK_ApplicationWorkflowStep_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId])
)

GO

CREATE CLUSTERED INDEX [IX_ApplicationWorkflowStep_ApplicationWorkflowId_StepOrder] ON [dbo].[ApplicationWorkflowStep] ([ApplicationWorkflowId], [StepOrder])

GO

CREATE INDEX [IX_ApplicationWorkflowStep_StepName] ON [dbo].[ApplicationWorkflowStep] ([StepName])
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStep] TO [web-p2rmis]
    AS [dbo];