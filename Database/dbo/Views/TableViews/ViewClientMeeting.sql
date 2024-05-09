CREATE VIEW [dbo].ViewClientMeeting AS
SELECT [ClientMeetingId]
      ,[LegacyMeetingId]
      ,[ClientId]
      ,[MeetingAbbreviation]
      ,[MeetingDescription]
      ,[StartDate]
      ,[EndDate]
      ,[MeetingLocation]
      ,[MeetingTypeId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ClientMeeting]
WHERE [DeletedFlag] = 0

