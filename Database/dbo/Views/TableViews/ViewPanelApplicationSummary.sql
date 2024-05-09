CREATE VIEW [dbo].ViewPanelApplicationSummary AS
SELECT [PanelApplicationSummaryId]
      ,[PanelApplicationId]
      ,[SummaryText]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelApplicationSummary]
WHERE [DeletedFlag] = 0

