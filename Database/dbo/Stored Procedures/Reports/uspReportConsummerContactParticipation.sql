-- =============================================
-- Author:	Ngan Dinh
-- Create date: 1/31/2018
-- Description:	This report displays contact and participation information for consumer reviewers 
--				for selected program(s) and or panel(s).
-- PRMIS-23700
-- ============================================= 

CREATE PROCEDURE [dbo].[uspReportConsummerContactParticipation] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(5000),
	@FiscalYearList varchar(5000),
	@PanelList varchar(5000)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList)),
	-- Temp table 
	Consumer(
			ClientProgramID, UserID, ProgramAbbreviation,Program,year, PanelAbbreviation,Panel, ParticipantTypeAbbreviation, PanelEndDate)
	AS
		(SELECT ClientProgram.ClientProgramId,
				ViewPanelUserAssignment.UserID,
				ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription,
				ViewProgramYear.Year,
				ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName,
				ClientParticipantType.ParticipantTypeAbbreviation, ViewSessionPanel.EndDate
		FROM ClientProgram
			JOIN ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
			JOIN ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
			JOIN ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
			JOIN ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
			INNER JOIN ClientParticipantType ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId
			--=====================================================================================---
			INNER JOIN         
					ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
					FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY
		WHERE ClientParticipantType.ConsumerFlag = 1),
	Ratings(
			ClientProgramID, UserID, Rating, AuthorName, AuthorPartType, Year, PanelAbbreviation, PanelEndDate)
	AS 
		(SELECT ClientProgram.ClientProgramId,
				ViewPanelUserAssignment.UserID,
				ViewReviewerEvaluation.Rating,
				ViewUserInfo.LastName,
				CASE WHEN ClientParticipantType.ClientParticipantTypeId IS NULL THEN 'Staff' ELSE ClientParticipantType.ParticipantTypeAbbreviation END,
				ViewProgramYear.Year, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.EndDate
		FROM ClientProgram
			JOIN ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
			JOIN ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
			JOIN ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
			JOIN ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
			INNER JOIN ClientParticipantType ReviewerPartType ON ViewPanelUserAssignment.ClientParticipantTypeId = ReviewerPartType.ClientParticipantTypeId
			INNER JOIN ViewReviewerEvaluation ON ViewPanelUserAssignment.PanelUserAssignmentId = ViewReviewerEvaluation.PanelUserAssignmentId
			INNER JOIN ViewUserInfo ON ViewReviewerEvaluation.CreatedBy = ViewUserInfo.UserId
			LEFT JOIN ViewPanelUserAssignment AuthorParticipation ON ViewUserInfo.UserId = AuthorParticipation.UserId AND ViewSessionPanel.SessionPanelId = AuthorParticipation.SessionPanelId
			LEFT JOIN ClientParticipantType ON AuthorParticipation.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
			--=====================================================================================---
			INNER JOIN	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId 
			INNER JOIN	FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY
		WHERE ReviewerPartType.ConsumerFlag = 1)
	-- Main query
	SELECT  Client.ClientID,
			ClientProgram.ClientProgramId,
			ViewUserInfo.UserID,
			ViewUserInfo.userInfoID,
			ViewUserInfo.LastName,
			ViewUserInfo.FirstName,
			State.StateAbbreviation,
			P.Phone,
			ViewUserEmail.Email,
			ViewUserInfo.Institution AS [Nominating Org],
			ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription AS Program,
			STUFF((Select ', ' + Consumer.year
				FROM Consumer
				WHERE Consumer.UserID = ViewUserInfo.UserID AND Consumer.ClientProgramID = ClientProgram.ClientProgramId
				ORDER BY Consumer.year desc, Consumer.PanelEndDate desc, Consumer.PanelAbbreviation
				FOR XML PATH(''), TYPE).value('.', 'varchar(max)'),1,1,'') AS FYs,
			STUFF((Select ', ' + Consumer.PanelAbbreviation
				FROM Consumer
				WHERE Consumer.UserID = ViewUserInfo.UserID AND Consumer.ClientProgramID = ClientProgram.ClientProgramId
				ORDER BY Consumer.year desc, Consumer.PanelEndDate desc, Consumer.PanelAbbreviation
				FOR XML PATH(''), TYPE).value('.', 'varchar(max)'),1,1,'') AS Panels,
			STUFF((Select ', ' + Consumer.ParticipantTypeAbbreviation
				FROM Consumer
				WHERE Consumer.UserID = ViewUserInfo.UserID AND Consumer.ClientProgramID = ClientProgram.ClientProgramId
				ORDER BY Consumer.year desc, Consumer.PanelEndDate desc, Consumer.PanelAbbreviation
				FOR XML PATH(''), TYPE).value('.', 'varchar(max)'),1,1,'') AS Participations,
			STUFF((Select ', ' + CAST(Ratings.Rating AS varchar) + ' - ' + Ratings.AuthorName + ' (' + Ratings.AuthorPartType + ')'
				FROM Ratings
				WHERE Ratings.UserID = ViewUserInfo.UserID AND Ratings.ClientProgramID = ClientProgram.ClientProgramId
				ORDER BY Ratings.year desc, Ratings.PanelEndDate desc, Ratings.PanelAbbreviation
				FOR XML PATH(''), TYPE).value('.', 'varchar(max)'),1,1,'') AS Ratings,
			AccountStatus.AccountStatusName AS [status],
			C.DegreeName,
			M.Major,
			Ethnicity,
			IIF(UCB.Blocked > 0, 'Yes', 'No') as Blocked

	FROM ClientProgram
			JOIN Client ON Client.ClientID =  ClientProgram.ClientId
			--=====================================================================================---
			INNER JOIN	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId 
			--=====================================================================================---
			INNER JOIN (SELECT pua.UserId, py.ClientProgramId
			FROM ViewPanelUserAssignment pua
			INNER JOIN ClientParticipantType ON ClientParticipantType.ClientParticipantTypeId = pua.ClientParticipantTypeId
			INNER JOIN ViewSessionPanel sp ON pua.SessionPanelId = sp.SessionPanelId
			INNER JOIN ViewProgramPanel pp ON sp.SessionPanelId = pp.SessionPanelId
			INNER JOIN ViewProgramYear py ON pp.ProgramYearId = py.ProgramYearId
			INNER JOIN FiscalYearParams ON py.Year = FiscalYearParams.FY
			WHERE ClientParticipantType.ConsumerFlag = 1
			GROUP BY pua.UserId, py.ClientProgramId) Participation ON ClientProgram.ClientProgramId = Participation.ClientProgramId
			--=====================================================================================---
			JOIN ViewUserInfo ON ViewUserInfo.UserID = Participation.UserId
			JOIN ViewUserAddress ON ViewUserAddress.UserInfoID = ViewUserInfo.UserInfoID
				AND ViewUserAddress.PrimaryFlag = 1
			JOIN State ON State.StateId = ViewUserAddress.StateId
			JOIN Country ON Country.CountryId = ViewUserAddress.CountryId
			JOIN ViewUserEmail ON ViewUserEmail.UserInfoID = ViewUserInfo.UserInfoID
								AND ViewUserEmail.PrimaryFlag = 1
			INNER JOIN ViewUserAccountStatus ON ViewUserInfo.UserID = ViewUserAccountStatus.UserId
			INNER JOIN AccountStatus ON ViewUserAccountStatus.AccountStatusId = AccountStatus.AccountStatusId
			LEFT JOIN Ethnicity on Ethnicity.EthnicityId = ViewUserInfo.EthnicityId
			LEFT JOIN (SELECT UserInfoID, Phone = STUFF( (SELECT ', '+ IIF(a2.PrimaryFlag = 1, 'P:' + a2.Phone, 'S:' + a2.Phone)
												FROM ViewUserPhone a2
												WHERE a1.UserInfoID = a2.UserInfoID
												ORDER BY a2.PrimaryFlag desc
												FOR XML PATH(''), TYPE).value('.', 'varchar(max)') ,1,1,'') 
					FROM ViewUserInfo a1
					GROUP BY a1.UserInfoID
			) P ON P.UserInfoID = ViewUserInfo.UserInfoID 
			LEFT JOIN ( SELECT  ViewUserInfo.UserID,
							DegreeName = STUFF((SELECT ', ' + Degree.DegreeName
											FROM Degree
												INNER JOIN ViewUserDegree ON ViewUserDegree.DegreeId = Degree.DegreeId
											WHERE ViewUserInfo.UserInfoID = ViewUserDegree.UserInfoId
											FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
					FROM ViewUserInfo
			) C ON C.userID = ViewUserInfo.userID 
			LEFT JOIN (SELECT UserInfoID, Major = STUFF( (SELECT ', '+ Major 
												FROM ViewUserDegree a2
												WHERE a1.UserInfoID = a2.UserInfoID
												FOR XML PATH(''), TYPE).value('.', 'varchar(max)') ,1,1,'') 
					FROM ViewUserDegree a1
					GROUP BY UserInfoID
			) M ON M.UserInfoID = ViewUserInfo.UserInfoID 
			left JOIN (SELECT ClientProgram.ClientId, ClientProgram.ClientProgramId, ViewUserInfo.UserInfoID, Count(*) AS Blocked
				   FROM UserClientBlock
				   JOIN ClientProgram ON UserClientBlock.ClientId = ClientProgram.ClientId
				   JOIN ViewUserInfo ON ViewUserInfo.UserInfoID = UserClientBlock.UserInfoId
				   WHERE UserClientBlock.DeletedFlag = 0
				   GROUP BY ClientProgram.ClientId, ClientProgram.ClientProgramId, ViewUserInfo.UserInfoID
			) UCB ON UCB.UserInfoID = ViewUserInfo.UserInfoID AND UCB.ClientProgramId = Participation.ClientProgramId 
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportConsummerContactParticipation] TO [NetSqlAzMan_Users]
    AS [dbo];