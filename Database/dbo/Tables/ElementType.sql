CREATE TABLE [dbo].[ElementType]
(
	[ElementTypeId] INT NOT NULL PRIMARY KEY,
	[ElementTypeName] VARCHAR(20) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the type of element to be associated with a template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ElementType',
    @level2type = N'COLUMN',
    @level2name = N'ElementTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier for an element type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ElementType',
    @level2type = N'COLUMN',
    @level2name = N'ElementTypeId'
GO
GRANT SELECT
    ON OBJECT::[dbo].[ElementType] TO [web-p2rmis]
    AS [dbo];