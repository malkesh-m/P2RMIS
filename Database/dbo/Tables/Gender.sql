CREATE TABLE [dbo].[Gender]
(
	[GenderId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Gender] VARCHAR(10) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a gender',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Gender',
    @level2type = N'COLUMN',
    @level2name = N'GenderId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display value for a gender',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Gender',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores the possible values for a user''s gender',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Gender',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[Gender] TO [web-p2rmis]
    AS [dbo];