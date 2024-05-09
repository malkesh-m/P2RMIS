

INSERT INTO [dbo].[ProgramMechanism]
(--[ProgramMechanismId]
      [ProgramYearId]
      ,[ClientAwardTypeId]
      ,[ReceiptCycle]
      ,[LegacyAtmId]
      ,[ReceiptDeadline]
      ,[AbstractFormat]
      ,[ModifiedBy]
      ,[ModifiedDate])


 SELECT py.[ProgramYearID] ProgramYearId
	,cat.[ClientAwardTypeId] ClientAwardType
    ,pp.[receipt_cycle] ReceiptCycle
	,ptm.[ATM_ID] LegacyAtmID
	,ptm.[REC_DEADLINE] ReceiptDeadline
	,paf.[abstract_type] AbstractFormat
	,v.[userid] ModifiedBy
    ,ptm.[LAST_UPDATE_DATE] ModifiedDate


  FROM [$(P2RMIS)].[dbo].[PRO_Award_Type_Member] ptm
  left join [dbo].[ViewLegacyUserNameToUserId] v on ptm.[LAST_UPDATED_BY] = v.[username]
  left join [$(P2RMIS)].[dbo].[PRO_Abstract_Format] paf on ptm.[atm_id] = paf.[atm_id]
  left join [$(P2RMIS)].[dbo].[PRG_Program_PA] pp on ptm.[pa_id] = pp.[pa_id]
  left join [dbo].[ProgramYear] py on pp.[prg_id] = py.[LegacyProgramId]
  left join [dbo].[ClientProgram] cp on py.ClientProgramId = cp.ClientProgramId
  left join [dbo].[ClientAwardType] cat on ptm.[award_type] = cat.[LegacyAwardTypeId] 
  AND cp.ClientId = cat.ClientId
  WHERE NOT EXISTS (Select 'X' FROM ViewProgramMechanism WHERE ProgramYearId = py.ProgramYearId AND ClientAwardTypeId = cat.ClientAwardTypeId)
  order by cat.[ClientAwardTypeId]
