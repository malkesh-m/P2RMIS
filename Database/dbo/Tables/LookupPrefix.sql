CREATE TABLE [dbo].[LookupPrefix] (
    [PrefixID]   INT           IDENTITY (1, 1) NOT NULL,
    [PrefixName] NVARCHAR (10) NULL,
    CONSTRAINT [PK_LookupPrefix] PRIMARY KEY CLUSTERED ([PrefixID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Renamed to Prefix',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupPrefix',
    @level2type = NULL,
    @level2name = NULL