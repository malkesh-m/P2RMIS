CREATE TABLE [dbo].[ProgramEmailTemplate]
(
	[ProgramEmailTemplateId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [TemplateName] VARCHAR(100) NOT NULL, 
    [TemplateDescription] VARCHAR(1000) NOT NULL, 
    [FileLocation] VARCHAR(200) NOT NULL, 
    [ActiveFlag] BIT NOT NULL, 
    [LegacyEtId] INT NULL,
	[CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ProgramEmailTemplate_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program specific email template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramEmailTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Display name for an email template document',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'TemplateName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description for an email template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'TemplateDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Http location of the file',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'FileLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the file should be displayed to the end user',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramEmailTemplate',
    @level2type = N'COLUMN',
    @level2name = N'LegacyEtId'
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ProgramEmailTemplate] TO [web-p2rmis]
    AS [dbo];