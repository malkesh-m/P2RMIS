CREATE FUNCTION [dbo].[udfLegacyToNewParticipantTypeMap]
(
	@LegacyParticipantTypeId varchar(20),
	@SpecialistFlag bit,
	@MeetingTypeId int,
	@ClientId int
)
RETURNS TABLE
AS
RETURN
	SELECT CASE WHEN @SpecialistFlag = 1 AND @LegacyParticipantTypeId IN ('SR', 'OR', 'TC', 'AH') THEN 'SPR'
	WHEN @LegacyParticipantTypeId IN ('SR', 'OR', 'TC', 'AH') THEN 'SR'
	WHEN @LegacyParticipantTypeId IN ('CR','OCR','CRT') AND @ClientId = 9 THEN 'AR'
	WHEN @LegacyParticipantTypeId IN ('CR','OCR','CRT') THEN 'CR'
	WHEN @LegacyParticipantTypeId IN ('CH','OCH','CHT') THEN 'CH'
	WHEN @LegacyParticipantTypeId = 'SRA' THEN 'SRO'
	WHEN @LegacyParticipantTypeId = 'Constella' THEN 'SRA'
	ELSE @LegacyParticipantTypeId 
	END AS NewParticipantTypeAbbreviation,
	CASE WHEN @LegacyParticipantTypeId IN ('SR','CH','CR') THEN 1
	WHEN @LegacyParticipantTypeId IN ('OR','OCH','OCR','TC','CRT','CHT') THEN 2
	WHEN @MeetingTypeId = 1 THEN 1
	ELSE 2
	END As NewParticipantMethod,
	CASE WHEN @LegacyParticipantTypeId = 'AH' THEN 1
	ELSE 0 END As RestrictedAssignedFlag

