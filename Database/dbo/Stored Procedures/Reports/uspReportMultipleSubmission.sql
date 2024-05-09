CREATE procedure [dbo].[uspReportMultipleSubmission]
(@ProgramList varchar(5000),
@FiscalYearList varchar(5000),
@CycleList varchar (5000)
)

AS


BEGIN
SET NOCOUNT ON;

DECLARE @ClientID int;

SELECT @ClientID =  ClientProgram.ClientId
					FROM ViewProgramYear
					JOIN ClientProgram on ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId
					JOIN Client on Client.ClientId = ClientProgram.ClientId
					WHERE ViewProgramYear.Year = @FiscalYearList
					AND ClientProgram.ClientProgramId = @ProgramList;

WITH Programs(ProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
Years(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
Cycles(CY)AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList)) ,
multiple_submission(PIName, Year , Program, ClientProgramID, AppID, GrantID, ApplicationTitle, Award, PIFirstName, PILastName, PIMiddleInitial, PIOrganizationName, ReceiptDeadline, ReceiptCycle,PIName2)
AS

(SELECT * FROM
	-- Get data for all programs same Client same year
	(SELECT      SUBSTRING(dbo.ViewApplicationPersonnel.FirstName, 1, 4)+ ViewApplicationPersonnel.LastName AS PIName,
				 ViewProgramYear.Year , 
				 ClientProgram.ProgramAbbreviation AS Program, 
				 ClientProgram.ClientProgramID,
				 ViewApplication.LogNumber AS AppID, 
				 ViewApplicationInfo.InfoText AS GrantID, 
				 ViewApplication.ApplicationTitle, 
				 ViewClientAwardType.AwardAbbreviation AS Award, 
				 ViewApplicationPersonnel.FirstName AS PIFirstName,
				 ViewApplicationPersonnel.LastName AS PILastName, 
				 ViewApplicationPersonnel.MiddleInitial AS PIMiddleInitial, 
				 ViewApplicationPersonnel.OrganizationName AS PIOrganizationName,
				 ViewProgramMechanism.ReceiptDeadline, 
				 ViewProgramMechanism.ReceiptCycle

	FROM         dbo.ClientProgram INNER JOIN
				 dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
				 dbo.ViewProgramMechanism ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramMechanism.ProgramYearId INNER JOIN
				 dbo.ViewApplication ON dbo.ViewApplication.ProgramMechanismId = dbo.ViewProgramMechanism.ProgramMechanismId INNER JOIN
				 dbo.ViewApplicationInfo ON dbo.ViewApplicationInfo.ApplicationId = ViewApplication.ApplicationId INNER JOIN
				 dbo.ViewClientAwardType ON dbo.ViewClientAwardType.ClientAwardTypeId = dbo.ViewProgramMechanism.ClientAwardTypeId INNER JOIN
				 dbo.ViewApplicationPersonnel ON dbo.ViewApplication.ApplicationId = dbo.ViewApplicationPersonnel.ApplicationId AND ViewApplicationPersonnel.PrimaryFlag = 1
				 INNER JOIN
				 Years on Years.FY = ViewProgramYear.Year
	WHERE		 (ClientProgram.CLIENTID =  @ClientID)
	) t1

	INNER JOIN	

	-- Get PIName from given parameters
	(SELECT     SUBSTRING(dbo.ViewApplicationPersonnel.FirstName, 1, 4)+ ViewApplicationPersonnel.LastName AS PIName
	FROM        dbo.ClientProgram INNER JOIN
				dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
				dbo.ViewProgramMechanism ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramMechanism.ProgramYearId INNER JOIN
				dbo.ViewApplication ON dbo.ViewApplication.ProgramMechanismId = dbo.ViewProgramMechanism.ProgramMechanismId INNER JOIN
				dbo.ViewApplicationInfo ON dbo.ViewApplicationInfo.ApplicationId = ViewApplication.ApplicationId INNER JOIN
				dbo.ViewClientAwardType ON dbo.ViewClientAwardType.ClientAwardTypeId = dbo.ViewProgramMechanism.ClientAwardTypeId INNER JOIN
				dbo.ViewApplicationPersonnel ON dbo.ViewApplication.ApplicationId = dbo.ViewApplicationPersonnel.ApplicationId AND ViewApplicationPersonnel.PrimaryFlag = 1
				INNER JOIN
				Programs on Programs.ProgramID = ClientProgram.ClientProgramId JOIN
				Years on Years.FY = ViewProgramYear.Year JOIN
				Cycles on Cycles.CY = 0 or Cycles.CY = ViewProgramMechanism.ReceiptCycle
	GROUP BY SUBSTRING(dbo.ViewApplicationPersonnel.FirstName, 1, 4)+ ViewApplicationPersonnel.LastName
	) t2 ON (t1.PIName = t2.PIName)

)

-- Return dataset for PIName appear > 1 (same PI submit applications multiple time for same year same client accross program) 
SELECT * 
FROM multiple_submission 
JOIN 
(SELECT PIName3,  COUNT(*) AS NameDup
FROM multiple_submission 
GROUP BY PIName
HAVING count(*) > 1 ) a ON multiple_submission.PIName = a.PIName3
ORDER BY a.PIName3


END


GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportMultipleSubmission] TO [NetSqlAzMan_Users]
    AS [dbo];
