--We only submit work log entries for assigned reviewers for stage 1. Is there a way to determine stage 2 from online scoring and do we need that?
INSERT INTO .[dbo].[ApplicationWorkflowStepWorkLog]
           ([ApplicationWorkflowStepId]
           ,[UserId]
		   ,[CheckInUserId]
           ,[CheckOutDate]
           ,[CheckInDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      ApplicationWorkflowStep.ApplicationWorkflowStepId, PanelUserAssignment.UserId, PanelUserAssignment.UserId, ISNULL(ApplicationWorkflowStep.ModifiedDate, '1/1/2002'), ApplicationWorkflowStep.ResolutionDate,
		ApplicationWorkflowStep.ModifiedBy, ApplicationWorkflowStep.ModifiedDate
FROM         ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId 
WHERE     (ApplicationStage.ReviewStageId = 1) AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepWorkLog WHERE ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId)