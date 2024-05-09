
CREATE PROCEDURE [dbo].[uspReportIndividualScores]
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@CycleList varchar(4000)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
WITH ProgramParams(ClientProgramId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
CycleParams(Cycle) 
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList))
SELECT Client.ClientAbrv, ClientProgram.ProgramAbbreviation, ViewSessionPanel.PanelAbbreviation, ViewApplication.LogNumber, ViewClientElement.ElementAbbreviation, 
ClientScoringScale.ScoreType, ViewProgramMechanism.ReceiptCycle,ViewMechanismTemplateElement.OverallFlag, ViewSessionPanel.PanelName, ViewMechanismTemplateElement.SortOrder, 
ViewClientAwardType.AwardDescription, MeetingType.MeetingTypeAbbreviation, MeetingType.MeetingTypeName, StepType.StepTypeName, 
ClientAssignmentType.AssignmentAbbreviation, ViewUserInfo.LastName + ' (' + ClientAssignmentType.AssignmentAbbreviation + ')' AS Rev_Name, 
ViewPanelApplicationReviewerAssignment.SortOrder AS Sort_Order, ClientParticipantType.ClientParticipantTypeId, ReviewStatus.ReviewStatusLabel, 
ViewProgramYear.Year, ViewApplicationWorkflowStepElementContent.Score, ClientScoringScaleAdjectival.ScoreLabel, ViewApplicationWorkflowStep.StepTypeId,
SummaryReviewerDescription.DisplayName, ClientProgram.ProgramDescription, ViewClientElement.ElementDescription, ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId,
NULL AS Assignment, ReviewStatus.ReviewStatusId
FROM ClientProgram INNER JOIN
ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
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
ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 1 INNER JOIN
ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
ViewApplicationWorkflowStepElement ON ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
ViewApplicationTemplateElement ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewClientElement ON ViewMechanismTemplateElement.ClientElementId = ViewClientElement.ClientElementId INNER JOIN
ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
MeetingType ON ViewClientMeeting.MeetingTypeId = MeetingType.MeetingTypeId INNER JOIN
ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId INNER JOIN
StepType ON ViewApplicationWorkflowStep.StepTypeId = StepType.StepTypeId LEFT OUTER JOIN
ViewApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER JOIN
ClientScoringScale ON ViewApplicationWorkflowStepElement.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND ViewApplicationWorkflowStepElementContent.Score = ClientScoringScaleAdjectival.NumericEquivalent INNER JOIN
ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
ClientRole ON ViewPanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId INNER JOIN
--We use special joins in order to force NULLs to act as wildcards
SummaryReviewerDescription ON (ViewProgramMechanism.ProgramMechanismId = SummaryReviewerDescription.ProgramMechanismId OR SummaryReviewerDescription.ProgramMechanismId IS NULL) AND 
(ClientParticipantType.ClientParticipantTypeId = SummaryReviewerDescription.ClientParticipantTypeId OR SummaryReviewerDescription.ClientParticipantTypeId IS NULL) AND 
(ClientRole.ClientRoleId = SummaryReviewerDescription.ClientRoleId OR SummaryReviewerDescription.ClientRoleId IS NULL) AND
(ViewPanelApplicationReviewerAssignment.SortOrder = SummaryReviewerDescription.AssignmentOrder OR SummaryReviewerDescription.AssignmentOrder IS NULL)INNER JOIN
ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                      FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
					  CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle 
WHERE (ClientAssignmentType.AssignmentTypeId <> 8 AND ClientAssignmentType.AssignmentTypeId <> 7) AND (ReviewStatus.ReviewStatusTypeId = 1) AND 
(ViewMechanismTemplateElement.ScoreFlag = 1)
ORDER BY ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId, Sort_Order, ViewMechanismTemplateElement.SortOrder
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportIndividualScores] TO [NetSqlAzMan_Users]
    AS [dbo];
