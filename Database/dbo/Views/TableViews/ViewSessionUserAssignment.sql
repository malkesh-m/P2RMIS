CREATE VIEW [dbo].[ViewSessionUserAssignment]
	AS SELECT SessionUserAssignmentId, [UserId], MeetingSessionId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate FROM [SessionUserAssignment] WHERE DeletedFlag = 0
