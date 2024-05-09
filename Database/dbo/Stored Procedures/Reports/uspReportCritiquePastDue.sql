-- =============================================
-- Author:		Alberto Catuche
-- Create date: 5/2016
-- Description:	Used as source for report Critique Past Due Notice
-- =============================================

CREATE PROCEDURE [dbo].[uspReportCritiquePastDue] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

SELECT     Application.ApplicationId, Application.LogNumber, ApplicationWorkflowStep.StepTypeId, ApplicationWorkflowStep.Resolution, ViewProgramYear.Year, 
                      ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, UserInfo.UserID, UserInfo.FirstName, UserInfo.LastName, 
                      UserInfo.FirstName + ' ' + UserInfo.LastName AS Reviewer, ViewSessionPanel.PanelAbbreviation, ClientAwardType.AwardDescription,
					  ProgramMechanism.ReceiptCycle, PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId, ViewMeetingSession.MeetingSessionId,
					  ViewMeetingSession.SessionDescription, ViewClientMeeting.ClientMeetingId, ViewClientMeeting.MeetingDescription
FROM         ViewApplicationWorkflow ApplicationWorkflow CROSS APPLY
                      udfApplicationWorkflowActiveStep(ApplicationWorkflow.ApplicationWorkflowId) AS ActiveStep INNER JOIN
					  ViewApplicationWorkflowStep ApplicationWorkflowStep  ON ActiveStep.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					  ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					  ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
					  ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND
						PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
                      ViewProgramYear ON ProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
                      ViewUserInfo UserInfo ON PanelUserAssignment.UserId = UserInfo.UserID INNER JOIN
                      ViewSessionPanel ON PanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
					  ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
					  ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
                      ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
					  ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
	             ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
				 FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	             PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
WHERE     (ApplicationWorkflowStep.Resolution = 0) AND ClientAssignmentType.AssignmentTypeId IN (5, 6, 9)
			--AND (ViewProgramYear.Year = '2013') AND (ClientProgram.ProgramAbbreviation = 'ARP')
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCritiquePastDue] TO [NetSqlAzMan_Users]
    AS [dbo];