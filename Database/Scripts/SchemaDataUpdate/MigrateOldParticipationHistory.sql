--Add program years that did not exist in 1.0 which had participation and/or associated panels
INSERT INTO [dbo].[ProgramYear]
           ([ClientProgramId]
           ,[Year]
           ,[DateClosed]
           ,[ModifiedDate]
           ,[ModifiedBy])
SELECT DISTINCT
           ClientProgram.ClientProgramId
           ,Part.FY
           ,'1/1/2002'
           ,dbo.GetP2rmisDateTime()
           ,10
FROM	[$(P2RMIS)].dbo.PRG_Participants Part
INNER JOIN ClientProgram ON Part.Program = ClientProgram.LegacyProgramId
WHERE NOT EXISTS (SELECT 'X' FROM ViewProgramYear WHERE ClientProgramId = ClientProgram.ClientProgramId AND [Year] = Part.FY)		
UNION
SELECT DISTINCT
           ClientProgram.ClientProgramId
           ,Pan.FY
           ,'1/1/2002'
           ,dbo.GetP2rmisDateTime()
           ,10
FROM	[$(P2RMIS)].dbo.PAN_Master Pan
INNER JOIN ClientProgram ON Pan.Program = ClientProgram.LegacyProgramId
WHERE NOT EXISTS (SELECT 'X' FROM ViewProgramYear WHERE ClientProgramId = ClientProgram.ClientProgramId AND [Year] = Pan.FY);

--Add program associations for these panels
  INSERT INTO [dbo].[ProgramPanel] (
   [ProgramYearId]
  ,[SessionPanelId]
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
 WHERE NOT EXISTS (Select 'X' FROM ViewProgramPanel WHERE ProgramYearId = py.ProgramYearId AND SessionPanelId = sp.SessionPanelId);
