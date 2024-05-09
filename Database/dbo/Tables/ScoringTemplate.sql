CREATE TABLE [dbo].[ScoringTemplate]
(
	[ScoringTemplateId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [TemplateName] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ScoringTemplate_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([ClientId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a scoring template definition',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ScoringTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Client identifier',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'ClientId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name describing the purpose of the template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ScoringTemplate',
    @level2type = N'COLUMN',
    @level2name = N'TemplateName'