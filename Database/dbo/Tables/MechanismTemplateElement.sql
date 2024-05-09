CREATE TABLE [dbo].[MechanismTemplateElement]
(
	[MechanismTemplateElementId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED, 
	[MechanismTemplateId] INT NOT NULL,
	[LegacyEcmId] INT NULL DEFAULT next value for seq_MechanismTemplateElement_LegacyEcmId,
    [ClientElementId] INT NOT NULL, 
	[InstructionText] VARCHAR(MAX) NULL,
	[SortOrder] INT NOT NULL, 
    [RecommendedWordCount] INT NULL, 
    [ScoreFlag] BIT NOT NULL, 
    [TextFlag] BIT NOT NULL, 
    [OverallFlag] BIT NOT NULL, 
	[ShowAbbreviationOnScoreboard] BIT NOT NULL DEFAULT 0,
	[SummarySortOrder] INT NULL,
	[SummaryIncludeFlag] BIT NOT NULL DEFAULT 1,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_MechanismTemplateElement_MechanismTemplate] FOREIGN KEY ([MechanismTemplateId]) REFERENCES [MechanismTemplate]([MechanismTemplateId]), 
    CONSTRAINT [FK_MechanismTemplateElement_ClientElement] FOREIGN KEY ([ClientElementId]) REFERENCES [ClientElement]([ClientElementId]), 
    CONSTRAINT [UN_MechanismTemplateElement_MechanismTemplateId_ClientElementId] UNIQUE (MechanismTemplateId, ClientElementId, [DeletedDate]),

)

GO

CREATE CLUSTERED INDEX [IX_MechanismTemplateElement_MechanismTemplateId_SortOrder] ON [dbo].[MechanismTemplateElement] ([MechanismTemplateId],[SortOrder])
GO
CREATE INDEX [IX_MechanismTemplateElement_ClientElementId] ON [dbo].[MechanismTemplateElement] ([ClientElementId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a mechanism template element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'MechanismTemplateElementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a mechanism template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'MechanismTemplateId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Legacy identifier for a mechansim criteria',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'LegacyEcmId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'ClientElementId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text provided to describe how the element should be evaluated',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'InstructionText'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which element is presented within the template',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Recommended length of element evaluation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'RecommendedWordCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the element is scored during evaluation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'ScoreFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether text evaluation is expected for the element',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'TextFlag'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the element is considered to be an overall evaluation',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'OverallFlag'

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[MechanismTemplateElement] TO [web-p2rmis]
    AS [dbo];
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the scoreboard should display the element abbreviation or full description',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = 'ShowAbbreviationOnScoreboard'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order in which criteria is to appear on summary statements',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'SummarySortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Whether the criteria should appear on summary statements',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'MechanismTemplateElement',
    @level2type = N'COLUMN',
    @level2name = N'SummaryIncludeFlag'