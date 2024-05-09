INSERT INTO [dbo].[MeetingRegistration]
           ([PanelUserAssignmentId]
           ,[LegacyMrId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT PanelUserAssignment.PanelUserAssignmentId, MTG_Registration.MR_ID, VUN.UserId, MTG_Registration.Last_Update_Date
FROM	[P2RMIS-Dev].dbo.MTG_Registration MTG_Registration INNER JOIN
		ViewPanelUserAssignment PanelUserAssignment ON MTG_Registration.Prg_Part_ID = PanelUserAssignment.LegacyParticipantId LEFT OUTER JOIN
		ViewLegacyUserNameToUserId VUN ON MTG_Registration.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewMeetingRegistration WHERE LegacyMrId = MTG_Registration.MR_ID)