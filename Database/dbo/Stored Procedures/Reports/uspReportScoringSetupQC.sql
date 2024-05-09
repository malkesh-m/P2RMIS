CREATE PROCEDURE [dbo].[uspReportScoringSetupQC]
	@ProgramList VARCHAR(5000),
	@FiscalYearList VARCHAR(5000),
	@CycleList VARCHAR(4000)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Programs TABLE(
		ProgramID VARCHAR(100)
	)

	INSERT INTO @Programs SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)

	DECLARE @Years TABLE(
		 FY VARCHAR(10)
	)

	INSERT INTO @Years SELECT ParameterValue FROM dbo.SplitReportParameterInt(@FiscalYearList)

	DECLARE @Cycle TABLE(
		Cycle VARCHAR(100)
	)

	INSERT INTO @Cycle SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList)

	SELECT 
			Program.ProgramDescription Program, ProgramYear.Year, ProgramMechanism.ReceiptCycle Cycle, ClientAwardType.AwardAbbreviation
			, ClientElement.ElementDescription Criterion
			, ST.StepTypeName Phase
			, MTE.ScoreFlag
			, MTE.OverallFlag 
			, CSS.HighValue 
			, CSS.LowValue
			, MTE.TextFlag
			, MTE.SummarySortOrder SummaryStatementOrder
	FROM	dbo.ClientProgram Program
			INNER JOIN dbo.ViewProgramYear ProgramYear ON Program.ClientProgramId = ProgramYear.ClientProgramId 
			INNER JOIN dbo.ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId
			---------------------------Parameters-----------------------------
			INNER JOIN @Programs Programs ON Programs.ProgramID= Program.ClientProgramId 
			INNER JOIN @Years Years ON Years.FY = ProgramYear.Year 
			INNER JOIN @Cycle Cycle ON Cycle.Cycle = 0 or Cycle.Cycle = ProgramMechanism.ReceiptCycle
			------------------------------------------------------------------
			INNER JOIN ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId 
			INNER JOIN ViewClientElement ClientElement ON Program.ClientId = ClientElement.ClientId  
			INNER JOIN ViewMechanismTemplate MT ON ProgramMechanism.ProgramMechanismId = MT.ProgramMechanismId
			INNER JOIN ViewMechanismTemplateElement MTE ON MT.MechanismTemplateId = MTE.MechanismTemplateId and ClientElement.ClientElementId = MTE.ClientElementId
			INNER JOIN ViewMechanismTemplateElementScoring MTES ON MTE.MechanismTemplateElementId = MTES.MechanismTemplateElementId
			INNER JOIN DBO.StepType ST ON MTES.StepTypeId = ST.StepTypeId AND ST.ReviewStageId = 1 --Asynchronous
			INNER JOIN DBO.ClientScoringScale CSS ON MTES.ClientScoringId = CSS.ClientScoringId AND Program.ClientId = CSS.ClientId

		WHERE ClientElement.ElementTypeId = 1 --Review Criteria
		ORDER BY ProgramMechanism.ProgramMechanismId, ClientElement.ElementDescription, ST.SortOrder, MTE.SummarySortOrder
		   

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportScoringSetupQC] TO [NetSqlAzMan_Users]
    AS [dbo];