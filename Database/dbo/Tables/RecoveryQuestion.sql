CREATE TABLE [dbo].[RecoveryQuestion]
(
	[RecoveryQuestionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionText] VARCHAR(250) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for a recovery question',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RecoveryQuestion',
    @level2type = N'COLUMN',
    @level2name = N'RecoveryQuestionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text of the recovery question',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RecoveryQuestion',
    @level2type = N'COLUMN',
    @level2name = N'QuestionText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Possible recovery questions for user account recovery',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RecoveryQuestion',
    @level2type = NULL,
    @level2name = NULL