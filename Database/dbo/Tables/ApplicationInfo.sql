CREATE TABLE [dbo].[ApplicationInfo]
(
	[ApplicationInfoId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [ClientApplicationInfoTypeId] INT NOT NULL, 
    [InfoText] VARCHAR(200) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationInfo_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationInfo_ClientApplicationInfoType] FOREIGN KEY ([ClientApplicationInfoTypeId]) REFERENCES [ClientApplicationInfoType]([ClientApplicationInfoTypeId])
)

GO

CREATE INDEX [IX_ApplicationInfo_ApplicationId_ClientApplicationInfoTypeId] ON [dbo].[ApplicationInfo] ([ApplicationId], [ClientApplicationInfoTypeId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a piece of application information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationInfo',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationInfoId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationInfo',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for what type of information is stored',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationInfo',
    @level2type = N'COLUMN',
    @level2name = N'ClientApplicationInfoTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text value of the information',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationInfo',
    @level2type = N'COLUMN',
    @level2name = N'InfoText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Miscellaneous application information, often client specific',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationInfo',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ApplicationInfo] TO [web-p2rmis]
    AS [dbo];