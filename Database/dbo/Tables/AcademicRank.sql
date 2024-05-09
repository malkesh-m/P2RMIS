CREATE TABLE [dbo].[AcademicRank]
(
	[AcademicRankId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Rank] NCHAR(25) NOT NULL,
    [RankAbbreviation] NCHAR(25) NOT NULL, 
    [SortOrder] INT NOT NULL 

)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Academic Rank primary key',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AcademicRank',
    @level2type = N'COLUMN',
    @level2name = N'AcademicRankId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The academic rank text',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AcademicRank',
    @level2type = N'COLUMN',
    @level2name = N'Rank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The dropdown sort order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AcademicRank',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Academic Rank Abbreviation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AcademicRank',
    @level2type = N'COLUMN',
    @level2name = N'RankAbbreviation'