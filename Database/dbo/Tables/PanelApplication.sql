CREATE TABLE [dbo].[PanelApplication]
(
	[PanelApplicationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SessionPanelId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    [ReviewOrder] INT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelApplication_Panel] FOREIGN KEY ([SessionPanelId]) REFERENCES [SessionPanel]([SessionPanelId]), 
    CONSTRAINT [FK_PanelApplication_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([ApplicationId]), 
    CONSTRAINT [UN_PanelApplication_SessionPanelId_ApplicationId] UNIQUE ([SessionPanelId], [ApplicationId], [DeletedDate])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a application assignment to a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplication',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplication',
    @level2type = N'COLUMN',
    @level2name = 'SessionPanelId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplication',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Custom sort order for an application ',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplication',
    @level2type = N'COLUMN',
    @level2name = N'ReviewOrder'
GO

CREATE INDEX [IX_PanelApplication_SessionPanelId_ApplicationId] ON [dbo].[PanelApplication] ([SessionPanelId], [ApplicationId])

GO

CREATE INDEX [IX_PanelApplication_ApplicationId] ON [dbo].[PanelApplication] ([ApplicationId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Applications to be reviewed by a particular review panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplication',
    @level2type = NULL,
    @level2name = NULL
GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelApplication] TO [web-p2rmis]
    AS [dbo];
GO

CREATE NONCLUSTERED INDEX [IX_PanelApplication_DeletedFlag_SessionPanelId_ApplicationId_PanelApplicationId] ON [dbo].[PanelApplication]
(
	[DeletedFlag] ASC,
	[SessionPanelId] ASC,
	[ApplicationId] ASC,
	[PanelApplicationId] ASC
)
INCLUDE ( 	[ReviewOrder]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_PanelApplication_ApplicationId_PanelApplicationId_SessionPanelId_DeletedFlag] ON [dbo].[PanelApplication]
(
	[ApplicationId] ASC,
	[PanelApplicationId] ASC,
	[SessionPanelId] ASC,
	[DeletedFlag] ASC
)
INCLUDE ( 	[ReviewOrder]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_PanelApplication_ApplicationId_SessionPanelId_PanelApplicationId_DeletedFlag] ON [dbo].[PanelApplication]
(
	[ApplicationId] ASC,
	[SessionPanelId] ASC,
	[PanelApplicationId] ASC,
	[DeletedFlag] ASC
)
INCLUDE ( 	[ReviewOrder]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
