CREATE PROCEDURE [dbo].[uspReportDistributionofScoresAwardDel]
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
	

SELECT     ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewClientAwardType.AwardAbbreviation, 
                      ViewClientAwardType.AwardDescription, ViewProgramMechanism.ReceiptCycle, COUNT(ViewApplicationRevStdev.ApplicationID) AS CountOfApplications, 
                      CAST(ViewApplicationRevStdev.AvgScore AS decimal(18, 1)) AS avg
FROM         dbo.ClientProgram INNER JOIN
                      dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      dbo.ViewProgramMechanism ON ViewProgramYear.ProgramYearId = ViewProgramMechanism.ProgramYearId INNER JOIN
                      dbo.ViewClientAwardType ON ClientProgram.ClientId = ViewClientAwardType.ClientId AND 
                      ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
                      dbo.ViewApplication ON ViewProgramMechanism.ProgramMechanismId = ViewApplication.ProgramMechanismId INNER JOIN
                      dbo.ViewApplicationRevStdev ON ViewApplication.ApplicationId = ViewApplicationRevStdev.ApplicationID INNER JOIN
                      dbo.ViewPanelApplication ON dbo.ViewApplication.ApplicationId = dbo.ViewPanelApplication.ApplicationId INNER JOIN
                      dbo.ViewApplicationReviewStatus ON dbo.ViewPanelApplication.PanelApplicationId = dbo.ViewApplicationReviewStatus.PanelApplicationId
				      INNER JOIN ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN 
                      FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN 
					  CycleParams ON CycleParams.Cycle = 0 OR ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle
Where (dbo.ViewApplicationReviewStatus.ReviewStatusId  in (2,6))
GROUP BY ViewProgramYear.Year, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription, ViewClientAwardType.AwardAbbreviation, 
                      ViewClientAwardType.AwardDescription, ViewProgramMechanism.ReceiptCycle, CAST(ViewApplicationRevStdev.AvgScore AS decimal(18, 1))


END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportDistributionOfScoresAwardDel] TO [NetSqlAzMan_Users]
    AS [dbo];
