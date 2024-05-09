INSERT INTO [dbo].[MeetingSession] 
( --[MeetingSessionId],
      [LegacySessionId]
     ,[ClientMeetingId]
      ,[SessionAbbreviation]
      ,[SessionDescription]
      ,[StartDate]
      ,[EndDate]
     -- ,[CreatedBy]
     -- ,[CreatedDate]
      ,[ModifiedDate]
      ,[ModifiedBy]
 )
  
SELECT DISTINCT ms.[Session_ID] LegacySessionID
      ,cm.[ClientMeetingId] ClientMeetingID
      ,ms.[Session_ID] SessionAbbrev
      ,ms.[Session_Desc] SessionDesc
      ,ms.[Start_Date] StartDate
      ,ms.[End_Date] EndDate
      --,[critique_due_date]
      --,[final_critique_due_date]
      ,ms.[LAST_UPDATE_DATE]
      --,ms.[LAST_UPDATED_BY]
      --,v.[username]
      ,v.[userid]
     -- ,ms.[Meeting_ID] 
      --,cm.[LegacyMeetingId]
  FROM [$(P2RMIS)].[dbo].[MTG_Session] ms
  inner join [$(P2RMIS)].[dbo].PAN_Master pan on ms.Session_ID = pan.Session_ID
  inner join [$(P2RMIS)].[dbo].PRG_Program_LU prg on pan.Program = prg.Program
  inner join [dbo].Client cl on prg.Client = cl.ClientAbrv
  left join [dbo].[ViewLegacyUserNameToUserId] v on ms.[LAST_UPDATED_BY] = v.[username]
  inner join [dbo].[ClientMeeting] cm on ms.[Meeting_ID] = cm.[LegacyMeetingId] AND cl.ClientId = cm.ClientId
WHERE NOT EXISTS (Select 'X' FROM ViewMeetingSession WHERE LegacySessionId = ms.Session_ID AND ClientMeetingId = cm.ClientMeetingId)
