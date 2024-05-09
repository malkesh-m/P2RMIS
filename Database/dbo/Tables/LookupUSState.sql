CREATE TABLE [dbo].[LookupUSState] (
    [USStateID]       INT           IDENTITY (1, 1) NOT NULL,
    [USStateName]     NVARCHAR (2)  NULL,
    [USStateFullName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupUSState] PRIMARY KEY CLUSTERED ([USStateID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Table renamed to State',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupUSState',
    @level2type = NULL,
    @level2name = NULL