CREATE PROCEDURE [dbo].[uspReportParameterPanel]
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
		Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
		Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
		CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList))

		SELECT DISTINCT 
                      CASE WHEN dbo.ViewPanelApplication.SessionPanelId IS NULL 
                      THEN 0 ELSE dbo.ViewPanelApplication.SessionPanelId END AS SessionPanelId, (CASE WHEN dbo.ViewSessionPanel.PanelAbbreviation IS NULL 
                      THEN 'No Assigned Panel' ELSE ViewSessionPanel.PanelAbbreviation END) AS Panel
		FROM         dbo.ViewSessionPanel INNER JOIN
							  dbo.ViewPanelApplication ON dbo.ViewSessionPanel.SessionPanelId = dbo.ViewPanelApplication.SessionPanelId RIGHT OUTER JOIN
							  dbo.ClientProgram INNER JOIN
							  dbo.ViewProgramYear ON dbo.ClientProgram.ClientProgramId = dbo.ViewProgramYear.ClientProgramId INNER JOIN
							  dbo.ViewProgramMechanism AS ViewProgramMechanism ON dbo.ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId INNER JOIN
							  dbo.ViewApplication ON ViewProgramMechanism.ProgramMechanismId = dbo.ViewApplication.ProgramMechanismId ON 
							  dbo.ViewPanelApplication.ApplicationId = dbo.ViewApplication.ApplicationId INNER JOIN
							  CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle
		Where dbo.ClientProgram.ClientProgramId IN (Select ProgramID FROM Programs)  and (ViewProgramYear.Year IN (Select FY FROM Years)) 
		ORDER BY Panel
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportParameterPanel] TO [NetSqlAzMan_Users]
    AS [dbo];