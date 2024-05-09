CREATE TABLE [dbo].[ApplicationSummaryLog]
(
	[ApplicationSummaryLogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationWorkflowStepId] INT NULL, 
    [UserId] INT NOT NULL, 
    [CompletedFlag] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationSummaryLog_ApplicationWorkflowStep] FOREIGN KEY ([ApplicationWorkflowStepId]) REFERENCES [ApplicationWorkflowStep]([ApplicationWorkflowStepId]), 
    CONSTRAINT [FK_ApplicationSummaryLog_User] FOREIGN KEY (UserId) REFERENCES [User](UserID) 
)
