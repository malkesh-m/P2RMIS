CREATE VIEW [dbo].ViewApplicationResearchCategory AS
SELECT [ApplicationResearchCategoryId]
      ,[ApplicationId]
      ,[ApplicationResearchCategory]
      ,[ResearchCategoryTypeId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationResearchCategory]
WHERE [DeletedFlag] = 0

