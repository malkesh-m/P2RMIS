/*****These must be run manually, one at a time*******/
--Run this script repeatedly until it returns 0 records
DELETE FROM P2RMIS.dbo.PRG_Criteria_Eval WHERE Criteria_Eval_ID IN
(SELECT Criteria_Eval_ID FROM P2RMIS.dbo.PRG_Criteria_Eval Up
WHERE CAST(ECM_ID AS NVARCHAR(200))+ ',' + CAST(Critique_ID AS NVARCHAR(200)) + ',' + Scoring_Phase IN
(SELECT     CAST(ECM_ID AS NVARCHAR(200))+ ',' + CAST(Critique_ID AS NVARCHAR(200)) + ',' + Scoring_Phase
FROM         P2RMIS.dbo.PRG_Criteria_Eval Down
GROUP BY ECM_ID, Critique_ID, Scoring_Phase
HAVING      (COUNT(Criteria_Eval_ID) > 1)) AND Up.Criteria_Eval_ID = (SELECT TOP(1)(Criteria_Eval_ID) FROM Prg_Criteria_Eval I WHERE (Up.ECM_ID = I.ECM_ID) AND (Up.Critique_ID=I.Critique_ID) AND (Up.Scoring_Phase=I.Scoring_Phase) ORDER BY LAST_UPDATE_DATE, Criteria_Eval_ID)
)


/****This is the large query and will take several hours to run*********/
--First insert assigned reviewers content from PRG_Criteria_Eval
DECLARE @BatchSize INT = 100000

WHILE 1 = 1
BEGIN 

