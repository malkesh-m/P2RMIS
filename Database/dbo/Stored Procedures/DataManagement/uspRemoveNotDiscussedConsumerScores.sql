CREATE PROCEDURE [dbo].[uspRemoveNotDiscussedConsumerScores]
	@SessionPanelId int
AS
DECLARE @LastUpdateDateTime datetime2(0) = dbo.GetP2rmisDateTime();

UPDATE ApplicationWorkflowStepElementContent SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = @LastUpdateDateTime
FROM ApplicationWorkflowStepElementContent
INNER JOIN ViewApplicationWorkflowStepElement ON ApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId
INNER JOIN ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId
INNER JOIN ViewApplicationWorkflow ON ViewApplicationWorkflowStep.ApplicationWorkflowId = ViewApplicationWorkflow.ApplicationWorkflowId
INNER JOIN ViewApplicationStage ON ViewApplicationWorkflow.ApplicationStageId = ViewApplicationStage.ApplicationStageId
INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
INNER JOIN ViewPanelUserAssignment ON ViewApplicationWorkflow.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId
INNER JOIN ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
INNER JOIN ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId
INNER JOIN ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId
WHERE ViewPanelApplication.SessionPanelId = @SessionPanelId AND ClientParticipantType.ConsumerFlag = 1 AND ReviewStatus.ReviewStatusId = 1 AND
ApplicationWorkflowStepElementContent.DeletedFlag = 0 AND ViewApplicationStage.ReviewStageId = 1 AND ReviewStatus.ReviewStatusTypeId = 1;
--Re-insert the recently deleted with scores removed
INSERT INTO ApplicationWorkflowStepElementContent
           ([ApplicationWorkflowStepElementId]
           ,[Score]
           ,[ContentText]
           ,[ContentTextNoMarkup]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [ApplicationWorkflowStepElementId]
           ,NULL
           ,[ContentText]
           ,[ContentTextNoMarkup]
           ,[Abstain]
           ,[CritiqueRevised]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
FROM ApplicationWorkflowStepElementContent
WHERE DeletedFlag = 1 AND DeletedBy = 10 AND DeletedDate = @LastUpdateDateTime;