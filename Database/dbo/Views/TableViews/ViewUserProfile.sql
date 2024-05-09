CREATE VIEW [dbo].ViewUserProfile AS
SELECT [UserProfileId]
      ,[UserInfoId]
      ,[ProfileTypeId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserProfile]
WHERE [DeletedFlag] = 0

