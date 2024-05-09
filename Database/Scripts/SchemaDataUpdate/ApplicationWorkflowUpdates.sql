--Update Panel UserAssignmentId, Date Assigned, Date Closed, ModifiedBy, and ModifiedDate for each Workflow
UPDATE ApplicationWorkflow
SET PanelUserAssignmentId = subq.PanelUserAssignmentId, DateAssigned = subq.DateAssigned, DateClosed = subq.DateClosed, ModifiedBy = subq.ModifiedBy,
	ModifiedDate = subq.ModifiedDate
FROM ApplicationWorkflow INNER JOIN
(
	SELECT     ApplicationWorkflow.ApplicationWorkflowId, PanelUserAssignmentId = ApplicationWorkflow.ApplicationWorkflowName, DateAssigned = ApplicationWorkflowStepAssignment.ModifiedDate, DateClosed = ApplicationWorkflowStepWorkLog.ModifiedDate, ModifiedBy = COALESCE(ApplicationWorkflowStepWorkLog.ModifiedBy, ApplicationWorkflowStepAssignment.ModifiedBy),
		ModifiedDate = COALESCE(ApplicationWorkflowStepWorkLog.ModifiedDate, ApplicationWorkflowStepAssignment.ModifiedDate)
	FROM         ApplicationWorkflow INNER JOIN
						  ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
						  PanelUserAssignment ON ApplicationWorkflow.ApplicationWorkflowName = CAST(PanelUserAssignment.PanelUserAssignmentId AS varchar(8)) INNER JOIN
						  ApplicationWorkflowStepAssignment ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId LEFT OUTER JOIN
						  ApplicationWorkflowStepWorkLog ON ApplicationWorkflowStepAssignment.ApplicationWorkflowStepId = ApplicationWorkflowStepWorkLog.ApplicationWorkflowStepId AND
						  ApplicationWorkflowStepAssignment.UserId = ApplicationWorkflowStepWorkLog.UserId
	WHERE ISNUMERIC(ApplicationWorkflow.ApplicationWorkflowName) = 1
	GROUP BY ApplicationWorkflow.ApplicationWorkflowId, ApplicationWorkflowStepAssignment.ModifiedBy, ApplicationWorkflowStepAssignment.ModifiedDate, ApplicationWorkflow.ApplicationWorkflowName,
		ApplicationWorkflowStepWorkLog.ModifiedDate, ApplicationWorkflowStepWorkLog.ModifiedBy
) subq ON ApplicationWorkflow.ApplicationWorkflowId = subq.ApplicationWorkflowId
WHERE ApplicationWorkflow.PanelUserAssignmentId IS NULL AND
	ApplicationWorkflow.DateAssigned IS NULL AND
	ApplicationWorkflow.DateClosed IS NULL AND
	ApplicationWorkflow.ModifiedBy IS NULL AND
	ApplicationWorkflow.ModifiedDate IS NULL