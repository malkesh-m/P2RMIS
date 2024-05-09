INSERT INTO [dbo].[ApplicationStageStepDiscussionComment]
           ([ApplicationStageStepDiscussionId]
           ,[Comment]
		   ,[CreatedBy]
		   ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ApplicationStageStepDiscussion.ApplicationStageStepDiscussionId, CAST(PRG_Panel_Proposal_Review_Discussion.Discussion_Text AS nvarchar(4000)), U.UserId, PRG_Panel_Proposal_Review_Discussion.LAST_UPDATE_DATE, U.UserId, PRG_Panel_Proposal_Review_Discussion.LAST_UPDATE_DATE
FROM ApplicationStageStepDiscussion INNER JOIN
	ApplicationStageStep ON ApplicationStageStepDiscussion.ApplicationStageStepId = ApplicationStageStep.ApplicationStageStepId INNER JOIN
	ViewApplicationStage ApplicationStage ON ApplicationStageStep.ApplicationStageId = ApplicationStage.ApplicationStageId INNER JOIN
	ViewPanelApplication PanelApplication ON ApplicationStage.PanelApplicationId = PanelApplication.PanelApplicationId INNER JOIN
	[ViewApplication] [Application] ON PanelApplication.ApplicationId = [Application].ApplicationId INNER JOIN
	[$(P2RMIS)].dbo.PRG_Panel_Proposal_Review_Discussion PRG_Panel_Proposal_Review_Discussion ON [Application].LogNumber = PRG_Panel_Proposal_Review_Discussion.LOG_NO INNER JOIN
	[$(P2RMIS)].dbo.PRG_Participants PRG_Participants ON PRG_Panel_Proposal_Review_Discussion.Prg_Part_ID = PRG_Participants.Prg_Part_ID INNER JOIN
	[$(P2RMIS)].dbo.PPL_People PPL_People ON PRG_Participants.Person_ID = PPL_People.Person_ID LEFT OUTER JOIN
	[ViewUser] U ON PPL_People.Person_ID = U.PersonID
WHERE NOT EXISTS (Select 'X' FROM ApplicationStageStepDiscussionComment WHERE DeletedFlag = 0 AND ApplicationStageStepDiscussionId = ApplicationStageStepDiscussion.ApplicationStageStepDiscussionId)
