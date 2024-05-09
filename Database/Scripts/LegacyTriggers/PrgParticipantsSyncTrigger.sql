CREATE TRIGGER [PrgParticipantsSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Participants
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	--NOTE: This sproc won't work for batch inserts (of panel and program), can't think of any functionaity that batch inserts for this table
	SET NOCOUNT ON
	DECLARE @PanelUserAssignmentId int;

	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		--Program participant
		IF EXISTS (Select * FROM inserted WHERE inserted.Panel_ID IS NULL)
			UPDATE [$(DatabaseName)].[dbo].[ProgramUserAssignment]
			SET UserId = U.UserId, ClientParticipantTypeId = cpt.ClientParticipantTypeId, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
				[$(DatabaseName)].[dbo].[ProgramUserAssignment] ProgramUserAssignment ON inserted.PRG_Part_ID = ProgramUserAssignment.LegacyParticipantId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewProgramYear] ProgramYear ON ProgramUserAssignment.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
				[$(DatabaseName)].[dbo].[ClientProgram] ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
				[$(DatabaseName)].[dbo].[ClientParticipantType] cpt ON  inserted.Part_Type = cpt.LegacyPartTypeId AND
				ClientProgram.ClientId = cpt.ClientId LEFT OUTER JOIN
				[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
			WHERE ProgramUserAssignment.DeletedFlag = 0
		--Panel participant
		ELSE	
			UPDATE [$(DatabaseName)].[dbo].[PanelUserAssignment]
			SET UserId = U.UserId, SessionPanelId = SP.SessionPanelId, ClientParticipantTypeId = cpt.ClientParticipantTypeId, 
			ClientRoleId = cr.ClientRoleId,	RestrictedAssignedFlag = PartMapping.RestrictedAssignedFlag, 
			ParticipationMethodId = PartMapping.NewParticipantMethod, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
				[$(P2RMIS)].[dbo].PRG_Program_LU PRG ON inserted.Program = PRG.Program INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
				[$(DatabaseName)].[dbo].[PanelUserAssignment] pua ON inserted.PRG_PART_ID = pua.LegacyParticipantId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewSessionPanel] SP ON inserted.Panel_ID = SP.LegacyPanelId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewMeetingSession] MS ON SP.MeetingSessionId = MS.MeetingSessionId INNER JOIN
				[$(DatabaseName)].[dbo].[ClientMeeting] CM ON MS.ClientMeetingId = CM.ClientMeetingId LEFT OUTER JOIN
				[$(P2RMIS)].[dbo].PRG_Part_Role_Type PRT ON inserted.Part_Role_Type = PRT.Part_Role_Type AND Prg.Client = PRT.Client LEFT OUTER JOIN
				[$(DatabaseName)].[dbo].[ClientRole] CR ON PRT.Role_ID = CR.LegacyRoleId AND 
				CM.ClientId = CR.ClientId CROSS APPLY
				[$(DatabaseName)].[dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, ISNULL(CR.SpecialistFlag, 0), CM.MeetingTypeId, CM.ClientId) PartMapping INNER JOIN
				[$(DatabaseName)].[dbo].[ClientParticipantType] cpt ON  PartMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND
				CM.ClientId = cpt.ClientId  LEFT OUTER JOIN
				[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
			WHERE pua.DeletedFlag = 0
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
		--Program participant
		IF EXISTS (Select * FROM inserted WHERE inserted.Panel_ID IS NULL)
			INSERT INTO [$(DatabaseName)].[dbo].[ProgramUserAssignment]
           ([ProgramYearId]
           ,[UserId]
           ,[ClientParticipantTypeId]
           ,[LegacyParticipantId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
			SELECT ProgramYear.ProgramYearId, U.UserId, cpt.ClientParticipantTypeId, inserted.PRG_Part_Id, VUN.UserId,
			inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
				[$(DatabaseName)].[dbo].[ClientProgram] ClientProgram ON inserted.Program = ClientProgram.LegacyProgramId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewProgramYear] ProgramYear ON inserted.FY = ProgramYear.Year AND ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
				[$(DatabaseName)].[dbo].[ClientParticipantType] cpt ON  inserted.Part_Type = cpt.LegacyPartTypeId AND
				ClientProgram.ClientId = cpt.ClientId LEFT OUTER JOIN
				[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		--Panel participant
		ELSE
		BEGIN
			INSERT INTO [$(DatabaseName)].[dbo].[PanelUserAssignment]
           ([SessionPanelId]
           ,[UserId]
           ,[ClientParticipantTypeId]
           ,[ClientRoleId]
           ,[LegacyParticipantId]
		   ,[RestrictedAssignedFlag]
		   ,[ParticipationMethodId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT SP.SessionPanelId, U.UserId, cpt.ClientParticipantTypeId, cr.ClientRoleId, inserted.PRG_Part_Id, PartMapping.RestrictedAssignedFlag, 
		   ParticipationMethodId = PartMapping.NewParticipantMethod, VUN.UserId,
			inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
				[$(P2RMIS)].[dbo].PRG_Program_LU PRG ON inserted.Program = PRG.Program INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewSessionPanel] SP ON inserted.Panel_ID = SP.LegacyPanelId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewMeetingSession] MS ON SP.MeetingSessionId = MS.MeetingSessionId INNER JOIN
				[$(DatabaseName)].[dbo].[ClientMeeting] CM ON MS.ClientMeetingId = CM.ClientMeetingId LEFT OUTER JOIN
				[$(P2RMIS)].[dbo].PRG_Part_Role_Type PRT ON inserted.Part_Role_Type = PRT.Part_Role_Type AND Prg.Client = PRT.Client LEFT OUTER JOIN
				[$(DatabaseName)].[dbo].[ClientRole] CR ON PRT.Role_ID = CR.LegacyRoleId AND CM.ClientId = CR.ClientId CROSS APPLY
				[$(DatabaseName)].[dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, ISNULL(CR.SpecialistFlag, 0), CM.MeetingTypeId, CM.ClientId) PartMapping INNER JOIN
				[$(DatabaseName)].[dbo].[ClientParticipantType] cpt ON  PartMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND
				CM.ClientId = cpt.ClientId LEFT OUTER JOIN
				[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
			WHERE U.DeletedFlag = 0
			SELECT @PanelUserAssignmentId = SCOPE_IDENTITY();
			--Add registration steps
			--IF @PanelUserAssignmentId IS NOT NULL
			--	EXEC [$(DatabaseName)].dbo.uspCreatePanelUserRegistration @PanelUserAssignmentId, 10
			--Mark user's profile as not verified
			UPDATE U
			SET Verified = 0, W9Verified = null
			FROM inserted INNER JOIN
				[$(DatabaseName)].[dbo].[User] U ON inserted.Person_ID = U.PersonId
			END

	END
	--DELETE
	ELSE
	BEGIN
		--Program participant
		IF EXISTS (Select * FROM deleted WHERE deleted.Panel_ID IS NULL)
			UPDATE [$(DatabaseName)].[dbo].[ProgramUserAssignment] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.ProgramUserAssignment ProgramUserAssignment ON deleted.PRG_Part_ID = ProgramUserAssignment.LegacyParticipantId
			WHERE ProgramUserAssignment.DeletedFlag = 0
		--Panel participant
		ELSE
		BEGIN
			UPDATE [$(DatabaseName)].[dbo].[PanelUserRegistrationDocumentItem] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistration PanelUserRegistration ON PanelUserAssignment.PanelUserAssignmentId = PanelUserRegistration.PanelUserAssignmentId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistrationDocument PanelUserRegistrationDocument ON PanelUserRegistration.PanelUserRegistrationId = PanelUserRegistrationDocument.PanelUserRegistrationId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistrationDocumentItem PanelUserRegistrationDocumentItem ON PanelUserRegistrationDocument.PanelUserRegistrationDocumentId = PanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId
			WHERE PanelUserRegistrationDocumentItem.DeletedFlag = 0
			UPDATE [$(DatabaseName)].[dbo].[PanelUserRegistrationDocument] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistration PanelUserRegistration ON PanelUserAssignment.PanelUserAssignmentId = PanelUserRegistration.PanelUserAssignmentId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistrationDocument PanelUserRegistrationDocument ON PanelUserRegistration.PanelUserRegistrationId = PanelUserRegistrationDocument.PanelUserRegistrationId 
			WHERE PanelUserRegistrationDocument.DeletedFlag = 0
			UPDATE [$(DatabaseName)].[dbo].[PanelUserRegistration] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserRegistration PanelUserRegistration ON PanelUserAssignment.PanelUserAssignmentId = PanelUserRegistration.PanelUserAssignmentId 
			WHERE PanelUserRegistration.DeletedFlag = 0
		--Soft delete all data under the ApplicationWorkflow for the critique/scores that were deleted
				--Soft delete all data under the ApplicationWorkflow for the critique/scores that were deleted
		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElementContent] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
		WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0
		
		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepElement] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStepElement.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepWorkLog] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepWorkLog ApplicationWorkflowStepWorkLog ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId
		WHERE ApplicationWorkflowStepWorkLog.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStepAssignment] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStepAssignment ApplicationWorkflowStepAssignment ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId 
		WHERE ApplicationWorkflowStepAssignment.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflowStep] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
		WHERE ApplicationWorkflowStep.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[ApplicationWorkflow] 
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].dbo.ApplicationWorkflow ApplicationWorkflow ON PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId
		WHERE ApplicationWorkflow.DeletedFlag = 0
			UPDATE [$(DatabaseName)].[dbo].[PanelUserPotentialAssignment] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.ViewPanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
					[$(DatabaseName)].dbo.PanelUserPotentialAssignment PanelUserPotentialAssignment ON PanelUserAssignment.UserId = PanelUserPotentialAssignment.UserId AND PanelUserAssignment.SessionPanelId = PanelUserPotentialAssignment.SessionPanelId
			WHERE PanelUserPotentialAssignment.DeletedFlag = 0
			UPDATE [$(DatabaseName)].[dbo].[PanelUserAssignment] 
			SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
			FROM deleted INNER JOIN
					[$(DatabaseName)].dbo.PanelUserAssignment PanelUserAssignment ON deleted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId
			WHERE PanelUserAssignment.DeletedFlag = 0
		END
	END
END
