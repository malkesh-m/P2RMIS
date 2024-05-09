CREATE VIEW [dbo].ViewMeetingSession AS
SELECT [MeetingSessionId]
      ,[LegacySessionId]
      ,[ClientMeetingId]
      ,[SessionAbbreviation]
      ,[SessionDescription]
      ,[StartDate]
      ,[EndDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[MeetingSession]
WHERE [DeletedFlag] = 0

