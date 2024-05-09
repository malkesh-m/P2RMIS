CREATE procedure [dbo].[uspReportReceiptCycle](@ProgramList varchar(5000),@FiscalYearList varchar(5000),@PanelList varchar (5000))

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

Select distinct ViewProgramMechanism.ReceiptCycle



from

Clientprogram join

viewClientAwardType on ViewClientAwardType.ClientId=clientprogram.ClientId join ViewProgramYear on clientprogram.ClientProgramId= ViewProgramYear.ClientProgramId join

ViewProgramPanel on viewprogrampanel.ProgramYearId=viewprogramyear.ProgramYearId join

ViewSessionPanel on ViewSessionPanel.SessionPanelId =ViewProgramPanel.SessionPanelId join

programs on programs.ProgramID= clientprogram.ClientProgramId join

Years on Years.FY =ViewProgramYear.Year join

panel on panel.PA =0 or panel.PA=ViewSessionPanel.SessionPanelId join

ViewPanelApplication on viewpanelapplication.SessionPanelId =ViewProgramPanel.SessionPanelId left join--

ViewApplicationPersonnel on ViewApplicationPersonnel.ApplicationId=ViewPanelApplication.ApplicationId and viewapplicationpersonnel.PrimaryFlag=1 join

viewapplication on viewapplication.ApplicationId =viewpanelapplication.ApplicationId left join

ViewPanelApplicationReviewerAssignment on ViewPanelApplicationReviewerAssignment.PanelApplicationId=ViewPanelApplication.PanelApplicationId left join--

viewPanelUserAssignment on ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId left join--

viewuserinfo on ViewUserInfo.UserID =ViewPanelUserAssignment.UserId join ApplicationCompliance on ApplicationCompliance.ApplicationId = ViewApplication.ApplicationId join

ComplianceStatus on ComplianceStatus.ComplianceStatusId =ApplicationCompliance.ComplianceStatusId join ViewProgramMechanism on

ViewProgramMechanism.ClientAwardTypeId=ViewClientAwardType.ClientAwardTypeId
and  ViewProgramMechanism.ProgramYearId=ViewProgramYear.ProgramYearId 
 and ViewProgramMechanism.ProgramMechanismId = viewapplication.ProgramMechanismId


order by ReceiptCycle

end