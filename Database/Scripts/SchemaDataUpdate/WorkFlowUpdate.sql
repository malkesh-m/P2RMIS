-- Update SS workflow for CPRIT
UPDATE  dbo.Workflow
 SET WorkflowName = 'CPRIT Res/Rec',
 WorkflowDescription = 'Summary Statement Workflow used for CPRIT Research and Recruitment programs.'
WHERE     (ClientId = 9) AND (WorkflowId = 75)

UPDATE  dbo.Workflow
 SET WorkflowName = 'CPRIT Prv/Pdr',
 WorkflowDescription = 'Summary Statement Workflow used by the CPRIT Prevention and Product Development programs.'
WHERE     (ClientId = 9) AND (WorkflowId = 76)
