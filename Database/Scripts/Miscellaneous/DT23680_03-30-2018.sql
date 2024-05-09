-- Do not run again in production
DECLARE @SessionPanelId INT;
SET @SessionPanelId = 4305;

UPDATE ApplicationWorkflow 
SET DeletedFlag=1, DeletedBy=10, DeletedDate=dbo.GetP2rmisDateTime() where ApplicationWorkflowId in (
select ApplicationWorkflowId from ApplicationWorkflow A where A.PanelUserAssignmentId in (
	select PanelUserAssignmentId from ApplicationWorkflow where PanelUserAssignmentId in 
	(select PanelUserAssignmentId from PanelUserAssignment where SessionpanelId=@SessionPanelId)
	group by ApplicationStageId, PanelUserAssignmentId having count(ApplicationWorkflowId)>1) and ApplicationWorkflowName='Pre-Meeting Critique'
	and exists (select top 1 * from ApplicationWorkflow B where A.PanelUserAssignmentId = B.PanelUserAssignmentId
		and A.ApplicationStageId = B.ApplicationStageId and B.ApplicationWorkflowName<>'Pre-Meeting Critique'))