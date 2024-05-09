INSERT INTO [dbo].[ProgramYear]
(     [LegacyProgramId]
      ,[ClientProgramId]
      ,[Year]
      ,[DateClosed]
      ,[ModifiedDate]
      ,[ModifiedBy])
SELECT p.[PRG_ID] LegacyProgramID
      ,cp.[ClientProgramId] ClientProgramID
      ,p.[FY] ProgramYear
      ,p.[CLOSED] DateClosed
      ,p.[LAST_UPDATE_DATE] ModifiedDate
      ,v.[userid] ModifiedBy
  FROM [$(P2RMIS)].[dbo].[PRG_Program] p
    left join [$(P2RMIS)].[dbo].[PRG_Program_LU] pl on p.[program] = pl.[program]
    left join [dbo].[ViewLegacyUserNameToUserId] v on p.[LAST_UPDATED_BY] = v.[username]
    left join [dbo].[ClientProgram] cp on p.[program] = cp.[legacyProgramId]
    where p.[prg_id] not in 
    (select [LegacyProgramId]
    from [dbo].[ProgramYear])
    order by p.[prg_id]
 