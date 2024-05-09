
INSERT INTO [ApplicationPersonnel]
           ([ApplicationId]
           ,[ClientApplicationPersonnelTypeId]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[OrganizationName]
           ,[PhoneNumber]
           ,[EmailAddress]
           ,[PrimaryFlag]
           ,[Source]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   --Admin1 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin1_First_Name, pro.admin1_LAST_NAME, pro.admin1_MIDDLE_INITIAL, pro.admin1_ORG_NAME, 
pro.admin1_phone_number, pro.admin1_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-1'
WHERE pro.admin1_LAST_NAME IS NOT NULL OR pro.admin1_ORG_NAME IS NOT NULL
UNION ALL
--admin2 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin2_First_Name, pro.admin2_LAST_NAME, pro.admin2_MIDDLE_INITIAL, NULL, 
pro.admin2_phone_number, pro.admin2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-2'
WHERE pro.admin2_LAST_NAME IS NOT NULL
UNION ALL
--admin3 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin3_First_Name, pro.admin3_LAST_NAME, pro.admin3_MIDDLE_INITIAL, NULL, 
pro.admin3_phone_number, pro.admin3_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[Application] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-3'
WHERE pro.admin3_LAST_NAME IS NOT NULL 