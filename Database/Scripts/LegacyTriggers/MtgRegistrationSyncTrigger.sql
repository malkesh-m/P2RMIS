CREATE TRIGGER [MtgRegistrationSyncTrigger]
ON [$(P2RMIS)].dbo.[Mtg_Registration]
FOR INSERT, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--INSERT
	IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistration]
           ([PanelUserAssignmentId]
           ,[RegistrationSubmittedFlag]
           ,[RegistrationSubmittedDate]
           ,[LegacyMrId]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT PanelUserAssignment.PanelUserAssignmentId, 0, NULL, MTG_Registration.MR_ID, VUN.UserId, MTG_Registration.Last_Update_Date, VUN.UserId, MTG_Registration.Last_Update_Date
	FROM inserted MTG_Registration INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON MTG_Registration.Prg_Part_ID = PanelUserAssignment.LegacyParticipantId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON MTG_Registration.LAST_UPDATED_BY = VUN.UserName 
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistration] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[MeetingRegistration]  MeetingRegistration ON deleted.MR_ID = MeetingRegistration.LegacyMrId
	WHERE MeetingRegistration.DeletedFlag = 0
	END
END