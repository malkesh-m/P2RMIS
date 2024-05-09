CREATE TABLE [dbo].[ReviewerEvaluationGroupGuidance]
(
	[ReviewerEvaluationGroupGuidanceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientReviewerEvaluationGroupId] INT NOT NULL, 
    [Rating] INT NOT NULL, 
    [RatingDescription] NVARCHAR(200) NOT NULL,
    CONSTRAINT [FK_ReviewerEvaluationGroupGuidance_ClientReviewerEvaluationGroup] FOREIGN KEY ([ClientReviewerEvaluationGroupId]) REFERENCES [ClientReviewerEvaluationGroup]([ClientReviewerEvaluationGroupId]),
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The rating guidance for a specific client reviewer evaluation group',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluationGroupGuidance',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for the evaluation group guidance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluationGroupGuidance',
    @level2type = N'COLUMN',
    @level2name = N'ReviewerEvaluationGroupGuidanceId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The group that is being rated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluationGroupGuidance',
    @level2type = N'COLUMN',
    @level2name = N'ClientReviewerEvaluationGroupId'
	GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The rating number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluationGroupGuidance',
    @level2type = N'COLUMN',
    @level2name = N'Rating'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The rating description guidance',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewerEvaluationGroupGuidance',
    @level2type = N'COLUMN',
    @level2name = N'RatingDescription'