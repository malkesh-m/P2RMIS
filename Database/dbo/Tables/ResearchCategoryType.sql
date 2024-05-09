CREATE TABLE [dbo].[ResearchCategoryType]
(
	[ResearchCategoryTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResearchCategoryName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a type of research category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ResearchCategoryType',
    @level2type = N'COLUMN',
    @level2name = N'ResearchCategoryTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the research category type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ResearchCategoryType',
    @level2type = N'COLUMN',
    @level2name = N'ResearchCategoryName'

GO
GRANT SELECT
    ON OBJECT::[dbo].[ResearchCategoryType] TO [web-p2rmis]
    AS [dbo];