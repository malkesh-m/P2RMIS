-- Get peer review data for web service
CREATE PROCEDURE [dbo].[uspGetPeerReviewData]
	@ClientId int
AS
BEGIN
	DECLARE @CoiDocumentTypeId INT;
	SET @CoiDocumentTypeId = 2;

	SELECT ViewApplication.[ApplicationId], ViewApplication.[LogNumber], ViewPanelApplication.[PanelApplicationId], 
	ViewSessionPanel.[PanelName], ViewSessionPanel.[PanelAbbreviation], ViewMeetingSession.[StartDate], ViewMeetingSession.[EndDate], 
	MeetingType.[MeetingTypeName], ViewApplicationStage.[AssignmentReleaseDate] AS [AssignmentReleaseDate], 
	ReviewStatus.[ReviewStatusId], ReviewStatus.[ReviewStatusLabel], COALESCE(ApplicationStageCalculatedScore.AverageScore, CASE WHEN ViewProgramYear.ClientProgramId = 18 AND ReviewStatus.ReviewStatusId = 1 Then ScoresNoCr.AvgScore Else Scores.AvgScore END) as AvgScore,
	COALESCE(ApplicationStageCalculatedScore.StandardDeviation, CASE WHEN ViewProgramYear.ClientProgramId = 18 AND ReviewStatus.ReviewStatusId = 1 Then ScoresNoCr.[StDev] Else Scores.[StDev] END) AS [StDev],	ReviewerAssignment.[FirstName], ReviewerAssignment.[LastName], 
	ReviewerAssignment.[ClientAssignmentTypeId], ReviewerAssignment.[AssignmentLabel], ReviewerAssignment.[SortOrder], 
	ReviewerAssignment.[CoiSignedDate] AS [CoiSignedDate], ReviewerAssignment.[ResolutionDate] AS [ResolutionDate],
	ViewPanelStageStep.StartDate AS ScreeningTcDate, ReviewerAssignment.ScreeningTcCritiqueDate AS ScreeningTcCritiqueDate,
	ViewProgramYear.[Year], ViewProgramMechanism.ReceiptCycle
	FROM ViewApplication
	INNER JOIN ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId
	INNER JOIN ViewPanelApplication ON ViewApplication.[ApplicationId] = ViewPanelApplication.[ApplicationId]
	INNER JOIN ViewSessionPanel ON ViewPanelApplication.[SessionPanelId] = ViewSessionPanel.[SessionPanelId]
	INNER JOIN ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
	INNER JOIN ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
	INNER JOIN ViewMeetingSession ON ViewSessionPanel.[MeetingSessionId] = ViewMeetingSession.[MeetingSessionId]
	INNER JOIN ViewClientMeeting ON ViewMeetingSession.[ClientMeetingId] = ViewClientMeeting.[ClientMeetingId]
	INNER JOIN MeetingType ON ViewClientMeeting.[MeetingTypeId] = MeetingType.[MeetingTypeId]
	INNER JOIN ViewApplicationReviewStatus ON ViewPanelApplication.[PanelApplicationId] = ViewApplicationReviewStatus.[PanelApplicationId]
	INNER JOIN ReviewStatus ON (ViewApplicationReviewStatus.[ReviewStatusId] = ReviewStatus.[ReviewStatusId]
		AND ReviewStatus.ReviewStatusTypeId = 1)
	INNER JOIN ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewPanelStage.ReviewStageId = 1
	INNER JOIN ViewApplicationStage ON (ViewPanelApplication.[PanelApplicationId] = ViewApplicationStage.[PanelApplicationId]	
		AND ViewApplicationStage.ReviewStageId = 1)
	LEFT OUTER JOIN ViewApplicationStage MeetingStage ON ViewPanelApplication.PanelApplicationId = MeetingStage.PanelApplicationId AND MeetingStage.ReviewStageId = 2
	LEFT OUTER JOIN ViewMechanismTemplate MeetingTemplate ON ViewProgramMechanism.ProgramMechanismId = MeetingTemplate.ProgramMechanismId AND MeetingTemplate.ReviewStageId = 2 
	LEFT OUTER JOIN ViewMechanismTemplateElement MeetingOverall ON MeetingTemplate.MechanismTemplateId = MeetingOverall.MechanismTemplateId AND MeetingOverall.OverallFlag = 1
	LEFT OUTER JOIN ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId AND ViewPanelStageStep.StepTypeId = 6
	OUTER APPLY udfLastUpdatedCritiquePhaseAverageOverall(ViewPanelApplication.PanelApplicationId) Scores
	OUTER APPLY udfLastUpdatedCritiquePhaseAverageOverallWoCRs(ViewPanelApplication.PanelApplicationId) ScoresNoCr
	LEFT OUTER JOIN ApplicationStageCalculatedScore ON MeetingStage.ApplicationStageId = ApplicationStageCalculatedScore.ApplicationStageId AND MeetingOverall.MechanismTemplateElementId = ApplicationStageCalculatedScore.MechanismTemplateElementId
	LEFT OUTER JOIN (
		SELECT ViewUserInfo.[FirstName], ViewUserInfo.[LastName], ClientAssignmentType.[ClientAssignmentTypeId], 
		ClientAssignmentType.[AssignmentLabel], ViewPanelApplicationReviewerAssignment.[SortOrder], 
		COALESCE(Registration.[DateSigned], ParticipantInfoTracking.ModifiedDate) AS [CoiSignedDate], COALESCE(PrelimCritiqueStep.[ResolutionDate], PrelimCritiqueStep.ModifiedDate) AS [ScreeningTcCritiqueDate], 
		ViewPanelApplicationReviewerAssignment.[PanelApplicationId], ViewApplicationWorkflow.[ApplicationStageId],
		PrelimCritiqueStep.ResolutionDate AS ResolutionDate
		FROM ViewPanelApplicationReviewerAssignment
		INNER JOIN ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.[PanelUserAssignmentId] = ViewPanelUserAssignment.[PanelUserAssignmentId]
		INNER JOIN ViewApplicationStage ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 1
		INNER JOIN ViewUser ON ViewPanelUserAssignment.[UserId] = ViewUser.[UserID]
		INNER JOIN ViewUserInfo ON ViewUser.[UserID] = ViewUserInfo.[UserID]
		INNER JOIN ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.[ClientAssignmentTypeId] = ClientAssignmentType.[ClientAssignmentTypeId]
		LEFT OUTER JOIN (SELECT ViewPanelUserRegistration.PanelUserAssignmentId, ViewPanelUserRegistrationDocument.[DateSigned]
			FROM ViewPanelUserRegistration 
			INNER JOIN ViewPanelUserRegistrationDocument ON ViewPanelUserRegistration.[PanelUserRegistrationId] = ViewPanelUserRegistrationDocument.[PanelUserRegistrationId]
			INNER JOIN ClientRegistrationDocument ON ViewPanelUserRegistrationDocument.[ClientRegistrationDocumentId] = ClientRegistrationDocument.[ClientRegistrationDocumentId]
			 AND ClientRegistrationDocument.[RegistrationDocumentTypeId] = @CoiDocumentTypeId) Registration ON ViewPanelUserAssignment.PanelUserAssignmentId = Registration.PanelUserAssignmentId
		LEFT OUTER JOIN ParticipantInfoTracking ON ViewPanelUserAssignment.LegacyParticipantId = ParticipantInfoTracking.PrgPartId AND ParticipantInfoTracking.DocumentName = 'Bias and Conflict of Interest'
		LEFT OUTER JOIN ViewApplicationWorkflow ON ViewPanelUserAssignment.[PanelUserAssignmentId] = ViewApplicationWorkflow.[PanelUserAssignmentId] AND ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId
		LEFT OUTER JOIN ViewApplicationWorkflowStep PrelimCritiqueStep ON ViewApplicationWorkflow.[ApplicationWorkflowId] = PrelimCritiqueStep.[ApplicationWorkflowId] AND PrelimCritiqueStep.[StepOrder] = 1
		) AS ReviewerAssignment ON ReviewerAssignment.[PanelApplicationId] = ViewPanelApplication.[PanelApplicationId]
	WHERE ViewClientMeeting.[ClientId] = @ClientId
	ORDER BY ViewApplication.[ApplicationId]
END
GO

	GRANT EXECUTE
    ON OBJECT::[dbo].[uspGetPeerReviewData] TO [NetSqlAzMan_Users], [web-p2rmis]
    AS [dbo];