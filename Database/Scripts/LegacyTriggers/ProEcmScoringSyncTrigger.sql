CREATE TRIGGER [ProEcmScoringSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRO_ECM_Scoring]
FOR INSERT, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--INSERT
	IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[MechanismTemplateElementScoring]
           ([MechanismTemplateElementId]
           ,[ClientScoringId]
           ,[StepTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId,
	CASE WHEN inserted.Scoring_Phase = 'initial' THEN 5 WHEN inserted.Scoring_Phase = 'revised' THEN 6 ELSE 7 END,
	VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM  inserted INNER JOIN
	[$(DatabaseName)].[dbo].MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
	[$(DatabaseName)].[dbo].ViewMechanismTemplate MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
	[$(DatabaseName)].[dbo].ClientElement ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId INNER JOIN
	[$(DatabaseName)].[dbo].ClientScoringScale ClientScoringScale ON inserted.Hi_Val = ClientScoringScale.HighValue AND
	ISNULL(inserted.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
	inserted.Middle_Val = ClientScoringScale.MiddleValue AND
	ISNULL(inserted.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
	inserted.Low_Val = ClientScoringScale.LowValue AND
	ISNULL(inserted.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
	inserted.Scoring_Type = ClientScoringScale.ScoreType AND ClientElement.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE MechanismTemplate.ReviewStageId = 1 AND MechanismTemplateElement.DeletedFlag = 0
	INSERT INTO [$(DatabaseName)].[dbo].[MechanismTemplateElementScoring]
           ([MechanismTemplateElementId]
           ,[ClientScoringId]
           ,[StepTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId,
	8,	VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM  inserted INNER JOIN
	[$(DatabaseName)].[dbo].MechanismTemplateElement MechanismTemplateElement ON inserted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
	[$(DatabaseName)].[dbo].ViewMechanismTemplate MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
	[$(DatabaseName)].[dbo].ClientElement ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId INNER JOIN
	[$(DatabaseName)].[dbo].ClientScoringScale ClientScoringScale ON inserted.Hi_Val = ClientScoringScale.HighValue AND
	ISNULL(inserted.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
	inserted.Middle_Val = ClientScoringScale.MiddleValue AND
	ISNULL(inserted.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
	inserted.Low_Val = ClientScoringScale.LowValue AND
	ISNULL(inserted.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
	inserted.Scoring_Type = ClientScoringScale.ScoreType AND ClientElement.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE MechanismTemplate.ReviewStageId = 2 AND MechanismTemplateElement.DeletedFlag = 0 AND inserted.Scoring_Phase = 'meeting'
	END
	--DELETE (soft delete all for an ECM_ID to save time)
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[MechanismTemplateElementScoring]
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].MechanismTemplateElement MechanismTemplateElement ON deleted.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
		[$(DatabaseName)].[dbo].MechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId
	WHERE MechanismTemplateElementScoring.DeletedFlag = 0
END