CREATE VIEW [dbo].ViewUserSystemRole AS
SELECT [UserSystemRoleID]
      ,[UserID]
      ,[SystemRoleId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserSystemRole]
WHERE [DeletedFlag] = 0

