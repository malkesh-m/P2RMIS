create PROCEDURE [dbo].[uspReportMeetingPlanningSummarySub]
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
	Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
			Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
			PanelParams(PanelID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@PanelList))

	select distinct 

	    clientprogram.ClientProgramId programID,
		clientprogram.ProgramAbbreviation as [Program],
		ViewProgramYear.Year as [FY],
		ViewSessionPanel.SessionPanelId panelID
		, ClientParticipantType.ChairpersonFlag
		, ClientParticipantType.ParticipantTypeAbbreviation
	    , ViewUserInfo.LastName 
		,  ViewUserInfo.FirstName 
		

	from
		clientprogram 
		join ViewProgramYear on ViewProgramYear.ClientProgramId = clientprogram.ClientProgramId
		join ViewProgramPanel on ViewProgramPanel.ProgramYearId = ViewProgramYear.ProgramYearId
		join ViewSessionPanel on ViewSessionPanel.SessionPanelId = ViewProgramPanel.SessionPanelId
			INNER JOIN ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId
				INNER JOIN ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
			join ViewUserInfo on ViewUserInfo.UserID = ViewPanelUserAssignment.UserId

	INNER JOIN
					programs on programs.ProgramID= clientprogram.ClientProgramId join
					Years on Years.FY =ViewProgramYear.Year join
					PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId

end

go

GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportMeetingPlanningSummarySub] TO [NetSqlAzMan_Users]
    AS [dbo];