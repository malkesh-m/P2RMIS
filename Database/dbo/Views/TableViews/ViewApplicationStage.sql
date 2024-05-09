CREATE VIEW [dbo].ViewApplicationStage AS
SELECT [ApplicationStageId]
      ,[PanelApplicationId]
      ,[ReviewStageId]
      ,[StageOrder]
      ,[ActiveFlag]
      ,[AssignmentVisibilityFlag]
	  ,[AssignmentReleaseDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationStage]
WHERE [DeletedFlag] = 0

