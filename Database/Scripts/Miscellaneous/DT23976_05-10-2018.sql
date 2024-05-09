UPDATE MeetingSession SET DeletedFlag = 0, DeletedDate = NULL
WHERE MeetingSessionId = 1455;

UPDATE PanelStageStep SET DeletedFlag = 0, DeletedDate = NULL
FROM         PanelStageStep INNER JOIN
                      dbo.PanelStage ON dbo.PanelStageStep.PanelStageId = dbo.PanelStage.PanelStageId INNER JOIN
                      dbo.MeetingSession INNER JOIN
                      dbo.SessionPanel ON dbo.MeetingSession.MeetingSessionId = dbo.SessionPanel.MeetingSessionId ON 
                      dbo.PanelStage.SessionPanelId = dbo.SessionPanel.SessionPanelId
WHERE     (dbo.MeetingSession.MeetingSessionId = 1455)