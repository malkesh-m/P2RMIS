CREATE TABLE [dbo].[ApplicationWorkflowStepAssignment]
(
	[ApplicationWorkflowStepAssignmentId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ApplicationWorkflowStepId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [AssignmentId] INT NOT NULL, 
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_StepAssignment_ApplicationWorkflowStep] FOREIGN KEY ([ApplicationWorkflowStepId]) REFERENCES [ApplicationWorkflowStep]([ApplicationWorkflowStepId]), 
    CONSTRAINT [FK_StepAssignment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [UN_StepAssignment] UNIQUE (ApplicationWorkflowStepId, UserId, AssignmentId, [DeletedDate]), 
    CONSTRAINT [FK_StepAssignment_AssignmentLookup] FOREIGN KEY ([AssignmentId]) REFERENCES [AssignmentType]([AssignmentTypeId])
)

GO

CREATE CLUSTERED INDEX [IX_StepAssignment_ApplicationWorkflowStepId_AssignmentId] ON [dbo].[ApplicationWorkflowStepAssignment] ([ApplicationWorkflowStepId], [UserId])

GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStepAssignment] TO [web-p2rmis]
    AS [dbo];