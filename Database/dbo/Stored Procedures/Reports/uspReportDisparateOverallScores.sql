-- =============================================
-- Author:		Alberto Catuche
-- Create date: 7/2016
-- Description:	Used as source for report Disparate Overall Scores
-- =============================================

CREATE PROCEDURE [dbo].[uspReportDisparateOverallScores] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)
	--@CycleList varchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), --@ProgramList 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), --@FiscalYearList 
	--CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)) ,--@CycleList
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList)) --@PanelList 
    SELECT DISTINCT 
          ClientProgram.ClientProgramId, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewProgramYear.Year, 
          ViewProgramMechanism.ReceiptCycle, MeetingType.MeetingTypeAbbreviation, MeetingType.MeetingTypeName, ViewSessionPanel.SessionPanelId, 
          ViewSessionPanel.PanelAbbreviation, ViewPanelApplication.ApplicationId, ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId, 
          StepType.StepTypeName, ViewApplicationWorkflowStepElementContent.Score, ClientScoringScaleAdjectival.ScoreLabel, ClientScoringScale.ScoreType, 
          ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId, ViewMechanismTemplateElement.OverallFlag, ViewClientElement.ElementDescription, 
          ViewMechanismTemplateElement.SortOrder, ClientAssignmentType.AssignmentAbbreviation, ClientParticipantType.ParticipantTypeAbbreviation, 
          ViewUserInfo.LastName, ViewUserInfo.FirstName, ViewPanelApplicationReviewerAssignment.SortOrder AS Sort_Order, SummaryReviewerDescription.DisplayName, 
          ClientRole.RoleAbbreviation, 
          ClientParticipantType.ParticipantTypeAbbreviation + '-' + ParticipationMethod.ParticipationMethodLabel + '-' + CASE WHEN dbo.ViewPanelUserAssignment.RestrictedAssignedFlag
           = 0 THEN 'Full' ELSE 'Adhoc' END + CASE WHEN ClientRole.RoleAbbreviation IS NULL 
          THEN '' ELSE '-' + ClientRole.RoleAbbreviation END AS Assignment_Particip_Role
          , MAX(ViewApplicationWorkflowStepElementContent.Score) OVER (PARTITION BY ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId) AS 'max_score'
          , MIN(ViewApplicationWorkflowStepElementContent.Score) OVER (PARTITION BY ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId) AS 'min_score'
    FROM  ClientProgram INNER JOIN
          ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
          ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
          ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
		  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
          FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
          PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
          ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
          ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
          Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
          ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
          ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
          ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
          ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
          ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
          ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID INNER JOIN
          ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
          ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId INNER JOIN
          ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND 
          ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
          ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
          ViewApplicationWorkflowStepElement ON 
          ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
          ViewApplicationTemplateElement ON 
          ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
          ViewMechanismTemplateElement ON 
          ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId INNER JOIN
          ViewClientElement ON ViewMechanismTemplateElement.ClientElementId = ViewClientElement.ClientElementId INNER JOIN
          ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
          ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
          MeetingType ON ViewClientMeeting.MeetingTypeId = MeetingType.MeetingTypeId INNER JOIN
          ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
          ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId INNER JOIN
          StepType ON ViewApplicationWorkflowStep.StepTypeId = StepType.StepTypeId LEFT OUTER JOIN
          ViewApplicationWorkflowStepElementContent ON 
          ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER JOIN
          ClientScoringScale ON ViewApplicationWorkflowStepElement.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
          ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND 
          ViewApplicationWorkflowStepElementContent.Score = ClientScoringScaleAdjectival.NumericEquivalent INNER JOIN
          ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
          ClientRole ON ViewPanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId CROSS APPLY
		  udfGetSummaryReviewerDescription(ViewProgramMechanism.ProgramMechanismId, ClientParticipantType.ClientParticipantTypeId, ClientRole.ClientRoleId, ViewPanelApplicationReviewerAssignment.SortOrder) SummaryReviewerDescription INNER JOIN
          ParticipationMethod ON ViewPanelUserAssignment.ParticipationMethodId = ParticipationMethod.ParticipationMethodId 
    WHERE (NOT (ViewApplicationWorkflowStepElementContent.Score IS NULL)) AND (ClientAssignmentType.AssignmentTypeId <> 8) AND 
          (ClientAssignmentType.AssignmentTypeId <> 7) AND (ReviewStatus.ReviewStatusTypeId = 1) AND 
          (ViewMechanismTemplateElement.OverallFlag = 1)
    ORDER BY ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId, Sort_Order, ViewMechanismTemplateElement.SortOrder, ViewUserInfo.LastName, ViewUserInfo.FirstName

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportDisparateOverallScores] TO [NetSqlAzMan_Users]
    AS [dbo];