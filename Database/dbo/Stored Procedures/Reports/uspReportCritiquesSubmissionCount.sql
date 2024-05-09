-- =============================================
-- Author:		Alberto Catuche
-- Create date: 5/2016
-- Description:	Used as source for report Critiques Submission Count
-- =============================================

CREATE PROCEDURE [dbo].[uspReportCritiquesSubmissionCount] 
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
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), --@ProgramList --55=ARP
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), --@FiscalYearList --2013
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList)) --@PanelList --2757=PLT, 2758=ID

/* Critiques Submission Count */
SELECT ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
	  ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, ViewMeetingSession.SessionAbbreviation, 
	  ClientParticipantType.ParticipantTypeAbbreviation, ClientParticipantType.ParticipantTypeName, ClientParticipantType.ParticipantScope, 
	  ClientParticipantType.ReviewerFlag, ViewPanelUserAssignment.PanelUserAssignmentId, ViewPanelUserAssignment.SessionPanelId, 
	  Prelim_StartDate, Prelim_EndDate, Revised_StartDate, Revised_EndDate, MOD_StartDate, MOD_EndDate, 
	  ViewPanelUserAssignment.UserId, ViewPanelUserAssignment.ClientRoleId, ClientRole.RoleAbbreviation, ClientRole.RoleName, ClientRole.SpecialistFlag, 
	  ViewUserInfo.FirstName, ViewUserInfo.MiddleName, ViewUserInfo.LastName, CritCnts_DS.AssignedCritiqueCount, CritCnts_DS.PrelimCritiqueCompletedCount, 
	  CritCnts_DS.PrelimCritiqueNotCompletedCount, CritCnts_DS.RevisedCritiqueCompletedCount, CritCnts_DS.RevisedCritiqueNotCompletedCount,
	  CritCnts_DS.MODCritiqueCompletedCount, CritCnts_DS.MODCritiqueNotCompletedCount,
	  totApp.ApplicationCount AS TotalAppCount,
	  totApp.MODAppCount AS TotalModCount
