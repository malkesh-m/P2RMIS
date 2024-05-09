CREATE FUNCTION [dbo].[udfPanelSrosDelimited]
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
	WHERE ViewSessionPanel.SessionPanelId = @SessionPanelId AND ClientParticipantType.LegacyPartTypeId = 'SRA' 
	FOR XML PATH(''),TYPE).value('text()[1]','varchar(max)'),1,2,'') AS PanelSros
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfPanelSrosDelimited] TO [NetSqlAzMan_Users]
    AS [dbo];
