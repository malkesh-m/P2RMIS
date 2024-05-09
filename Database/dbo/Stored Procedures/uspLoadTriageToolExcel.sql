
CREATE PROCEDURE [dbo].[uspLoadTriageToolExcel]
@SessionID nvarchar(10),
@AwardType nvarchar (10)

AS
DECLARE 	
		@EvalLabel nvarchar(250),
		@SortOrder int,
		@EvalCount int,
		@ECMCount int,
		@ECMID int,
		@CritiqueID int,
		@ECMSortOrder int,
		@EvalCriteria nvarchar(20),
		@ECMCriteriaScore decimal(3,1),
		@ECMCriteriaScore1 decimal(3,1),
		@ECMCriteriaScore2 decimal(3,1),
		@ECMCriteriaScore3 decimal(3,1),
		@ECMCriteriaScore4 decimal(3,1),
		@ECMCriteriaScore5 decimal(3,1),
		@ECMCriteriaScore6 decimal(3,1),
		@ECMCriteriaScore7 decimal(3,1),
		@ECMCriteriaScore8 decimal(3,1),
		@ECMCriteriaScore9 decimal(3,1),
		@ECMCriteriaScore10 decimal(3,1),
		@AssignOrder int,
		@EvalLabel1 nvarchar(250),
		@EvalLabel2 nvarchar(250),
		@EvalLabel3 nvarchar(250),
		@EvalLabel4 nvarchar(250),
		@EvalLabel5 nvarchar(250),
		@EvalLabel6 nvarchar(250),
		@EvalLabel7 nvarchar(250),
		@EvalLabel8 nvarchar(250),
		@EvalLabel9 nvarchar(250),
		@EvalLabel10 nvarchar(250),
		@LogNo nvarchar(20), 
		@AwardShortDesc nvarchar(20),
		@PanelAbrv nvarchar(20),
		@FY int,
		@Program nvarchar (250),
		@Award nvarchar (250),
		@Overall_Eval bit
		
		DELETE FROM dbo.TempTriageTool
		
/*
CREATE TABLE dbo.#ApplicationScores (
	CritiqueID int,
	LogNo nvarchar(20), 
	AwardType nvarchar(10),
	AwardShortDesc nvarchar(20),
	EvalLabel1 nvarchar(250),
	ECMCriteriaScore1 decimal(3,1),
	EvalLabel2 nvarchar(250),
	ECMCriteriaScore2 decimal(3,1),
	EvalLabel3 nvarchar(250),
	ECMCriteriaScore3 decimal(3,1),
	EvalLabel4 nvarchar(250),
	ECMCriteriaScore4 decimal(3,1),
	EvalLabel5 nvarchar(250),
	ECMCriteriaScore5 decimal(3,1),
	EvalLabel6 nvarchar(250),
	ECMCriteriaScore6 decimal(3,1),
	EvalLabel7 nvarchar(250),
	ECMCriteriaScore7 decimal(3,1),
	EvalLabel8 nvarchar(250),
	ECMCriteriaScore8 decimal(3,1),
	EvalLabel9 nvarchar(250),
	ECMCriteriaScore9 decimal(3,1),
	EvalLabel10 nvarchar(250),
	ECMCriteriaScore10 decimal(3,1),	
	PanelAbrv nvarchar(20),
	AssignOrder int
)
*/
SET NOCOUNT ON

/* Get the types of awards for this panel */
DECLARE AwardCursor CURSOR FOR 
SELECT     ClientAwardType.ClientAwardTypeId
FROM         ClientAwardType INNER JOIN
					ViewProgramMechanism ON ClientAwardType.ClientAwardTypeId = ViewProgramMechanism.ClientAwardTypeId INNER JOIN
                      ViewApplication ON ViewProgramMechanism.ProgramMechanismId = ViewApplication.ProgramMechanismId INNER JOIN
                      ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId INNER JOIN
                      ViewSessionPanel ON ViewPanelApplication.SessionPanelId = ViewSessionPanel.SessionPanelId 
WHERE     ( ViewSessionPanel.MeetingSessionId  IN (1475)) AND (ClientAwardType.ClientAwardTypeId IN (152,302,797))
GROUP BY ClientAwardType.ClientAwardTypeId

