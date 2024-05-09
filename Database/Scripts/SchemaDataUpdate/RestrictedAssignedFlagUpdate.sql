UPDATE PanelUserAssignment SET RestrictedAssignedFlag = 1
WHERE ClientParticipantTypeId IN (Select ClientParticipantTypeId FROM ClientParticipantType WHERE LegacyPartTypeId = 'AH')