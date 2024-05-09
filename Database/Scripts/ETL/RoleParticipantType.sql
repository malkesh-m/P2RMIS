--SRO
INSERT INTO [dbo].[RoleParticipantType]
           ([SystemRoleId]
           ,[ClientId]
           ,[ClientParticipantTypeId])
SELECT 11, ClientId, ClientParticipantTypeId
FROM ClientParticipantType
WHERE ParticipantTypeAbbreviation = 'SRO';
--RTA and Task Lead
INSERT INTO [dbo].[RoleParticipantType]
           ([SystemRoleId]
           ,[ClientId]
           ,[ClientParticipantTypeId])
SELECT 22, ClientId, ClientParticipantTypeId
FROM ClientParticipantType
WHERE ParticipantTypeAbbreviation = 'RTA'
UNION ALL
SELECT 28, ClientId, ClientParticipantTypeId
FROM ClientParticipantType
WHERE ParticipantTypeAbbreviation = 'RTA';
