CREATE TABLE [dbo].[LookupStage] (
    [StageID]   INT            IDENTITY (1, 1) NOT NULL,
    [StageName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_LookupStatus] PRIMARY KEY CLUSTERED ([StageID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Refactored: Renamed to AccountStatus',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupStage',
    @level2type = NULL,
    @level2name = NULL