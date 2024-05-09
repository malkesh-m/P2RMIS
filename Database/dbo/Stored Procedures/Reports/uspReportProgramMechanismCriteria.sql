/****** Object:  StoredProcedure [dbo].[uspReportProgramMechanismCriteria]    Script Date: 1/5/2018 3:18:46 PM ******/
CREATE PROCEDURE [dbo].[uspReportProgramMechanismCriteria]

@ProgramList varchar(4000),
@FiscalYearList varchar(4000)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;
-- Insert statements for procedure here
WITH 
ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList))
SELECT DISTINCT dbo.ViewClientElement.ElementIdentifier, dbo.ViewClientElement.ElementAbbreviation, dbo.ViewClientElement.ElementDescription
FROM dbo.ViewProgramMechanism INNER JOIN
dbo.ViewProgramYear INNER JOIN
dbo.ClientProgram ON dbo.ViewProgramYear.ClientProgramId = dbo.ClientProgram.ClientProgramId ON 
dbo.ViewProgramMechanism.ProgramYearId = dbo.ViewProgramYear.ProgramYearId INNER JOIN
dbo.ClientAwardType ON dbo.ViewProgramMechanism.ClientAwardTypeId = dbo.ClientAwardType.ClientAwardTypeId INNER JOIN
dbo.ViewMechanismTemplate ON dbo.ViewProgramMechanism.ProgramMechanismId = dbo.ViewMechanismTemplate.ProgramMechanismId INNER JOIN
dbo.ViewMechanismTemplateElement ON 
dbo.ViewMechanismTemplate.MechanismTemplateId = dbo.ViewMechanismTemplateElement.MechanismTemplateId INNER JOIN
dbo.ViewClientElement ON dbo.ViewMechanismTemplateElement.ClientElementId = dbo.ViewClientElement.ClientElementId AND 
dbo.ClientProgram.ClientId = dbo.ViewClientElement.ClientId INNER JOIN
ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
FiscalYearParams ON FiscalYearParams.FY = 0 OR ViewProgramYear.Year = FiscalYearParams.FY
WHERE (dbo.ViewMechanismTemplate.ReviewStageId = 1) 
END 

GO

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportProgramMechanismCriteria] TO [NetSqlAzMan_Users]
    AS [dbo];


