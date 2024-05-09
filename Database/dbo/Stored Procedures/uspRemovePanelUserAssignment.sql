CREATE PROCEDURE [dbo].[uspRemovePanelUserAssignment]
	@PanelUserAssignmentId int,
	@UserId int
AS
BEGIN
--ensure entire transaction rolls back in case of error
SET XACT_ABORT ON;
DECLARE @CurrentDateTime DATETIME2(0) = dbo.GetP2rmisDateTime(),
		@ApplicationWorkflowId int
	
	--Remove all data related to panel user assignment and children in reverse order
	--First looping through all related ApplicationWorkflow information
	DECLARE ApplicationWorkflowCursor CURSOR FOR
		SELECT ViewApplicationWorkflow.ApplicationWorkflowId
		FROM ViewApplicationWorkflow
		INNER JOIN ViewPanelUserAssignment ON ViewApplicationWorkflow.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId
		WHERE ViewPanelUserAssignment.PanelUserAssignmentId = @PanelUserAssignmentId
	OPEN ApplicationWorkflowCursor;
	FETCH NEXT FROM ApplicationWorkflowCursor INTO @ApplicationWorkflowId;			
	WHILE @@FETCH_STATUS=0
	BEGIN
		EXEC uspRemoveApplicationWorkflow @ApplicationWorkflowId, @UserId;
		FETCH NEXT FROM ApplicationWorkflowCursor INTO @ApplicationWorkflowId;	
	END
	CLOSE ApplicationWorkflowCursor;
	DEALLOCATE ApplicationWorkflowCursor;
	--Remove Meeting registration and related
	UPDATE MeetingRegistrationAttendance
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM MeetingRegistrationAttendance
	INNER JOIN ViewMeetingRegistration ON MeetingRegistrationAttendance.MeetingRegistrationId = ViewMeetingRegistration.MeetingRegistrationId
	WHERE ViewMeetingRegistration.PanelUserAssignmentId = @PanelUserAssignmentId AND MeetingRegistrationAttendance.DeletedFlag = 0;
	UPDATE MeetingRegistrationTravel
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM MeetingRegistrationTravel
	INNER JOIN ViewMeetingRegistration ON MeetingRegistrationTravel.MeetingRegistrationId = ViewMeetingRegistration.MeetingRegistrationId
	WHERE ViewMeetingRegistration.PanelUserAssignmentId = @PanelUserAssignmentId AND MeetingRegistrationTravel.DeletedFlag = 0;
	UPDATE MeetingRegistrationHotel
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM MeetingRegistrationHotel
	INNER JOIN ViewMeetingRegistration ON MeetingRegistrationHotel.MeetingRegistrationId = ViewMeetingRegistration.MeetingRegistrationId
	WHERE ViewMeetingRegistration.PanelUserAssignmentId = @PanelUserAssignmentId AND MeetingRegistrationHotel.DeletedFlag = 0;
	UPDATE MeetingRegistration
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
	--Remove Panel registration and related
	UPDATE PanelUserRegistrationDocumentItem
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM PanelUserRegistrationDocumentItem
	INNER JOIN ViewPanelUserRegistrationDocument ON PanelUserRegistrationDocumentItem.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId
	INNER JOIN ViewPanelUserRegistration ON ViewPanelUserRegistrationDocument.PanelUserRegistrationId = ViewPanelUserRegistration.PanelUserRegistrationId
	WHERE PanelUserRegistrationDocumentItem.DeletedFlag = 0 AND ViewPanelUserRegistration.PanelUserAssignmentId = @PanelUserAssignmentId;
	UPDATE PanelUserRegistrationDocumentContract
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM PanelUserRegistrationDocumentContract
	INNER JOIN ViewPanelUserRegistrationDocument ON PanelUserRegistrationDocumentContract.PanelUserRegistrationDocumentId = ViewPanelUserRegistrationDocument.PanelUserRegistrationDocumentId
	INNER JOIN ViewPanelUserRegistration ON ViewPanelUserRegistrationDocument.PanelUserRegistrationId = ViewPanelUserRegistration.PanelUserRegistrationId
	WHERE PanelUserRegistrationDocumentContract.DeletedFlag = 0 AND ViewPanelUserRegistration.PanelUserAssignmentId = @PanelUserAssignmentId;
	UPDATE PanelUserRegistrationDocument
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM PanelUserRegistrationDocument
	INNER JOIN ViewPanelUserRegistration ON PanelUserRegistrationDocument.PanelUserRegistrationId = ViewPanelUserRegistration.PanelUserRegistrationId
	WHERE PanelUserRegistrationDocument.DeletedFlag = 0 AND ViewPanelUserRegistration.PanelUserAssignmentId = @PanelUserAssignmentId;
	UPDATE PanelUserRegistration
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
	--Remove expertise, COI and application assignments
	UPDATE PanelApplicationReviewerCoiDetail
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM PanelApplicationReviewerCoiDetail
	INNER JOIN ViewPanelApplicationReviewerExpertise ON PanelApplicationReviewerCoiDetail.PanelApplicationReviewerExpertiseId = ViewPanelApplicationReviewerExpertise.PanelApplicationReviewerExpertiseId
	WHERE ViewPanelApplicationReviewerExpertise.PanelUserAssignmentId = @PanelUserAssignmentId AND PanelApplicationReviewerCoiDetail.DeletedFlag = 0;
	UPDATE PanelApplicationReviewerExpertise
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
	UPDATE PanelApplicationReviewerAssignment
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
	--Finally evaluations and the potential/full assignment itself
	UPDATE ReviewerEvaluation
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
	UPDATE PanelUserPotentialAssignment
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	FROM PanelUserPotentialAssignment
	INNER JOIN ViewPanelUserAssignment ON PanelUserPotentialAssignment.SessionPanelId = ViewPanelUserAssignment.SessionPanelId 
		AND PanelUserPotentialAssignment.UserId = ViewPanelUserAssignment.UserId
	WHERE ViewPanelUserAssignment.PanelUserAssignmentId = @PanelUserAssignmentId AND PanelUserPotentialAssignment.DeletedFlag = 0;
	UPDATE PanelUserAssignment
	SET DeletedFlag = 1, DeletedDate = @CurrentDateTime, DeletedBy = @UserId
	WHERE DeletedFlag = 0 AND PanelUserAssignmentId = @PanelUserAssignmentId;
END
GO
	GRANT EXECUTE
    ON OBJECT::[dbo].[uspRemovePanelUserAssignment] TO [NetSqlAzMan_Users]
    AS [dbo];