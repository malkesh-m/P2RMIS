-- =============================================
-- Author:		Craig Henson
-- Create date: 6/27/2016
-- Description:	Gets a summary statement display label for a given set of information
-- =============================================
CREATE FUNCTION [dbo].udfGetSummaryReviewerDescription
(	
	@ProgramMechanismId int,
	@ClientParticipantTypeId int, 
	@ClientRoleId int,
	@AssignmentOrder int
)
RETURNS TABLE 
AS
RETURN 
(
	--This allows the table to work as a wildcard, with order of precedence going from left to right in table columns
	SELECT TOP(1) CustomOrder, DisplayName
	FROM  ViewSummaryReviewerDescription
	WHERE	(ProgramMechanismId = @ProgramMechanismId OR ProgramMechanismId IS NULL) AND
			(ClientParticipantTypeId = @ClientParticipantTypeId OR ClientParticipantTypeId IS NULL) AND
			(ClientRoleId = @ClientRoleId OR ClientRoleId IS NULL) AND
			(AssignmentOrder = @AssignmentOrder OR AssignmentOrder IS NULL)
	ORDER BY ProgramMechanismId desc, ClientParticipantTypeId desc, ClientRoleId desc, AssignmentOrder desc
)
GO
GRANT SELECT
    ON OBJECT::[dbo].[udfGetSummaryReviewerDescription] TO [NetSqlAzMan_Users]
    AS [dbo];