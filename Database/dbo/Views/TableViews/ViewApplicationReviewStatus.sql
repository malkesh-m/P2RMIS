CREATE VIEW [dbo].ViewApplicationReviewStatus AS
SELECT [ApplicationReviewStatusId]
      ,[PanelApplicationId]
      ,[ReviewStatusId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationReviewStatus]
WHERE [DeletedFlag] = 0

