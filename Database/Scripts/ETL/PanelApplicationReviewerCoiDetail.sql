INSERT INTO [PanelApplicationReviewerCoiDetail]
           ([PanelApplicationReviewerExpertiseId]
           ,[ClientCoiTypeId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panre.PanelApplicationReviewerExpertiseId, cct.ClientCoiTypeId, vun.UserId, coi.LAST_UPDATE_DATE
FROM ViewPanelApplicationReviewerExpertise panre INNER JOIN
ViewPanelApplication panapp ON panre.PanelApplicationId = panapp.PanelApplicationId INNER JOIN 
ViewPanelUserAssignment pua ON panre.PanelUserAssignmentId = pua.PanelUserAssignmentId INNER JOIN
[ViewApplication] app ON panapp.ApplicationId = app.ApplicationId INNER JOIN
[$(P2RMIS)].dbo.PRG_Reviewer_Preferences_COI coi ON app.LogNumber = coi.Log_No AND pua.LegacyParticipantId = coi.Prg_Part_ID INNER JOIN
[$(P2RMIS)].dbo.PRG_REV_COI_Type_LU coitype ON coi.COI_Type = coitype.COI_Type INNER JOIN
ViewProgramMechanism pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
ClientAwardType cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
ClientCoiType cct ON cat.ClientId = cct.ClientId AND coitype.COI_Type_DESC = cct.CoiTypeDescription LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun ON coi.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (SELECT 'X' FROM ViewPanelApplicationReviewerCoiDetail WHERE PanelApplicationReviewerExpertiseId = panre.PanelApplicationReviewerExpertiseId)