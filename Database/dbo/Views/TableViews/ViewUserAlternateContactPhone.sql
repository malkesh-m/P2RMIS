CREATE VIEW [dbo].ViewUserAlternateContactPhone AS
SELECT [UserAlternateContactPhoneId]
      ,[UserAlternateContactId]
      ,[PhoneTypeId]
      ,[PrimaryFlag]
      ,[PhoneNumber]
      ,[PhoneExtension]
      ,[International]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAlternateContactPhone]
WHERE [DeletedFlag] = 0

