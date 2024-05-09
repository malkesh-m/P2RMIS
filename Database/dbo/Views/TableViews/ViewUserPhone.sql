CREATE VIEW [dbo].ViewUserPhone AS
SELECT [PhoneID]
      ,[UserInfoID]
      ,[PhoneTypeId]
      ,[LegacyPhoneId]
      ,[Phone]
      ,[Extension]
      ,[PrimaryFlag]
      ,[International]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserPhone]
WHERE [DeletedFlag] = 0

