--Bad data in the database means some Panels will not show under any program as the program in PAN_Master was never updated.

INSERT INTO [dbo].[ProgramPanel] (
--[ProgramPanelId]
   [ProgramYearId]
  ,[SessionPanelId]
--,[CreatedBy]
--,[CreatedDate]
  ,[ModifiedBy]
  ,[ModifiedDate]
 )
 SELECT py.[ProgramYearId] ProgramYearID
  ,sp.[SessionPanelId] SessionPanelID
  ,py.[ModifiedBy] ModifiedBy
  ,py.[ModifiedDate] ModifiedDate      
 FROM [SessionPanel] sp
 INNER JOIN [$(P2RMIS)].dbo.PAN_Master pm ON sp.LegacyPanelId = pm.Panel_Id
 INNER JOIN [$(P2RMIS)].dbo.PRG_Program_LU prg ON pm.Program = prg.Program
 INNER JOIN Client ON prg.Client = Client.ClientAbrv
 INNER JOIN ClientProgram cp ON prg.Program = cp.LegacyProgramId AND Client.ClientId = cp.ClientId
 INNER JOIN ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND pm.FY = py.Year 
 LEFT JOIN [MeetingSession] ms ON sp.MeetingSessionId = ms.MeetingSessionId
 LEFT JOIN [ClientMeeting] cm ON ms.ClientMeetingId = cm.ClientMeetingId AND cm.ClientId = Client.ClientID
 WHERE NOT EXISTS (Select 'X' FROM ViewProgramPanel WHERE ProgramYearId = py.ProgramYearId AND SessionPanelId = sp.SessionPanelId)