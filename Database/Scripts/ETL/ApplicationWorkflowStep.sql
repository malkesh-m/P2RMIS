--Disable trigger first
INSERT INTO [dbo].[ApplicationWorkflowStep]
           ([ApplicationWorkflowId]
           ,[StepTypeId]
           ,[StepName]
           ,[Active]
           ,[StepOrder]
           ,[Resolution]
		   ,[ResolutionDate]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
SELECT     ApplicationWorkflow.ApplicationWorkflowId, WorkflowStep.StepTypeId, WorkflowStep.StepName, 
                      CASE WHEN COUNT([$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion.Discussion_ID) > 0 THEN 1 ELSE WorkflowStep.ActiveDefault END AS Expr2, 
                      WorkflowStep.StepOrder, CASE WHEN PRG_Critique_Phase.Date_Submitted IS NULL THEN 0 ELSE 1 END, PRG_Critique_Phase.Date_Submitted, VUN.UserID, PRG_Critique_Phase.LAST_UPDATE_DATE 
FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
                      Workflow ON ApplicationWorkflow.WorkflowId = Workflow.WorkflowId INNER JOIN
                      WorkflowStep ON Workflow.WorkflowId = WorkflowStep.WorkflowId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					  ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplication [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					  ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Proposal_Assignments PRG_Proposal_Assignments ON Application.LogNumber = PRG_Proposal_Assignments.Log_No AND PanelUserAssignment.LegacyParticipantId = PRG_Proposal_Assignments.Prg_Part_ID LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Critiques PRG_Critiques ON PRG_Proposal_Assignments.PA_ID = PRG_Critiques.PA_ID LEFT OUTER JOIN
					  [$(P2RMIS)].dbo.PRG_Critique_Phase PRG_Critique_Phase ON PRG_Critiques.Critique_ID = PRG_Critique_Phase.Critique_ID AND WorkflowStep.StepTypeId = CASE PRG_Critique_Phase.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 ELSE 7 END   LEFT OUTER JOIN
                      [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion ON 
                      Application.LogNumber = [$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion.LOG_NO LEFT OUTER JOIN
                      dbo.ViewLegacyUserNameToUserId VUN ON  PRG_Critique_Phase.LAST_UPDATED_BY = VUN.UserName
WHERE     NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStep WHERE ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId AND StepTypeId = WorkflowStep.StepTypeId)
GROUP BY ApplicationWorkflow.ApplicationWorkflowId, WorkflowStep.StepTypeId, WorkflowStep.StepName, WorkflowStep.StepOrder, WorkflowStep.ActiveDefault, PRG_Critique_Phase.Date_Submitted, VUN.UserID, PRG_Critique_Phase.LAST_UPDATE_DATE

Update ApplicationWorkflowStep
           SET Resolution = CASE WHEN PRG_Critique_Phase.Date_Submitted IS NULL THEN 0 ELSE 1 END, ResolutionDate = PRG_Critique_Phase.Date_Submitted
FROM         ViewApplicationWorkflow ApplicationWorkflow INNER JOIN
			ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
					  ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
					  ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewApplication [Application] ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
					  ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId LEFT OUTER JOIN
					  [P2RMIS].dbo.PRG_Proposal_Assignments PRG_Proposal_Assignments ON Application.LogNumber = PRG_Proposal_Assignments.Log_No AND PanelUserAssignment.LegacyParticipantId = PRG_Proposal_Assignments.Prg_Part_ID LEFT OUTER JOIN
					  [P2RMIS].dbo.PRG_Critiques PRG_Critiques ON PRG_Proposal_Assignments.PA_ID = PRG_Critiques.PA_ID LEFT OUTER JOIN
					  [P2RMIS].dbo.PRG_Critique_Phase PRG_Critique_Phase ON PRG_Critiques.Critique_ID = PRG_Critique_Phase.Critique_ID AND ApplicationWorkflowStep.StepTypeId = CASE PRG_Critique_Phase.Scoring_Phase WHEN 'initial' THEN 5 WHEN 'revised' THEN 6 ELSE 7 END   LEFT OUTER JOIN
                      dbo.ViewLegacyUserNameToUserId VUN ON  PRG_Critique_Phase.LAST_UPDATED_BY = VUN.UserName
WHERE    PRG_Critique_Phase.Date_Submitted > '7/1/2018' AND PRG_Critique_Phase.Date_Submitted > ISNULL(ApplicationWorkflowStep.ResolutionDate, '1/1/1900') AND ApplicationWorkflowStep.DeletedFlag = 0