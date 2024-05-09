CREATE TABLE [dbo].[PanelApplicationSummary]
(
	[PanelApplicationSummaryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelApplicationId] INT NOT NULL, 
    [SummaryText] VARCHAR(MAX) NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
    [DeletedFlag] BIT NOT NULL DEFAULT 0, 
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelApplicationSummary_PanelApplication] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel application summary',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationSummary',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationSummaryId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationSummary',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Text of the panel summary',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationSummary',
    @level2type = N'COLUMN',
    @level2name = N'SummaryText'
GO

CREATE INDEX [IX_PanelApplicationSummary_PanelApplicationId] ON [dbo].[PanelApplicationSummary] ([PanelApplicationId])
