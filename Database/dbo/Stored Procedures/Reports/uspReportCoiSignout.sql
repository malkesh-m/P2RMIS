Create procedure [dbo].[uspReportCOIsignout] (@ProgramList varchar(5000),@FiscalYearList varchar(5000),@PanelList varchar (5000))
as
Begin

with Programs(ProgramID)
as
	(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
Years(FY)
as
	(SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
Panel(PA)
as
	(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))

Select 
 ViewApplication.LogNumber as [Application Number],ViewApplicationPersonnel.LastName+','+' '+ ViewApplicationPersonnel.firstname as [Application/PD/PI Name],
ViewapplicationPersonnel.OrganizationName as [Application/PD/PI Organization],viewuserinfo.LastName+','+' '+viewuserinfo.FirstName as [Reviewer Name],MeetingType.MeetingTypeName,
viewprogramyear.Year,clientprogram.ProgramDescription,viewsessionpanel.PanelName,clientprogram.ProgramAbbreviation,viewprogramyear.year,ViewSessionPanel.PanelAbbreviation,clientprogram.clientprogramid,
ViewSessionPanel.SessionPanelId,Assignmenttype.AssignmentTypeId


from

Clientprogram join
ViewProgramYear on clientprogram.ClientProgramId= ViewProgramYear.ClientProgramId join
ViewProgramPanel on viewprogrampanel.ProgramYearId=viewprogramyear.ProgramYearId join
ViewSessionPanel on ViewSessionPanel.SessionPanelId =ViewProgramPanel.SessionPanelId join
programs on programs.ProgramID= clientprogram.ClientProgramId join
Years on Years.FY =ViewProgramYear.Year join
panel on panel.PA =0 or panel.PA=ViewSessionPanel.SessionPanelId join
ViewPanelApplication on viewpanelapplication.SessionPanelId =ViewProgramPanel.SessionPanelId left join

ViewApplicationPersonnel on ViewApplicationPersonnel.ApplicationId=ViewPanelApplication.ApplicationId and viewapplicationpersonnel.PrimaryFlag=1 join
viewapplication on viewapplication.ApplicationId =viewpanelapplication.ApplicationId left join
ViewPanelApplicationReviewerAssignment on  ViewPanelApplicationReviewerAssignment.PanelApplicationId=ViewPanelApplication.PanelApplicationId left join
ClientAssignmentType on ClientAssignmentType.ClientAssignmentTypeId= ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId 
and ClientProgram.clientid=ClientAssignmentType.ClientId join
ViewMeetingSession ON ViewSessionPanel.MeetingSessionId = ViewMeetingSession.MeetingSessionId INNER JOIN
ViewClientMeeting ON ViewMeetingSession.ClientMeetingId = ViewClientMeeting.ClientMeetingId INNER JOIN
MeetingType ON ViewClientMeeting.MeetingTypeId = MeetingType.MeetingTypeId left join

AssignmentType on AssignmentType.AssignmentTypeId =ClientAssignmentType.AssignmentTypeId left join
viewPanelUserAssignment on ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId left join
viewuserinfo on ViewUserInfo.UserID =ViewPanelUserAssignment.UserId





order by viewapplication.lognumber asc ,viewuserinfo.LastName+','+' '+viewuserinfo.FirstName desc
end


go




GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportCoiSignout] TO [NetSqlAzMan_Users]
    AS [dbo];