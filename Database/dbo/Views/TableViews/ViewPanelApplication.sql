CREATE VIEW [dbo].ViewPanelApplication AS
SELECT [PanelApplicationId]
      ,[SessionPanelId]
      ,[ApplicationId]
      ,[ReviewOrder]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelApplication]
WHERE [DeletedFlag] = 0

