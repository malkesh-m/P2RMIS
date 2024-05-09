/*INSERT INTO [dbo].[MeetingRegistrationAttendance]
           ([MeetingRegistrationId]
           ,[AttendanceStartDate]
           ,[AttendanceEndDate]
           ,[MealRequestComments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT MeetingRegistration.MeetingRegistrationId, MIN(MTG_Attendance.Schd_Attend_Day), MAX(MTG_Attendance.Schd_Attend_Day), MTG_Housing.Special_Requests,
		(Select VUN.UserId FROM ViewLegacyUserNameToUserId VUN WHERE VUN.UserName = MAX(MTG_Attendance.LAST_UPDATED_BY)), MAX(MTG_Attendance.LAST_UPDATE_DATE)
FROM [$(P2RMIS)].dbo.MTG_Attendance MTG_Attendance INNER JOIN
	MeetingRegistration ON MTG_Attendance.MR_ID = MeetingRegistration.LegacyMrId LEFT OUTER JOIN
	[$(P2RMIS)].dbo.MTG_Housing MTG_Housing ON MTG_Attendance.MR_ID = MTG_Housing.MR_ID
GROUP BY MeetingRegistration.MeetingRegistrationId, MTG_Housing.Special_Requests
*/