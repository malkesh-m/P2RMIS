CREATE VIEW [dbo].ViewUserAlternateContact AS
SELECT [UserAlternateContactId]
      ,[UserInfoId]
      ,[AlternateContactTypeId]
      ,[PrimaryFlag]
      ,[FirstName]
      ,[LastName]
      ,[EmailAddress]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAlternateContact]
WHERE [DeletedFlag] = 0

