CREATE VIEW [dbo].ViewUserAccountRecovery AS
SELECT [UserAccountRecoveryId]
      ,[UserId]
      ,[RecoveryQuestionId]
      ,[Answer]
      ,[QuestionOrder]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[UserAccountRecovery]
WHERE [DeletedFlag] = 0

