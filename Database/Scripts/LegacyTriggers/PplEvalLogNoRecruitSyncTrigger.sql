CREATE TRIGGER [PplEvalLogNoRecruitSyncTrigger]
ON [$(P2RMIS)].dbo.[PPL_Eval_Log_NoRecruit]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[UserEvaluationLog]
	SET [EvaluationComment] = inserted.Eval_Comments
      ,[EvaluationDate] = inserted.Eval_Date
      ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].dbo.UserEvaluationLog UserEvaluationLog ON inserted.RevEvalBKID = UserEvaluationLog.LegacyRevEvalBlockId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE UserEvaluationLog.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[UserEvaluationLog]
           ([UserInfoId]
		   ,[BlockFlag]
           ,[ShowCommentFlag]
           ,[EvaluationComment]
           ,[EvaluationDate]
		   ,[LegacyRevEvalBlockId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT UserInfo.UserInfoID, 1, 1, [PPL_Eval_Log_NoRecruit].[Eval_Comments], [PPL_Eval_Log_NoRecruit].[Eval_Date], [PPL_Eval_Log_NoRecruit].RevEvalBkID, vun.UserId, [PPL_Eval_Log_NoRecruit].[LAST_UPDATE_DATE], vun.UserId, [PPL_Eval_Log_NoRecruit].[LAST_UPDATE_DATE]
	FROM inserted PPL_Eval_Log_NoRecruit  INNER JOIN
		[$(DatabaseName)].dbo.[ViewUser] [User] ON PPL_Eval_Log_NoRecruit.Person_ID = [User].PersonID INNER JOIN
		[$(DatabaseName)].dbo.ViewUserInfo [UserInfo] ON [User].[UserID] = UserInfo.UserID LEFT OUTER JOIN 
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId vun ON [PPL_Eval_Log_NoRecruit].LAST_UPDATED_BY = vun.UserName
	END
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[UserEvaluationLog]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.UserEvaluationLog UserEvaluationLog ON deleted.RevEvalBKID = UserEvaluationLog.LegacyRevEvalBlockId
	WHERE UserEvaluationLog.DeletedFlag = 0
END