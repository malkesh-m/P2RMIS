CREATE VIEW [dbo].ViewUserWebsite AS
SELECT [UserWebsiteId]
      ,[UserInfoId]
      ,[WebsiteTypeId]
      ,[WebsiteAddress]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserWebsite]
WHERE [DeletedFlag] = 0

