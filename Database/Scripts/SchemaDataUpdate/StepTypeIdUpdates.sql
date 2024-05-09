--StepTypeId changed to represent an individual phase
--Script updates the affected tables (ApplicationWorkflowStep, PanelStageStep, WorkflowStep)

UPDATE ApplicationWorkflowStep SET StepTypeId = CASE ApplicationWorkflowStep.StepName WHEN 'Preliminary' THEN 5 WHEN 'Revised' THEN 6 When 'Final' THEN 7 When 'Online Discussion' THEN 7 WHEN 'Meeting' THEN 8 END
FROM ApplicationWorkflowStep
WHERE ApplicationWorkflowStep.StepName IN ('Preliminary', 'Revised', 'Final', 'Meeting', 'Online Discussion')

UPDATE PanelStageStep SET StepTypeId = CASE PanelStageStep.StepName WHEN 'Preliminary' THEN 5 WHEN 'Revised' THEN 6 When 'Final' THEN 7 When 'Online Discussion' THEN 7 WHEN 'Meeting' THEN 8 END
FROM PanelStageStep
WHERE PanelStageStep.StepName IN ('Preliminary', 'Revised', 'Final', 'Meeting', 'Online Discussion')

UPDATE WorkflowStep SET StepTypeId = CASE WorkflowStep.StepName WHEN 'Preliminary' THEN 5 WHEN 'Revised' THEN 6 When 'Final' THEN 7 When 'Online Discussion' THEN 7 WHEN 'Meeting' THEN 8 END
FROM WorkflowStep
WHERE WorkflowStep.StepName IN ('Preliminary', 'Revised', 'Final', 'Meeting', 'Online Discussion')