FROM   ViewSessionPanel INNER JOIN
	  ViewPanelUserAssignment INNER JOIN
	  ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId LEFT OUTER JOIN
	  ClientRole ON ViewPanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId INNER JOIN
	  ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
	  ClientProgram INNER JOIN
	  ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
	  ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId ON 
	  ViewPanelUserAssignment.SessionPanelId = ViewProgramPanel.SessionPanelId ON 
	  ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId INNER JOIN
	  ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId 

	  INNER JOIN
	  ProgramParams ON dbo.ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
	  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
	  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId	  
	  
	  LEFT OUTER JOIN
		(SELECT PrelimCnt_DS.UserId, PrelimCnt_DS.SessionPanelId, PrelimCnt_DS.SessionAbbreviation, 
			PrelimCnt_DS.StartDate AS Prelim_StartDate, PrelimCnt_DS.EndDate AS Prelim_EndDate, 
			RevisedCnt_DS.StartDate AS Revised_StartDate, RevisedCnt_DS.EndDate AS Revised_EndDate,
			MODCnt_DS.StartDate AS MOD_StartDate, MODCnt_DS.EndDate AS MOD_EndDate,
			(PrelimCnt_DS.PrelimCritiqueCompletedCount + PrelimCnt_DS.PrelimCritiqueNotCompletedCount) AS AssignedCritiqueCount, 
			PrelimCnt_DS.PrelimCritiqueCompletedCount, PrelimCnt_DS.PrelimCritiqueNotCompletedCount, 
			RevisedCnt_DS.RevisedCritiqueCompletedCount, RevisedCnt_DS.RevisedCritiqueNotCompletedCount,
			MODCnt_DS.MODCritiqueCompletedCount, MODCnt_DS.MODCritiqueNotCompletedCount
		FROM  (SELECT    ViewPanelUserAssignment.UserId, ViewSessionPanel_2.SessionPanelId, ViewMeetingSession_2.SessionAbbreviation, 
                      ViewPanelStageStep_2.StartDate, ViewPanelStageStep_2.EndDate, SUM(CASE WHEN ViewApplicationWorkflowStep_2.Resolution = 1 AND 
                      ViewApplicationWorkflowStep_2.StepTypeId = 5 THEN 1 ELSE 0 END) AS PrelimCritiqueCompletedCount, 
                      SUM(CASE WHEN ViewApplicationWorkflowStep_2.Resolution = 0 AND ViewApplicationWorkflowStep_2.StepTypeId = 5 THEN 1 ELSE 0 END) 
                      AS PrelimCritiqueNotCompletedCount
			  FROM    ViewPanelUserAssignment INNER JOIN
                      ViewSessionPanel AS ViewSessionPanel_2 ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel_2.SessionPanelId INNER JOIN
                      ViewMeetingSession AS ViewMeetingSession_2 ON ViewMeetingSession_2.MeetingSessionId = ViewSessionPanel_2.MeetingSessionId INNER JOIN
                      ViewPanelStage AS ViewPanelStage_2 ON ViewSessionPanel_2.SessionPanelId = ViewPanelStage_2.SessionPanelId INNER JOIN
					  ViewPanelStageStep AS ViewPanelStageStep_2 ON ViewPanelStageStep_2.PanelStageId = ViewPanelStage_2.PanelStageId INNER JOIN
					  ViewPanelApplication AS PanApp_2 ON ViewSessionPanel_2.SessionPanelId = PanApp_2.SessionPanelId INNER JOIN
                      ViewApplication AS ViewApplication_2 ON PanApp_2.ApplicationId = ViewApplication_2.ApplicationId INNER JOIN
			  ViewApplicationStage AS AppStage_2 ON PanApp_2.PanelApplicationId = AppStage_2.PanelApplicationId AND ViewPanelStage_2.ReviewStageId = AppStage_2.ReviewStageId LEFT OUTER JOIN
			  ViewApplicationWorkflow AS ViewApplicationWorkflow_2 ON AppStage_2.ApplicationStageId = ViewApplicationWorkflow_2.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow_2.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewApplicationWorkflowStep AS ViewApplicationWorkflowStep_2 ON 
                      ViewApplicationWorkflow_2.ApplicationWorkflowId = ViewApplicationWorkflowStep_2.ApplicationWorkflowId AND ViewPanelStageStep_2.StepTypeId = ViewApplicationWorkflowStep_2.StepTypeId
			  WHERE     (ViewPanelStageStep_2.StepTypeId = 5)
			  GROUP BY ViewPanelUserAssignment.UserId, ViewSessionPanel_2.SessionPanelId, ViewMeetingSession_2.SessionAbbreviation, 
                      ViewPanelStageStep_2.StartDate, ViewPanelStageStep_2.EndDate) AS PrelimCnt_DS 
			  
			  LEFT OUTER JOIN
			  (SELECT     ViewPanelUserAssignment.UserId, ViewSessionPanel_1.SessionPanelId, ViewMeetingSession_1.SessionAbbreviation, 
                      ViewPanelStageStep_1.StartDate, ViewPanelStageStep_1.EndDate, SUM(CASE WHEN ViewApplicationWorkflowStep_1.Resolution = 1 AND 
                      ViewApplicationWorkflowStep_1.StepTypeId = 6 THEN 1 ELSE 0 END) AS RevisedCritiqueCompletedCount, 
                      SUM(CASE WHEN ViewApplicationWorkflowStep_1.Resolution = 0 AND ViewApplicationWorkflowStep_1.StepTypeId = 6 THEN 1 ELSE 0 END) 
                      AS RevisedCritiqueNotCompletedCount
			  FROM    ViewPanelUserAssignment INNER JOIN
                      ViewSessionPanel AS ViewSessionPanel_1 ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel_1.SessionPanelId INNER JOIN
                      ViewMeetingSession AS ViewMeetingSession_1 ON ViewMeetingSession_1.MeetingSessionId = ViewSessionPanel_1.MeetingSessionId INNER JOIN
                      ViewPanelStage AS ViewPanelStage_1 ON ViewSessionPanel_1.SessionPanelId = ViewPanelStage_1.SessionPanelId INNER JOIN
					  ViewPanelStageStep AS ViewPanelStageStep_1 ON ViewPanelStageStep_1.PanelStageId = ViewPanelStage_1.PanelStageId INNER JOIN
					  ViewPanelApplication AS PanApp_1 ON ViewSessionPanel_1.SessionPanelId = PanApp_1.SessionPanelId INNER JOIN
                      ViewApplication AS ViewApplication_1 ON PanApp_1.ApplicationId = ViewApplication_1.ApplicationId INNER JOIN
			  ViewApplicationStage AS AppStage_1 ON PanApp_1.PanelApplicationId = AppStage_1.PanelApplicationId AND ViewPanelStage_1.ReviewStageId = AppStage_1.ReviewStageId LEFT OUTER JOIN
			  ViewApplicationWorkflow AS ViewApplicationWorkflow_1 ON AppStage_1.ApplicationStageId = ViewApplicationWorkflow_1.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow_1.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewApplicationWorkflowStep AS ViewApplicationWorkflowStep_1 ON 
                      ViewApplicationWorkflow_1.ApplicationWorkflowId = ViewApplicationWorkflowStep_1.ApplicationWorkflowId AND ViewPanelStageStep_1.StepTypeId = ViewApplicationWorkflowStep_1.StepTypeId
			  WHERE     (ViewPanelStageStep_1.StepTypeId = 6)
			  GROUP BY ViewPanelUserAssignment.UserId, ViewSessionPanel_1.SessionPanelId, ViewMeetingSession_1.SessionAbbreviation, 
                      ViewPanelStageStep_1.StartDate, ViewPanelStageStep_1.EndDate
			  ) AS RevisedCnt_DS 
			  
			  ON PrelimCnt_DS.UserId = RevisedCnt_DS.UserId AND PrelimCnt_DS.SessionPanelId = RevisedCnt_DS.SessionPanelId --AND PrelimCnt_DS.SessionAbbreviation = RevisedCnt_DS.SessionAbbreviation

			  LEFT OUTER JOIN
			  (SELECT     ViewPanelUserAssignment.UserId, ViewSessionPanel_3.SessionPanelId, ViewMeetingSession_3.SessionAbbreviation, 
					  ViewPanelStageStep_3.StartDate, ViewPanelStageStep_3.EndDate, 
					  SUM(CASE WHEN ViewApplicationWorkflowStep_3.Resolution = 1 AND NumComments.Cnt > 0 THEN 1 ELSE 0 END) AS MODCritiqueCompletedCount, 
					  SUM(CASE WHEN ViewApplicationWorkflowStep_3.Resolution = 0 AND NumComments.Cnt > 0 THEN 1 ELSE 0 END) AS MODCritiqueNotCompletedCount
			  			  FROM    ViewPanelUserAssignment INNER JOIN
                      ViewSessionPanel AS ViewSessionPanel_3 ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel_3.SessionPanelId INNER JOIN
                      ViewMeetingSession AS ViewMeetingSession_3 ON ViewMeetingSession_3.MeetingSessionId = ViewSessionPanel_3.MeetingSessionId INNER JOIN
                      ViewPanelStage AS ViewPanelStage_3 ON ViewSessionPanel_3.SessionPanelId = ViewPanelStage_3.SessionPanelId INNER JOIN
					  ViewPanelStageStep AS ViewPanelStageStep_3 ON ViewPanelStageStep_3.PanelStageId = ViewPanelStage_3.PanelStageId INNER JOIN
					  ViewPanelApplication AS PanApp_3 ON ViewSessionPanel_3.SessionPanelId = PanApp_3.SessionPanelId INNER JOIN
                      ViewApplication AS ViewApplication_3 ON PanApp_3.ApplicationId = ViewApplication_3.ApplicationId INNER JOIN
			  ViewApplicationStage AS AppStage_3 ON PanApp_3.PanelApplicationId = AppStage_3.PanelApplicationId AND ViewPanelStage_3.ReviewStageId = AppStage_3.ReviewStageId LEFT OUTER JOIN
			  ViewApplicationWorkflow AS ViewApplicationWorkflow_3 ON AppStage_3.ApplicationStageId = ViewApplicationWorkflow_3.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow_3.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewApplicationWorkflowStep AS ViewApplicationWorkflowStep_3 ON 
                      ViewApplicationWorkflow_3.ApplicationWorkflowId = ViewApplicationWorkflowStep_3.ApplicationWorkflowId AND ViewPanelStageStep_3.StepTypeId = ViewApplicationWorkflowStep_3.StepTypeId LEFT OUTER JOIN
					  ViewApplicationStageStep ON AppStage_3.ApplicationStageId = ViewApplicationStageStep.ApplicationStageId AND ViewPanelStageStep_3.PanelStageStepId = ViewApplicationStageStep.PanelStageStepId LEFT OUTER JOIN
					  ViewApplicationStageStepDiscussion ON ViewApplicationStageStep.ApplicationStageStepId = ViewApplicationStageStepDiscussion.ApplicationStageStepId LEFT OUTER JOIN
					  (SELECT Count(*) AS Cnt, ApplicationStageStepDiscussionId FROM ApplicationStageStepDiscussionComment Where DeletedFlag = 0 GROUP BY ApplicationStageStepDiscussionId) AS NumComments ON ViewApplicationStageStepDiscussion.ApplicationStageStepDiscussionId = NumComments.ApplicationStageStepDiscussionId
			  WHERE     (ViewPanelStageStep_3.StepTypeId = 7) 
			  GROUP BY ViewPanelUserAssignment.UserId, ViewSessionPanel_3.SessionPanelId, ViewMeetingSession_3.SessionAbbreviation, 
					ViewPanelStageStep_3.StartDate, ViewPanelStageStep_3.EndDate
			  ) AS MODCnt_DS

			  ON PrelimCnt_DS.UserId = MODCnt_DS.UserId AND PrelimCnt_DS.SessionPanelId = MODCnt_DS.SessionPanelId) CritCnts_DS 
		ON ViewUserInfo.UserID = CritCnts_DS.UserId and ViewSessionPanel.SessionPanelId = CritCnts_DS.SessionPanelId
	LEFT OUTER JOIN 
	  (
		SELECT vpa.SessionPanelId,
			COUNT(DISTINCT vpa.PanelApplicationId) AS ApplicationCount, 
			SUM(CASE WHEN vassd.ApplicationStageStepId IS NOT NULL THEN 1 ELSE 0 END) AS ModAppCount 
		FROM dbo.ViewPanelApplication vpa
			INNER JOIN [dbo].[ViewApplicationStage] vas ON vas.PanelApplicationId = vpa.PanelApplicationId
			INNER JOIN [dbo].[ViewApplicationStageStep] vass ON vass.ApplicationStageId = vas.ApplicationStageId
			LEFT JOIN (SELECT DISTINCT ApplicationStageStepId FROM dbo.ViewApplicationStageStepDiscussion) vassd ON vassd.ApplicationStageStepId = vass.ApplicationStageStepId
		GROUP BY vpa.SessionPanelId
		) totApp ON totApp.SessionPanelId = ViewSessionPanel.SessionPanelId

WHERE (ClientParticipantType.ReviewerFlag = 1) 
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCritiquesSubmissionCount] TO [NetSqlAzMan_Users]
    AS [dbo];