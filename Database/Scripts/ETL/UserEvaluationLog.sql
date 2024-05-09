DELETE FROM UserEvaluationLog;

INSERT INTO UserEvaluationLog ([UserInfoId]
           ,[BlockFlag]
           ,[ShowCommentFlag]
           ,[EvaluationComment]
           ,[EvaluationDate]
		   ,[LegacyRevEvalId]
		   ,[LegacyRevEvalBlockId]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfo.UserInfoID, 1, 1, [PPL_Eval_Log_NoRecruit].[Eval_Comments], [PPL_Eval_Log_NoRecruit].[Eval_Date], NULL, [PPL_Eval_Log_NoRecruit].RevEvalBKID, vun2.UserId, vun.UserId, [PPL_Eval_Log_NoRecruit].[LAST_UPDATE_DATE]
FROM [$(P2RMIS)].dbo.PPL_Eval_Log_NoRecruit PPL_Eval_Log_NoRecruit  INNER JOIN
[User] ON PPL_Eval_Log_NoRecruit.Person_ID = [User].PersonID INNER JOIN
UserInfo ON [User].[UserID] = UserInfo.UserID LEFT OUTER JOIN 
ViewLegacyUserNameToUserId vun ON [PPL_Eval_Log_NoRecruit].LAST_UPDATED_BY = vun.UserName LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun2 ON [PPL_Eval_Log_NoRecruit].ORG_Entered_BY = vun2.UserName
UNION ALL
SELECT UserInfo.UserInfoID, 0, [PPL_Eval_Log].Show_Comment_flg, [PPL_Eval_Log].[Eval_Comments], [PPL_Eval_Log].[Eval_Date], [PPL_Eval_Log].RevEvalID, NULL, vun2.UserId, vun.UserId, [PPL_Eval_Log].[LAST_UPDATE_DATE]
FROM [$(P2RMIS)].dbo.PPL_Eval_Log PPL_Eval_Log  INNER JOIN
[User] ON PPL_Eval_Log.Person_ID = [User].PersonID INNER JOIN
UserInfo ON [User].[UserID] = UserInfo.UserID LEFT OUTER JOIN 
ViewLegacyUserNameToUserId vun ON [PPL_Eval_Log].LAST_UPDATED_BY = vun.UserName LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun2 ON [PPL_Eval_Log].ORG_Entered_BY = vun2.UserName