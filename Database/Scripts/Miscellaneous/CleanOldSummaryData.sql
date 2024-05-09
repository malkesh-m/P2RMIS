﻿DELETE FROM ApplicationWorkflowStepElementContentHistory

DELETE FROM ApplicationWorkflowStepElementContent WHERE ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationWorkflowStepElement WHERE ApplicationWOrkflowStepElementId IN
(Select ApplicationWorkflowStepElementId FROM ApplicationWorkflowStepElement 
INNER JOIN ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId 
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationWorkflowStepAssignment WHERE ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationWorkflowStepWorkLog WHERE ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationWorkflowStep WHERE ApplicationWOrkflowStepId IN
(Select ApplicationWorkflowStepId FROM ApplicationWorkflowStep
INNER JOIN ApplicationWorkflow ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationWorkflow WHERE ApplicationWOrkflowId IN
(Select ApplicationWorkflowId FROM ApplicationWorkflow
INNER JOIN ApplicationTemplate ON ApplicationWorkflow.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationTemplateElement WHERE ApplicationTemplateElementId IN
(Select ApplicationTemplateElementId FROM ApplicationTemplateElement
INNER JOIN ApplicationTemplate ON ApplicationTemplateElement.ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId
WHERE ApplicationTemplate.CreatedDate IS NOT NULL)

DELETE FROM ApplicationTemplate WHERE ApplicationTemplate.CreatedDate IS NOT NULL

DELETE FROM MechanismTemplateElementScoring WHERE MechanismTemplateElementId IN 
(Select MechanismTemplateElementId FROM MechanismTemplateElement INNER JOIN
MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplate.MechanismTemplateId
WHERE
ReviewStageId = 3);

DELETE FROM MechanismTemplateElement WHERE MechanismTemplateId IN (
	SELECT MechanismTemplateId FROM MechanismTemplate WHERE ReviewStageId = 3);

DELETE FROM MechanismTemplate WHERE ReviewStageId = 3;