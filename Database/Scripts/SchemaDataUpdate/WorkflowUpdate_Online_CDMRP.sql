----Online Phase updates

UPDATE       WorkflowStep
SET                StepOrder =12, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 670);

UPDATE       WorkflowStep
SET                StepOrder =11, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 669);

UPDATE       WorkflowStep
SET                StepOrder =10, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 667);

UPDATE       WorkflowStep
SET                StepOrder =9, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 666);

UPDATE       WorkflowStep
SET                StepOrder =8, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 668);

UPDATE       WorkflowStep
SET                StepOrder =7, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId = 664);

UPDATE       WorkflowStep
SET                StepOrder =6, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 663);

UPDATE       WorkflowStep
SET                StepOrder =5, ModifiedBy =4635, ModifiedDate =getdate()
WHERE        (WorkflowStepId= 662);

---Insert New phase
INSERT INTO WorkflowStep
                         (WorkflowId, StepTypeId, StepName, StepOrder, ActiveDefault, CreatedBy, CreatedDate, ModifiedBy,ModifiedDate)
VALUES        (3,10,'Production-S',4,1,4635,getdate(),4635,getdate());