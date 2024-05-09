CREATE PROCEDURE [dbo].[uspReportReviewerExpertisePostAssignment]
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
		Program varchar(1000)
		, FY int
		, Panel varchar(1000)
		, SROName varchar(200)
		, ProgramMechanismId int
		, LogNumber varchar(50)
		, panelApplicationId int
		, PanelUserAssignmentId int
		, ClientAssignmentTypeId int
		, AssignmentAbbreviation varchar(20)
		, ClientExpertiseRatingId int
		, RatingAbbreviation varchar(40)
		
	)

	INSERT INTO @AppReviewerList
	SELECT DISTINCT
				ClientProgram.ProgramDescription Program, ProgramYear.Year, SessionPanel.PanelAbbreviation Panel
				,  (SELECT TOP 1 SROUserInfo.LastName + ', ' + SROUserInfo.FirstName  
					FROM dbo.ViewPanelUserAssignment as SROUserAssign  
					JOIN dbo.ClientParticipantType AS SROClientParticipantType ON SROClientParticipantType.ClientParticipantTypeId = SROUserAssign.ClientParticipantTypeId 
						AND SROClientParticipantType.LegacyPartTypeId = 'SRA' AND SROClientParticipantType.ClientId = ClientProgram.ClientId 
					INNER JOIN dbo.ViewUserInfo AS SROUserInfo ON SROUserInfo.UserID = SROUserAssign.UserId
					where SROUserAssign.SessionPanelId = PanelApplication.SessionPanelId) as SROName
				, ProgramMechanism.ProgramMechanismId, Appl.LogNumber, PanelApplication.PanelApplicationId 
				, para.PanelUserAssignmentId
				, para.ClientAssignmentTypeId
				, cat.AssignmentAbbreviation
				, isnull(pare.ClientExpertiseRatingId, 0) ClientExpertiseRatingId
				, isnull(cer.RatingAbbreviation, 'NoSelect') RatingAbbreviation
					
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
	
		WHERE cat.AssignmentTypeId IN (5, 8) --SR, COI
		ORDER BY ProgramMechanism.ProgramMechanismId, Appl.LogNumber
		   
	DECLARE @ReviewerRatingCount TABLE
	(
		Program varchar(1000)
		, FY int
		, Panel varchar(1000)
		, SROName varchar(200) 
		, LogNumber varchar(50)
		, RatingAbbreviation varchar(40)
		, RatingCount int
	)

	INSERT INTO @ReviewerRatingCount
	SELECT Program, FY, panel, SROName, LogNumber, RatingAbbreviation, count(RatingAbbreviation) RatingCount
	FROM @AppReviewerList
	GROUP BY Program, FY, panel, SROName, LogNumber, RatingAbbreviation
	ORDER BY Program, FY, panel, SROName, LogNumber, RatingAbbreviation

	SELECT Program, FY, panel, SROName, LogNumber
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'High'), 0) High
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'Med'), 0) Medium
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'Low'), 0) Low
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'None'), 0) None
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'COI'), 0) COI
	, isnull((SELECT RatingCount FROM @ReviewerRatingCount WHERE SROName = rrc.SROName and LogNumber = rrc.LogNumber and RatingAbbreviation = 'NoSelect'), 0) NoSelection
	FROM @ReviewerRatingCount rrc
	GROUP BY Program, FY, panel, SROName, LogNumber
	ORDER BY Program, FY, panel, SROName, LogNumber    
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerExpertisePostAssignment] TO [NetSqlAzMan_Users]
    AS [dbo];