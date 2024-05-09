CREATE TABLE [dbo].[SessionPhase]
(
	[SessionPhaseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MeetingSessionId] INT NOT NULL, 
    [StepTypeId] INT NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [StartDate] DATETIME2(0) NOT NULL, 
    [EndDate] DATETIME2(0) NOT NULL,
    [ReopenDate] DATETIME2(0) NULL, 
    [CloseDate] DATETIME2(0) NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_SessionPhase_MeetingSession] FOREIGN KEY ([MeetingSessionId]) REFERENCES [MeetingSession]([MeetingSessionId]), 
    CONSTRAINT [FK_SessionPhase_StepType] FOREIGN KEY ([StepTypeId]) REFERENCES [StepType]([StepTypeId])
)

GO

CREATE UNIQUE INDEX [IX_SessionPhase_MeetingSessionId_StepTypeId_DeletedDate] ON [dbo].[SessionPhase] ([MeetingSessionId], StepTypeId, DeletedDate)
