CREATE TABLE [dbo].[SystemConfiguration]
(
	[SystemConfigurationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ConfigurationName] VARCHAR(50) NOT NULL, 
    [ConfigurationDescription] VARCHAR(200) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a system configuration type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'SystemConfigurationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the configuration type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ConfigurationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the configuration type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ConfigurationDescription'