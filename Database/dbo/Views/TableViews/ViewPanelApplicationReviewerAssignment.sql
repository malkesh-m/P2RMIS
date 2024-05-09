CREATE VIEW [dbo].ViewPanelApplicationReviewerAssignment AS
SELECT [PanelApplicationReviewerAssignmentId]
      ,[PanelApplicationId]
      ,[PanelUserAssignmentId]
      ,[ClientAssignmentTypeId]
      ,[SortOrder]
      ,[LegacyProposalAssignmentId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelApplicationReviewerAssignment]
WHERE [DeletedFlag] = 0

