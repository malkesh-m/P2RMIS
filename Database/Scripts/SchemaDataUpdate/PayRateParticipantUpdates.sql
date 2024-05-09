UPDATE SessionPayRate
SET ClientParticipantTypeId = CptNew.ClientParticipantTypeId, ParticipantMethodId = PartTypeMapping.NewParticipantMethod, RestrictedAssignedFlag = PartTypeMapping.RestrictedAssignedFlag
FROM SessionPayRate INNER JOIN
	ClientParticipantType ON SessionPayRate.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId CROSS APPLY
	 udfLegacyToNewParticipantTypeMap(ClientParticipantType.LegacyPartTypeId, 0, 1, ClientParticipantType.ClientId) PartTypeMapping INNER JOIN
	 ClientParticipantType CptNew ON PartTypeMapping.NewParticipantTypeAbbreviation = CptNew.ParticipantTypeAbbreviation AND ClientParticipantType.ClientId = CptNew.ClientId;

UPDATE ProgramPayRate
SET ClientParticipantTypeId = CptNew.ClientParticipantTypeId, ParticipantMethodId = PartTypeMapping.NewParticipantMethod, RestrictedAssignedFlag = PartTypeMapping.RestrictedAssignedFlag
FROM SessionPayRate INNER JOIN
	ClientParticipantType ON SessionPayRate.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId CROSS APPLY
	 udfLegacyToNewParticipantTypeMap(ClientParticipantType.LegacyPartTypeId, 0, 1, ClientParticipantType.ClientId) PartTypeMapping INNER JOIN
	 ClientParticipantType CptNew ON PartTypeMapping.NewParticipantTypeAbbreviation = CptNew.ParticipantTypeAbbreviation AND ClientParticipantType.ClientId = CptNew.ClientId;