-- Reset signature data if incomplete
Update A set A.DocumentFile=null, A.dateCompleted=null, A.signedby=null, A.datesigned=null
from PanelUserRegistrationDocument A where A.DeletedFlag=0 and A.DateSigned is not null
and exists (select top 1 * from PanelUserRegistrationDocument B where A.PanelUserRegistrationId=B.PanelUserRegistrationId
and B.DeletedFlag=0 and B.DateSigned is null)
and A.PanelUserRegistrationId in (select PanelUserRegistrationId from PanelUserRegistration where deletedflag=0 and
PanelUserAssignmentId in (select PanelUserAssignmentId from PanelUserAssignment where deletedflag=0 and
SessionPanelId in (select SessionPanelId from SessionPanel where StartDate>=dbo.GetP2rmisDateTime())))
