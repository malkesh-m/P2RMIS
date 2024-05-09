/*
	Reviewer had an assignment in 1.0 which did not get transferred initially to 2.0 due to issues with MRMC data in early 2018.
	Reviewer was then added in 2.0 which caused a duplicate when his 1.0 participation was transferred.
	To fix we will 1) soft delete newly added 1.0 assignment 2) update the legacy participant ID to match the original 1.0 assignment.
	This way when we transfer missing app critiques from 1.0 they will be transferred under his 2.0 account while preserving those existing in 2.0
	This script is only ran once
*/
UPDATE PanelUserAssignment SET DeletedFlag = 1, DeletedDate = SYSDATETIME(), DeletedBy = 10
WHERE LegacyParticipantId = 110186;

UPDATE PanelUserAssignment SET LegacyParticipantId = 110186
WHERE LegacyParticipantId = 113872;
