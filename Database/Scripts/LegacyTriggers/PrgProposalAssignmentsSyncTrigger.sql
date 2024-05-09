CREATE TRIGGER [PrgProposalAssignmentsSyncTrigger]
ON [$(P2RMIS)].[dbo].[PRG_Proposal_Assignments]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--NOTE: Much of the infrastructure for assignments/workflow/evaluation will be taken care of in the PRG_Critiques trigger.
	--UPDATE
	IF EXISTS (Select * FROM inserted)
	BEGIN
	--P2RMIS 1.0 is strange in that it adds assignments in a 2 step process.  To preserve data integrity we insert only on the second step which is an update
	--If it doesn't exist in 2.0 we add it here
	IF NOT EXISTS (Select * FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[PanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment ON 
	inserted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId WHERE PanelApplicationReviewerAssignment.DeletedFlag = 0) AND NOT EXISTS (Select 'X' FROM inserted WHERE assignment_id IS NULL)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[PanelApplicationReviewerAssignment]
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
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].ViewPanelUserAssignment PanelUserAssignment ON inserted.PRG_Part_ID = PanelUserAssignment.LegacyParticipantId INNER JOIN
		[$(DatabaseName)].[dbo].ViewApplication Application ON inserted.Log_No = Application.LogNumber INNER JOIN
		[$(DatabaseName)].[dbo].ViewPanelApplication PanelApplication ON PanelUserAssignment.SessionPanelId = PanelApplication.SessionPanelId AND
		Application.ApplicationId = PanelApplication.ApplicationId INNER JOIN
		[$(DatabaseName)].[dbo].ViewProgramMechanism ProgramMechanism ON Application.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAwardType ClientAwardType ON ProgramMechanism.ClientAwardTypeId = ClientAwardType.ClientAwardTypeId INNER JOIN
		[$(DatabaseName)].[dbo].AssignmentType AssignmentType ON inserted.Assignment_ID = AssignmentType.LegacyAssignmentId INNER JOIN
		[$(DatabaseName)].[dbo].ClientAssignmentType ClientAssignmentType ON AssignmentType.AssignmentTypeId = ClientAssignmentType.AssignmentTypeId AND
		ClientAwardType.ClientId = ClientAssignmentType.ClientId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	END
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerAssignment]
	SET SortOrder = inserted.Sort_Order, ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId,
	ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[PanelApplicationReviewerAssignment] PanelApplicationReviewerAssignment ON 
		inserted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId INNER JOIN
	[$(DatabaseName)].[dbo].[AssignmentType] AssignmentType ON inserted.Assignment_ID = AssignmentType.LegacyAssignmentId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAssignmentType] Old ON PanelApplicationReviewerAssignment.ClientAssignmentTypeId = Old.ClientAssignmentTypeId INNER JOIN
	[$(DatabaseName)].[dbo].[ClientAssignmentType] ClientAssignmentType ON Old.ClientId = ClientAssignmentType.ClientId AND 
		AssignmentType.AssignmentTypeId = ClientAssignmentType.AssignmentTypeId LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	END
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[PanelApplicationReviewerAssignment]
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].dbo.PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment ON 
		deleted.PA_ID = PanelApplicationReviewerAssignment.LegacyProposalAssignmentId
	WHERE PanelApplicationReviewerAssignment.DeletedFlag = 0
END
