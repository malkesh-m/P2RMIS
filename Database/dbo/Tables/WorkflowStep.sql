CREATE TABLE [dbo].[WorkflowStep]
(
	[WorkflowStepId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY NONCLUSTERED, 
	[WorkflowId] INT NOT NULL,
	[StepTypeId] INT NOT NULL,
    [StepName] VARCHAR(50) NOT NULL, 
    [StepOrder] INT NOT NULL, 
    [ActiveDefault] BIT NOT NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_WorkflowStep_Workflow] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow]([WorkflowId]), 
    CONSTRAINT [FK_WorkflowStep_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId]), 
    CONSTRAINT [UN_WorkflowStep_WorkflowId_StepOrder] UNIQUE ([WorkflowId],[StepOrder], [DeletedDate])
)

GO

CREATE CLUSTERED INDEX [IX_WorkflowStep_WorkflowIdStepOrder] ON [dbo].[WorkflowStep] ([WorkflowId], [StepOrder]) 

GO
GRANT SELECT
    ON OBJECT::[dbo].[WorkflowStep] TO [web-p2rmis]
    AS [dbo];