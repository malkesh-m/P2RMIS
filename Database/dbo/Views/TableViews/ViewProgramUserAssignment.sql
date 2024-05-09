CREATE VIEW [dbo].ViewProgramUserAssignment AS
SELECT [ProgramUserAssignmentId]
      ,[ProgramYearId]
      ,[UserId]
      ,[ClientParticipantTypeId]
      ,[LegacyParticipantId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[ProgramUserAssignment]
WHERE [DeletedFlag] = 0

