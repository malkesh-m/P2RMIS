CREATE VIEW [dbo].ViewProgramPanel AS
SELECT [ProgramPanelId]
      ,[ProgramYearId]
      ,[SessionPanelId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ProgramPanel]
WHERE [DeletedFlag] = 0

