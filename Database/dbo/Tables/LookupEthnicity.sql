CREATE TABLE [dbo].[LookupEthnicity] (
    [EthnicityID] INT           IDENTITY (1, 1) NOT NULL,
    [Ethnicity]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupEthnicity] PRIMARY KEY CLUSTERED ([EthnicityID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Renamed to Ethnicity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupEthnicity',
    @level2type = NULL,
    @level2name = NULL