UPDATE WorkflowStep SET StepTypeId = 10
WHERE WorkflowId IN (75,76) AND StepName = 'Final';

UPDATE ApplicationWorkflowStep SET StepTypeId = 10
FROM ApplicationWorkflowStep INNER JOIN
ViewApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
WHERE ApplicationWorkflowStep.DeletedFlag = 0 AND ViewApplicationWorkflow.WorkflowId IN (75, 76) AND ApplicationWorkflowStep.StepName = 'Final';