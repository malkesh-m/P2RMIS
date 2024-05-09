CREATE TRIGGER [PplEvalLogSyncTrigger]
ON [$(P2RMIS)].dbo.[PPL_Eval_Log]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[UserEvaluationLog]
	SET [ShowCommentFlag] = inserted.Show_Comment_flg
      ,[EvaluationComment] = inserted.Eval_Comments
      ,[EvaluationDate] = inserted.Eval_Date
      ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].dbo.UserEvaluationLog UserEvaluationLog ON inserted.RevEvalID = UserEvaluationLog.LegacyRevEvalId LEFT OUTER JOIN
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
		   ,[LegacyRevEvalId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT UserInfo.UserInfoID, 0, [PPL_Eval_Log].Show_Comment_flg, [PPL_Eval_Log].[Eval_Comments], [PPL_Eval_Log].[Eval_Date], [PPL_Eval_Log].RevEvalID, vun.UserId, [PPL_Eval_Log].[LAST_UPDATE_DATE], vun.UserId, [PPL_Eval_Log].[LAST_UPDATE_DATE]
	FROM inserted PPL_Eval_Log  INNER JOIN
		[$(DatabaseName)].dbo.[User] [User] ON PPL_Eval_Log.Person_ID = [User].PersonID INNER JOIN
		[$(DatabaseName)].dbo.UserInfo [UserInfo] ON [User].[UserID] = UserInfo.UserID LEFT OUTER JOIN 
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId vun ON [PPL_Eval_Log].LAST_UPDATED_BY = vun.UserName
	END
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[UserEvaluationLog]
	SET DeletedFlag = 1, DeletedDate = SYSDATETIME()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.UserEvaluationLog UserEvaluationLog ON deleted.RevEvalID = UserEvaluationLog.LegacyRevEvalId
	WHERE UserEvaluationLog.DeletedFlag = 0
END