--Updating StepTypeId of SMR and SMR-2 phases so it is no longer enabled/disabled by priority 1 updates

UPDATE WorkflowStep SET StepTypeId = 10
WHERE StepName IN ('SMR','SMR-2') AND StepTypeId = 9 AND DeletedFlag = 0;

UPDATE ApplicationWorkflowStep SET StepTypeId = 10
WHERE     StepName IN ('SMR','SMR-2') AND (StepTypeId = 9) AND (Resolution = 0) AND (Active = 1) AND (DeletedFlag = 0);