
INSERT INTO [dbo].[SessionPanel]
(--[SessionPanelId]
      [LegacyPanelId]
      ,[MeetingSessionId]
      ,[PanelAbbreviation]
      ,[PanelName]
      ,[StartDate]
      ,[EndDate]
      --,[CreatedBy]
     -- ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate])
 SELECT pm.[panel_id] LegacyPanelID
		,ms2.[MeetingSessionID]
		--,ms.[meeting_id]
		--,cm.[LegacyMeetingId] MeetingSessionID
		,pm.[Panel_Abrv] PanelAbbreviation
		,pm.[Panel_Name] PanelName
		--,ms.[Session_Desc]
		,ms2.[StartDate] StartDate
		,ms2.[EndDate] EndDate
		--,ms.[critique_due_date]
		--,ms.[final_critique_due_date]
		,v.[userid] ModifiedBy
		,pm.[LAST_UPDATE_DATE] ModifiedDate
		--,ms.session_id
		--,pm.session_id
		--,ms.meeting_id
  FROM  [$(P2RMIS)].[dbo].[PAN_Master] pm 
  inner join [$(P2RMIS)].[dbo].[PRG_Program_LU] prg on pm.Program = prg.Program
  inner join [dbo].[Client] cl on prg.Client = cl.ClientAbrv
  left join [dbo].[MeetingSession] ms2 on pm.[session_id] = ms2.[LegacySessionId]
  left join [dbo].[ClientMeeting] cm on cl.ClientId = cm.ClientId AND cm.ClientMeetingId = ms2.ClientMeetingId
  --for legacy purposes some panels did not have a session, but we want to keep record of participation regardless
  left join [dbo].[ViewLegacyUserNameToUserId] v on pm.[LAST_UPDATED_BY] = v.[username]
  WHERE NOT EXISTS (Select 'X' FROM ViewSessionPanel WHERE LegacyPanelId = pm.Panel_ID AND MeetingSessionId = ms2.MeetingSessionId) AND ms2.MeetingSessionId IS NOT NULL