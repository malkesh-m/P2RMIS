CREATE TABLE [dbo].[WeekDay]
(
	[WeekDayId] INT NOT NULL PRIMARY KEY, 
    [Abbreviation] CHAR(2) NOT NULL, 
    [Name] VARCHAR(20) NOT NULL,
    [SortOrder] int NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key for Weekday Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeekDay',
    @level2type = N'COLUMN',
    @level2name = N'WeekDayId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Week day short name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeekDay',
    @level2type = N'COLUMN',
    @level2name = N'Abbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Weekday name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeekDay',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Determines sorting order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeekDay',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'