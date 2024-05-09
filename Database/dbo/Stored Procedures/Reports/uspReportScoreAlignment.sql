-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 3/24/2020
-- Description: Storeprocedure to Create Client Report Score Alignment
-- =============================================
CREATE PROCEDURE [dbo].[uspReportScoreAlignment] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@PanelList varchar(4000)

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
PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT        ClientProgram.ProgramAbbreviation, ViewSessionPanel.PanelAbbreviation, ViewApplication.LogNumber, ViewClientElement.ElementAbbreviation, ViewMechanismTemplateElement.OverallFlag, ViewSessionPanel.PanelName, 
                         ViewMechanismTemplateElement.SortOrder, ViewClientAwardType.AwardAbbreviation,ViewClientAwardType.AwardDescription, ViewApplicationWorkflowStep.StepName AS StepTypeName, ViewPanelApplicationReviewerAssignment.SortOrder AS Sort_Order, ViewProgramYear.Year, 
                         ClientScoringScale.ScoreType, CASE WHEN ClientScoringScale.ScoreType = 'Integer' THEN CAST(CAST(ViewApplicationWorkflowStepElementContent.Score AS DECIMAL(9, 6)) AS float) 
						 ELSE ViewApplicationWorkflowStepElementContent.Score END AS Score, ViewApplicationWorkflowStepElementContent.Abstain,ClientScoringScaleAdjectival.ScoreLabel, ViewApplicationWorkflowStep.StepTypeId, ClientProgram.ProgramDescription, ViewClientElement.ElementDescription, ViewMechanismTemplateElement.ScoreFlag,
                         ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId, CASE WHEN ViewApplicationWorkflowStepElementContent.Score = 9 OR
                         ViewApplicationWorkflowStepElementContent.Score = 10 THEN 'O' WHEN ViewApplicationWorkflowStepElementContent.Score = 8 OR
                         ViewApplicationWorkflowStepElementContent.Score = 7 THEN 'E' WHEN ViewApplicationWorkflowStepElementContent.Score = 6 OR
                         ViewApplicationWorkflowStepElementContent.Score = 5 THEN 'G' WHEN ViewApplicationWorkflowStepElementContent.Score = 4 OR
                         ViewApplicationWorkflowStepElementContent.Score = 3 THEN 'F' WHEN ViewApplicationWorkflowStepElementContent.Score = 2 OR
                         ViewApplicationWorkflowStepElementContent.Score = 1 THEN 'D' ELSE ' ' END AS Label
FROM            ClientProgram INNER JOIN
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
                         ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId INNER JOIN
                         ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND 
                         ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
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
                         ClientScoringScale ON ClientScoringScale.ClientScoringId = ViewApplicationWorkflowStepElement.ClientScoringId LEFT OUTER JOIN
                         ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND 
                         ViewApplicationWorkflowStepElementContent.Score = ClientScoringScaleAdjectival.NumericEquivalent INNER JOIN
                         ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId  INNER JOIN               
                      ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
						FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
						--CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
						PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
WHERE        (ClientAssignmentType.AssignmentTypeId <> 8) AND (ClientAssignmentType.AssignmentTypeId <> 7) AND (ReviewStatus.ReviewStatusTypeId = 1) AND (ViewMechanismTemplateElement.ScoreFlag = 1) AND 
                         (ClientParticipantType.ClientParticipantTypeId in (109,175,198,103))
ORDER BY ViewClientAwardType.AwardDescription, ViewMechanismTemplateElement.SortOrder, ViewApplication.LogNumber, Sort_Order, ViewApplicationWorkflowStep.StepTypeId
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportScoreAlignment] TO [NetSqlAzMan_Users]
    AS [dbo];
