CREATE TABLE [dbo].[PanelApplicationReviewerAssignment]
(
	[PanelApplicationReviewerAssignmentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelApplicationId] INT NOT NULL, 
    [PanelUserAssignmentId] INT NOT NULL, 
    [ClientAssignmentTypeId] INT NOT NULL, 
    [SortOrder] INT NULL, 
	[LegacyProposalAssignmentId] INT NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelApplicationReviewerAssignment_PanelApplication] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId]), 
    CONSTRAINT [FK_PanelApplicationReviewerAssignment_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]), 
    CONSTRAINT [FK_PanelApplicationReviewerAssignment_ClientAssignmentType] FOREIGN KEY ([ClientAssignmentTypeId]) REFERENCES [ClientAssignmentType]([ClientAssignmentTypeId]) 
)


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'General reviewer assignments to an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a reviewer assignment to a panel application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationReviewerAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel application assignment',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a user assignment to a panel',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = N'COLUMN',
    @level2name = N'PanelUserAssignmentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Order of precedence for the application''s assigned reviewers',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = N'COLUMN',
    @level2name = N'SortOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a client specific assignment type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerAssignment',
    @level2type = N'COLUMN',
    @level2name = 'ClientAssignmentTypeId'
GO

CREATE INDEX [IX_PanelApplicationReviewerAssignment_PanelApplicationId_SortOrder] ON [dbo].[PanelApplicationReviewerAssignment] ([PanelApplicationId], [SortOrder])

GO

CREATE INDEX [IX_PanelApplicationReviewerAssignment_PanelUserAssignmentId] ON [dbo].[PanelApplicationReviewerAssignment] ([PanelUserAssignmentId])

GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelApplicationReviewerAssignment] TO [web-p2rmis]
    AS [dbo];
GO

CREATE NONCLUSTERED INDEX [IX_PanelApplicationReviewerAssignme_DeletedFlag_PanelApplicationId_PanelApplicationReviewAssignmentId] ON [dbo].[PanelApplicationReviewerAssignment]
(
	[DeletedFlag] ASC,
	[PanelApplicationId] ASC,
	[PanelApplicationReviewerAssignmentId] ASC,
	[ClientAssignmentTypeId] ASC,
	[PanelUserAssignmentId] ASC
)
INCLUDE ( 	[SortOrder]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

GO
