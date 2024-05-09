CREATE PROCEDURE [dbo].[uspReportDistributionOfScoresDel]
--Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@CycleList varchar(4000)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH ProgramParams(ClientProgramId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	CycleParams(Cycle)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))
	

SELECT     CAST(ViewApplicationRevStdev.AvgScore AS decimal(18, 1)) AS avg, ViewPanelApplication.SessionPanelId, COUNT(ViewApplicationRevStdev.ApplicationID) 
                      AS Countof_Applications, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, 
                      ClientProgram.ProgramDescription
FROM         dbo.ViewApplicationRevStdev INNER JOIN
                      dbo.ViewPanelApplication ON ViewApplicationRevStdev.ApplicationID = ViewPanelApplication.ApplicationId AND 
                      dbo.ViewApplicationRevStdev.ApplicationID = dbo.ViewPanelApplication.ApplicationId INNER JOIN
                      dbo.ViewProgramPanel ON ViewPanelApplication.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
                      dbo.ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      dbo.ViewProgramYear ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      dbo.ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      dbo.ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                      dbo.ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
                      dbo.ViewApplicationReviewStatus ON dbo.ViewPanelApplication.PanelApplicationId = dbo.ViewApplicationReviewStatus.PanelApplicationId
                      INNER JOIN ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                      FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
					  CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle
Where  (dbo.ViewApplicationReviewStatus.ReviewStatusId in (2,6) )
GROUP BY CAST(ViewApplicationRevStdev.AvgScore AS decimal(18, 1)), ViewPanelApplication.SessionPanelId, ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.PanelName, 
                      ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription


END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportDistributionOfScoresDel] TO [NetSqlAzMan_Users]
    AS [dbo];
