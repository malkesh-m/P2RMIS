--Does some initial population inferring which scoring template correlates to which mechanism
INSERT INTO [dbo].[MechanismScoringTemplate]
           ([ProgramMechanismId]
           ,[ScoringTemplateId]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, 3, ProgramMechanism.CreatedBy, ProgramMechanism.CreatedDate, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM ViewProgramMechanism ProgramMechanism
WHERE ProgramMechanism.ProgramMechanismId IN
(Select MechanismTemplate.ProgramMechanismId
FROM ViewMechanismTemplate MechanismTemplate INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId INNER JOIN
	ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId
WHERE MechanismTemplateElement.OverallFlag = 1 AND MechanismTemplateElementScoring.ClientScoringId = 146);

INSERT INTO [dbo].[MechanismScoringTemplate]
           ([ProgramMechanismId]
           ,[ScoringTemplateId]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, 2, ProgramMechanism.CreatedBy, ProgramMechanism.CreatedDate, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM ViewProgramMechanism ProgramMechanism
WHERE ProgramMechanism.ProgramMechanismId IN
(Select MechanismTemplate.ProgramMechanismId
FROM ViewMechanismTemplate MechanismTemplate INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId INNER JOIN
	ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId
WHERE MechanismTemplateElement.OverallFlag = 1 AND MechanismTemplateElementScoring.StepTypeId = 5 AND MechanismTemplateElementScoring.ClientScoringId = 1);



INSERT INTO [dbo].[MechanismScoringTemplate]
           ([ProgramMechanismId]
           ,[ScoringTemplateId]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ProgramMechanism.ProgramMechanismId, 1, ProgramMechanism.CreatedBy, ProgramMechanism.CreatedDate, ProgramMechanism.ModifiedBy, ProgramMechanism.ModifiedDate
FROM ViewProgramMechanism ProgramMechanism
WHERE ProgramMechanism.ProgramMechanismId IN
(Select MechanismTemplate.ProgramMechanismId
FROM ViewMechanismTemplate MechanismTemplate INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId INNER JOIN
	ViewMechanismTemplateElementScoring MechanismTemplateElementScoring ON MechanismTemplateElement.MechanismTemplateElementId = MechanismTemplateElementScoring.MechanismTemplateElementId
WHERE MechanismTemplateElement.OverallFlag = 1 AND MechanismTemplateElementScoring.StepTypeId = 5 AND MechanismTemplateElementScoring.ClientScoringId = 3) AND
NOT EXISTS (Select 'X' FROM MechanismScoringTemplate WHERE ProgramMechanismId = ProgramMechanism.ProgramMechanismId);