CREATE TABLE [dbo].[ApplicationBudget]
(
	[ApplicationBudgetId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [DirectCosts] MONEY NULL, 
    [IndirectCosts] MONEY NULL, 
    [TotalFunding] MONEY NULL, 
    [Comments] VARCHAR(MAX) NULL,
	[CommentModifiedBy] INT NULL,
	[CommentModifiedDate] datetime2(0) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationBudget_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [UN_ApplicationBudget_ApplicationId] UNIQUE ([ApplicationId], [DeletedDate]),

)

GO

CREATE INDEX [IX_ApplicationBudget_ApplicationId] ON [dbo].[ApplicationBudget] ([ApplicationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an applications budget data',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationBudgetId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Requested costs directly related to the purpose of the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'DirectCosts'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Requested costs not directly related to the purpose of the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'IndirectCosts'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Total funding requested by the application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'TotalFunding'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Comments associated with the budget such as admin notes (legacy)',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'Comments'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Budget information proposed for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationBudget] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Id of User to last modify the budget comment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'CommentModifiedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date/time the comment was last modified',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationBudget',
    @level2type = N'COLUMN',
    @level2name = N'CommentModifiedDate'
