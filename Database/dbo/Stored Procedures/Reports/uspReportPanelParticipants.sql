CREATE PROCEDURE [dbo].[uspReportPanelParticipants]
	

@ProgramList varchar(4000)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	WITH 
	ProgramParams(ClientProgramId) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList))

                        SELECT DISTINCT ClientParticipantType.ParticipantTypeAbbreviation, ClientParticipantType.ClientParticipantTypeId
FROM         dbo.ClientParticipantType INNER JOIN
                      dbo.ClientProgram ON ClientParticipantType.ClientId = ClientProgram.ClientId INNER JOIN
		              ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId 

WHERE     (ClientParticipantType.ActiveFlag = 1) AND (ClientParticipantType.ParticipantScope = 'Panel') AND (ClientParticipantType.ReviewerFlag = 1) OR
                      (ClientParticipantType.ParticipantTypeAbbreviation = 'RTA') OR
                      (ClientParticipantType.ParticipantTypeAbbreviation = 'SRO') OR
					  (ClientParticipantType.ParticipantTypeAbbreviation = 'CHCPRIT')


END		

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportPanelParticipants] TO [NetSqlAzMan_Users]
    AS [dbo];	