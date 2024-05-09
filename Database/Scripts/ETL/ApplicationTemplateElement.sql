INSERT INTO [dbo].[ApplicationTemplateElement]
           ([ApplicationTemplateId]
           ,[MechanismTemplateElementId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationTemplate.ApplicationTemplateId, MechanismTemplateElement.MechanismTemplateElementId, 
	MechanismTemplateElement.ModifiedBy, MechanismTemplateElement.ModifiedDate
FROM ViewApplicationTemplate ApplicationTemplate INNER JOIN
	ViewMechanismTemplate MechanismTemplate ON ApplicationTemplate.MechanismTemplateId = MechanismTemplate.MechanismTemplateId INNER JOIN
	ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId
WHERE NOT EXISTS (Select 'X' FROM ViewApplicationTemplateElement WHERE ApplicationTemplateId = ApplicationTemplate.ApplicationTemplateId AND MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId)