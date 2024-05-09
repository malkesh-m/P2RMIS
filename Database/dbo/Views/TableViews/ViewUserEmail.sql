CREATE VIEW [dbo].ViewUserEmail AS
SELECT [EmailID]
      ,[UserInfoID]
      ,[EmailAddressTypeId]
      ,[Email]
      ,[PrimaryFlag]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserEmail]
WHERE [DeletedFlag] = 0

