CREATE VIEW [dbo].ViewPanelApplicationReviewerExpertise AS
SELECT [PanelApplicationReviewerExpertiseId]
      ,[PanelApplicationId]
      ,[PanelUserAssignmentId]
      ,[ClientExpertiseRatingId]
      ,[ExpertiseComments]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelApplicationReviewerExpertise]
WHERE [DeletedFlag] = 0

