CREATE VIEW [dbo].ViewReviewerEvaluation AS
SELECT [ReviewerEvaluationId]
      ,[PanelUserAssignmentId]
      ,[Rating]
      ,[Comments]
      ,[RecommendChairFlag]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ReviewerEvaluation]
WHERE [DeletedFlag] = 0

