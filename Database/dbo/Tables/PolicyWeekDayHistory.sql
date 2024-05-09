CREATE TABLE [dbo].[PolicyWeekDayHistory]
(
	[PolicyWeekDayHistoryId] INT NOT NULL PRIMARY KEY,
    [PolicyHistoryId] INT,
	[PolicyId] INT NOT NULL, 
    [VersionId] INT NOT NULL,
    [WeekDayId] INT NOT NULL,     
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,
    CONSTRAINT [FK_PolicyWeekDayHistory_PolicyHistory] FOREIGN KEY ([PolicyHistoryId]) REFERENCES [PolicyHistory]([PolicyHistoryId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Weekday on which this policy applies.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDayHistory',
    @level2type = N'COLUMN',
    @level2name = N'WeekDayId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Change version Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDayHistory',
    @level2type = N'COLUMN',
    @level2name = N'VersionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy to apply this weekday',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDayHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Histiory id ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDayHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyHistoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key to store Policy Weekday History Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDayHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyWeekDayHistoryId'