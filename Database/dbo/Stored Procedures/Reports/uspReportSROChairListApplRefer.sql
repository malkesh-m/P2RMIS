Create PROCEDURE [dbo].[uspReportSROChairListApplRefer] 
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


	SELECT DISTINCT 	  ClientProgram.ClientProgramId, 
						  ViewProgramYear.year, 
						  ViewSessionPanel.SessionPanelId,
						  COUNT(DISTINCT ApplicationId) AppRefCount						 						  

	FROM                  ClientProgram
	INNER JOIN            ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId	
	INNER JOIN            ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId 	
	INNER JOIN            ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId	
	LEFT OUTER JOIN		  ReferralMappingData ON ViewSessionPanel.SessionPanelId = ReferralMappingData.SessionPanelId
	INNER JOIN            ViewPanelUserAssignment ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
	INNer JOIN            ClientParticipantType ON ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId
	INNER JOIN			  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId 
	INNER JOIN			  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY 
	INNER JOIN			  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId 

	WHERE			      (ClientParticipantType.LegacyPartTypeId = 'SRA') OR
					      (ClientParticipantType.ChairpersonFlag = 1) AND 
					      (ViewSessionPanel.PanelAbbreviation IS NOT NULL)
	
	GROUP BY 			  ViewSessionPanel.SessionPanelId,
						  ClientProgram.ClientProgramId, 
						  ViewProgramYear.year
    

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportSROChairListApplRefer] TO [NetSqlAzMan_Users]
    AS [dbo];