
--This query above is to speed up performance when matching legacy scoring to new scoring scales
INSERT INTO .[dbo].[ApplicationWorkflowStepElement]
           ([ApplicationWorkflowStepId]
           ,[ApplicationTemplateElementId]
           ,[AccessLevelId]
           ,[ClientScoringId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 1) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)
UNION ALL
--Assigned reviewers get all criteria for meeting stage
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 2) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)
UNION ALL
--Unassigned reviewers get only scored criteria
SELECT DISTINCT    ApplicationWorkflowStep.ApplicationWorkflowStepId, ApplicationTemplateElement.ApplicationTemplateElementId, 1, MechanismTemplateElementScoring.ClientScoringId, ApplicationTemplateElement.ModifiedBy, 
                      ApplicationTemplateElement.ModifiedDate
FROM         ViewApplicationTemplate ApplicationTemplate INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationTemplate.ApplicationTemplateId = ApplicationTemplateElement.ApplicationTemplateId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationTemplate.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationTemplate.ApplicationTemplateId = ApplicationWorkflow.ApplicationTemplateId AND 
                      ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON 
                      ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
					  ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId	LEFT OUTER JOIN
					  ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId AND ApplicationWorkflowStep.StepTypeId = MechanismTemplateElementScoring.StepTypeId
WHERE     (ApplicationStage.ReviewStageId = 2) AND PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL AND MechanismTemplateElement.ScoreFlag = 1 AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElement WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId AND ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId)