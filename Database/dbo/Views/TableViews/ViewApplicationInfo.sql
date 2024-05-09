CREATE VIEW [dbo].ViewApplicationInfo AS
SELECT [ApplicationInfoId]
      ,[ApplicationId]
      ,[ClientApplicationInfoTypeId]
      ,[InfoText]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ApplicationInfo]
WHERE [DeletedFlag] = 0

GO
GRANT SELECT ON [ViewApplicationInfo] TO [web-p2rmis]