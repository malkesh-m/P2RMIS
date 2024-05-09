INSERT INTO [dbo].[PanelApplicationReviewerAssignment]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientAssignmentTypeId]
           ,[SortOrder]
           ,[LegacyProposalAssignmentId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT PanelApplication.PanelApplicationId, PanelUserAssignment.PanelUserAssignmentId, ClientAssignmentType.ClientAssignmentTypeId,
	inserted.Sort_Order, inserted.PA_ID, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM [$(P2RMIS)].dbo.PRG_Proposal_Assignments inserted INNER JOIN
		[dbo].ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[dbo].ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[dbo].ViewPanelApplication PanelApplication ON PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId AND
		Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		[dbo].ViewProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		[dbo].ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		[dbo].AssignmentType AssignmentType ON inserted.Assignment_ID = AssignmentType.LegacyAssignmentId INNER JOIN
		[dbo].ClientAssignmentType ClientAssignmentType ON AssignmentType.AssignmentTypeId = ClientAssignmentType.AssignmentTypeId AND
		ClientAwardType.ClientId = ClientAssignmentType.ClientId LEFT OUTER JOIN
		dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplicationReviewerAssignment WHERE LegacyProposalAssignmentId = pa.PA_ID)