CREATE PROCEDURE [dbo].[uspReportParameterCycle]
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
	SELECT DISTINCT ProgramMechanism.ReceiptCycle
	FROM ViewProgramMechanism ProgramMechanism INNER JOIN
	ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
	ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
	ProgramPanel ON ProgramYear.ProgramYearId = ProgramPanel.ProgramYearId INNER JOIN
	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
	FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
	PanelParams ON PanelParams.PanelId = 0 OR ProgramPanel.SessionPanelId = PanelParams.PanelId
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportParameterCycle] TO [NetSqlAzMan_Users]
    AS [dbo];