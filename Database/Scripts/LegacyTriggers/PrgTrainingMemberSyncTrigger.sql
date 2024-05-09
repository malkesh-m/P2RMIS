CREATE TRIGGER [PrgTrainingMemberSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Training_Member]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--INSERT/UPDATE
	IF EXISTS (Select * FROM inserted)
	BEGIN
	--We just soft delete all records for the document and re-add for simplicity
	UPDATE [$(DatabaseName)].[dbo].[TrainingDocumentAccess]
		SET	DeletedFlag = 1, DeletedDate = SYSDATETIME()
	FROM inserted PRG_Training_Member INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocument] TrainingDocument ON PRG_Training_Member.TR_ID = TrainingDocument.LegacyTrId INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocumentAccess] TrainingDocumentAccess ON TrainingDocument.TrainingDocumentId = TrainingDocumentAccess.TrainingDocumentId
	WHERE TrainingDocumentAccess.DeletedFlag = 0
	INSERT INTO [$(DatabaseName)].[dbo].[TrainingDocumentAccess]
	([TrainingDocumentId]
			   ,[MeetingTypeId]
			   ,[ClientParticipantTypeId]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
	SELECT TrainingDocument.TrainingDocumentId, MeetingType.MeetingTypeId, ClientParticipantType.ClientParticipantTypeId, VUN.UserID, PRG_Training_Member.Last_Update_Date, VUN.UserID, PRG_Training_Member.Last_Update_Date
	FROM [$(P2RMIS)].dbo.PRG_Training_Member PRG_Training_Member INNER JOIN
	inserted ON PRG_Training_Member.TR_ID = inserted.TR_ID INNER JOIN
	[$(DatabaseName)].[dbo].TrainingDocument TrainingDocument ON PRG_Training_Member.TR_ID = TrainingDocument.LegacyTrId INNER JOIN
	[$(DatabaseName)].[dbo].ProgramYear ProgramYear ON TrainingDocument.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
	[$(DatabaseName)].[dbo].ClientProgram ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
	[$(DatabaseName)].[dbo].ClientParticipantType ClientParticipantType ON ClientProgram.ClientId = ClientParticipantType.ClientId AND
	(ClientParticipantType.ReviewerFlag = 1 OR ClientParticipantType.LegacyPartTypeId IN ('SRA','RTA')) AND ((CASE WHEN PRG_Training_Member.Part_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Part_Type = ClientParticipantType.LegacyPartTypeId)) INNER JOIN
	[$(DatabaseName)].[dbo].MeetingType MeetingType ON ((CASE WHEN PRG_Training_Member.Review_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Review_Type = MeetingType.LegacyMeetingTypeId)) LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON PRG_Training_Member.Last_Updated_By = VUN.UserName
	WHERE TrainingDocument.DeletedFlag = 0
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[TrainingDocumentAccess]
		SET	DeletedFlag = 1, DeletedDate = SYSDATETIME()
	FROM deleted PRG_Training_Member INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocument] TrainingDocument ON PRG_Training_Member.TR_ID = TrainingDocument.LegacyTrId INNER JOIN
		[$(DatabaseName)].[dbo].ProgramYear ProgramYear ON TrainingDocument.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
		[$(DatabaseName)].[dbo].ClientProgram ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
		[$(DatabaseName)].[dbo].ClientParticipantType ClientParticipantType ON ClientProgram.ClientId = ClientParticipantType.ClientId AND
		(ClientParticipantType.ReviewerFlag = 1 OR ClientParticipantType.LegacyPartTypeId IN ('SRA','RTA')) AND ((CASE WHEN PRG_Training_Member.Part_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Part_Type = ClientParticipantType.LegacyPartTypeId)) INNER JOIN
		[$(DatabaseName)].[dbo].MeetingType MeetingType ON ((CASE WHEN PRG_Training_Member.Review_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Review_Type = MeetingType.LegacyMeetingTypeId)) INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocumentAccess] TrainingDocumentAccess ON TrainingDocument.TrainingDocumentId = TrainingDocumentAccess.TrainingDocumentId AND
		MeetingType.MeetingTypeId = TrainingDocumentAccess.MeetingTypeId AND ClientParticipantType.ClientParticipantTypeId = TrainingDocumentAccess.ClientParticipantTypeId
	WHERE TrainingDocumentAccess.DeletedFlag = 0
	END
END