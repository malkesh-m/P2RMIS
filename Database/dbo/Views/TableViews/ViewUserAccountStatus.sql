CREATE VIEW [dbo].ViewUserAccountStatus AS
SELECT [UserAccountStatusId]
      ,[UserId]
      ,[AccountStatusId]
      ,[AccountStatusReasonId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAccountStatus]
WHERE [DeletedFlag] = 0