OPEN AwardCursor
FETCH NEXT FROM AwardCursor INTO @AwardType
WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set all evaluation labels to Null */
		SELECT @EvalLabel1 = NULL
		SELECT @EvalLabel2 = NULL
		SELECT @EvalLabel3 = NULL	
		SELECT @EvalLabel4 = NULL
		SELECT @EvalLabel5 = NULL
		SELECT @EvalLabel6 = NULL
		SELECT @EvalLabel7 = NULL
		SELECT @EvalLabel8 = NULL
		SELECT @EvalLabel9 = NULL
		SELECT @EvalLabel10 = NULL

		DECLARE EvalLabelCursor CURSOR FOR
		SELECT DISTINCT MTE.MechanismTemplateElementId, CE.ElementDescription, MTE.SortOrder, MTE.OverallFlag
		FROM         ViewApplication App INNER JOIN
						ViewProgramMechanism PM ON App.ProgramMechanismId = PM.ProgramMechanismId INNER JOIN
		                      ViewClientAwardType [AT] ON PM.ClientAwardTypeId = [AT].ClientAwardTypeId INNER JOIN
							  ViewMechanismTemplate MT ON PM.ProgramMechanismId = MT.ProgramMechanismId INNER JOIN
							  ViewMechanismTemplateElement MTE ON MT.MechanismTemplateId = MTE.MechanismTemplateId INNER JOIN
		                      ClientElement CE ON MTE.ClientElementId = CE.ClientElementId INNER JOIN
		                      ViewPanelApplication PanApp ON App.ApplicationId =  PanApp.ApplicationId INNER JOIN
		                      ViewSessionPanel SP ON PanApp.SessionPanelId = SP.SessionPanelId
		WHERE     	([AT].ClientAwardTypeId = @AwardType) AND  (MTE.ScoreFlag = 1)  AND ( SP.MeetingSessionId  IN (1475)) AND MT.ReviewStageId = 1
		--AND MTE.MechanismTemplateElementId IN (List of mismatching elements to exclude)
		ORDER BY MTE.OverallFlag desc, MTE.SortOrder

		
		SELECT @EvalCount = 0
		OPEN EvalLabelCursor
		FETCH NEXT FROM EvalLabelCursor INTO @ECMID, @EvalLabel, @SortOrder, @Overall_Eval
		WHILE @@FETCH_STATUS = 0			
			BEGIN
				SET @EvalCount = @EvalCount + 1
				IF @EvalCount = 1 
					SELECT @EvalLabel1  = @EvalLabel
				IF @EvalCount = 2 
					SELECT @EvalLabel2  = @EvalLabel
				IF @EvalCount = 3 
					SELECT @EvalLabel3  = @EvalLabel
				IF @EvalCount  = 4
					SELECT @EvalLabel4 = @EvalLabel
				IF @EvalCount = 5
					SELECT @EvalLabel5 = @EvalLabel
				IF @EvalCount = 6
					SELECT @EvalLabel6 = @EvalLabel
				IF @EvalCount= 7
					SELECT @EvalLabel7 = @EvalLabel
				IF @EvalCount = 8
					SELECT @EvalLabel8 = @EvalLabel		
				IF @EvalCount  = 9
					SELECT @EvalLabel9 = @EvalLabel
				IF @EvalCount = 10
					SELECT @EvalLabel10 = @EvalLabel
				FETCH NEXT FROM EvalLabelCursor INTO  @ECMID, @EvalLabel, @SortOrder, @Overall_Eval
			END	
		/*Close the EvalLabel Cursor */
		CLOSE EvalLabelCursor
		DEALLOCATE EvalLabelCursor
			
		/* Create a cursor that gets the values for the specifc award type */
		DECLARE EvalCursor CURSOR FOR
SELECT DISTINCT 
                      AW.ApplicationWorkflowId, App.LogNumber, CAT.AwardAbbreviation, SP.PanelAbbreviation, 
                      Para.SortOrder, Py.[Year], CP.ProgramDescription AS Program_Desc, 
                      CAT.AwardDescription AS Award_Desc
