EXEC sp_executesql @statement = N'CREATE PROCEDURE [dbo].[stpr_Rev_Assignments_Delete]
@Log nvarchar(12),
@DeletePrgPartId  nvarchar(50)
AS

DECLARE 	@CritiqueID int,
		@PSID int,
		@CSID int,
		@PP_LogNo nvarchar(12),
		@AssignmentID int

SET NOCOUNT ON

SELECT @PSID = 0
SELECT @CritiqueID = 0
SELECT @PP_LogNo = NULL
SELECT @CSID = 0
SELECT @AssignmentID 




SELECT @CritiqueID =  dbo.PRG_Criteria_Eval.Critique_ID
FROM         dbo.PRG_Criteria_Eval INNER JOIN
                      dbo.PRG_Critiques ON dbo.PRG_Criteria_Eval.Critique_ID = dbo.PRG_Critiques.Critique_ID INNER JOIN
                      dbo.PRG_Proposal_Assignments ON dbo.PRG_Critiques.PA_ID = dbo.PRG_Proposal_Assignments.PA_ID
WHERE     (dbo.PRG_Proposal_Assignments.Prg_Part_ID =@DeletePrgPartId) AND (dbo.PRG_Proposal_Assignments.Log_No = @Log)

IF @CritiqueID <> 0
	BEGIN
		/*Backup pre-meeting scores in PRG_Criteria_Eval*/
		INSERT INTO dbo.PRG_Criteria_Eval_BK (Criteria_Eval_ID, ECM_ID, Critique_ID, Criteria_Txt, Criteria_Score, LAST_UPDATE_DATE, LAST_UPDATED_BY, Scoring_Phase)
		SELECT Criteria_Eval_ID,  ECM_ID, Critique_ID, Criteria_Txt, Criteria_Score, LAST_UPDATE_DATE, LAST_UPDATED_BY, Scoring_Phase
		FROM  dbo.PRG_Criteria_Eval
		WHERE Critique_ID = @CritiqueID
		
		/*Delete from pre-meeting scores from PRG_Criteria_Eval*/
		DELETE FROM dbo.PRG_Criteria_Eval
		WHERE Critique_ID = @CritiqueID
	
		/*Backup Critique in PRG_Critique*/
		INSERT INTO dbo.PRG_Critiques_BK (Critique_ID, PA_ID, Critique, Adjective_Score, Last_Updated, Date_Completed, Last_Updated_By)
		SELECT Critique_ID, PA_ID, Critique, Adjective_Score, Last_Updated, Date_Completed, Last_Updated_By
		FROM dbo.PRG_Critiques
		WHERE Critique_ID = @CritiqueID
		
		/*Remove Critique from PRG_Critique*/
		DELETE FROM dbo.PRG_Critiques
		WHERE Critique_ID = @CritiqueID
		
		/*Check for Critique in Summary Statement*/
		SELECT @CSID = CS_ID
		FROM dbo.SS_Critique_Section
		WHERE Critique_ID = @CritiqueID 
		
		IF @CSID <> 0
			BEGIN
			
			/*Backup Critique in SS_Critique_Session*/
			INSERT INTO SS_Critique_Section_BK (CS_ID, Critique_ID, EVAL_CRITERIA, Crit_Text, LAST_UPDATE_DATE, LAST_UPDATED_BY)
			SELECT CS_ID, Critique_ID, EVAL_CRITERIA, Crit_Text, LAST_UPDATE_DATE, LAST_UPDATED_BY
			FROM   dbo.SS_Critique_Section
			WHERE CS_ID = @CSID
			
			/*Remove from SS_Critique_Session*/
			DELETE FROM SS_Critique_Section
			WHERE CS_ID = @CSID
			
			END
	END
		
/*Check for meeting scores in PRG_Panel_Scores*/
SELECT     @PSID = PS_ID
FROM       dbo.PRG_Panel_Scores
WHERE     (Prg_Part_ID = @DeletePrgPartId) AND (LOG_NO = @Log)

IF @PSID <> 0
	BEGIN
		/*Backup Panel Scores*/
		INSERT INTO dbo.PRG_Panel_Scores_BK (PS_ID, LOG_NO, Prg_Part_ID, Global_Score, Eval1, Eval2, Eval3, Eval4, Eval5, Eval6, Eval7, Eval8, Eval9, Eval10, LAST_UPDATE_DATE, 
               LAST_UPDATED_BY, importFilename)
		SELECT PS_ID, LOG_NO, Prg_Part_ID, Global_Score, Eval1, Eval2, Eval3, Eval4, Eval5, Eval6, Eval7, Eval8, Eval9, Eval10, LAST_UPDATE_DATE, 
               LAST_UPDATED_BY, importFilename
		FROM  dbo.PRG_Panel_Scores
		WHERE PS_ID = @PSID
	
		/*Remove Panel Scores*/
		DELETE FROM dbo.PRG_Panel_Scores
		WHERE PS_ID = @PSID
	END

/*Backup Assignment in PRG_Proposal_Assignments*/
INSERT INTO dbo.PRG_Proposal_Assignments_BK (PA_ID, Prg_Part_ID, Log_No, Assignment_ID, Sort_Order, LAST_UPDATE_DATE, LAST_UPDATED_BY)
SELECT PA_ID, Prg_Part_ID, Log_No, Assignment_ID, Sort_Order, LAST_UPDATE_DATE, LAST_UPDATED_BY
FROM  dbo.PRG_Proposal_Assignments
WHERE (Log_No = @Log) AND Prg_part_id IN (@DeletePrgPartId)

SELECT @AssignmentID = Assignment_ID
FROM dbo.PRG_Proposal_Assignments
WHERE (Log_No = @Log) AND (Prg_Part_ID=@DeletePrgPartId)
/*Remove expertise rating if COI*/
IF @AssignmentID = 9
BEGIN
	DELETE FROM PRG_Reviewer_Preferences
	WHERE (Log_No = @Log) AND (Prg_part_id = @DeletePrgPartId)
END
/*Remove assignment from PRG_Proposal_Assignments*/
DELETE FROM dbo.PRG_Proposal_Assignments
WHERE (Log_No = @Log) AND Prg_part_id IN (@DeletePrgPartId)'