--First insert step assignments for reviewers assigned at the application level for stage 1 and 2
INSERT INTO [dbo].[ApplicationWorkflowStepAssignment]
           ([ApplicationWorkflowStepId]
           ,[UserId]
           ,[AssignmentId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      DISTINCT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, ClientAssignmentType.AssignmentTypeId, 
                      PanelApplicationReviewerAssignment.ModifiedBy, PanelApplicationReviewerAssignment.ModifiedDate
FROM         ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelApplication PanelApplication ON PanelApplicationReviewerAssignment.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
                      PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
                      ClientAssignmentType ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId
WHERE ApplicationStage.ReviewStageId IN (1,2) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepAssignment WHERE UserId = PanelUserAssignment.UserId AND ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId)
UNION ALL
--Next, insert step assignments for reviewers not assigned at the application level but on the panel for stage 2
SELECT     DISTINCT ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, CASE WHEN ClientParticipantType.LegacyPartTypeId IN ('OCR', 'CRT', 'CR') 
                      THEN 6 ELSE 5 END AS Expr1, PanelUserAssignment.ModifiedBy, PanelUserAssignment.ModifiedDate
FROM         ViewPanelUserAssignment PanelUserAssignment INNER JOIN
                      ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
                      ViewPanelApplication PanelApplication ON PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId INNER JOIN
                      ViewApplicationStage ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId AND 
                      PanelUserAssignment.PanelUserAssignmentId = ApplicationWorkflow.PanelUserAssignmentId INNER JOIN
                      ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND 
                      PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId
WHERE     (ClientParticipantType.ReviewerFlag = 1) AND (ApplicationStage.ReviewStageId = 2) AND 
                      (PanelApplicationReviewerAssignment.PanelApplicationReviewerAssignmentId IS NULL) AND
					  NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepAssignment WHERE UserId = PanelUserAssignment.UserId AND ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId)
ORDER BY ApplicationWorkflowStepId, UserId