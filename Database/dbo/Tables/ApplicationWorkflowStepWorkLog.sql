CREATE TABLE [dbo].[ApplicationWorkflowStepWorkLog]
(
	[ApplicationWorkflowStepWorkLogId] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ApplicationWorkflowStepId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [CheckInUserId] INT NULL, 
    [CheckOutDate] datetime2(0) NOT NULL, 
    [CheckInDate] datetime2(0) NULL, 
	[CheckoutBackupFile] varbinary(max) NULL,
	[CheckinBackupFile] varbinary(max) NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_WorkLog_ApplicationWorkflowStep] FOREIGN KEY ([ApplicationWorkflowStepId]) REFERENCES [ApplicationWorkflowStep]([ApplicationWorkflowStepId]), 
    CONSTRAINT [FK_WorkLog_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_WorkLog_CheckInUser] FOREIGN KEY ([CheckInUserId]) REFERENCES [User]([UserId])
)

GO

CREATE CLUSTERED INDEX [IX_WorkLog_ApplicationWorkflowStepId_UserId] ON [dbo].[ApplicationWorkflowStepWorkLog] ([ApplicationWorkflowStepId], [CheckOutDate])

GO

CREATE INDEX [IX_WorkLog_UserId_CheckOutDate] ON [dbo].[ApplicationWorkflowStepWorkLog] ([UserId], [CheckOutDate])

GO

CREATE INDEX [IX_WorkLog_CheckOutDate_CheckInDate] ON [dbo].[ApplicationWorkflowStepWorkLog] ([CheckOutDate], [CheckInDate]) INCLUDE ([ApplicationWorkflowStepId], [UserId]) WHERE ([DeletedFlag] = 0)

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationWorkflowStepWorkLog] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application workflow step work log entry.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowStepWorkLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date a check out took place for an application workflow step',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'CheckOutDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date a check in took place for an application workflow step',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'CheckInDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Binary representation of the document checked out',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'CheckoutBackupFile'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the user checking out the transaction',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the user checking in the transaction',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'CheckInUserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the application workflow step associated with the transaction',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationWorkflowStepId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Binary representation of the document checked in',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationWorkflowStepWorkLog',
    @level2type = N'COLUMN',
    @level2name = N'CheckinBackupFile'