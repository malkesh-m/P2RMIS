CREATE TABLE [dbo].[AssignmentTypeThreshold]
(
	[AssignmentTypeThresholdId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SessionPanelId] INT NOT NULL, 
    [ScientistReviewerSortOrder] INT NULL, 
    [AdvocateConsumerSortOrder] INT NULL,
    [SpecialistReviewerSortOrder] INT NULL,
	CONSTRAINT [FK_AssignmentTypeThreshold_SessionPanelId] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'AssignmentTypeThreshold primary key',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentTypeThreshold',
    @level2type = N'COLUMN',
    @level2name = N'AssignmentTypeThresholdId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Scientist Reviewer number ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentTypeThreshold',
    @level2type = N'COLUMN',
    @level2name = N'ScientistReviewerSortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Advocate Reviewer Number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentTypeThreshold',
    @level2type = N'COLUMN',
    @level2name = N'AdvocateConsumerSortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Specialist Reviewer Number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentTypeThreshold',
    @level2type = N'COLUMN',
    @level2name = N'SpecialistReviewerSortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Session Panel Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AssignmentTypeThreshold',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'