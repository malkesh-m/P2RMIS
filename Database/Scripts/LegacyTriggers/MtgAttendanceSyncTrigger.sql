CREATE TRIGGER [MtgAttendanceSyncTrigger]
ON [$(P2RMIS)].dbo.[Mtg_Attendance]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--INSERT
	IF EXISTS (Select * FROM inserted) AND NOT EXISTS (Select * FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON inserted.MR_ID = MeetingRegistration.LegacyMrId INNER JOIN
		[$(DatabaseName)].[dbo].ViewMeetingRegistrationAttendance MeetingRegistrationAttendance ON MeetingRegistration.MeetingRegistrationId = MeetingRegistrationAttendance.MeetingRegistrationId)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[MeetingRegistrationAttendance]
           ([MeetingRegistrationId]
           ,[AttendanceStartDate]
           ,[AttendanceEndDate]
           ,[MealRequestComments]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT MeetingRegistration.MeetingRegistrationId, MIN(MTG_Attendance.Schd_Attend_Day), MAX(MTG_Attendance.Schd_Attend_Day), MTG_Housing.Special_Requests,
			(Select VUN.UserId FROM [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN WHERE VUN.UserName = MAX(MTG_Attendance.LAST_UPDATED_BY)), MAX(MTG_Attendance.LAST_UPDATE_DATE),
			(Select VUN.UserId FROM [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN WHERE VUN.UserName = MAX(MTG_Attendance.LAST_UPDATED_BY)), MAX(MTG_Attendance.LAST_UPDATE_DATE)
	FROM inserted MTG_Attendance INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Attendance.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.MTG_Housing MTG_Housing ON MTG_Attendance.MR_ID = MTG_Housing.MR_ID
	WHERE MeetingRegistration.DeletedFlag = 0
	GROUP BY MeetingRegistration.MeetingRegistrationId, MTG_Housing.Special_Requests
	END
	--DELETE
	ELSE IF EXISTS (Select * FROM deleted) AND NOT EXISTS (Select * FROM deleted INNER JOIN [$(P2RMIS)].dbo.MTG_Attendance MTG_Attendance ON deleted.MR_ID = MTG_Attendance.MR_ID)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationAttendance] 
	SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON deleted.MR_ID = MeetingRegistration.LegacyMrId INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistrationAttendance MeetingRegistrationAttendance ON MeetingRegistration.MeetingRegistrationId = MeetingRegistrationAttendance.MeetingRegistrationId
	WHERE MeetingRegistrationAttendance.DeletedFlag = 0
	END
	--UPDATE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[MeetingRegistrationAttendance]
	SET [AttendanceStartDate] = AttendanceTable.StartDate
      ,[AttendanceEndDate] = AttendanceTable.EndDate
      ,[ModifiedBy] = (Select VUN.UserId FROM [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN WHERE VUN.UserName = AttendanceTable.LastUpdatedBy)
      ,[ModifiedDate] = AttendanceTable.LastUpdateDate
	FROM inserted MTG_Attendance INNER JOIN
	[$(DatabaseName)].[dbo].ViewMeetingRegistration MR ON MTG_Attendance.MR_ID = MR.LegacyMrId INNER JOIN
	[$(DatabaseName)].[dbo].MeetingRegistrationAttendance MeetingRegistrationAttendance ON MR.MeetingRegistrationId = MeetingRegistrationAttendance.MeetingRegistrationId INNER JOIN
		(SELECT MeetingRegistration.MeetingRegistrationId, MIN(MTG_Attendance.Schd_Attend_Day) StartDate, MAX(MTG_Attendance.Schd_Attend_Day) EndDate, MAX(MTG_Attendance.LAST_UPDATE_DATE) LastUpdateDate, MAX(MTG_Attendance.LAST_UPDATED_BY) LastUpdatedBy
		FROM MTG_Attendance INNER JOIN
		[$(DatabaseName)].[dbo].MeetingRegistration MeetingRegistration ON MTG_Attendance.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
		[$(P2RMIS)].dbo.MTG_Housing MTG_Housing ON MTG_Attendance.MR_ID = MTG_Housing.MR_ID
		WHERE MeetingRegistration.DeletedFlag = 0
	GROUP BY MeetingRegistration.MeetingRegistrationId) AttendanceTable ON MR.MeetingRegistrationId = AttendanceTable.MeetingRegistrationId
	WHERE MeetingRegistrationAttendance.DeletedFlag = 0

	
	END
END