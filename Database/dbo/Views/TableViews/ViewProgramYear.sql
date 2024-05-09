CREATE VIEW [dbo].ViewProgramYear AS
SELECT [ProgramYearId]
      ,[LegacyProgramId]
      ,[ClientProgramId]
      ,[Year]
      ,[DateClosed]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[ModifiedDate]
      ,[ModifiedBy]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ProgramYear]
WHERE [DeletedFlag] = 0

