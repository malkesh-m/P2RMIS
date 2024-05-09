
/*

CREATE TRIGGER [PplCvSyncTrigger]
ON [$(P2RMIS)].[dbo].[PPL_CV]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[UserResume]
	SET DocType = inserted.DocType, ResumeData = CAST(CAST(PPL_CV.CV_Data AS varbinary(max)) AS varchar(max)),
	ReceivedDate = inserted.CV_Received, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(P2RMIS)].dbo.PPL_CV PPL_CV ON inserted.CV_ID = PPL_CV.CV_ID INNER JOIN
	[$(DatabaseName)].dbo.[UserResume] UserResume ON inserted.CV_ID = UserResume.LegacyCvId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 

	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[UserResume]
           ([UserInfoId]
           ,[LegacyCvId]
           ,[DocType]
           ,[ResumeData]
           ,[ReceivedDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT UserInfo.UserInfoID, inserted.CV_ID, inserted.DocType, CAST(CAST(PPL_CV.CV_Data AS varbinary(max)) AS varchar(max)), inserted.CV_Received, VUN.UserId,
	inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(P2RMIS)].dbo.PPL_CV PPL_CV ON inserted.CV_ID = PPL_CV.CV_ID INNER JOIN
		[$(DatabaseName)].dbo.[User] U ON inserted.Person_ID = U.PersonID INNER JOIN
		[$(DatabaseName)].dbo.[UserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 	 
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[UserResume]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].dbo.[UserResume] UserResume ON deleted.CV_ID = UserResume.LegacyCvId
	WHERE UserResume.DeletedFlag = 0
END
*/