FROM         ViewApplication App INNER JOIN
					  ViewPanelApplication PanApp ON App.ApplicationId = PanApp.ApplicationId INNER JOIN
                      ViewPanelApplicationReviewerAssignment Para ON PanApp.PanelApplicationId = Para.PanelApplicationId INNER JOIN
					  ViewPanelUserAssignment Pua ON Para.PanelUserAssignmentId = Pua.PanelUserAssignmentId INNER JOIN
					  ClientAssignmentType [AT] ON Para.ClientAssignmentTypeId = [AT].ClientAssignmentTypeId INNER JOIN
                      ViewUserInfo UI ON Pua.UserId = UI.UserID INNER JOIN 
                      ViewSessionPanel SP ON PanApp.SessionPanelId = SP.SessionPanelId INNER JOIN
                      ViewProgramMechanism PM ON App.ProgramMechanismId = PM.ProgramMechanismId INNER JOIN
					  ViewClientAwardType CAT ON PM.ClientAwardTypeId = CAT.ClientAwardTypeId INNER JOIN
					  ViewProgramYear PY ON PM.ProgramYearId = PY.ProgramYearId INNER JOIN
					  ClientProgram CP ON PY.ClientProgramId = CP.ClientProgramId INNER JOIN
					  ViewApplicationStage AppStage ON PanApp.PanelApplicationId = AppStage.PanelApplicationId INNER JOIN
					  ViewApplicationWorkflow AW ON AppStage.ApplicationStageId = AW.ApplicationStageId AND Pua.PanelUserAssignmentId = AW.PanelUserAssignmentId
		WHERE     (CAT.ClientAwardTypeId =  @AwardType) AND ( SP.MeetingSessionId IN (1475)) AND AppStage.ReviewStageId = 1 AND [AT].AssignmentTypeId NOT IN (7, 8)
		ORDER BY App.LogNumber, Para.SortOrder
		OPEN EvalCursor 
		FETCH NEXT FROM EvalCursor INTO  @CritiqueID,@LogNo,   @AwardShortDesc,  @PanelAbrv,  @AssignOrder,@FY,@Program,@Award
		
		WHILE @@FETCH_STATUS = 0
			BEGIN
				
				/*Set all ECMCriteriaScore variables to 0.0 */
				SELECT @ECMCriteriaScore1  = 0.0
				SELECT @ECMCriteriaScore2  = 0.0
				SELECT @ECMCriteriaScore3  = 0.0
				SELECT @ECMCriteriaScore4  = 0.0
				SELECT @ECMCriteriaScore5  = 0.0
				SELECT @ECMCriteriaScore6  = 0.0
				SELECT @ECMCriteriaScore7  = 0.0
				SELECT @ECMCriteriaScore8  = 0.0
				SELECT @ECMCriteriaScore9  = 0.0
				SELECT @ECMCriteriaScore10  = 0.0
	
			
			
				
				DECLARE CriteriaScoreCursor CURSOR FOR
				SELECT     MTE.SortOrder, AWSEC.[Score]
				FROM         ViewApplicationWorkflow AW CROSS APPLY
							udfApplicationWorkflowLastStep(AW.ApplicationWorkflowId) AppStep INNER JOIN
							ViewApplicationWorkflowStepElement AWSE ON AppStep.ApplicationWorkflowStepId = AWSE.ApplicationWorkflowStepId LEFT JOIN
							ViewApplicationWorkflowStepElementContent AWSEC ON AWSE.ApplicationWorkflowStepElementId = AWSEC.ApplicationWorkflowStepElementId INNER JOIN
							ViewApplicationTemplateElement ATE ON AWSE.ApplicationTemplateElementId = ATE.ApplicationTemplateElementId INNER JOIN
							ViewMechanismTemplateElement MTE ON ATE.MechanismTemplateElementId = MTE.MechanismTemplateElementId
				WHERE     (AW.ApplicationWorkflowId = @CritiqueID) AND (MTE.ScoreFlag = 1)
				--AND MTE.MechanismTemplateElementId IN (List of mismatching elements to exclude)
				ORDER BY MTE.OverallFlag DESC, MTE.SortOrder


				SELECT @ECMCount = 0
				OPEN CriteriaScoreCursor 
				FETCH NEXT FROM CriteriaScoreCursor INTO  @ECMSortOrder, @ECMCriteriaScore
				WHILE @@FETCH_STATUS = 0			
					BEGIN	
						SET @ECMCount = @ECMCount + 1					
						IF  @ECMCount= 1 
							SELECT @ECMCriteriaScore1  = @ECMCriteriaScore
						IF  @ECMCount = 2 
							SELECT @ECMCriteriaScore2  = @ECMCriteriaScore
						IF  @ECMCount = 3 
							SELECT @ECMCriteriaScore3  = @ECMCriteriaScore
						IF  @ECMCount = 4
							SELECT @ECMCriteriaScore4 = @ECMCriteriaScore
						IF  @ECMCount = 5
							SELECT @ECMCriteriaScore5 = @ECMCriteriaScore
						IF @ECMCount = 6
							SELECT @ECMCriteriaScore6 = @ECMCriteriaScore
						IF @ECMCount= 7
							SELECT @ECMCriteriaScore7 = @ECMCriteriaScore
						IF @ECMCount = 8
							SELECT @ECMCriteriaScore8 = @ECMCriteriaScore	
						IF @ECMCount  = 9
							SELECT @ECMCriteriaScore9= @ECMCriteriaScore
						IF @ECMCount = 10
							SELECT @ECMCriteriaScore10 = @ECMCriteriaScore

						FETCH NEXT FROM CriteriaScoreCursor INTO  @ECMSortOrder, @ECMCriteriaScore
					END	
				/*Close the CriteriaScoreCursor Cursor */
				CLOSE CriteriaScoreCursor 
				DEALLOCATE CriteriaScoreCursor 
				
					
	

				/* Set fields for insert */

				
				IF @EvalLabel1 IS NULL
					SET @EvalLabel1 = 'None'
				IF @EvalLabel2 IS NULL
					SET @EvalLabel2 = 'None'
				IF @EvalLabel3 IS NULL
					SET @EvalLabel3 = 'None'
				IF @EvalLabel4 IS NULL
					SET @EvalLabel4 = 'None'
				IF @EvalLabel5 IS NULL
					SET @EvalLabel5 = 'None'
				IF @EvalLabel6 IS NULL
					SET @EvalLabel6= 'None'
				IF @EvalLabel7 IS NULL
					SET @EvalLabel7 = 'None'
				IF @EvalLabel8 IS NULL
					SET @EvalLabel8 = 'None'
				IF @EvalLabel9 IS NULL 
					SET @EvalLabel9 = 'None'
				IF @EvalLabel10 IS NULL
					SET @EvalLabel10 = 'None'
				IF @ECMCriteriaScore1 IS NULL
					SET @ECMCriteriaScore1 = 0.0
				IF @ECMCriteriaScore2 IS NULL
					SET @ECMCriteriaScore2 = 0.0
				IF @ECMCriteriaScore3 IS NULL
					SET @ECMCriteriaScore3 = 0.0
				IF @ECMCriteriaScore4 IS NULL
					SET @ECMCriteriaScore4 = 0.0
				IF @ECMCriteriaScore5 IS NULL
					SET @ECMCriteriaScore5 = 0.0
				IF @ECMCriteriaScore6 IS NULL
					SET @ECMCriteriaScore6 = 0.0 
				IF @ECMCriteriaScore7 IS NULL
					SET @ECMCriteriaScore7 = 0.0
				IF @ECMCriteriaScore8 IS NULL
					SET @ECMCriteriaScore8 = 0.0
				IF @ECMCriteriaScore9 IS NULL
					SET @ECMCriteriaScore9 = 0.0
				IF @ECMCriteriaScore10 IS NULL
					SET @ECMCriteriaScore10 = 0.0

				

