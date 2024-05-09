CREATE TABLE [dbo].[ApplicationResearchCategory]
(
	[ApplicationResearchCategoryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [ApplicationResearchCategory] VARCHAR(200) NOT NULL, 
	[ResearchCategoryTypeId] INT NOT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_ApplicationResearchCategory_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [FK_ApplicationResearchCategory_ResearchCategoryType] FOREIGN KEY ([ResearchCategoryTypeId]) REFERENCES [ResearchCategoryType]([ResearchCategoryTypeId])
)

GO

CREATE INDEX [IX_ApplicationResearchCategory_ApplicationId] ON [dbo].[ApplicationResearchCategory] ([ApplicationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application''s research category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationResearchCategory',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationResearchCategoryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationResearchCategory',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Value used to describe the application''s research category',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationResearchCategory',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationResearchCategory'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a research category type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationResearchCategory',
    @level2type = N'COLUMN',
    @level2name = N'ResearchCategoryTypeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Research categories to which an application is considered',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ApplicationResearchCategory',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT
    ON OBJECT::[dbo].[ApplicationResearchCategory] TO [web-p2rmis]
    AS [dbo];