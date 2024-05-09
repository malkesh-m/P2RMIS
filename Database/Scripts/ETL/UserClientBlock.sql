WITH cte
AS (Select [UserEvaluationLogId], ROW_NUMBER() OVER (Partition By  UserInfoId Order By UserEvaluationLogId asc) AS dupCount
From [UserEvaluationLog] 
WHERE [DeletedFlag] = 0 AND (BlockFlag = 1))
INSERT INTO [dbo].[UserClientBlock]
           ([UserInfoId]
           ,[ClientId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])

SELECT    UserInfoId, 19, CreatedBy, 
                      CreatedDate, ModifiedBy, ModifiedDate
FROM         UserEvaluationLog INNER JOIN
cte ON cte.UserEvaluationLogId = UserEvaluationLog.UserEvaluationLogId
WHERE    [DeletedFlag] = 0 AND (BlockFlag = 1) AND (cte.dupCount) = 1;