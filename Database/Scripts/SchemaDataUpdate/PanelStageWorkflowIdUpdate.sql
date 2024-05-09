--Updates newly added WorkflowId for PanelStage
UPDATE PanelStage SET WorkflowId = subq.WorkflowId
FROM PanelStage INNER JOIN
(
	SELECT Workflows.WorkflowId, PanelStages.PanelStageId, Workflows.WorkflowSteps
	FROM	Client INNER JOIN
			ClientMeeting ON Client.ClientId = ClientMeeting.ClientId INNER JOIN
			MeetingSession ON ClientMeeting.ClientMeetingId = MeetingSession.ClientMeetingId INNER JOIN
			SessionPanel ON MeetingSession.MeetingSessionId = SessionPanel.MeetingSessionId INNER JOIN
			(SELECT   PanelStageId = PanelStage.PanelStageId, SessionPanelId = PanelStage.SessionPanelId,
			 PanelSteps = Stuff((SELECT  ', ' + PanelStageStep.StepName AS [text()]
								FROM PanelStageStep
								WHERE PanelStageStep.PanelStageId = PanelStage.PanelStageId
								ORDER BY PanelStageStep.StepOrder
								FOR XML PATH ('')),1,1,'')
	FROM     PanelStage) PanelStages ON SessionPanel.SessionPanelId = PanelStages.SessionPanelId INNER JOIN
	(SELECT   WorkflowId = Workflow.WorkflowId, Workflow.ClientId,
			 WorkflowSteps = Stuff((SELECT  ', ' + WorkflowStep.StepName AS [text()]
								FROM WorkflowStep
								WHERE WorkflowStep.WorkflowId = Workflow.WorkflowId
								ORDER BY WorkflowStep.StepOrder
								FOR XML PATH ('')),1,1,'')
	FROM	 Workflow) Workflows ON Client.ClientId = Workflows.ClientId AND PanelStages.PanelSteps = Workflows.WorkflowSteps 
) subq ON PanelStage.PanelStageId = subq.PanelStageId
