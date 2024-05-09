CREATE TABLE [dbo].[LookupGender] (
    [GenderID] INT           IDENTITY (1, 1) NOT NULL,
    [Gender]   NVARCHAR (10) NULL,
    CONSTRAINT [PK_LookupGender] PRIMARY KEY CLUSTERED ([GenderID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Renamed to Gender',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupGender',
    @level2type = NULL,
    @level2name = NULL