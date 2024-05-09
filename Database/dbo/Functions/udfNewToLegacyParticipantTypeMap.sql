
/*
Given the inputs from 2.0, returns the corresponding participant type in 1.0
Note: Should this instead be a scalar valued function?
*/
CREATE FUNCTION [dbo].[udfNewToLegacyParticipantTypeMap]
(
	@ParticipantTypeAbbreviation varchar(20),
	@ParticipantMethod int,
	@RestrictedAssignmentFlag bit,
	@MeetingTypeId int
)
RETURNS TABLE
AS
RETURN
/*
	Lookups
	ParticipantMethod: 1 = InPerson, 2 = Remote
	MeetingType: 1= Onsite, 2 = TC, 3 = OL, 4 = VC
*/
SELECT CASE WHEN @RestrictedAssignmentFlag = 1 THEN 'AH'
		WHEN @ParticipantTypeAbbreviation IN ('CR','AR') AND @ParticipantMethod = 1 AND @MeetingTypeId = 1 THEN 'CR'
		WHEN @ParticipantTypeAbbreviation IN ('CR','AR') AND @ParticipantMethod = 2 AND @MeetingTypeId = 1 THEN 'CRT'
		WHEN @ParticipantTypeAbbreviation IN ('CR','AR') AND @ParticipantMethod = 2 AND @MeetingTypeId IN (2, 4) THEN 'CRT'
		WHEN @ParticipantTypeAbbreviation IN ('CR','AR') AND @ParticipantMethod = 2 AND @MeetingTypeId = 3 THEN 'OCR'
		WHEN @ParticipantTypeAbbreviation IN ('SR','SPR') AND @ParticipantMethod = 1 AND @MeetingTypeId = 1 THEN 'SR'
		WHEN @ParticipantTypeAbbreviation IN ('SR','SPR') AND @ParticipantMethod = 2 AND @MeetingTypeId IN (1,2,4) THEN 'TC'
		WHEN @ParticipantTypeAbbreviation IN ('SR','SPR') AND @ParticipantMethod = 2 AND @MeetingTypeId = 3 THEN 'OR'
		WHEN @ParticipantTypeAbbreviation = 'CH' AND @ParticipantMethod = 1 AND @MeetingTypeId = 1 THEN 'CH'
		WHEN @ParticipantTypeAbbreviation = 'CH' AND @ParticipantMethod = 2 AND @MeetingTypeId IN (1,2,4) THEN 'CHT'
		WHEN @ParticipantTypeAbbreviation = 'CH' AND @ParticipantMethod = 2 AND @MeetingTypeId = 3 THEN 'OCH'
		--Otherwise we assume nothing has changed
		ELSE @ParticipantTypeAbbreviation END AS LegacyParticipantTypeId

