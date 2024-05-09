CREATE PROCEDURE [dbo].[uspReportReviewerExpertisePostAssignmentSummary]
	@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar(5000)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Programs TABLE(
		ProgramID varchar(100)
	);
	INSERT INTO @Programs SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)

	DECLARE @Years TABLE(
		 FY varchar(10)
	);
	INSERT INTO @Years SELECT ParameterValue FROM dbo.SplitReportParameterInt(@FiscalYearList)

	DECLARE @Panel TABLE(
		PA varchar(100)
	);
	INSERT INTO @Panel SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList)


	-- Insert statements for procedure here
	DECLARE @AppReviewerList TABLE
	(
		LogNumber varchar(50)
		, RatingAbbreviation varchar(40)
		, RatingCount int
	)

	INSERT INTO @AppReviewerList
	SELECT DISTINCT
				Appl.LogNumber
				, isnull(cer.RatingAbbreviation, 'NoSelect') RatingAbbreviation
				, count(cer.RatingAbbreviation) RatingCount
					
		FROM	dbo.ClientProgram 
				INNER JOIN dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId 
				INNER JOIN dbo.ViewProgramPanel ProgramPanel ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId 
				INNER JOIN dbo.ViewSessionPanel SessionPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId 
				---------------------------Parameters-----------------------------
				INNER JOIN @Programs Programs ON Programs.ProgramID= clientprogram.ClientProgramId 
				INNER JOIN @Years Years ON Years.FY = ProgramYear.Year 
				INNER JOIN @Panel Panel ON Panel.PA =0 or Panel.PA = SessionPanel.SessionPanelId  
				------------------------------------------------------------------
				INNER JOIN dbo.ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId 
				INNER JOIN dbo.ViewApplication Appl ON ProgramMechanism.ProgramMechanismId = Appl.ProgramMechanismId 
				INNER JOIN dbo.ViewPanelApplication PanelApplication ON Appl.ApplicationId = PanelApplication.ApplicationId  

				INNER JOIN dbo.ViewPanelApplicationReviewerAssignment para ON PanelApplication.PanelApplicationId = para.PanelApplicationId
				INNER JOIN dbo.ClientAssignmentType cat ON para.ClientAssignmentTypeId = cat.ClientAssignmentTypeId 
				LEFT OUTER JOIN dbo.ViewPanelApplicationReviewerExpertise pare ON para.PanelApplicationId = pare.PanelApplicationId and para.PanelUserAssignmentId = pare.PanelUserAssignmentId
				LEFT OUTER JOIN dbo.ClientExpertiseRating cer ON pare.ClientExpertiseRatingId = cer.ClientExpertiseRatingId 
     	WHERE cat.AssignmentTypeId = 5 --SR
		GROUP BY Appl.LogNumber, cer.RatingAbbreviation
		ORDER BY Appl.LogNumber
		   
	DECLARE @SummaryCount TABLE
	(
			LogNumber varchar(50)
		, High int
		, Medium int
		, Low int
		, None int
		, Missing int
	)

	insert into @SummaryCount
	SELECT LogNumber
	, isnull((SELECT RatingCount FROM @AppReviewerList WHERE LogNumber = rrc.LogNumber and RatingAbbreviation = 'High'), 0) High
	, isnull((SELECT RatingCount FROM @AppReviewerList WHERE LogNumber = rrc.LogNumber and RatingAbbreviation = 'Med'), 0) Medium
	, isnull((SELECT RatingCount FROM @AppReviewerList WHERE LogNumber = rrc.LogNumber and RatingAbbreviation = 'Low'), 0) Low
	, isnull((SELECT RatingCount FROM @AppReviewerList WHERE LogNumber = rrc.LogNumber and RatingAbbreviation = 'None'), 0) None
	, isnull((SELECT RatingCount FROM @AppReviewerList WHERE LogNumber = rrc.LogNumber and RatingAbbreviation = 'NoSelect'), 0) Missing
	FROM @AppReviewerList rrc
	GROUP BY LogNumber
	ORDER BY LogNumber   

	SELECT 
		CASE WHEN High = 2 THEN 1 ELSE 0 END HighHigh
	, CASE WHEN High = 1 AND Medium = 1 THEN 1 ELSE 0 END HighMedium
	, CASE WHEN Medium = 2 THEN 1 ELSE 0 END MediumMedium
	, CASE WHEN High = 1 AND Low = 1 THEN 1 ELSE 0 END HighLow
	, CASE WHEN Medium = 1 AND Low = 1 THEN 1 ELSE 0 END MediumLow
	, CASE WHEN Low = 2 THEN 1 ELSE 0 END LowLow
	, None, Missing
	from @SummaryCount
			  
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerExpertisePostAssignmentSummary] TO [NetSqlAzMan_Users]
    AS [dbo];