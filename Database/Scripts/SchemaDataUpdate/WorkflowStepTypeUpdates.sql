--Update steptype references for new StepTypeId of 9
UPDATE WorkflowStep SET StepTypeId = 9
WHERE StepName IN ('SM Review')
--Update all optional steps to inactive
UPDATE WorkflowStep SET ActiveDefault = 0
WHERE StepName IN ('SM Review','Client Review')