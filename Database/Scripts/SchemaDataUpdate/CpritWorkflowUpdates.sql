﻿UPDATE Workflow SET WorkflowName = 'CPRIT Res/Prod', WorkflowDescription = 'Summary Statement Workflow used for CPRIT Research and Product Development programs.'
WHERE Workflow.WorkflowId = 75

UPDATE Workflow SET WorkflowName = 'CPRIT Prev', WorkflowDescription = 'Summary Statement Workflow used by the CPRIT Prevention program.'
WHERE Workflow.WorkflowId = 76

DELETE FROM WorkflowStep WHERE WorkflowId IN (75, 76, 77)
DELETE FROM Workflow WHERE WorkflowId = 77

INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'Editing'            ,1            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'QA Editing'            ,2            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'Query Review'            ,3            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'SM Review'            ,4            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'Proofread'            ,5            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'All Edits Done'            ,6            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (75            ,2            ,'Final'            ,7            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,1            ,'Writing'            ,1            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'Editing'            ,2            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'QA Editing'            ,3            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'Query Review'            ,4            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'SRO Review'            ,5            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'SM Review'            ,6            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'Proofread'            ,7            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'All Edits Done'            ,8            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
INSERT INTO [dbo].[WorkflowStep]            ([WorkflowId]            ,[StepTypeId]            ,[StepName]            ,[StepOrder]            ,[ActiveDefault]            ,[CreatedBy]            ,[CreatedDate]            ,[ModifiedBy]            ,[ModifiedDate])      VALUES            (76            ,2            ,'Final'            ,9            ,1            ,10            ,dbo.GetP2rmisDateTime()            ,10            ,dbo.GetP2rmisDateTime())
