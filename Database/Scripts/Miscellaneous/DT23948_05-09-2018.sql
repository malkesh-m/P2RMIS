--Add missing sessions from 1.0
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
WHERE NOT EXISTS (Select 'X' FROM ViewMeetingSession WHERE LegacySessionId = ms.Session_ID AND ClientMeetingId = cm.ClientMeetingId) AND ms.Session_ID IN ('S1-1710OS2', 'S2-1710OS2');
--Update existing panels to correct sessions
UPDATE sp SET MeetingSessionId = ms.MeetingSessionId
FROM SessionPanel sp 
inner join [$(P2RMIS)].[dbo].PAN_Master pan on sp.LegacyPanelId = pan.Panel_Id
inner join ViewMeetingSession ms on pan.Session_ID = ms.LegacySessionId
WHERE sp.LegacyPanelId IN (4360, 4454, 4361);

--Panel name changed after link broke
UPDATE SessionPanel SET PanelAbbreviation = 'TAU-GEI', PanelName = 'Tau Protein - Gene Environment Interactions'
WHERE LegacyPanelId = 4454;

--This panel was removed/combined in 1.0
UPDATE SessionPanel SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE LegacyPanelId = 4455;

--Import missing panel
DECLARE @LegacyPanelId INT = 4453;

:r ../DataManagement/TransferPanelData.sql
