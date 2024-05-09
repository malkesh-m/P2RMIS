INSERT INTO ProgramEmailTemplate
([ProgramYearId]
           ,[TemplateName]
           ,[TemplateDescription]
           ,[FileLocation]
           ,[ActiveFlag]
           ,[LegacyEtId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramYear.ProgramYearId, PRG_Email_Templates.Heading, PRG_Email_Templates.Description, CASE WHEN LEFT(PRG_Email_Templates.Link, 1) = '/' THEN 'https://p2rmis.com' + PRG_Email_Templates.Link ELSE PRG_Email_Templates.Link END,
		PRG_Email_Templates.Active, PRG_Email_Templates.ET_ID, VUN.UserId, PRG_Email_Templates.Last_Update_Date
FROM [$(P2RMIS)].dbo.PRG_Email_Templates PRG_Email_Templates INNER JOIN
	ClientProgram ON PRG_Email_Templates.Program = ClientProgram.LegacyProgramId INNER JOIN
	ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND PRG_Email_Templates.FY = ProgramYear.[Year] LEFT OUTER JOIN
	ViewLegacyUserNameToUserId VUN ON PRG_Email_Templates.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ProgramEmailTemplate WHERE DeletedFlag = 0 AND LegacyEtId = PRG_Email_Templates.ET_ID)
