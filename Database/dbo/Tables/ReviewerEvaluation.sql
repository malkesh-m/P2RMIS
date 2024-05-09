CREATE TABLE [dbo].[ReviewerEvaluation]
(
	[ReviewerEvaluationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelUserAssignmentId] INT NOT NULL,
	[Rating] INT NULL, 
    [Comments] VARCHAR(MAX) NULL, 
    [RecommendChairFlag] BIT NOT NULL DEFAULT 0, 
	[CreatedBy] INT NOT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL,
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ReviewerEvaluation_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId])
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a evaluation of a panel reviewer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'ReviewerEvaluationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user assignment to a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The rating given to the reviewer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'Rating'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A comment about the reviewer''s rating',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'Comments'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A boolean flag determining if the reviewer was recommended to be a chair or not',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'RecommendChairFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The user that evaluated the reviewer',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = N'COLUMN',
    @level2name = N'CreatedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Table for storage of reviewer evaluation data',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluation',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_ReviewerEvaluation_PanelUserAssignmentId] ON [dbo].[ReviewerEvaluation] ([PanelUserAssignmentId])
