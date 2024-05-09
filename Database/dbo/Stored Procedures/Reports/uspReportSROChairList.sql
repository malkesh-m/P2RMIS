Create PROCEDURE [dbo].[uspReportSROChairList] 
	-- Add the parameters for the stored procedure here
		-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

	SELECT DISTINCT 
                      ClientProgram.ClientProgramId, ViewProgramYear.year, ViewSessionPanel.SessionPanelId, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
                      ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, MilitaryRank.MilitaryRankAbbreviation, COUNT(DISTINCT dbo.ReferralMappingData.ApplicationId)  AS count
FROM         dbo.ClientProgram INNER JOIN
                      dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
                      dbo.ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      dbo.ClientParticipantType ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId INNER JOIN
                      dbo.ViewUserInfo ON ViewUserInfo.userID = ViewPanelUserAssignment.UserId LEFT OUTER JOIN
                      dbo.ReferralMappingData ON dbo.ViewSessionPanel.SessionPanelId = dbo.ReferralMappingData.SessionPanelId LEFT OUTER JOIN
                      dbo.MilitaryRank ON MilitaryRank.MilitaryRankId = ViewUserInfo.MilitaryRankId INNER JOIN   
  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN   
  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN   
  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId 


WHERE   (ClientParticipantType.LegacyPartTypeId = 'SRA') OR
      (ClientParticipantType.ChairpersonFlag = 1) AND 
      (ViewSessionPanel.PanelAbbreviation IS NOT NULL)
  GROUP BY ClientProgram.ClientProgramId, ViewProgramYear.year, ViewSessionPanel.SessionPanelId, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
                      ViewSessionPanel.StartDate, ViewSessionPanel.EndDate, MilitaryRank.MilitaryRankAbbreviation

END​

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSROChairList] TO [NetSqlAzMan_Users]
    AS [dbo];