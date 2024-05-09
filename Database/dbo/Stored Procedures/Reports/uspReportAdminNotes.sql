-- =============================================
-- Author:		Alberto Catuche
-- Create date: 2/5/2016
-- Description:	Used as source for report Administrative Notes Report
-- ============================================= 

CREATE PROCEDURE [dbo].[uspReportAdminNotes] 
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
--	@PanelList varchar(4000)
	@CycleList varchar(4000)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
--	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList))
	CycleParams(Cycle) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList))
SELECT     ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ViewSessionPanel.PanelAbbreviation, ViewClientAwardType.AwardAbbreviation, 
                      ViewApplication.LogNumber, ViewApplicationBudget.Comments,ViewSessionPanel.SessionPanelId
FROM         ViewApplication INNER JOIN
                      ViewProgramMechanism INNER JOIN
                      ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
                      ViewProgramYear ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
                      ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId ON 
                      ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                      ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ViewPanelApplication ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId AND 
                      ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId INNER JOIN
		              ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
					  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
--		              PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
					  CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle
WHERE LEN(LTRIM(ViewApplicationBudget.Comments)) > 0
ORDER BY ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ViewSessionPanel.PanelAbbreviation, ViewClientAwardType.AwardAbbreviation, 
                      ViewApplication.LogNumber
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportAdminNotes] TO [NetSqlAzMan_Users]
    AS [dbo];