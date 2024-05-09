CREATE TABLE [dbo].[ApplicationWorkflowStepElementContentHistory]
(
	[ApplicationWorkflowStepElementContenHistorytId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ApplicationWorkflowStepWorkLog] INT NOT NULL,
	[ApplicationWorkflowStepElementId] INT NOT NULL,
    [Score] DECIMAL(4, 1) NULL, 
    [ContentText] NVARCHAR(MAX) NULL, 
    [Abstain] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NOT NULL, 
    [ModifiedBy] INT NOT NULL, 
    [ModifiedDate] datetime2(0) NOT NULL, 
    CONSTRAINT [FK_ApplicationWorkflowStepElementContentHistory_ApplicationWorkflowStepWorkLog] FOREIGN KEY ([ApplicationWorkflowStepWorkLog]) REFERENCES [ApplicationWorkflowStepWorkLog]([ApplicationWorkflowStepWorkLogId])
)

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStepElementContentHistory] TO [web-p2rmis]
    AS [dbo];