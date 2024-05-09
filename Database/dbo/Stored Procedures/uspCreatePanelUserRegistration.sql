CREATE PROCEDURE [dbo].[uspCreatePanelUserRegistration]
	@PanelUserAssignmentId int,
	@UserId int
AS
	/*
		This procedure sets up data for a PanelUser instance of Registration
	*/
BEGIN
	--First check that registration wasn't already set up
	DECLARE @PanelUserRegistrationId int,
	@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime()

	SELECT @PanelUserRegistrationId = PanelUserRegistrationId
	FROM ViewPanelUserRegistration
	WHERE PanelUserAssignmentId = @PanelUserAssignmentId
	IF @PanelUserRegistrationId IS NULL
	BEGIN
		--Insert records for PanelUserRegistration
		INSERT INTO PanelUserRegistration
		([ClientRegistrationId]
           ,[PanelUserAssignmentId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT ClientRegistration.ClientRegistrationId, @PanelUserAssignmentId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM PanelUserAssignment INNER JOIN
		SessionPanel ON PanelUserAssignment.SessionPanelId = SessionPanel.SessionPanelId INNER JOIN
		MeetingSession ON SessionPanel.MeetingSessionId = MeetingSession.MeetingSessionId INNER JOIN
		ClientMeeting ON MeetingSession.ClientMeetingId = ClientMeeting.ClientMeetingId INNER JOIN
		ClientRegistration ON ClientMeeting.ClientId = ClientRegistration.ClientId
		WHERE ClientRegistration.ActiveFlag = 1 AND PanelUserAssignment.PanelUserAssignmentId = @PanelUserAssignmentId
		SET @PanelUserRegistrationId = SCOPE_IDENTITY()
		--Insert records for PanelUserRegistrationDocument
		INSERT INTO [PanelUserRegistrationDocument]
           ([PanelUserRegistrationId]
           ,[ClientRegistrationDocumentId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT @PanelUserRegistrationId, ClientRegistrationDocument.ClientRegistrationDocumentId, @UserId, @CurrentDateTime, @UserId, @CurrentDateTime
		FROM PanelUserRegistration INNER JOIN
		ClientRegistration ON PanelUserRegistration.ClientRegistrationId = ClientRegistration.ClientRegistrationId INNER JOIN
		ClientRegistrationDocument ON ClientRegistration.ClientRegistrationId = ClientRegistrationDocument.ClientRegistrationId
		WHERE PanelUserRegistration.PanelUserRegistrationId = @PanelUserRegistrationId
	END
	--Finally we flag their profile as un-verified
	UPDATE U
	SET Verified = 0, W9Verified = null
	FROM PanelUserAssignment INNER JOIN
	[dbo].[User] U ON PanelUserAssignment.UserId = U.UserID
	WHERE PanelUserAssignment.PanelUserAssignmentId = @PanelUserAssignmentId
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspCreatePanelUserRegistration] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];
