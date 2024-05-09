CREATE VIEW [dbo].ViewPanelStage AS
SELECT [PanelStageId]
      ,[SessionPanelId]
      ,[ReviewStageId]
      ,[StageOrder]
      ,[WorkflowId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[PanelStage]
WHERE [DeletedFlag] = 0

