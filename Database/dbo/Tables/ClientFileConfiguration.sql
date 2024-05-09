CREATE TABLE [dbo].[ClientFileConfiguration]
(
	[ClientFileConfigurationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [FileSuffix] VARCHAR(50) NOT NULL, 
    [DisplayLabel] VARCHAR(50) NOT NULL, 
    [AbstractFlag] BIT NOT NULL DEFAULT 0, 
	[CreatedBy] INT NULL,
	[CreatedDate] datetime2(0) NULL,
	[ModifiedBy] INT NULL,
	[ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ClientFileConfiguration_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId]), 
    CONSTRAINT [AK_ClientFileConfiguration_ClientId_FileSuffix] UNIQUE ([ClientId],[FileSuffix])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client file configuration',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientFileConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ClientFileConfigurationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientFileConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Suffix proceeding a file name to identify the type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientFileConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'FileSuffix'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Label displayed to the user to identify the file type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientFileConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'DisplayLabel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the application is treated as an abstract',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientFileConfiguration',
    @level2type = N'COLUMN',
    @level2name = N'AbstractFlag'