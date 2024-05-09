---Standard Update Existing Phases
UPDATE       WorkflowStep
SET                StepOrder =12, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 648);

UPDATE       WorkflowStep
SET                StepOrder =11, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 647);

UPDATE       WorkflowStep
SET                StepOrder =10, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 10);

UPDATE       WorkflowStep
SET                StepOrder =9, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 9);

UPDATE       WorkflowStep
SET                StepOrder =8, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 11);

UPDATE       WorkflowStep
SET                StepOrder =7, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 6);

UPDATE       WorkflowStep
SET                StepOrder =6, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 5);

UPDATE       WorkflowStep
SET                StepOrder =5, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 4);

---Insert New phase
INSERT INTO WorkflowStep
                         (WorkflowId, StepTypeId, StepName, StepOrder, ActiveDefault, CreatedBy, CreatedDate, ModifiedBy,ModifiedDate)
VALUES        (2,10,'Production-S',4,1,4635,getdate(),4635,getdate());

