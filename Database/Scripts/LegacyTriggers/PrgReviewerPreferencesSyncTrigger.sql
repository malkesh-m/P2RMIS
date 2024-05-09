CREATE TRIGGER [PrgReviewerPreferencesSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Reviewer_Preferences]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise] 
	SET ClientExpertiseRatingId = cer.ClientExpertiseRatingId, ModifiedBy = VUN.UserId, ModifiedDate = rp.LAST_UPDATE_DATE
	FROM inserted rp INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelUserAssignment] pua ON rp.Prg_Part_ID = pua.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewApplication] app ON rp.Log_No = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND pua.SessionPanelId = panapp.SessionPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise] panre ON panapp.PanelApplicationId = panre.PanelApplicationId AND pua.PanelUserAssignmentId = panre.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] ca ON pm.ClientAwardTypeId = ca.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientExpertiseRating] cer ON rp.Rev_Pref = cer.RatingAbbreviation AND ca.ClientId = cer.ClientId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.PRG_Reviewer_Preferences_COI coi ON rp.Prg_Part_ID = coi.Prg_Part_ID AND rp.Log_No = coi.Log_No LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON rp.LAST_UPDATED_BY = vun.UserName
	WHERE panre.DeletedFlag = 0

	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientExpertiseRatingId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT panapp.PanelApplicationId, pua.PanelUserAssignmentId, cer.ClientExpertiseRatingId, VUN.UserId,
	rp.LAST_UPDATE_DATE, VUN.UserId, rp.LAST_UPDATE_DATE
	FROM inserted rp INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelUserAssignment] pua ON rp.Prg_Part_ID = pua.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewApplication] app ON rp.Log_No = app.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND pua.SessionPanelId = panapp.SessionPanelId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientAwardType] ca ON pm.ClientAwardTypeId = ca.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].[ClientExpertiseRating] cer ON rp.Rev_Pref = cer.RatingAbbreviation AND ca.ClientId = cer.ClientId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.PRG_Reviewer_Preferences_COI coi ON rp.Prg_Part_ID = coi.Prg_Part_ID AND rp.Log_No = coi.Log_No LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON rp.LAST_UPDATED_BY = vun.UserName
	WHERE [app].DeletedFlag = 0
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM [$(DatabaseName)].[dbo].PanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].PanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].PanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		deleted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID
	WHERE panre.DeletedFlag = 0
END