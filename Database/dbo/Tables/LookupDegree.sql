CREATE TABLE [dbo].[LookupDegree] (
    [DegreeID]   INT           IDENTITY (1, 1) NOT NULL,
    [DegreeName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_LookupDegree] PRIMARY KEY CLUSTERED ([DegreeID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Renamed to degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupDegree',
    @level2type = NULL,
    @level2name = NULL