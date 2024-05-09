CREATE TABLE [dbo].[ProgramPanel]
(
	[ProgramPanelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramYearId] INT NOT NULL, 
    [SessionPanelId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [UN_ProgramPanel_ClientProgramId_SessionPanelId] UNIQUE ([ProgramYearId], [SessionPanelId], [DeletedDate]), 
    CONSTRAINT [FK_ProgramPanel_ProgramYear] FOREIGN KEY ([ProgramYearId]) REFERENCES [ProgramYear]([ProgramYearId]), 
    CONSTRAINT [FK_ProgramPanel_SessionPanel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId])
)

GO

CREATE INDEX [IX_ProgramPanel_ClientProgramId_SessionPanelId] ON [dbo].[ProgramPanel] ([ProgramYearId], [SessionPanelId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program panel assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPanel',
    @level2type = N'COLUMN',
    @level2name = N'ProgramPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a program instance for a year',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPanel',
    @level2type = N'COLUMN',
    @level2name = N'ProgramYearId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPanel',
    @level2type = N'COLUMN',
    @level2name = N'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Program assignments to review panels',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramPanel',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ProgramPanel] TO [web-p2rmis]
    AS [dbo];
GO

CREATE INDEX [IX_ProgramPanel_Session_PanelId] ON [dbo].[ProgramPanel] ([SessionPanelId],[ProgramYearId]) WHERE DeletedFlag = 0
