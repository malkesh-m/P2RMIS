UPDATE PanelUserAssignment SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE PanelUserAssignment.DeletedFlag = 0 AND PanelUserAssignmentId IN (
SELECT PanelUserAssignmentId
FROM ViewPanelUserAssignment LEFT OUTER JOIN
ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID LEFT OUTER JOIN
ViewSessionPanel ON ViewPanelUserAssignment.SessionPanelId = ViewSessionPanel.SessionPanelId
WHERE ViewUser.UserID IS NULL OR ViewSessionPanel.SessionPanelId IS NULL)