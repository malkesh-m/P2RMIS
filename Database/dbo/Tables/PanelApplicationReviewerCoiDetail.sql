CREATE TABLE [dbo].[PanelApplicationReviewerCoiDetail]
(
	[PanelApplicationReviewerCoiDetailId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelApplicationReviewerExpertiseId] INT NOT NULL, 
    [ClientCoiTypeId] INT NOT NULL, 
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelApplicationReviewerCoiDetail_PanelApplicationReviewerExpertise] FOREIGN KEY ([PanelApplicationReviewerExpertiseId]) REFERENCES [PanelApplicationReviewerExpertise]([PanelApplicationReviewerExpertiseId]), 
    CONSTRAINT [FK_PanelApplicationReviewerCoiDetail_ClientCoiType] FOREIGN KEY ([ClientCoiTypeId]) REFERENCES [ClientCoiType]([ClientCoiTypeId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a reviewer coi info on an panel application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerCoiDetail',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationReviewerCoiDetailId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a panel reviewer expertise evaluation for an application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerCoiDetail',
    @level2type = N'COLUMN',
    @level2name = N'PanelApplicationReviewerExpertiseId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Identifier for a Conflict of Interest type',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerCoiDetail',
    @level2type = N'COLUMN',
    @level2name = N'ClientCoiTypeId'
GO

CREATE INDEX [IX_PanelApplicationReviewerCoiDetail_PanelApplicationReviewerExpertiseId] ON [dbo].[PanelApplicationReviewerCoiDetail] ([PanelApplicationReviewerExpertiseId])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reviewer specific conflict of interest category for a panel application.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerCoiDetail',
    @level2type = NULL,
    @level2name = NULL
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelApplicationReviewerCoiDetail] TO [web-p2rmis]
    AS [dbo];