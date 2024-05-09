CREATE VIEW [dbo].ViewProgramMechanism AS
SELECT [ProgramMechanismId]
      ,[ProgramYearId]
      ,[ClientAwardTypeId]
      ,[ReceiptCycle]
      ,[LegacyAtmId]
      ,[ReceiptDeadline]
      ,[AbstractFormat]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ProgramMechanism]
WHERE [DeletedFlag] = 0