INSERT INTO  [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT     DISTINCT dbo.ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score, 
                      P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt, CASE WHEN P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score IS NULL AND CAST(P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
					  VUN.UserId, P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE
FROM                  ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON 
                      ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN                      
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Criteria_Eval ON P2RMIS.dbo.PRG_Critiques.Critique_ID = P2RMIS.dbo.PRG_Criteria_Eval.Critique_ID AND 
                      CASE ApplicationWorkflowStep.StepTypeId WHEN 5 THEN 'Initial' WHEN 6 THEN 'Revised' ELSE 'Meeting' END = P2RMIS.dbo.PRG_Criteria_Eval.Scoring_Phase AND
                      MechanismTemplateElement.LegacyEcmId = P2RMIS.dbo.PRG_Criteria_Eval.Ecm_ID  LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName     
WHERE ApplicationStage.ReviewStageId IN (1,2)
ORDER BY ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId


IF @@ROWCOUNT < @BatchSize BREAK

END


--Next update assigned reviewers scores from PRG_Panel_Scores
UPDATE ApplicationWorkflowStepElementContent
SET Score = CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END
FROM [ViewApplicationWorkflowStepElementContent] ApplicationWorkflowStepElementContent INNER JOIN
	ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow  ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND
	ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	P2RMIS.dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = P2RMIS.dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = P2RMIS.dbo.PRG_Panel_Scores.Log_No
WHERE ApplicationStage.ReviewStageId = 2


--Finally insert the rest of reviewers scores from PRG_Panel_Scores

INSERT INTO [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END,
		CASE WHEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score IS NULL AND P2RMIS.dbo.PRG_Panel_Scores.Eval1 IS NULL THEN 1 ELSE 0 END,
		VUN.UserId, P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATE_DATE
FROM 	ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	P2RMIS.dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = P2RMIS.dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = P2RMIS.dbo.PRG_Panel_Scores.Log_No	LEFT OUTER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATED_BY = VUN.UserName
WHERE ApplicationStage.ReviewStageId = 2 AND NOT EXISTS (Select 1 FROM ApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)

--Update ApplicationReviewStatus for scored apps
UPDATE ApplicationReviewStatus
SET ReviewStatusId = 6
WHERE PanelApplicationId IN
(Select PanelApplication.PanelApplicationId
FROM PanelApplication INNER JOIN
ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationWorkflowStep.StepTypeId = 8 AND ApplicationWorkflowStepElementContent.DeletedFlag = 0)

--Update content text for meeting phase from last submitted critique from 1.0
UPDATE ApplicationWorkflowStepElementContent
SET ContentText = PRG_Criteria_Eval.Criteria_Txt, Abstain = CASE WHEN ApplicationWorkflowStepElementContent.Score IS NULL AND CAST(PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END, CritiqueRevised = CASE WHEN PRG_Criteria_Eval.Scoring_Phase = 'Meeting' THEN 1 ELSE 0 END
FROM ApplicationWorkflowStepElementContent INNER JOIN
ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
	(Select Critique_ID, Criteria_Txt, Scoring_Phase, ECM_ID, ROW_NUMBER() OVER (Partition By Critique_ID, ECM_ID Order By Last_Update_Date DESC) AS Ranking
	FROM [$(P2RMIS)].PRG_Criteria_Eval) PRG_Criteria_Eval ON  P2RMIS.dbo.PRG_Critiques.Critique_ID = PRG_Criteria_Eval.Critique_ID AND MechanismTemplateElement.LegacyEcmId = PRG_Criteria_Eval.ECM_ID AND PRG_Criteria_Eval.Ranking = 1
WHERE ViewApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 2

--Insert unscored criteria evaluation for assigned reviewer
INSERT INTO ApplicationWorkflowStepElementContent ([ApplicationWorkflowStepElementId]
           ,[ContentText]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, PRG_Criteria_Eval.Criteria_Txt, CASE WHEN CAST(PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
	CASE WHEN PRG_Criteria_Eval.Scoring_Phase = 'Meeting' THEN 1 ELSE 0 END, VUN.UserId, PRG_Criteria_Eval.LAST_UPDATE_DATE
		   FROM ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
	(Select Critique_ID, Criteria_Txt, Scoring_Phase, ECM_ID, PRG_Criteria_Eval.Last_Updated_By, PRG_Criteria_Eval.LAST_UPDATE_DATE, ROW_NUMBER() OVER (Partition By Critique_ID, ECM_ID Order By Last_Update_Date DESC) AS Ranking
	FROM P2RMIS.dbo.PRG_Criteria_Eval) PRG_Criteria_Eval ON  P2RMIS.dbo.PRG_Critiques.Critique_ID = PRG_Criteria_Eval.Critique_ID AND MechanismTemplateElement.LegacyEcmId = PRG_Criteria_Eval.ECM_ID AND PRG_Criteria_Eval.Ranking = 1 LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName  
WHERE ApplicationStage.ReviewStageId = 2 AND MechanismTemplateElement.ScoreFlag = 0 AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)

/*************************************************UPDATE Scripts**************************************************************/


DECLARE @LastUpdateDate datetime2(0) = '9/20/2016';
--Stage1 insert
INSERT INTO  [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT     DISTINCT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score, 
                      CAST(P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)), CASE WHEN P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score IS NULL AND CAST(P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
					  VUN.UserId, P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE
FROM                  ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON 
                      ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN                      
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Criteria_Eval ON P2RMIS.dbo.PRG_Critiques.Critique_ID = P2RMIS.dbo.PRG_Criteria_Eval.Critique_ID AND 
                      CASE ApplicationWorkflowStep.StepTypeId WHEN 5 THEN 'Initial' WHEN 6 THEN 'Revised' ELSE 'Meeting' END = P2RMIS.dbo.PRG_Criteria_Eval.Scoring_Phase AND
                      MechanismTemplateElement.LegacyEcmId = P2RMIS.dbo.PRG_Criteria_Eval.Ecm_ID  LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName     
WHERE ApplicationStage.ReviewStageId IN (1,2) AND P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE > @LastUpdateDate AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)
--Stage1 updates
UPDATE ApplicationWorkflowStepElementContent SET Score = P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score,
ContentText = P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt, Abstain = CASE WHEN P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Score IS NULL AND CAST(P2RMIS.dbo.PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
ModifiedBy = VUN.UserId, ModifiedDate = P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE
FROM                  ViewApplicationWorkflowStep ApplicationWorkflowStep INNER JOIN
                      ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
                      ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON 
                      ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
					  ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN
                      ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
                      ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
                      ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN                      
                      ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
                      ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN
                      P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Criteria_Eval ON P2RMIS.dbo.PRG_Critiques.Critique_ID = P2RMIS.dbo.PRG_Criteria_Eval.Critique_ID AND 
                      CASE ApplicationWorkflowStep.StepTypeId WHEN 5 THEN 'Initial' WHEN 6 THEN 'Revised' ELSE 'Meeting' END = P2RMIS.dbo.PRG_Criteria_Eval.Scoring_Phase AND
                      MechanismTemplateElement.LegacyEcmId = P2RMIS.dbo.PRG_Criteria_Eval.Ecm_ID  LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName     
WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 1 AND P2RMIS.dbo.PRG_Criteria_Eval.LAST_UPDATE_DATE > @LastUpdateDate AND ISNULL(ApplicationWorkflowStepElementContent.ModifiedDate, '1/1/1900') < PRG_Criteria_Eval.Last_Update_Date
--Stage 2 assigned meeting score update NEEDS WORK
UPDATE ApplicationWorkflowStepElementContent
SET Score = CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END
FROM ApplicationWorkflowStepElementContent INNER JOIN
	ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow  ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId AND
	ApplicationWorkflow.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON PanelApplicationReviewerAssignment.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	P2RMIS.dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = P2RMIS.dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = P2RMIS.dbo.PRG_Panel_Scores.Log_No
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATE_DATE > @LastUpdateDate  AND (ISNULL(ApplicationWorkflowStepElementContent.ModifiedDate, '1/1/1900') < PRG_Panel_Scores.Last_Update_Date OR ( 
	ApplicationWorkflowStepElementContent.Score IS NULL AND (CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END) IS NOT NULL)
)
--Stage 2 score/unassigned insert
INSERT INTO [dbo].[ApplicationWorkflowStepElementContent]
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[Abstain]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END,
		CASE WHEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score IS NULL AND P2RMIS.dbo.PRG_Panel_Scores.Eval1 IS NULL THEN 1 ELSE 0 END,
		VUN.UserId, P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATE_DATE
FROM 	ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	P2RMIS.dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = P2RMIS.dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = P2RMIS.dbo.PRG_Panel_Scores.Log_No	LEFT OUTER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATED_BY = VUN.UserName
WHERE ApplicationStage.ReviewStageId = 2 AND NOT EXISTS (Select 1 FROM ApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId) AND P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATE_DATE > @LastUpdateDate
--Stage 2 score/unassigned update
UPDATE ApplicationWorkflowStepElementContent SET Score = CASE WHEN ScorePosition.OverallFlag = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score
				WHEN ScorePosition.Position = 1 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval1
				WHEN ScorePosition.Position = 2 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval2
				WHEN ScorePosition.Position = 3 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval3
				WHEN ScorePosition.Position = 4 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval4
				WHEN ScorePosition.Position = 5 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval5
				WHEN ScorePosition.Position = 6 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval6
				WHEN ScorePosition.Position = 7 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval7
				WHEN ScorePosition.Position = 8 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval8
				WHEN ScorePosition.Position = 9 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval9
				WHEN ScorePosition.Position = 10 THEN P2RMIS.dbo.PRG_Panel_Scores.Eval10 END,
		Abstain = CASE WHEN P2RMIS.dbo.PRG_Panel_Scores.Global_Score IS NULL AND P2RMIS.dbo.PRG_Panel_Scores.Eval1 IS NULL THEN 1 ELSE 0 END,
		ModifiedBy = VUN.UserId, ModifiedDate = P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATE_DATE
FROM 	ApplicationWorkflowStepElementContent INNER JOIN
ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
	ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
	ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
	ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
	ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
	(SELECT DENSE_RANK() OVER (PARTITION BY MechanismTemplateElement.MechanismTemplateId ORDER BY MechanismTemplateElement.OverallFlag, MechanismTemplateElement.SortOrder) AS Position, MechanismTemplateElement.MechanismTemplateElementId, MechanismTemplateElement.LegacyEcmId, MechanismTemplateElement.OverallFlag
		FROM ViewMechanismTemplateElement MechanismTemplateElement
		WHERE MechanismTemplateElement.ScoreFlag = 1) ScorePosition ON ApplicationTemplateElement.MechanismTemplateElementId = ScorePosition.MechanismTemplateElementId INNER JOIN
	P2RMIS.dbo.PRG_Panel_Scores ON PanelUserAssignment.LegacyParticipantId = P2RMIS.dbo.PRG_Panel_Scores.PRG_Part_Id AND [Application].LogNumber = P2RMIS.dbo.PRG_Panel_Scores.Log_No	LEFT OUTER JOIN
	ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON P2RMIS.dbo.PRG_Panel_Scores.LAST_UPDATED_BY = VUN.UserName
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND P2RMIS.dbo.PRG_Panel_Scores.Last_Update_Date > @LastUpdateDate AND ISNULL(ApplicationWorkflowStepElementContent.ModifiedDate, '1/1/1900') <= PRG_Panel_Scores.Last_Update_Date

--Update ApplicationReviewStatus for scored apps
UPDATE ApplicationReviewStatus
SET ReviewStatusId = 6
WHERE ReviewStatusId = 2 AND PanelApplicationId IN
(Select PanelApplication.PanelApplicationId
FROM PanelApplication INNER JOIN
ApplicationStage ON PanelApplication.PanelApplicationId = ApplicationStage.PanelApplicationId INNER JOIN
ApplicationWorkflow ON ApplicationStage.ApplicationStageId = ApplicationWorkflow.ApplicationStageId INNER JOIN
ApplicationWorkflowStep ON ApplicationWorkflow.ApplicationWorkflowId = ApplicationWorkflowStep.ApplicationWorkflowId INNER JOIN
ApplicationWorkflowStepElement ON ApplicationWorkflowStep.ApplicationWorkflowStepId = ApplicationWorkflowStepElement.ApplicationWorkflowStepId INNER JOIN
ApplicationWorkflowStepElementContent ON ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationWorkflowStep.StepTypeId = 8 AND ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ApplicationWorkflowStepElementContent.ModifiedDate > @LastUpdateDate )

--Update content text for meeting phase from last submitted critique from 1.0
UPDATE ApplicationWorkflowStepElementContent
SET ContentText = PRG_Criteria_Eval.Criteria_Txt, Abstain = CASE WHEN ApplicationWorkflowStepElementContent.Score IS NULL AND CAST(PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END, CritiqueRevised = CASE WHEN PRG_Criteria_Eval.Scoring_Phase = 'Meeting' THEN 1 ELSE 0 END
FROM ApplicationWorkflowStepElementContent INNER JOIN
ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId INNER JOIN
ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
	(Select Critique_ID, Criteria_Txt, Scoring_Phase, ECM_ID, Last_Update_Date, ROW_NUMBER() OVER (Partition By Critique_ID, ECM_ID Order By Last_Update_Date DESC) AS Ranking
	FROM P2RMIS.dbo.PRG_Criteria_Eval) PRG_Criteria_Eval ON  P2RMIS.dbo.PRG_Critiques.Critique_ID = PRG_Criteria_Eval.Critique_ID AND MechanismTemplateElement.LegacyEcmId = PRG_Criteria_Eval.ECM_ID AND PRG_Criteria_Eval.Ranking = 1
WHERE ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ApplicationStage.ReviewStageId = 2 AND PRG_Criteria_Eval.Last_Update_Date > @LastUpdateDate AND (ISNULL(ApplicationWorkflowStepElementContent.ModifiedDate, '1/1/1900') < PRG_Criteria_Eval.Last_Update_Date OR ApplicationWorkflowStepElementContent.ContentText IS NULL)

INSERT INTO ApplicationWorkflowStepElementContent ([ApplicationWorkflowStepElementId]
           ,[ContentText]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId, PRG_Criteria_Eval.Criteria_Txt, CASE WHEN CAST(PRG_Criteria_Eval.Criteria_Txt AS varchar(max)) = 'n/a' THEN 1 ELSE 0 END,
	CASE WHEN PRG_Criteria_Eval.Scoring_Phase = 'Meeting' THEN 1 ELSE 0 END, VUN.UserId, PRG_Criteria_Eval.LAST_UPDATE_DATE
		   FROM ViewApplicationWorkflowStepElement ApplicationWorkflowStepElement INNER JOIN
ViewApplicationWorkflowStep ApplicationWorkflowStep ON ApplicationWorkflowStepElement.ApplicationWorkflowStepId = ApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
ViewApplicationWorkflow ApplicationWorkflow ON ApplicationWorkflowStep.ApplicationWorkflowId = ApplicationWorkflow.ApplicationWorkflowId INNER JOIN
ViewApplicationStage ApplicationStage ON ApplicationWorkflow.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
ViewApplication Application ON PanelApplication.ApplicationId = Application.ApplicationId INNER JOIN
ViewApplicationReviewStatus ApplicationReviewStatus ON PanelApplication.PanelApplicationId = ApplicationReviewStatus.PanelApplicationId INNER JOIN
ReviewStatus ON ApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId AND ReviewStatus.ReviewStatusTypeId = 1 INNER JOIN
ViewPanelUserAssignment PanelUserAssignment ON ApplicationWorkflow.PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId INNER JOIN 
ViewApplicationTemplateElement ApplicationTemplateElement ON ApplicationWorkflowStepElement.ApplicationTemplateElementId = ApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON ApplicationTemplateElement.MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId INNER JOIN
ViewPanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON PanelUserAssignment.PanelUserAssignmentId = PanelApplicationReviewerAssignment.PanelUserAssignmentId AND
	PanelApplication.PanelApplicationId = PanelApplicationReviewerAssignment.PanelApplicationId INNER JOIN
	P2RMIS.dbo.PRG_Proposal_Assignments ON 
                      PanelApplicationReviewerAssignment.LegacyProposalAssignmentId = P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID INNER JOIN
                      P2RMIS.dbo.PRG_Critiques ON P2RMIS.dbo.PRG_Proposal_Assignments.PA_ID = P2RMIS.dbo.PRG_Critiques.PA_ID INNER JOIN
	(Select Critique_ID, Criteria_Txt, Scoring_Phase, ECM_ID, PRG_Criteria_Eval.Last_Updated_By, PRG_Criteria_Eval.LAST_UPDATE_DATE, ROW_NUMBER() OVER (Partition By Critique_ID, ECM_ID Order By Last_Update_Date DESC) AS Ranking
	FROM P2RMIS.dbo.PRG_Criteria_Eval) PRG_Criteria_Eval ON  P2RMIS.dbo.PRG_Critiques.Critique_ID = PRG_Criteria_Eval.Critique_ID AND MechanismTemplateElement.LegacyEcmId = PRG_Criteria_Eval.ECM_ID AND PRG_Criteria_Eval.Ranking = 1 LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId VUN ON PRG_Criteria_Eval.LAST_UPDATED_BY = VUN.UserName  
WHERE ApplicationStage.ReviewStageId = 2 AND PRG_Criteria_Eval.Last_Update_Date > @LastUpdateDate AND MechanismTemplateElement.ScoreFlag = 0 AND NOT EXISTS (Select 'X' FROM ViewApplicationWorkflowStepElementContent WHERE ApplicationWorkflowStepElementId = ApplicationWorkflowStepElement.ApplicationWorkflowStepElementId)
AND ApplicationReviewStatus.ReviewStatusId = 6

--Application comments
INSERT INTO [dbo].[UserApplicationComment]
           ([UserID]
           ,[PanelApplicationId]
           ,[ApplicationID]
           ,[Comments]
           ,[CommentTypeID]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [UserInformation].UserID, [ViewPanelApplication].PanelApplicationId, [ViewPanelApplication].ApplicationId, [ClientAccessUserComment].Comments, [ClientAccessUserComment].CommentLkpID, [UserInformation].UserID, ClientAccessUserComment.CreatedDate, [UserInformation].UserID, ClientAccessUserComment.ModifiedDate
FROM [P2RMISNet].dbo.UserApplicationComment ClientAccessUserComment INNER JOIN
[P2RMISNet].dbo.[ViewUser] ClientAccessUser ON ClientAccessUserComment.UserID = ClientAccessUser.UserID INNER JOIN
[P2RMISNet].dbo.[ViewUserInfo] ClientAccessUserInfo ON ClientAccessUserComment.UserID = ClientAccessUserInfo.UserID LEFT JOIN
[P2RMISNet].dbo.[ViewUserEmail] UserEmail ON ClientAccessUserInfo.UserInfoID = UserEmail.UserInfoID AND UserEmail.PrimaryFlag = 1 INNER JOIN
[ViewApplication] ON ClientAccessUserComment.ApplicationID = ViewApplication.LogNumber INNER JOIN
[ViewPanelApplication] ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId LEFT OUTER JOIN
(SELECT FirstName, LastName, ViewUserEmail.Email, ViewUser.UserId, PersonID
FROM ViewUser INNER JOIN
ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID LEFT JOIN
ViewUserEmail ON ViewUserInfo.UserInfoID = ViewUserEmail.UserInfoID AND ViewUserEmail.PrimaryFlag = 1) [UserInformation] ON (ClientAccessUser.UserID = [UserInformation].UserID AND ClientAccessUserInfo.FirstName = UserInformation.FirstName AND ClientAccessUserInfo.LastName = UserInformation.LastName) OR ClientAccessUser.PersonID = UserInformation.PersonID
WHERE ClientAccessUserComment.DeletedFlag = 0 AND NOT EXISTS (SELECT 'X' FROM ViewUserApplicationComment WHERE ViewUserApplicationComment.Comments = ClientAccessUserComment.Comments AND ViewUserApplicationComment.PanelApplicationId = ViewPanelApplication.PanelApplicationId)

--Unnassigned reviewer comments
INSERT INTO [dbo].[UserApplicationComment]
           ([UserID]
           ,[PanelApplicationId]
           ,[ApplicationID]
           ,[Comments]
           ,[CommentTypeID]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT DISTINCT ViewPanelUserAssignment.UserID, ViewPanelApplication.PanelApplicationId, ViewPanelApplication.ApplicationId, CAST(PRG_Panel_Proposal_Comment.comment AS nvarchar(max)), 5, VUN.UserId, PRG_Panel_Proposal_Comment.Last_Update_Date
FROM [P2RMIS].dbo.PRG_Panel_Proposal_Comment PRG_Panel_Proposal_Comment
INNER JOIN [ViewApplication] ON PRG_Panel_Proposal_Comment.Log_No = [ViewApplication].LogNumber
INNER JOIN [ViewPanelApplication] ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId
INNER JOIN [ViewPanelUserAssignment] ON PRG_Panel_Proposal_Comment.Prg_Part_ID = ViewPanelUserAssignment.LegacyParticipantId
INNER JOIN ViewLegacyUserNameToUserId VUN ON PRG_Panel_Proposal_Comment.Last_Updated_By = VUN.UserName
WHERE LEN(LTRIM(CAST(comment AS nvarchar(max)))) > 1 AND NOT EXISTS (SELECT 'X' FROM ViewUserApplicationComment WHERE PanelApplicationId = ViewPanelApplication.PanelApplicationId AND UserId = ViewPanelUserAssignment.UserId AND CommentTypeID = 5)

--Application stage active status
UPDATE ApplicationStage SET ActiveFlag = 1
FROM ApplicationStage INNER JOIN
ViewPanelApplication ON ApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId INNER JOIN
ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId
WHERE ApplicationStage.ReviewStageId = 2 AND ApplicationStage.ActiveFlag = 0 AND ViewApplicationReviewStatus.ReviewStatusId = 6