CREATE TABLE [dbo].[UserTrainingDocument]
(
	[UserTrainingDocumentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [TrainingDocumentId] INT NOT NULL, 
    [ReviewDate] DATETIME2 NOT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] DATETIME2 NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] DATETIME2 NULL, 
    CONSTRAINT [FK_UserTrainingDocument_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserTrainingDocument_TrainingDocument] FOREIGN KEY ([TrainingDocumentId]) REFERENCES [TrainingDocument]([TrainingDocumentId]), 
    CONSTRAINT [AK_UserTrainingDocument_UserId_TrainingDocumentId] UNIQUE ([UserId],[TrainingDocumentId],[DeletedDate]), 
)

GO

CREATE INDEX [IX_UserTrainingDocument_UserId_TrainingDocumentId] ON [dbo].[UserTrainingDocument] ([UserId],[TrainingDocumentId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user training document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserTrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'UserTrainingDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user account',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserTrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'UserId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserTrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'TrainingDocumentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date document was reviewed',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserTrainingDocument',
    @level2type = N'COLUMN',
    @level2name = N'ReviewDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contains training document information related to users',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'UserTrainingDocument',
    @level2type = NULL,
    @level2name = NULL