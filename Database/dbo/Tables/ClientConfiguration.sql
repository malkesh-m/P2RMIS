CREATE TABLE [dbo].[ClientConfiguration]
(
	[ClientConfigurationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [SystemConfigurationId] INT NOT NULL, 
    [ConfigurationValue] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ClientConfiguration_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [FK_ClientConfiguration_SystemConfiguration] FOREIGN KEY ([SystemConfigurationId]) REFERENCES [SystemConfiguration]([SystemConfigurationId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client configuration identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ClientConfigurationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'System configuration identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'SystemConfigurationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Flag whether the configuration setting is on or off',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ConfigurationValue'