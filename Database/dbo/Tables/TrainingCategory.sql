CREATE TABLE [dbo].[TrainingCategory]
(
	[TrainingCategoryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryName] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [LegacyCatTypeId] VARCHAR(2) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a training category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingCategory',
    @level2type = N'COLUMN',
    @level2name = N'TrainingCategoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display name for a training category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingCategory',
    @level2type = N'COLUMN',
    @level2name = N'CategoryName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which the categories should be sorted when listed together',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingCategory',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Contains the possible categories for training documents',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingCategory',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a training category for mapping purposes',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TrainingCategory',
    @level2type = N'COLUMN',
    @level2name = N'LegacyCatTypeId'
	GO
	GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[TrainingCategory] TO [web-p2rmis]
    AS [dbo];
