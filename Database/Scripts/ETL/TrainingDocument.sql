INSERT INTO TrainingDocument
([ProgramYearId]
           ,[TrainingCategoryId]
           ,[DocumentName]
           ,[DocumentDescription]
           ,[FileLocation]
           ,[ExternalAddressFlag]
           ,[ActiveFlag]
		   ,[LegacyTrId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramYear.ProgramYearId, TrainingCategory.TrainingCategoryId, PRG_Training.Heading, PRG_Training.Description,
	CASE WHEN LEFT(PRG_Training.Link, 1) = '/' THEN 'https://p2rmis.com' + PRG_Training.Link ELSE PRG_Training.Link END, CASE WHEN LEFT(PRG_Training.Link, 1) = '/' OR LEFT(PRG_Training.Link, 14) = 'https://p2rmis' THEN 0 ELSE 1 END,
	PRG_Training.Active, PRG_Training.TR_ID, v.UserID, PRG_Training.Updated_Date
FROM [$(P2RMIS)].dbo.PRG_Training PRG_Training INNER JOIN
	ClientProgram ON PRG_Training.Program = ClientProgram.LegacyProgramId INNER JOIN
	ProgramYear ON PRG_Training.FY = ProgramYear.Year AND ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
	TrainingCategory ON PRG_Training.Cat_Type = TrainingCategory.LegacyCatTypeId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId v on PRG_Training.[Updated_By] = v.[username]
WHERE NOT EXISTS (Select 'X' FROM TrainingDocument WHERE LegacyTrId = PRG_Training.TR_ID)