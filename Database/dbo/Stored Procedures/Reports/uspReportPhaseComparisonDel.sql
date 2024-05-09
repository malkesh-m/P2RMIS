-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 1/18/2015
-- Description: Storeprocedure to Create Client Report Score Phase Comparison
-- =============================================
Create PROCEDURE [dbo].[uspReportPhaseComparisonDel] 
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
SELECT     Client.ClientAbrv, ClientProgram.ProgramAbbreviation, ViewSessionPanel.PanelAbbreviation, ViewApplication.LogNumber, ViewClientElement.ElementAbbreviation, 
                      ViewClientElement.ClientElementId, ViewPanelApplication.PanelApplicationId, ClientScoringScale.ScoreType, ViewMechanismTemplateElement.OverallFlag, 
                      ViewSessionPanel.PanelName, ViewMechanismTemplateElement.SortOrder, ViewClientAwardType.AwardDescription, MeetingType.MeetingTypeAbbreviation, 
                      MeetingType.MeetingTypeName, StepType.StepTypeName, ClientAssignmentType.AssignmentAbbreviation, LEFT(ViewUserInfo.FirstName, 4) 
                      + '.' + ' ' + ViewUserInfo.LastName AS LastName, LEFT(ViewUserInfo.FirstName, 1) 
                      + '.' + ' ' + ViewUserInfo.LastName + ' (' + ClientAssignmentType.AssignmentAbbreviation + ')' AS Rev, 
                      ViewPanelApplicationReviewerAssignment.SortOrder AS Sort_Order, ClientParticipantType.ClientParticipantTypeId, ReviewStatus.ReviewStatusLabel, 
                      ViewProgramYear.Year, ViewApplicationWorkflowStepElementContent.Score, ViewApplicationWorkflowStepElementContent.Abstain, 
                      ClientScoringScaleAdjectival.ScoreLabel, ViewApplicationWorkflowStep.StepTypeId, ClientProgram.ProgramDescription, ViewClientElement.ElementDescription, 
                      ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId, SummaryReviewerDescription.DisplayName,SummaryReviewerDescription.CustomOrder,ReviewStatus.ReviewStatusId,Cast((ViewApplicationRevStdev.AvgScore) as decimal (18,1)) as avg, Round(Round(ViewApplicationRevStdev.StDev, 2), 1) As Stdev
FROM         ClientProgram INNER JOIN
ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId  INNER JOIN
                      dbo.ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      dbo.ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      dbo.Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
                      dbo.ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                      dbo.ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
                      dbo.ViewPanelApplicationReviewerAssignment ON 
                      ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
                      dbo.ViewPanelUserAssignment ON 
                      ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      dbo.ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
                      dbo.ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID INNER JOIN
                      dbo.ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
                      dbo.ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId INNER JOIN
                      dbo.ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND 
                      ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      dbo.ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      dbo.ViewApplicationWorkflowStepElement ON 
                      ViewApplicationWorkflowStep.ApplicationWorkflowStepId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      dbo.ViewApplicationTemplateElement ON 
                      ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      dbo.ViewMechanismTemplateElement ON 
                      ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      dbo.ViewClientElement ON ViewMechanismTemplateElement.ClientElementId = ViewClientElement.ClientElementId INNER JOIN
                      dbo.ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
                      dbo.ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
                      dbo.MeetingType ON ViewClientMeeting.MeetingTypeId = MeetingType.MeetingTypeId INNER JOIN
                      dbo.ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
                      dbo.ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId INNER JOIN
                      dbo.StepType ON ViewApplicationWorkflowStep.StepTypeId = StepType.StepTypeId LEFT OUTER JOIN
                      dbo.ViewApplicationWorkflowStepElementContent ON 
                      ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId LEFT OUTER
                       JOIN
                      dbo.ClientScoringScale ON ViewApplicationWorkflowStepElement.ClientScoringId = ClientScoringScale.ClientScoringId LEFT OUTER JOIN
                      dbo.ClientScoringScaleAdjectival ON ClientScoringScale.ClientScoringId = ClientScoringScaleAdjectival.ClientScoringId AND 
                      ViewApplicationWorkflowStepElementContent.Score = ClientScoringScaleAdjectival.NumericEquivalent INNER JOIN
                      dbo.ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN               
                      ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
						FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
						--CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
						PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
OUTER APPLY					  udfGetSummaryReviewerDescription(ViewProgramMechanism.ProgramMechanismId, ClientParticipantType.ClientParticipantTypeId, ViewPanelUserAssignment.ClientRoleId, ViewPanelApplicationReviewerAssignment.SortOrder) SummaryReviewerDescription 
 
OUTER APPLY
udfFinalCritiquePhaseAverage(ViewPanelApplication.PanelApplicationId, ViewClientElement.ClientElementId) AS ViewApplicationRevStdev

WHERE     (ClientAssignmentType.AssignmentTypeId <> 8) AND (ClientAssignmentType.AssignmentTypeId <> 7) AND (ReviewStatus.ReviewStatusTypeId = 1) AND 
                      (ViewMechanismTemplateElement.ScoreFlag = 1) 
ORDER BY ViewApplication.LogNumber, ViewApplicationWorkflowStep.StepTypeId, Sort_Order, ViewMechanismTemplateElement.SortOrder
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPhaseComparisonDel] TO [NetSqlAzMan_Users]
    AS [dbo];
