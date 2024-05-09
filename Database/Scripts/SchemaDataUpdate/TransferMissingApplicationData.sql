--Missing PI info
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
SELECT app.ApplicationId, capt.ClientApplicationPersonnelTypeId, pro.PI_First_Name, pro.PI_LAST_NAME, pro.PI_MIDDLE_INITIAL, pro.PI_ORG_NAME, 
pro.PI_PHONE_NUMBER, pro.PI_EMAIL, 1, NULL, app.ModifiedBy, app.ModifiedDate
FROM [$(P2RMIS)].dbo.PRO_Proposal pro INNER JOIN
	[ViewApplication] app ON pro.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationPersonnelType] capt ON cat.ClientId = capt.ClientId AND capt.ApplicationPersonnelTypeAbbreviation = 'PI'
WHERE (pro.PI_LAST_NAME IS NOT NULL OR pro.PI_ORG_NAME IS NOT NULL) AND NOT EXISTS (Select 'X' FROM ViewApplicationPersonnel WHERE ApplicationId = app.ApplicationId AND ClientApplicationPersonnelTypeId = capt.ClientApplicationPersonnelTypeId);

--Missing review statuses
INSERT INTO [ApplicationReviewStatus]
           ([PanelApplicationId]
           ,[ReviewStatusId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panapp.PanelApplicationId, CASE opanapp.Triaged WHEN 1 THEN 1 ELSE 2 END, panapp.ModifiedBy, panapp.ModifiedDate
FROM [$(P2RMIS)].dbo.[PRG_Panel_Proposals] opanapp INNER JOIN
[ViewApplication] app ON opanapp.Log_No = app.LogNumber INNER JOIN
[ViewSessionPanel] sp ON opanapp.Panel_ID = sp.LegacyPanelId INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND sp.SessionPanelId = panapp.SessionPanelId
WHERE panapp.PanelApplicationId NOT IN (Select PanelApplicationId FROM ViewApplicationReviewStatus ars INNER JOIN ReviewStatus rs ON ars.ReviewStatusId = rs.ReviewStatusId WHERE ReviewStatusTypeId = 1);


