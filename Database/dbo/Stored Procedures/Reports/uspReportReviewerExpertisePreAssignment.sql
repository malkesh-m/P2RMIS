CREATE PROCEDURE [dbo].[uspReportReviewerExpertisePreAssignment]
	@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar(5000)
AS
BEGIN

	SET NOCOUNT ON;

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
				, ProgramMechanism.ProgramMechanismId, Appl.LogNumber
				, PanelApplication.PanelApplicationId 
				, pua.PanelUserAssignmentId 
				, pua.ClientParticipantTypeId  ClientAssignmentTypeId
				, cpt.ParticipantTypeAbbreviation AssignmentAbbreviation
				, isnull(pare.ClientExpertiseRatingId, 0) ClientExpertiseRatingId
				, isnull(cer.RatingAbbreviation, 'NoSelect') RatingAbbreviation
FROM	dbo.ClientProgram 
				INNER JOIN dbo.ViewProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId 
				INNER JOIN dbo.ViewProgramPanel ProgramPanel ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId 
				INNER JOIN dbo.ViewSessionPanel SessionPanel ON SessionPanel.SessionPanelId = ProgramPanel.SessionPanelId 
				
				INNER JOIN dbo.ViewProgramMechanism ProgramMechanism ON ProgramYear.ProgramYearId = ProgramMechanism.ProgramYearId 
				INNER JOIN dbo.ViewApplication Appl ON ProgramMechanism.ProgramMechanismId = Appl.ProgramMechanismId 
				INNER JOIN dbo.ViewPanelApplication PanelApplication ON Appl.ApplicationId = PanelApplication.ApplicationId  

				INNER JOIN dbo.ViewPanelApplicationReviewerExpertise pare on pare.PanelApplicationId = PanelApplication.PanelApplicationId
				INNER JOIN  dbo.ClientExpertiseRating cer ON pare.ClientExpertiseRatingId = cer.ClientExpertiseRatingId 
				INNER JOIN  dbo.ViewPanelUserAssignment pua ON pare.PanelUserAssignmentId = pua.PanelUserAssignmentId
				INNER JOIN dbo.ClientParticipantType cpt on pua.ClientParticipantTypeId = cpt.ClientParticipantTypeId
									AND cpt.ParticipantScope = 'Panel' AND cpt.ReviewerFlag = 1 AND cpt.ConsumerFlag = 0 AND cpt.clientid = ClientProgram.ClientId 
				where clientprogram.ClientProgramId = @ProgramList and ProgramYear.Year = @FiscalYearList and SessionPanel.SessionPanelId  = @PanelList
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
    ON OBJECT::[dbo].[uspReportReviewerExpertisePreAssignment] TO [NetSqlAzMan_Users]
    AS [dbo];