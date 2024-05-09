CREATE PROCEDURE [dbo].[uspReportAllReviewsParam_ReviewerList]
	-- Add the parameters for the stored procedure here
	@ProgramList varchar(4000),
	@FiscalYearList varchar(4000),
	@PanelList varchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
	ProgramParams(ClientProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@ProgramList)), 
	FiscalYearParams(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)), 
	PanelParams(PanelId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

SELECT DISTINCT
ClientParticipantType.ParticipantTypeAbbreviation
FROM	dbo.ClientProgram INNER JOIN
dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
dbo.ViewProgramPanel ON ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId INNER JOIN
dbo.ViewSessionPanel ON ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId INNER JOIN
dbo.PanelUserAssignment ON dbo.PanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
dbo.ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId

 INNER JOIN
                        
						 ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                         FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
                         PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId

WHERE	ClientParticipantType.ParticipantTypeAbbreviation in ('SRO','RTA') or ClientParticipantType.ReviewerFlag = 1

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportAllReviewsParam_ReviewerList] TO [NetSqlAzMan_Users]
    AS [dbo];