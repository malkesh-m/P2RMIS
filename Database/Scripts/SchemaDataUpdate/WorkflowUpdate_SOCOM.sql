UPDATE WorkflowStep SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM WorkflowStep
INNER JOIN Workflow ON WorkflowStep.WorkflowId = Workflow.WorkflowId
WHERE Workflow.ReviewStageId = 3 AND Workflow.ClientId = 20;

INSERT INTO [dbo].[WorkflowStep]
           ([WorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[StepOrder]
           ,[ActiveDefault]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT Workflow.WorkflowId, CDMRPWorkflowStep.StepTypeId, CDMRPWorkflowStep.StepName, CDMRPWorkflowStep.StepOrder, CDMRPWorkflowStep.ActiveDefault, CDMRPWorkflowStep.CreatedBy,
CDMRPWorkflowStep.CreatedDate, CDMRPWorkflowStep.ModifiedBy, CDMRPWorkflowStep.ModifiedDate
FROM Workflow 
INNER JOIN Workflow CDMRPWorkflow ON Workflow.WorkflowName = CDMRPWorkflow.WorkflowName AND Workflow.ReviewStageId = CDMRPWorkflow.ReviewStageId
INNER JOIN WorkflowStep CDMRPWorkflowStep ON CDMRPWorkflow.WorkflowId = CDMRPWorkflowStep.WorkflowId
WHERE Workflow.ClientId = 20 AND CDMRPWorkflow.ClientId = 19 AND Workflow.ReviewStageId = 3 AND CDMRPWorkflowStep.DeletedFlag = 0;
