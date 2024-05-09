CREATE TABLE [dbo].[EmploymentCategory]
(
	[EmploymentCategoryId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the employment category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'EmploymentCategory',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for an employment category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'EmploymentCategory',
    @level2type = N'COLUMN',
    @level2name = N'EmploymentCategoryId'
GO
GRANT SELECT
    ON OBJECT::[dbo].[EmploymentCategory] TO [web-p2rmis]
    AS [dbo];