UPDATE    dbo.ClientParticipantType
SET               SROFlag =1
WHERE     (LegacyPartTypeId = 'SRA')


UPDATE    dbo.ClientParticipantType
SET               RTAFlag =1
WHERE     (LegacyPartTypeId = 'RTA') 