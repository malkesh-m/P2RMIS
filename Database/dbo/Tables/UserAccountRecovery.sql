CREATE TABLE [dbo].[UserAccountRecovery]
(
	[UserAccountRecoveryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [RecoveryQuestionId] INT NOT NULL, 
    [Answer] NVARCHAR(100) NOT NULL, 
	[QuestionOrder] [int] NOT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_UserAccountRecovery_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserAccountRecovery_RecoveryQuestion] FOREIGN KEY ([RecoveryQuestionId]) REFERENCES [RecoveryQuestion]([RecoveryQuestionId]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a user''s recovery question',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'UserAccountRecoveryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Question identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = 'RecoveryQuestionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'User provided answer to the security question',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'Answer'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores a user recovery questions and answers',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_UserAccountRecovery_UserId_RecoveryQuestionId] ON [dbo].[UserAccountRecovery] ([UserId], [RecoveryQuestionId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'No records will be deleted from this table.  Required for type consistancy with table that contain records that can be deleted  ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'DeletedFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'No records will be deleted from this table.  Required for type consistancy with table that contain records that can be deleted  ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'DeletedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'No records will be deleted from this table.  Required for type consistancy with table that contain records that can be deleted  ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserAccountRecovery',
    @level2type = N'COLUMN',
    @level2name = N'DeletedDate'

