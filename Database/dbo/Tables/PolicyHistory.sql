CREATE TABLE [dbo].[PolicyHistory]
(
	[PolicyHistoryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PolicyId] INT NOT NULL,
    [VersionId] INT NOT NULL,
    [ClientId] INT NOT NULL,
	[TypeId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Details] VARCHAR(200) NULL, 
    [StartDateTime] DATETIME NOT NULL, 
    [EndDateTime] DATETIME NULL, 
    [RestrictionTypeId] INT NULL, 
    [RestrictionStartTime] TIME NULL, 
    [RestrictionEndTime] TIME NULL, 
    [Active] bit NOT NULL Default 1,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL,
    CONSTRAINT [FK_PolicyHistory_Policy] FOREIGN KEY ([PolicyID]) REFERENCES [Policy]([PolicyId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Key to store Policy History Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyHistoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'PolicyId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Change version Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'VersionId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client Id to whom this poilicy belongs. ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Type Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'TypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Name',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'Details'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy effective start date and time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'StartDateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy effective end date and time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'EndDateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Restriction Type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'RestrictionTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Restriction Start Time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'RestrictionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Restriction End Time',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'RestrictionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Policy Active or Inactive Flag',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PolicyHistory',
    @level2type = N'COLUMN',
    @level2name = N'Active'