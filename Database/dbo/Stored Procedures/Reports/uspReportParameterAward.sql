-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 6/9/2020
-- Description: Storeprocedure to Create Report Award Parameter
-- =============================================

CREATE PROCEDURE [dbo].[uspReportParameterAward]
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
SELECT DISTINCT ClientAwardType.ClientAwardTypeId, ClientAwardType.AwardDescription
FROM            ViewProgramMechanism AS ProgramMechanism INNER JOIN
                         ViewProgramYear AS ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
                         ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
                         ProgramPanel ON ProgramYear.ProgramYearId = ProgramPanel.ProgramYearId INNER JOIN
                         ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId AND ClientProgram.ClientId = ClientAwardType.ClientId INNER JOIN
                         ViewSessionPanel ON ProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                         ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                         ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId AND ProgramMechanism.ProgramMechanismId = ViewApplication.ProgramMechanismId
 INNER JOIN
	ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
	FiscalYearParams ON ProgramYear.Year = FiscalYearParams.FY INNER JOIN
	PanelParams ON PanelParams.PanelId = 0 OR ProgramPanel.SessionPanelId = PanelParams.PanelId
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportParameterAward] TO [NetSqlAzMan_Users]
    AS [dbo];