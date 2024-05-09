INSERT INTO [dbo].[ReviewerEvaluation]
           ([PanelUserAssignmentId]
           ,[Rating]
           ,[Comments]
           ,[RecommendChairFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelUserAssignment.PanelUserAssignmentId,  PRG_Part_Evaluation.Rating,  PRG_Part_Evaluation.Comments,  ISNULL(PRG_Part_Evaluation.Rec_Chair, 0), VUN2.UserId,  PRG_Part_Evaluation.Evaluation_Date, VUN.UserId,  PRG_Part_Evaluation.Last_Update_Date
FROM [P2RMIS-Dev].[dbo].PRG_Part_Evaluation 
	INNER JOIN ViewPanelUserAssignment PanelUserAssignment ON  PRG_Part_Evaluation.Prg_Part_ID = PanelUserAssignment.LegacyParticipantId
	LEFT OUTER JOIN [User] VUN2 ON PRG_Part_Evaluation.Owner_Person_ID = VUN2.PersonId
	LEFT OUTER JOIN [ViewLegacyUserNameToUserId] VUN ON PRG_Part_Evaluation.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewReviewerEvaluation WHERE PanelUserAssignmentId = PanelUserAssignment.PanelUserAssignmentId AND CreatedBy = VUN2.UserID) AND VUN2.UserID IS NOT NULL