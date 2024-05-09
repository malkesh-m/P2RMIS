--Correct assign remote flag to participants
UPDATE PanelUserAssignment
SET ParticipationMethodId = 2
WHERE ClientParticipantTypeId IN (Select ClientParticipantTypeId FROM ClientParticipantType WHERE LegacyPartTypeId IN ('TC','CRT','CHT','OR','OCR','OCH'));

UPDATE PanelUserAssignment
SET ParticipationMethodId = 1
WHERE ClientParticipantTypeId IN (Select ClientParticipantTypeId FROM ClientParticipantType WHERE LegacyPartTypeId IN ('SR','CR','CH','AH'));

--Map all legacy participant types to the new type

UPDATE PanelUserAssignment
SET ClientParticipantTypeId = ClientParticipantType2.ClientParticipantTypeId
FROM PanelUserAssignment INNER JOIN
ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
ClientParticipantType ClientParticipantType2 ON ClientParticipantType.ClientId = ClientParticipantType2.ClientId AND
CASE WHEN ClientParticipantType.LegacyPartTypeId IN ('AH','SR','OR','TC') THEN 'SR' 
	WHEN ClientParticipantType.LegacyPartTypeId IN ('CR', 'CRT', 'OCR') THEN 'CR'
	WHEN ClientParticipantType.LegacyPartTypeId IN ('CH', 'CHT', 'OCH') THEN 'CH' END = ClientParticipantType2.LegacyPartTypeId

-- Deactivate old participant type
UPDATE ClientParticipantType
SET ActiveFlag = 0
WHERE LegacyPartTypeId IN ('AH','OR','TC','CRT','OCR','CHT','OCH');

--Add new specialist reviewer type
INSERT INTO ClientParticipantType
([ClientId]
           ,[ParticipantTypeAbbreviation]
           ,[ParticipantTypeName]
           ,[ParticipantScope]
           ,[ActiveFlag]
           ,[ReviewerFlag]
           ,[ChairpersonFlag]
           ,[ConsumerFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
VALUES (19, 'SP', 'Specialist Reviewer', 'Panel', 1, 1, 0, 0, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime());
INSERT INTO ClientParticipantType
([ClientId]
           ,[ParticipantTypeAbbreviation]
           ,[ParticipantTypeName]
           ,[ParticipantScope]
           ,[ActiveFlag]
           ,[ReviewerFlag]
           ,[ChairpersonFlag]
           ,[ConsumerFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
VALUES (9, 'SP', 'Specialist Reviewer', 'Panel', 1, 1, 0, 0, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime());
--Set all existing specialist reviewers to new type
UPDATE PanelUserAssignment
SET ClientParticipantTypeId = ClientParticipantType2.ClientParticipantTypeId
FROM PanelUserAssignment INNER JOIN
ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
ClientParticipantType ClientParticipantType2 ON ClientParticipantType.ClientId = ClientParticipantType2.ClientId AND
ClientParticipantType2.ParticipantTypeAbbreviation = 'SP' INNER JOIN
ClientRole ON PanelUserAssignment.ClientRoleId = ClientRole.ClientRoleId
WHERE ClientRole.SpecialistFlag = 1;