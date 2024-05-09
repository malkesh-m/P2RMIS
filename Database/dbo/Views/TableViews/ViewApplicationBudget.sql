CREATE VIEW [dbo].ViewApplicationBudget AS
SELECT [ApplicationBudgetId]
      ,[ApplicationId]
      ,[DirectCosts]
      ,[IndirectCosts]
      ,[TotalFunding]
      ,[Comments]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationBudget]
WHERE [DeletedFlag] = 0

GO
GRANT SELECT ON [ViewApplicationBudget] TO [web-p2rmis]