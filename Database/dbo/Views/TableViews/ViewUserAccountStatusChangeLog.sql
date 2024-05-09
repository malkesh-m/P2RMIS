CREATE VIEW [dbo].ViewUserAccountStatusChangeLog AS
SELECT [UserAccountStatusChangeLogId]
      ,[UserId]
      ,[NewAccountStatusId]
      ,[OldAccountStatusId]
      ,[NewAccountStatusReasonId]
      ,[OldAccountStatusReasonId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAccountStatusChangeLog]
WHERE [DeletedFlag] = 0

