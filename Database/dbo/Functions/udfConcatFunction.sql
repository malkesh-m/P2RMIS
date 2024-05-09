CREATE FUNCTION [dbo].[udfConcatFunction]
(
  @sessionPanelID varchar(10),
  @condition varchar(10)
)
RETURNS NVARCHAR(MAX)
--WITH SCHEMABINDING 
AS 
BEGIN
  DECLARE @s NVARCHAR(MAX);
 
  SELECT  @s = COALESCE(@s + N'-', N'') + LastName + ', ' + FirstName
    FROM dbo.ViewPanelUserAssignment
	join ViewUserInfo on  ViewUserInfo.UserID = ViewPanelUserAssignment.UserId
	join ClientParticipantType on ClientParticipantType.ClientParticipantTypeId = ViewPanelUserAssignment.ClientParticipantTypeId
	WHERE sessionPanelID = @sessionPanelID
	and ClientParticipantType.ParticipantTypeAbbreviation = @condition

	ORDER BY LastName + ', ' + FirstName
 
  RETURN (@s)
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[udfConcatFunction] TO [NetSqlAzMan_Users]
    AS [dbo];

