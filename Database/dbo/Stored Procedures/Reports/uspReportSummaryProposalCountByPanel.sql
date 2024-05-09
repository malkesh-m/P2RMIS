

-- =============================================
-- Author:		Alberto Catuche
-- Create date: 1/17/2016
-- Description:	Used as source for report Panel Summary Proposal Count
-- =============================================

CREATE PROCEDURE [dbo].[uspReportSummaryProposalCountByPanel] 
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
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList))
	SELECT 
		dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramAbbreviation, dbo.ClientProgram.ProgramDescription, dbo.ViewSessionPanel.PanelName, 
		dbo.ViewSessionPanel.PanelAbbreviation, dbo.ViewClientAwardType.AwardDescription, dbo.ViewProgramMechanism.ReceiptCycle, COUNT(dbo.ViewApplication.ApplicationId) AS Total_Apps
	FROM 
		dbo.ViewApplication INNER JOIN
		dbo.ViewProgramMechanism INNER JOIN
		dbo.ViewClientAwardType ON dbo.ViewProgramMechanism.ClientAwardTypeId = dbo.ViewClientAwardType.ClientAwardTypeId INNER JOIN
		dbo.ViewProgramYear ON dbo.ViewProgramMechanism.ProgramYearId = dbo.ViewProgramYear.ProgramYearId INNER JOIN
		dbo.ClientProgram ON dbo.ViewProgramYear.ClientProgramId = dbo.ClientProgram.ClientProgramId ON 
		dbo.ViewApplication.ProgramMechanismId = dbo.ViewProgramMechanism.ProgramMechanismId INNER JOIN
		dbo.ViewProgramPanel ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramPanel.ProgramYearId INNER JOIN
		dbo.ViewSessionPanel ON dbo.ViewProgramPanel.SessionPanelId = dbo.ViewSessionPanel.SessionPanelId INNER JOIN
		dbo.ViewPanelApplication ON dbo.ViewApplication.ApplicationId = dbo.ViewPanelApplication.ApplicationId AND 
		dbo.ViewSessionPanel.SessionPanelId = dbo.ViewPanelApplication.SessionPanelId INNER JOIN
		ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
		FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
		PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId
	GROUP BY 
		dbo.ViewProgramMechanism.ProgramMechanismId, dbo.ViewProgramMechanism.ProgramYearId, dbo.ViewProgramMechanism.ClientAwardTypeId, 
		dbo.ViewProgramMechanism.ReceiptCycle, dbo.ViewProgramMechanism.LegacyAtmId, dbo.ViewProgramMechanism.ReceiptDeadline, dbo.ViewProgramMechanism.AbstractFormat, 
		dbo.ViewClientAwardType.AwardAbbreviation, dbo.ViewClientAwardType.AwardDescription, dbo.ViewProgramYear.Year, dbo.ClientProgram.ProgramAbbreviation, 
		dbo.ClientProgram.ProgramDescription, dbo.ViewSessionPanel.PanelAbbreviation, dbo.ViewSessionPanel.PanelName
	ORDER BY 
		dbo.ViewSessionPanel.PanelAbbreviation, dbo.ViewClientAwardType.AwardDescription
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSummaryProposalCountByPanel] TO [NetSqlAzMan_Users]
    AS [dbo];