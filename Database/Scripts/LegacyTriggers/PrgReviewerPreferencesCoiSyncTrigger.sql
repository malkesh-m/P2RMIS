CREATE TRIGGER [PrgReviewerPreferencesCoiSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Reviewer_Preferences_COI]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail]
	SET ClientCoiTypeId = cct.ClientCoiTypeId, ModifiedBy = VUN.UserId, ModifiedDate = coi.LAST_UPDATE_DATE
	FROM [$(DatabaseName)].[dbo].PanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].PanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].PanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		inserted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
		[$(DatabaseName)].[dbo].ProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription INNER JOIN
		[$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail] coidetail ON panre.PanelApplicationReviewerExpertiseId = coidetail.PanelApplicationReviewerExpertiseId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
	WHERE coidetail.DeletedFlag = 0
	IF UPDATE(Comments)
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise]
		SET ExpertiseComments = coi.Comments, ModifiedBy = VUN.UserId, ModifiedDate = coi.LAST_UPDATE_DATE
		FROM [$(DatabaseName)].[dbo].PanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].PanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].PanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		inserted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
		[$(DatabaseName)].[dbo].ProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription INNER JOIN
		[$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail] coidetail ON panre.PanelApplicationReviewerExpertiseId = coidetail.PanelApplicationReviewerExpertiseId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
		WHERE panre.DeletedFlag = 0
	END
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail]
           ([PanelApplicationReviewerExpertiseId]
           ,[ClientCoiTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT panre.PanelApplicationReviewerExpertiseId, cct.ClientCoiTypeId,
		VUN.UserId, coi.LAST_UPDATE_DATE, VUN.UserId, coi.LAST_UPDATE_DATE
	FROM [$(DatabaseName)].[dbo].ViewPanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].ViewPanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].ViewPanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		inserted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
		[$(DatabaseName)].[dbo].ViewProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
	WHERE [app].DeletedFlag = 0
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerExpertise]
		SET ExpertiseComments = coi.Comments, ModifiedBy = VUN.UserId, ModifiedDate = coi.LAST_UPDATE_DATE
		FROM [$(DatabaseName)].[dbo].PanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].PanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].PanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		inserted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
		[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
		[$(DatabaseName)].[dbo].ProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription INNER JOIN
		[$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail] coidetail ON panre.PanelApplicationReviewerExpertiseId = coidetail.PanelApplicationReviewerExpertiseId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
	WHERE panre.DeletedFlag = 0
	END
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM [$(DatabaseName)].[dbo].PanelApplicationReviewerExpertise panre INNER JOIN
		[$(DatabaseName)].[dbo].PanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
		[$(DatabaseName)].[dbo].PanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].[Application] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
		deleted coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
		[$(DatabaseName)].[dbo].[PanelApplicationReviewerCoiDetail] coidetail ON panre.PanelApplicationReviewerExpertiseId = coidetail.PanelApplicationReviewerExpertiseId
	WHERE coidetail.DeletedFlag = 0
END