INSERT INTO [dbo].[ProgramMeetingInformation]
           ([ProgramYearId]
           ,[DocumentName]
           ,[DocumentDescription]
           ,[FileLocation]
           ,[ExternalAddressFlag]
           ,[ActiveFlag]
           ,[LegacyMiId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramYear.ProgramYearId, MTG_Meeting_Info.Heading, MTG_Meeting_Info.[Description], MTG_Meeting_Info.Link, CASE WHEN LEFT(MTG_Meeting_Info.Link, 1) = '/' THEN 0 ELSE 1 END,
	MTG_Meeting_Info.Active, MTG_Meeting_Info.MI_ID, VUN.UserId, MTG_Meeting_Info.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.MTG_Meeting_Info MTG_Meeting_Info INNER JOIN
	ClientProgram ON MTG_Meeting_Info.Program = ClientProgram.LegacyProgramId INNER JOIN
	ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND MTG_Meeting_Info.FY = ProgramYear.[Year] LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON MTG_Meeting_Info.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ProgramMeetingInformation WHERE ProgramMeetingInformation.LegacyMiId = MTG_Meeting_Info.MI_ID)
