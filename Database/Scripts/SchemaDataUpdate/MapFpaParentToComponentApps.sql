UPDATE [Application] SET ParentApplicationId = ParentApplication.ApplicationId
FROM [Application]
INNER JOIN [ViewApplication] ParentApplication ON LEFT([Application].LogNumber, 8) = ParentApplication.LogNumber
WHERE [Application].ProgramMechanismId = 3423 AND [Application].ParentApplicationId IS NULL;