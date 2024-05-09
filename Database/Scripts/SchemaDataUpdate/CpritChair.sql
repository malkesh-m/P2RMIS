DELETE FROM [dbo].[RoleParticipantType] WHERE SystemRoleId = 30 AND ClientId = 9;
DELETE FROM [dbo].[ClientParticipantType] WHERE [ParticipantTypeName] = 'CPRIT Chair';
-- Add CPRIT Chair
DECLARE @ClientParticipantTypeId INT
INSERT INTO [dbo].[ClientParticipantType]
	([ClientId]
      ,[LegacyPartTypeId]
      ,[ParticipantTypeAbbreviation]
      ,[ParticipantTypeName]
      ,[ParticipantScope]
	  ,[ReviewerFlag]
      ,[ModifiedBy]
      ,[ModifiedDate])
VALUES (9, 'CH', 'CHCPRIT', 'CPRIT Chair', 'Panel', 0, 10, dbo.GetP2rmisDateTime())
SELECT @ClientParticipantTypeId = SCOPE_IDENTITY()
--CPRIT Chair
DELETE FROM [dbo].[RoleParticipantType] WHERE SystemRoleId = 30 AND ClientId = 9;
INSERT INTO [dbo].[RoleParticipantType]
           ([SystemRoleId]
           ,[ClientId]
           ,[ClientParticipantTypeId])
VALUES (30, 9, @ClientParticipantTypeId);