
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
--PI Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI_First_Name, pro.PI_LAST_NAME, pro.PI_MIDDLE_INITIAL, pro.PI_ORG_NAME, 
pro.PI_PHONE_NUMBER, pro.PI_EMAIL, 1, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI'
WHERE (pro.PI_LAST_NAME IS NOT NULL OR pro.PI_ORG_NAME IS NOT NULL) AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--PI2 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI2_First_Name, pro.PI2_LAST_NAME, pro.PI2_MIDDLE_INITIAL, NULL, 
NULL, pro.PI2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI2'
WHERE pro.PI2_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--Admin1 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin1_First_Name, pro.admin1_LAST_NAME, pro.admin1_MIDDLE_INITIAL, pro.admin1_ORG_NAME, 
pro.admin1_phone_number, pro.admin1_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-1'
WHERE (pro.admin1_LAST_NAME IS NOT NULL OR pro.admin1_ORG_NAME IS NOT NULL) AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--admin2 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin2_First_Name, pro.admin2_LAST_NAME, pro.admin2_MIDDLE_INITIAL, NULL, 
pro.admin2_phone_number, pro.admin2_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-2'
WHERE pro.admin2_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--admin3 Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.admin3_First_Name, pro.admin3_LAST_NAME, pro.admin3_MIDDLE_INITIAL, NULL, 
pro.admin3_phone_number, pro.admin3_EMAIL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'admin-3'
WHERE pro.admin3_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--Mentor Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.mentor_First_Name, pro.mentor_LAST_NAME, pro.mentor_MIDDLE_INITIAL, NULL, 
NULL, NULL, 0, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'mentor'
WHERE pro.mentor_LAST_NAME IS NOT NULL AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)
UNION ALL
--COI Information
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, procoi.FirstName, procoi.LASTNAME, NULL, procoi.orgname, 
	procoi.phone, procoi.email, 0, procoi.coisource, NULL, procoi.datetimestamp
	FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[$(P2RMIS)].dbo.PRO_COI procoi ON pro.Log_No = procoi.Log_No INNER JOIN 
	[$(P2RMIS)].dbo.PRO_COI_Type procoitype ON procoi.coitypeid = procoitype.coitypeid INNER JOIN 
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND procoitype.coitype = capt.ApplicationPersonnelType
WHERE NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId)