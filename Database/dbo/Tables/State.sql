CREATE TABLE [dbo].[State]
(
	[StateId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StateAbbreviation] CHAR(2) NOT NULL, 
    [StateName] VARCHAR(50) NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identiifer for a state',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'State',
    @level2type = N'COLUMN',
    @level2name = N'StateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Abbreviation for a state or territory',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'State',
    @level2type = N'COLUMN',
    @level2name = N'StateAbbreviation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of a state or territory',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'State',
    @level2type = N'COLUMN',
    @level2name = N'StateName'

GO
GRANT SELECT
    ON OBJECT::[dbo].[State] TO [web-p2rmis]
    AS [dbo];