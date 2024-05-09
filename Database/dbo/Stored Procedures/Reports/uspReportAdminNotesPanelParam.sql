
CREATE PROCEDURE [dbo].[uspReportAdminNotesPanelParam] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@CycleList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList))
SELECT  DISTINCT   ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.SessionPanelId
FROM         ViewSessionPanel 
			 INNER JOIN ViewProgramPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId
			 INNER JOIN ViewProgramYear ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId
			 INNER JOIN ClientProgram ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId
			 INNER JOIN ViewProgramMechanism ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId

			 INNER JOIN

			 ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
			 FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
		     CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle


ORDER BY ViewSessionPanel.PanelAbbreviation, ViewSessionPanel.SessionPanelId
END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportAdminNotesPanelParam] TO [NetSqlAzMan_Users]
    AS [dbo];