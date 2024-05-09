CREATE TABLE [dbo].[Degree]
(
	[DegreeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DegreeName] VARCHAR(10) NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Available academic degrees for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Degree',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an academic degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Degree',
    @level2type = N'COLUMN',
    @level2name = N'DegreeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Degree',
    @level2type = N'COLUMN',
    @level2name = 'DegreeName'
GO

GRANT SELECT
    ON OBJECT::[dbo].[Degree] TO [web-p2rmis]
    AS [dbo];