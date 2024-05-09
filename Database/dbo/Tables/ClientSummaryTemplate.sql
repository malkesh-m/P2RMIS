CREATE TABLE [dbo].[ClientSummaryTemplate]
(
	[ClientSummaryTemplateId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [TemplateName] VARCHAR(100) NOT NULL, 
    [TemplateFileName] VARCHAR(100) NOT NULL, 
    [StoredProcedureName] VARCHAR(50) NOT NULL, 
    [ActiveFlag] BIT NOT NULL DEFAULT 1, 
	[ExpeditedFlag] BIT NOT NULL DEFAULT 0,
    [PreviewLinkUrl] VARCHAR(500) NULL, 
    CONSTRAINT [FK_ClientSummaryTemplate_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client specific summary statement template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ClientSummaryTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client of the system',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Friendly name for the template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'TemplateName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Actual file name of the template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'TemplateFileName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Stored procedure used to populate summary data',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'StoredProcedureName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the template is available for new selections',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ActiveFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Link to preview original template definition',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'PreviewLinkUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the template is intended for use for expedited applications',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ClientSummaryTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ExpeditedFlag'