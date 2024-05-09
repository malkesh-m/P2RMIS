CREATE TABLE [dbo].[Ethnicity]
(
	[EthnicityId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Ethnicity] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a person ethnicity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Ethnicity',
    @level2type = N'COLUMN',
    @level2name = N'EthnicityId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Ethnicity group',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Ethnicity',
    @level2type = N'COLUMN',
    @level2name = 'Ethnicity'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stores the possible values for a user''s ethnicity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Ethnicity',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT
    ON OBJECT::[dbo].[Ethnicity] TO [web-p2rmis]
    AS [dbo];