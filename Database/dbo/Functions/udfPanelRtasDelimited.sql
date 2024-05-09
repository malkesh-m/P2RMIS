CREATE FUNCTION [dbo].[udfPanelRtasDelimited]
(
	@SessionPanelId int
)
RETURNS TABLE
AS
RETURN
SELECT Stuff(
	(SELECT ', ' + ViewUserInfo.FirstName + ' ' + ViewUserInfo.LastName
	FROM ViewSessionPanel INNER JOIN
	ViewPanelUserAssignment ON ViewSessionPanel.SessionPanelId = ViewPanelUserAssignment.SessionPanelId INNER JOIN
	ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
	ViewUserInfo ON ViewPanelUserAssignment.UserId = ViewUserInfo.UserID
	WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId AND ClientParticipantType.LegacyPartTypeId = 'RTA' 
	FOR XML PATH(''),TYPE).value('text()[1]','varchar(max)'),1,2,'') AS PanelRtas
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfPanelRtasDelimited] TO [NetSqlAzMan_Users]
    AS [dbo];
