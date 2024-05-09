INSERT INTO [dbo].[UserAccountRecovery]
           ([UserId]
           ,[RecoveryQuestionId]
           ,[Answer]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[QuestionOrder])
           
SELECT UserID, Q1ID, Answer1, CASE WHEN CreatedBy IS NULL THEN 10 ELSE CreatedBy END AS CreatedBy, CreatedDate, CASE WHEN ModifiedBy IS NULL 
               THEN 10 ELSE ModifiedBy END AS ModifiedBy, ModifiedDate, 1 AS Expr1
FROM  [User] 
WHERE (Q1ID IS NOT NULL)
UNION ALL
SELECT UserID, Q2ID, Answer2, CASE WHEN CreatedBy IS NULL THEN 10 ELSE CreatedBy END AS CreatedBy, CreatedDate, CASE WHEN ModifiedBy IS NULL 
               THEN 10 ELSE ModifiedBy END AS ModifiedBy, ModifiedDate, 2 AS Expr1
FROM  [User] AS User_1
WHERE (Q2ID IS NOT NULL)
UNION ALL
SELECT UserID, Q3ID, Answer3, CASE WHEN CreatedBy IS NULL THEN 10 ELSE CreatedBy END AS CreatedBy, CreatedDate, CASE WHEN ModifiedBy IS NULL 
               THEN 10 ELSE ModifiedBy END AS ModifiedBy, ModifiedDate, 3 AS Expr1
FROM  [User] AS User_2
WHERE (Q3ID IS NOT NULL)