CREATE VIEW [dbo].ViewPanelApplicationReviewerCoiDetail AS
SELECT [PanelApplicationReviewerCoiDetailId]
      ,[PanelApplicationReviewerExpertiseId]
      ,[ClientCoiTypeId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelApplicationReviewerCoiDetail]
WHERE [DeletedFlag] = 0

