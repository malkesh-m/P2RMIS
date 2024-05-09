CREATE TABLE [dbo].[PanelApplicationReviewerExpertise]
(
	[PanelApplicationReviewerExpertiseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PanelApplicationId] INT NOT NULL, 
    [PanelUserAssignmentId] INT NOT NULL, 
    [ClientExpertiseRatingId] INT NULL, 
	[ExpertiseComments] VARCHAR(8000) NULL,
    [CreatedBy] INT NULL, 
    [CreatedDate] datetime2(0) NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedDate] datetime2(0) NULL, 
	[DeletedFlag] bit NOT NULL Default 0,
    [DeletedBy] INT NULL, 
    [DeletedDate] datetime2(0) NULL, 
    CONSTRAINT [FK_PanelApplicationReviewerExpertise_PanelApplication] FOREIGN KEY ([PanelApplicationId]) REFERENCES [PanelApplication]([PanelApplicationId]), 
    CONSTRAINT [FK_PanelApplicationReviewerExpertise_PanelUserAssignment] FOREIGN KEY ([PanelUserAssignmentId]) REFERENCES [PanelUserAssignment]([PanelUserAssignmentId]), 
    CONSTRAINT [FK_PanelApplicationReviewerExpertise_ClientExpertiseRating] FOREIGN KEY ([ClientExpertiseRatingId]) REFERENCES [ClientExpertiseRating]([ClientExpertiseRatingId]) 

)

GO

CREATE INDEX [IX_PanelApplicationReviewerExpertise_PanelUserAssignmentId_ClientExpertiseRatingId] ON [dbo].[PanelApplicationReviewerExpertise] ([PanelUserAssignmentId],[ClientExpertiseRatingId]) WHERE (DeletedFlag = 0)

GO
CREATE UNIQUE INDEX [UNX_PanelApplicationReviewerExpertise_PanelApplicationId_PanelUserAssignmentId] ON [dbo].[PanelApplicationReviewerExpertise] ([PanelApplicationId], [PanelUserAssignmentId]) WHERE (DeletedFlag = 0)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Reviewer specified application for a panel application',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PanelApplicationReviewerExpertise',
    @level2type = NULL,
    @level2name = NULL

GO
GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[PanelApplicationReviewerExpertise] TO [web-p2rmis]
    AS [dbo];
