CREATE TABLE [dbo].[LookupQuestion] (
    [QID]          INT            IDENTITY (1, 1) NOT NULL,
    [QuestionText] NVARCHAR (250) NULL,
    CONSTRAINT [PK_LookupQuestion] PRIMARY KEY CLUSTERED ([QID] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Deprecated: Table renamed to RecoveryQuestion',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'LookupQuestion',
    @level2type = NULL,
    @level2name = NULL