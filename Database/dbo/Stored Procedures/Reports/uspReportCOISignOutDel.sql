CREATE PROCEDURE [dbo].[uspReportCOISignOutDel]
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
	WITH ProgramParams(ClientProgramId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
	FiscalYearParams(FY)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
	PanelParams(PanelId)
		AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT     ViewProgramYear.Year, ViewSessionPanel.PanelAbbreviation, ClientProgram.ProgramAbbreviation, ViewPanelApplication.ApplicationId, ViewApplication.LogNumber, 
                      ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId, ClientAssignmentType.AssignmentAbbreviation, ClientProgram.ProgramDescription, 
                      ViewSessionPanel.PanelName, ViewClientMeeting.MeetingTypeId, MeetingType.MeetingTypeName, 
                      ViewApplicationPersonnel.LastName + ',' + ' ' + ViewApplicationPersonnel.FirstName AS Name, 
                      ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation, ViewApplicationPersonnel.OrganizationName, 
                      ViewUserInfo.LastName + ',' + ' ' + ViewUserInfo.FirstName AS ReviewerName, Client.ClientAbrv, Client.ClientID,  Client.ClientDesc
                      ,ViewSessionPanel.SessionPanelId
FROM         ViewProgramYear INNER JOIN
                      ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
                      ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId 
					LEFT OUTER JOIN
                      ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId 
					 LEFT OUTER JOIN
                      ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId  
					  INNER JOIN AssignmentType ON AssignmentType.AssignmentTypeId = ClientAssignmentType.AssignmentTypeId
                     --   and AssignmentType.AssignmentTypeId = 8 
					INNER JOIN
                      ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
                      ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
                      MeetingType ON ViewClientMeeting.MeetingTypeId = MeetingType.MeetingTypeId INNER JOIN
                      ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId INNER JOIN
                      ClientApplicationPersonnelType ON 
                      ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId AND 
                      ClientProgram.ClientId = ClientApplicationPersonnelType.ClientId LEFT  OUTER JOIN
                      ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId AND 
                      ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId LEFT OUTER JOIN
                      ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID INNER JOIN
                      Client ON ClientProgram.ClientId = Client.ClientID 
	--	INNER JOIN ApplicationPersonnel ON ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId = ApplicationPersonnel.ClientApplicationPersonnelTypeId



 INNER JOIN
                        
						 ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
                         FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
                         PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId

--WHERE    (ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation = 'PI')
WHERE (ViewApplicationPersonnel.PrimaryFlag = 1) 
ORDER BY ViewApplication.LogNumber

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCOISignOutDel] TO [NetSqlAzMan_Users]
    AS [dbo];
