--
-- For CPRIT change the Consumer Reviewer 
--
UPDATE ClientParticipantType
SET 
	ParticipantTypeAbbreviation = 'AR', ParticipantTypeName = 'Advocate Reviewer'
WHERE 
	ClientId = 9 and
	LegacyPartTypeId = 'CR';