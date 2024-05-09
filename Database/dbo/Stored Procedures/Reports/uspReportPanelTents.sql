--Testing_purpose
--[dbo].[uspReportPanelTents] '37','2013','2771'
--[dbo].[uspReportPanelTents] '14','2012','2018'
--[dbo].[uspReportPanelTents] '47','2013','2643'
--[dbo].[uspReportPanelTents] '51','2011','2275'
 --[dbo].[uspReportPanelTents] '51,14','2011,2012','2275,2018'
create procedure [dbo].[uspReportPanelTents](@ProgramList varchar(5000),@FiscalYearList varchar(5000),@PanelList varchar (5000))

as

Begin
SET NOCOUNT ON;

--SET ANSI_NULLS ON
--GO
with Programs(ProgramID)

as

(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),

Years(FY)

as

(SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),

Panel(PA)

as

(SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))


Select distinct ClientProgram.ClientId,
ClientProgram.ClientProgramId,ViewSessionPanel.SessionPanelId,ViewSessionPanel.PanelName,ViewSessionPanel.PanelAbbreviation,
ClientProgram.ProgramAbbreviation,year, 

 ViewUserInfo.FirstName , ViewUserInfo.LastName 

,MilitaryRank.MilitaryRankAbbreviation,
ClientParticipantType.ParticipantTypeName,viewuserinfo.Institution,ViewUserInfo.BadgeName,  
ParticipantTypeAbbreviation,
UserProfile.ProfileTypeId 

from

Clientprogram 

join ViewProgramYear on clientprogram.ClientProgramId= ViewProgramYear.ClientProgramId  

join ViewProgramPanel on viewprogrampanel.ProgramYearId=viewprogramyear.ProgramYearId 

join ViewSessionPanel on ViewSessionPanel.SessionPanelId =ViewProgramPanel.SessionPanelId join


programs on programs.ProgramID= clientprogram.ClientProgramId join

Years on Years.FY =ViewProgramYear.Year join

panel on panel.PA =0 or panel.PA=ViewSessionPanel.SessionPanelId join

viewPanelUserAssignment on  ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId join--

 
 ClientParticipantType on ClientParticipantType.ClientId=ClientProgram.ClientId and viewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
  join

viewuserinfo on ViewUserInfo.UserID =ViewPanelUserAssignment.UserId 
 left outer join MilitaryRank on MilitaryRank.MilitaryRankId=viewuserinfo.MilitaryRankId 
 
 join ViewUserSystemRole on ViewUserSystemRole.UserID =ViewUserInfo.UserID 
 
 join UserProfile on UserProfile.UserInfoId=ViewUserInfo.UserInfoID 
 
 join ProfileType on profiletype.ProfileTypeId =UserProfile.ProfileTypeId
 WHERE (ViewPanelUserAssignment.ParticipationMethodId = 1) 
  
end

GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelTents] TO [NetSqlAzMan_Users]
    AS [dbo];          