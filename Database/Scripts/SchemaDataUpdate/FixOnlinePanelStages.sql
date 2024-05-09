/*
P2RMIS logic is considering a panel as final scoring if synchronous stage exists and panel dates are occuring which is messing up critique rules, 
since this issue is only present for panels that were created in 1.0 we are cleaning those up to fix
*/
UPDATE ApplicationStageStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM ViewSessionPanel
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
INNER JOIN ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId
INNER JOIN ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
INNER JOIN ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
INNER JOIN ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId 
INNER JOIN ApplicationStageStep ON ViewPanelStageStep.PanelStageStepId = ApplicationStageStep.PanelStageStepId
WHERE ViewClientMeeting.MeetingTypeId = 3 AND ViewSessionPanel.EndDate > SYSDATETIME() AND ViewPanelStage.ReviewStageId = 2 AND ApplicationStageStep.DeletedFlag = 0;

UPDATE PanelStageStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM ViewSessionPanel
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
INNER JOIN ViewPanelStage ON ViewSessionPanel.SessionPanelId = ViewPanelStage.SessionPanelId
INNER JOIN ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
INNER JOIN ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
INNER JOIN PanelStageStep ON ViewPanelStage.PanelStageId = PanelStageStep.PanelStageId 
WHERE ViewClientMeeting.MeetingTypeId = 3 AND ViewSessionPanel.EndDate > SYSDATETIME() AND ViewPanelStage.ReviewStageId = 2 AND PanelStageStep.DeletedFlag = 0;

UPDATE PanelStage SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM ViewSessionPanel
INNER JOIN ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId
INNER JOIN ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId
INNER JOIN PanelStage ON ViewSessionPanel.SessionPanelId = PanelStage.SessionPanelId
INNER JOIN ViewProgramPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
INNER JOIN ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
WHERE ViewClientMeeting.MeetingTypeId = 3 AND ViewSessionPanel.EndDate > SYSDATETIME() AND PanelStage.ReviewStageId = 2 AND PanelStage.DeletedFlag = 0;