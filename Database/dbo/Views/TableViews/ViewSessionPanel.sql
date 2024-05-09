CREATE VIEW [dbo].ViewSessionPanel AS
SELECT [SessionPanelId]
      ,[LegacyPanelId]
      ,[MeetingSessionId]
      ,[PanelAbbreviation]
      ,[PanelName]
      ,[StartDate]
      ,[EndDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[SessionPanel]
WHERE [DeletedFlag] = 0

