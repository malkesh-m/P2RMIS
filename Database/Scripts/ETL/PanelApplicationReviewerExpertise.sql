INSERT INTO [PanelApplicationReviewerExpertise]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientExpertiseRatingId]
           ,[ExpertiseComments]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT panapp.PanelApplicationId, pua.PanelUserAssignmentId, Cer.ClientExpertiseRatingId, coi.Comments, vun.UserId, rp.LAST_UPDATE_DATE
FROM [P2RMIS-Dev].dbo.PRG_Reviewer_Preferences rp INNER JOIN
[ViewPanelUserAssignment] pua ON rp.Prg_Part_ID = pua.LegacyParticipantId INNER JOIN
[ViewApplication] app ON rp.Log_No = app.LogNumber INNER JOIN
[ViewPanelApplication] panapp ON app.ApplicationId = panapp.ApplicationId AND pua.SessionPanelId = panapp.SessionPanelId INNER JOIN
[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
[ClientAwardType] ca ON pm.ClientAwardTypeId = ca.ClientAwardTypeId INNER JOIN
[ClientExpertiseRating] cer ON rp.Rev_Pref = cer.RatingAbbreviation AND ca.ClientId = cer.ClientId LEFT OUTER JOIN
[P2RMIS-Dev].dbo.PRG_Reviewer_Preferences_COI coi ON rp.Prg_Part_ID = coi.Prg_Part_ID AND rp.Log_No = coi.Log_No LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun ON rp.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerExpertise WHERE PanelApplicationId = panapp.PanelApplicationId AND PanelUserAssignmentId = pua.PanelUserAssignmentId)