INSERT INTO dbo.TempTriageTool (LogNo_Assign, CritiqueID, LogNo,  AwardType, AwardShortDesc, EvalLabel1, EvalLabel2,  EvalLabel3,  EvalLabel4,  EvalLabel5,  EvalLabel6,  EvalLabel7,  EvalLabel8,  EvalLabel9,  EvalLabel10,  ECMCriteriaScore1, ECMCriteriaScore2, ECMCriteriaScore3, ECMCriteriaScore4, ECMCriteriaScore5, ECMCriteriaScore6, ECMCriteriaScore7, ECMCriteriaScore8, ECMCriteriaScore9, ECMCriteriaScore10, PanelAbrv,AssignOrder,FY,Program,Award)
				VALUES ((cast(@LogNo  as nvarchar) +'.'+ cast(@AssignOrder as nvarchar)), @CritiqueID, @LogNo,  @AwardType, @AwardShortDesc, @EvalLabel1, @EvalLabel2,  @EvalLabel3,  @EvalLabel4,  @EvalLabel5,  @EvalLabel6,  @EvalLabel7,  @EvalLabel8,  @EvalLabel9,  @EvalLabel10, @ECMCriteriaScore1, @ECMCriteriaScore2, @ECMCriteriaScore3, @ECMCriteriaScore4, @ECMCriteriaScore5, @ECMCriteriaScore6, @ECMCriteriaScore7, @ECMCriteriaScore8, @ECMCriteriaScore9, @ECMCriteriaScore10, @PanelAbrv,@AssignOrder,@FY,@Program,@Award)
				FETCH NEXT FROM EvalCursor INTO @CritiqueID,@LogNo,  @AwardShortDesc,  @PanelAbrv,  @AssignOrder,@FY,@Program,@Award 
			END
			
		CLOSE EvalCursor
		DEALLOCATE EvalCursor
				
		FETCH NEXT FROM AwardCursor INTO @AwardType
	END			

CLOSE AwardCursor
DEALLOCATE AwardCursor

SET NOCOUNT OFF

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspLoadTriageToolExcel] TO [NetSqlAzMan_Users]
    AS [dbo];