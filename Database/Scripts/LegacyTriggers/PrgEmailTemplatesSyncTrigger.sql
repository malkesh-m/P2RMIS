CREATE TRIGGER [PrgEmailTemplatesSyncTrigger]
ON [$(P2RMIS)].dbo.PRG_Email_Templates
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ProgramEmailTemplate]
	SET [TemplateName] = inserted.Heading
	  ,[TemplateDescription] = inserted.[Description]
      ,[FileLocation] = CASE WHEN LEFT(inserted.Link, 14) = 'https://p2rmis' THEN STUFF(inserted.Link, 1, 18, '') ELSE inserted.Link END
      ,[ActiveFlag] = inserted.Active
	  ,[ModifiedBy] = VUN.UserId
      ,[ModifiedDate] = inserted.Last_Update_Date
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramEmailTemplate] ProgramEmailTemplate ON inserted.ET_ID = ProgramEmailTemplate.LegacyEtId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.Last_Updated_By = VUN.UserName
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[ProgramEmailTemplate]
           ([ProgramYearId]
           ,[TemplateName]
           ,[TemplateDescription]
           ,[FileLocation]
           ,[ActiveFlag]
           ,[LegacyEtId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ProgramYear.ProgramYearId, PRG_Email_Templates.Heading, PRG_Email_Templates.Description, CASE WHEN LEFT(PRG_Email_Templates.Link, 14) = 'https://p2rmis' THEN STUFF(PRG_Email_Templates.Link, 1, 18, '') ELSE PRG_Email_Templates.Link END,
		PRG_Email_Templates.Active, PRG_Email_Templates.ET_ID, VUN.UserId, PRG_Email_Templates.Last_Update_Date, VUN.UserId, PRG_Email_Templates.Last_Update_Date
	FROM inserted PRG_Email_Templates INNER JOIN
		[$(DatabaseName)].[dbo].ClientProgram ClientProgram ON PRG_Email_Templates.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].[dbo].ProgramYear ProgramYear ON ClientProgram.ClientProgramId = ProgramYear.ClientProgramId AND PRG_Email_Templates.FY = ProgramYear.[Year] LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON PRG_Email_Templates.Last_Updated_By = VUN.UserName
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[ProgramEmailTemplate]
	SET [DeletedFlag] = 1,
	[DeletedBy] = VUN.UserID,
	[DeletedDate] = SYSDATETIME()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[ProgramEmailTemplate] ProgramEmailTemplate ON deleted.ET_ID = ProgramEmailTemplate.LegacyEtId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON deleted.Last_Updated_By = VUN.UserName
	END
END