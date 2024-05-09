--+++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
-- Athor: Dinh, Ngan
-- Date : 2/8/2019
-- Purpose: For Nominator Information report - PRMIS-25719

-- exec [uspReportNominatorInformation] 47, 2013
--+++++++++++++++++++++++++++++++++++++++++++++++++++++++++--


CREATE PROCEDURE [dbo].[uspReportNominatorInformation]
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000)--, @Can be removed late
	--@List

AS
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	WITH ProgramParams(ClientProgramId) 	AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY) 	AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList))

SELECT DISTINCT   ClientProgram.ClientProgramId, ClientProgram.ProgramAbbreviation, ClientProgram.ProgramDescription , ViewProgramYear.Year, 
	Institution as [Nominating Organization],
	ProfessionalAffiliationId,
	null as [Nominator Last Name],
	null as [Nominator First Name],
	null as [Nominator Email]
FROM ViewUserInfo 
	INNER JOIN ViewPanelUserAssignment            ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID
	INNER JOIN ViewProgramPanel					  ON ViewProgramPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId
	INNER JOIN ViewProgramYear					  ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId 
	INNER JOIN ClientProgram					  ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId 
	INNER JOIN ProgramParams					  ON ProgramParams.ClientProgramId = 0 OR ClientProgram.ClientProgramId = ProgramParams.ClientProgramId
	INNER JOIN FiscalYearParams					  ON FiscalYearParams.FY = ViewProgramYear.Year

WHERE ProfessionalAffiliationId = 2

ORDER BY ClientProgram.ClientProgramId, ViewProgramYear.Year, [Nominating Organization], [Nominator Last Name]

END


go



GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportNominatorInformation] TO [NetSqlAzMan_Users]
    AS [dbo];