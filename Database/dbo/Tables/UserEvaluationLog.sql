CREATE TABLE [dbo].[UserEvaluationLog]
(
	[UserEvaluationLogId] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserInfoId] INT NOT NULL,
	[BlockFlag] BIT NOT NULL,
	[ShowCommentFlag] BIT NOT NULL,
	[EvaluationComment] VARCHAR(500) NOT NULL,
	[EvaluationDate] DATETIME2(0) NOT NULL,
	[LegacyRevEvalId] INT NULL,
	[LegacyRevEvalBlockId] INT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] DATETIME2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] DATETIME2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] DATETIME2(0) NULL, 
    CONSTRAINT [FK_UserEvaluationLog_UserInfo] FOREIGN KEY ([UserInfoId]) REFERENCES [UserInfo]([UserInfoId]),  
)

GO

CREATE INDEX [IX_UserEvaluationLog_BlockFlag_ShowCommentFlag_DeletedFlag] ON [dbo].[UserEvaluationLog] ([BlockFlag], [ShowCommentFlag], [DeletedFlag])

GO

CREATE INDEX [IX_UserEvaluationLog_UserId_ShowCommentFlag_BlockFlag_DeletedFlag] ON [dbo].[UserEvaluationLog] ([UserInfoId],[ShowCommentFlag],[BlockFlag],[DeletedFlag])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user evaluation log enttry',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'UserEvaluationLogId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user''s information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'UserInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the evaluation is a block recommendation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'BlockFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the comment is to be shown during recruitment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'ShowCommentFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comment explaining the evaluation and reason',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'EvaluationComment'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date the evaluation occurred',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserEvaluationLog',
    @level2type = N'COLUMN',
    @level2name = N'EvaluationDate'