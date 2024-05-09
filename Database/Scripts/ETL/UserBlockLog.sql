INSERT INTO [dbo].[UserBlockLog]
           ([UserInfoId]
           ,[EnteredByUserId]
           ,[BlockComment]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfoId, CreatedBy, EvaluationComment, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
FROM [UserEvaluationLog]
WHERE DeletedFlag = 0 AND BlockFlag = 1;