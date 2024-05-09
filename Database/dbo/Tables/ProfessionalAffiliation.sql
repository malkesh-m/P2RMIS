CREATE TABLE [dbo].[ProfessionalAffiliation]
(
	[ProfessionalAffiliationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Type] NCHAR(30) NOT NULL, 
    [SortOrder] INT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The primary key identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfessionalAffiliation',
    @level2type = N'COLUMN',
    @level2name = N'ProfessionalAffiliationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of affiliation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfessionalAffiliation',
    @level2type = N'COLUMN',
    @level2name = N'Type'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Sort order',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProfessionalAffiliation',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'