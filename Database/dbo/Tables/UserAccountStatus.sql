CREATE TABLE [dbo].[UserAccountStatus]
(
	[UserAccountStatusId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [AccountStatusId] INT NOT NULL, 
    [AccountStatusReasonId] INT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserAccountStatus_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserAccountStatus_AccountStatusId] FOREIGN KEY ([AccountStatusId]) REFERENCES [AccountStatus]([AccountStatusId]), 
    CONSTRAINT [FK_UserAccountStatus_AccountStatusReasonId] FOREIGN KEY ([AccountStatusReasonId]) REFERENCES [AccountStatusReason]([AccountStatusReasonId]) 
)

GO

CREATE INDEX [IX_UserAccountStatus_UserId] ON [dbo].[UserAccountStatus] ([UserId])

GO

CREATE TRIGGER [dbo].[Trigger_UserAccountStatus_AccountStatus_Sync]
    ON [dbo].[UserAccountStatus]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON
		IF EXISTS(Select * From inserted i 
			LEFT OUTER JOIN deleted d ON i.UserID = d.UserID AND (i.AccountStatusId <> d.AccountStatusId OR i.AccountStatusReasonId <> d.AccountStatusReasonId))
		BEGIN
			--Add to user change log
			--Update
			IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
			INSERT INTO [UserAccountStatusChangeLog] 
			([UserId]
			   ,[NewAccountStatusId]
			   ,[OldAccountStatusId]
			   ,[NewAccountStatusReasonId]
			   ,[OldAccountStatusReasonId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			SELECT inserted.UserId, inserted.AccountStatusId, deleted.AccountStatusId, inserted.AccountStatusReasonId, deleted.AccountStatusReasonId, 
			inserted.ModifiedBy, inserted.ModifiedDate, inserted.ModifiedBy, inserted.ModifiedDate
			FROM inserted LEFT OUTER JOIN deleted
			ON inserted.UserAccountStatusId = deleted.UserAccountStatusId
			--INSERT
			ELSE
			INSERT INTO [UserAccountStatusChangeLog] 
			([UserId]
			   ,[NewAccountStatusId]
			   ,[OldAccountStatusId]
			   ,[NewAccountStatusReasonId]
			   ,[OldAccountStatusReasonId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			SELECT inserted.UserId, inserted.AccountStatusId, deleted.AccountStatusId, inserted.AccountStatusReasonId, deleted.AccountStatusReasonId, 
			inserted.ModifiedBy, inserted.ModifiedDate, inserted.ModifiedBy, inserted.ModifiedDate
			FROM inserted LEFT OUTER JOIN deleted
			ON inserted.UserAccountStatusId = deleted.UserAccountStatusId
		END
		
		
    END