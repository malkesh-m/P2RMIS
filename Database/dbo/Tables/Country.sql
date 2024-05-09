CREATE TABLE [dbo].[Country]
(
	[CountryId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[CountryAbbreviation] VARCHAR(5), 
    [CountryName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a country',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Country',
    @level2type = N'COLUMN',
    @level2name = N'CountryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of a country',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Country',
    @level2type = N'COLUMN',
    @level2name = N'CountryName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for a country',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Country',
    @level2type = N'COLUMN',
    @level2name = N'CountryAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Available countries to associate with a user address',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Country',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[Country] TO [web-p2rmis]
    AS [dbo];