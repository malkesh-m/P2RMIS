CREATE TRIGGER [PrgTrainingSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Training]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[TrainingDocument]
	SET [TrainingCategoryId] = tc.TrainingCategoryId
      ,[DocumentName] = inserted.Heading
      ,[DocumentDescription] = inserted.Description
      ,[FileLocation] = CASE WHEN LEFT(inserted.Link, 1) = '/' THEN 'https://p2rmis.com' + inserted.Link ELSE inserted.Link END
      ,[ExternalAddressFlag] = CASE WHEN LEFT(inserted.Link, 1) = '/' OR LEFT(inserted.Link, 14) = 'https://p2rmis' THEN 0 ELSE 1 END
      ,[ActiveFlag] = inserted.Active
      ,[ModifiedBy] = vun.UserId
      ,[ModifiedDate] = inserted.Updated_Date
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocument] td ON inserted.TR_ID = td.LegacyTrId INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingCategory] tc ON inserted.Cat_Type = tc.LegacyCatTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId vun on inserted.[Updated_By] = vun.[username]
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[TrainingDocument]
	([ProgramYearId]
           ,[TrainingCategoryId]
           ,[DocumentName]
           ,[DocumentDescription]
           ,[FileLocation]
           ,[ExternalAddressFlag]
           ,[ActiveFlag]
           ,[LegacyTrId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ProgramYear.ProgramYearId, TrainingCategory.TrainingCategoryId, PRG_Training.Heading, PRG_Training.Description,
		CASE WHEN LEFT(PRG_Training.Link, 1) = '/' THEN 'https://p2rmis.com' + PRG_Training.Link ELSE PRG_Training.Link END, CASE WHEN LEFT(PRG_Training.Link, 1) = '/' OR LEFT(PRG_Training.Link, 14) = 'https://p2rmis' THEN 0 ELSE 1 END,
		PRG_Training.Active, PRG_Training.TR_ID, v.UserID, PRG_Training.Updated_Date, v.UserID, PRG_Training.Updated_Date
	FROM inserted PRG_Training INNER JOIN
		[$(DatabaseName)].[dbo].ClientProgram ClientProgram ON PRG_Training.Program = ClientProgram.LegacyProgramId INNER JOIN
		[$(DatabaseName)].[dbo].ProgramYear ProgramYear ON PRG_Training.FY = ProgramYear.Year AND ClientProgram.ClientProgramId = ProgramYear.ClientProgramId INNER JOIN
		[$(DatabaseName)].[dbo].TrainingCategory TrainingCategory ON PRG_Training.Cat_Type = TrainingCategory.LegacyCatTypeId LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId v on PRG_Training.[Updated_By] = v.[username]
	END
	--DELETE
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[TrainingDocument]
		SET	DeletedFlag = 1, DeletedDate = SYSDATETIME()
		FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[TrainingDocument] td ON deleted.TR_ID = td.LegacyTrId
		WHERE td.DeletedFlag = 0
	END
END