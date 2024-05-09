CREATE TABLE [dbo].[PolicyWeekDay]
(
	[WeekdayPolicyId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PolicyId] INT NOT NULL, 
    [WeekDayId] INT NOT NULL,     
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PolicyWeekDay_WeekDay] FOREIGN KEY ([WeekDayId]) REFERENCES [WeekDay]([WeekDayID]),
    CONSTRAINT [FK_PolicyWeekDay_Policy] FOREIGN KEY ([PolicyId]) REFERENCES [Policy]([PolicyID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key to store Weekday Policy Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDay',
    @level2type = N'COLUMN',
    @level2name = N'WeekdayPolicyId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy to apply this weekday',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDay',
    @level2type = N'COLUMN',
    @level2name = N'PolicyId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Weekday on which this policy applies.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyWeekDay',
    @level2type = N'COLUMN',
    @level2name = N'WeekDayId'