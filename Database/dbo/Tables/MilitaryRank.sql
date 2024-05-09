CREATE TABLE [dbo].[MilitaryRank]
(
	[MilitaryRankId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MilitaryRankAbbreviation] VARCHAR(20) NOT NULL, 
    [MilitaryRankName] VARCHAR(100) NOT NULL, 
    [Service] VARCHAR(100) NOT NULL, 
    [SortOrder] INT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Available military ranks for a user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a US military rank',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = N'COLUMN',
    @level2name = N'MilitaryRankId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for a military rank',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = N'COLUMN',
    @level2name = N'MilitaryRankAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Full name for a military rank',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = N'COLUMN',
    @level2name = 'MilitaryRankName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Branch of service the military rank belongs to',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = N'COLUMN',
    @level2name = N'Service'

GO
GRANT SELECT
    ON OBJECT::[dbo].[MilitaryRank] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort Order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MilitaryRank',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'