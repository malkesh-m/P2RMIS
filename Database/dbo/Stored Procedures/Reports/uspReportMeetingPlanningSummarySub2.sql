CREATE PROCEDURE [dbo].[uspReportMeetingPlanningSummarySub2]
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
		ViewSessionPanel.SessionPanelId panelID,
		ViewSessionPanel.PanelAbbreviation [Panel]
		, ClientParticipantType.ChairpersonFlag
		, ClientParticipantType.ParticipantTypeAbbreviation
		, ClientParticipantType.ReviewerFlag
		, ViewPanelUserAssignment.RestrictedAssignedFlag
		, ViewPanelUserAssignment.ParticipationMethodId
		, ClientParticipantType.ConsumerFlag
		, ViewUserInfo.LastName + ', ' + ViewUserInfo.FirstName FullName
		, CASE WHEN (NOT (ViewUserInfo.LastName IS NULL)) THEN 1 ELSE 0 END AS  [Total Participant Count]
		, CASE WHEN (ViewPanelUserAssignment.ParticipationMethodId = 1) THEN 1 ELSE 0 END AS  [Total Onsite Participant Count]
		, CASE WHEN (ClientParticipantType.ReviewerFlag = 1 and ViewPanelUserAssignment.ParticipationMethodId = 1) THEN 1 ELSE 0 END AS  [Total Onsite Reviewer]
		, CASE WHEN (ClientParticipantType.ReviewerFlag = 1 and ViewPanelUserAssignment.ParticipationMethodId = 2) THEN 1 ELSE 0 END AS  [Total Remote Reviewer]
		, CASE WHEN (ClientParticipantType.ReviewerFlag = 1 and ViewPanelUserAssignment.RestrictedAssignedFlag = 1) THEN 1 ELSE 0 END AS  [Total Onsite Adhoc Reviewer]
		, CASE WHEN (ClientParticipantType.ConsumerFlag = 1 and ViewPanelUserAssignment.ParticipationMethodId = 1) THEN 1 ELSE 0 END AS  [Total Onsite Consumers/Advocates]
		, A.Chairperson, A.SRO, A.RTA

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

				left join 
					(SELECT ViewPanelUserAssignment.SessionPanelID, 
							Chairperson = dbo.ConcatFunction(ViewPanelUserAssignment.SessionPanelID,'CH') ,
							SRO = dbo.udfConcatFunction(ViewPanelUserAssignment.SessionPanelID,'SRO') ,
							RTA = dbo.udfConcatFunction(ViewPanelUserAssignment.SessionPanelID,'RTA') 
					from ViewPanelUserAssignment 
					GROUP BY ViewPanelUserAssignment.SessionPanelID) A on A.SessionPanelId = PanelParams.PanelId or PanelParams.PanelId = 0
	

end


GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportMeetingPlanningSummarySub2] TO [NetSqlAzMan_Users]
    AS [dbo];