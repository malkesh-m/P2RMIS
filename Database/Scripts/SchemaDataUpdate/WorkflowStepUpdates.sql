-- Update SS workflow step for CDMRP
UPDATE dbo.WorkflowStep
SET ActiveDefault =1
WHERE (WorkflowId = 2)AND StepName='SMR'
UPDATE dbo.WorkflowStep
SET ActiveDefault =0
WHERE (WorkflowId = 2)AND StepName='Editing-3'
UPDATE       WorkflowStep
SET                DeletedFlag =1, DeletedBy =4635, DeletedDate =getdate()
WHERE        (StepName = 'Client Review') AND (WorkflowId = 2 OR
                         WorkflowId = 3)