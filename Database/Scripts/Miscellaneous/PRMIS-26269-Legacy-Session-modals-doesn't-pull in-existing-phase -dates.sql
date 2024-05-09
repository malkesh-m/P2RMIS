MERGE SessionPhase  AS SOURCE
USING ( SELECT DISTINCT
dbo.MeetingSession.MeetingSessionId, dbo.ViewPanelStageStep.StepTypeId,
CASE WHEN StepTypeID = 8 THEN 4 ELSE dbo.ViewPanelStageStep.StepOrder END AS SortOrder,
dbo.ViewPanelStageStep.StartDate, dbo.ViewPanelStageStep.EndDate, dbo.ViewPanelStageStep.ReOpenDate,
dbo.ViewPanelStageStep.ReCloseDate as CloseDate, dbo.MeetingSession.CreatedBy,
dbo.MeetingSession.CreatedDate, dbo.MeetingSession.ModifiedBy, dbo.MeetingSession.ModifiedDate
FROM    dbo.MeetingSession INNER JOIN
dbo.ViewSessionPanel ON dbo.MeetingSession.MeetingSessionId = dbo.ViewSessionPanel.MeetingSessionId INNER JOIN
dbo.ViewPanelStage ON dbo.ViewSessionPanel.SessionPanelId = dbo.ViewPanelStage.SessionPanelId INNER JOIN
dbo.ViewPanelStageStep ON dbo.ViewPanelStage.PanelStageId = dbo.ViewPanelStageStep.PanelStageId
WHERE   (NOT (dbo.MeetingSession.LegacySessionId IS NULL)) AND (dbo.MeetingSession.DeletedFlag = 0)
) AS TARGET
ON (TARGET.MeetingSessionId = SOURCE.MeetingSessionId)
WHEN MATCHED
and SOURCE.StepTypeId=TARGET.StepTypeId
THEN UPDATE SET SOURCE.MeetingSessionId=TARGET.MeetingSessionId,
--SOURCE.StepTypeId=TARGET.StepTypeId,
SOURCE.SortOrder=TARGET.SortOrder,
SOURCE.StartDate=TARGET.StartDate,
SOURCE.EndDate=TARGET.EndDate,
SOURCE.ReopenDate=TARGET.ReOpenDate,
SOURCE.CloseDate=TARGET.CloseDate,
SOURCE.CreatedBy=TARGET.CreatedBy,
SOURCE.CreatedDate=TARGET.CreatedDate,
SOURCE.ModifiedBy=TARGET.ModifiedBy,
SOURCE.ModifiedDate=TARGET.ModifiedDate;​