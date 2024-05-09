
-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 1/18/2015
-- Description: Storeprocedure to Create Application Count Report
-- =============================================
CREATE PROCEDURE [dbo].[uspReportApplicationCountDel] 
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
	MeetingParams(MeetingId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(null)),
	SessionParams(SessionId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(null)),
	PanelParams(PanelId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT        Client.ClientAbrv, ClientProgram.ProgramAbbreviation, ViewProgramYear.Year, ViewProgramMechanism.ReceiptCycle, ViewSessionPanel.PanelAbbreviation, ViewProgramMechanism.ProgramMechanismId, 
                         ClientAwardType.AwardAbbreviation, ViewApplicationPersonnel.StateAbbreviation AS State, CASE WHEN (NOT (dbo.ViewApplication.ApplicationID IS NULL)) THEN 1 ELSE 0 END AS ApplicationCount, 
                         CASE WHEN dbo.ViewApplicationReviewStatus.ReviewStatusId = 6 THEN 1 ELSE 0 END AS ScoredCount, CASE WHEN dbo.ViewApplicationReviewStatus.ReviewStatusId = 1 THEN 1 ELSE 0 END AS ExpeditedCount, 
                         CASE WHEN (NOT (dbo.ViewSessionPanel.PanelAbbreviation IS NULL)) THEN 1 ELSE 0 END AS AssignedCount, CASE WHEN dbo.ViewApplicationReviewStatus.ReviewStatusId = 2 AND ViewPanelStageStep.StepTypeId = 7 AND 
                         ClientMeeting.MeetingTypeId = 3 AND ViewPanelStageStep.StepTypeId <> 8 THEN 1 ELSE 0 END AS DiscussionPhaseCount, 
                         CASE WHEN ViewApplicationStageStepDiscussion.ApplicationStageStepDiscussionId <> 0 THEN 1 ELSE 0 END AS DiscussionCount, ViewPanelStageStep.StepTypeId, ClientMeeting.MeetingTypeId, ViewApplication.LogNumber, 
                         ViewApplicationStageStepDiscussion.ApplicationStageStepId
FROM            ViewApplicationStageStepDiscussion FULL OUTER JOIN
                         ViewApplicationReviewStatus LEFT OUTER JOIN
                         ViewApplicationPersonnel INNER JOIN
                         Client INNER JOIN
                         ClientProgram ON Client.ClientID = ClientProgram.ClientId INNER JOIN
                         ClientAwardType ON Client.ClientID = ClientAwardType.ClientId INNER JOIN
                         ViewProgramMechanism ON ClientAwardType.ClientAwardTypeId = ViewProgramMechanism.ClientAwardTypeId INNER JOIN
                         ViewApplication ON ViewProgramMechanism.ProgramMechanismId = ViewApplication.ProgramMechanismId INNER JOIN
                         ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId AND ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId ON 
                         ViewApplicationPersonnel.ApplicationId = ViewApplication.ApplicationId RIGHT OUTER JOIN
                         ClientMeeting INNER JOIN
                         ViewApplicationStageStep INNER JOIN
                         ViewSessionPanel INNER JOIN
                         ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                         ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId ON ViewApplicationStageStep.ApplicationStageId = ViewApplicationStage.ApplicationStageId INNER JOIN
                         ViewPanelStage ON ViewApplicationStage.ReviewStageId = ViewPanelStage.ReviewStageId AND ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId INNER JOIN
                         ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId AND ViewApplicationStageStep.PanelStageStepId = ViewPanelStageStep.PanelStageStepId INNER JOIN
                         ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId ON ClientMeeting.ClientMeetingId = ViewMeetingSession.ClientMeetingId ON 
                         ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId ON ViewApplicationReviewStatus.PanelApplicationId = ViewPanelApplication.PanelApplicationId ON 
                         ViewApplicationStageStepDiscussion.ApplicationStageStepId = ViewApplicationStageStep.ApplicationStageStepId

						 INNER JOIN ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                         FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
                         PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
WHERE        (ViewApplicationReviewStatus.ReviewStatusId = 2 OR
                         ViewApplicationReviewStatus.ReviewStatusId = 1 OR
                         ViewApplicationReviewStatus.ReviewStatusId = 6) AND (ViewApplicationStage.ReviewStageId = 1 OR
                         ViewApplicationStage.ReviewStageId = 2) AND (ViewPanelStageStep.StepTypeId = 7 OR
                         ViewPanelStageStep.StepTypeId = 8) AND (ViewApplicationPersonnel.PrimaryFlag = 1)
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportApplicationCountDel] TO [NetSqlAzMan_Users]
    AS [dbo];

