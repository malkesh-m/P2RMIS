CREATE TABLE [dbo].[ClientReviewerEvaluationGroup]
(
	[ClientReviewerEvaluationGroupId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [ReviewerEvaluationGroupName] VARCHAR(50) NOT NULL,
    CONSTRAINT [FK_ClientReviewerRatingGuidance_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The groups to be rated for reviewer evaluations by client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientReviewerEvaluationGroup',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for client rating group',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientReviewerEvaluationGroup',
    @level2type = N'COLUMN',
    @level2name = 'ClientReviewerEvaluationGroupId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientReviewerEvaluationGroup',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The rating group name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientReviewerEvaluationGroup',
    @level2type = N'COLUMN',
    @level2name = 'ReviewerEvaluationGroupName'