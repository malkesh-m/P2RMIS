INSERT INTO .[dbo].[MechanismTemplate]
           ([ProgramMechanismId]
           ,[ReviewStageId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, RS.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM   ViewProgramMechanism ProgramMechanism CROSS JOIN
ReviewStage RS
WHERE RS.ReviewStageId IN (1,2) AND NOT EXISTS (Select 'X' FROM ViewMechanismTemplate WHERE ProgramMechanismId = ProgramMechanism.ProgramMechanismId AND ReviewStageId = RS.ReviewStageId)
GROUP BY ProgramMechanism.ProgramMechanismId, RS.ReviewStageId, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
ORDER BY ProgramMechanismId, ReviewStageId;