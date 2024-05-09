CREATE TABLE [dbo].[ReviewStage]
(
	[ReviewStageId] INT NOT NULL PRIMARY KEY, 
    [ReviewStageName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewStage',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the review stage',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewStage',
    @level2type = N'COLUMN',
    @level2name = N'ReviewStageName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Functional stage of review within the system',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ReviewStage',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[ReviewStage] TO [web-p2rmis]
    AS [dbo];