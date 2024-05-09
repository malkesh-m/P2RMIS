CREATE PROCEDURE [dbo].[uspCpritFullReviewDataTransfer]
@LogNoList            nvarchar(max) ,
@CurrentPanelID int,
@NewPanelID int,
@ProgramMechanismId int,
@UserID int
AS

SET XACT_ABORT ON;
DECLARE @CurrentDate smalldatetime,
@LogXML xml,
@LogNo nvarchar(12),
@NewApplicationId int


SELECT  @CurrentDate = dbo.GetP2rmisDateTime()
SELECT @LogXML = CONVERT(xml,'<root><s>' + REPLACE(@LogNoList,',','</s><s>') + '</s></root>')


DECLARE Cur_LogNos CURSOR LOCAL SCROLL READ_ONLY FOR
SELECT T.c.value('.','varchar(12)')
FROM @LogXML.nodes('/root/s') T(c)

OPEN Cur_LogNos
FETCH FIRST From Cur_LogNos INTO @LogNo
WHILE @@FETCH_STATUS = 0
BEGIN 


 ---Insert Application Data                   
  
     INSERT INTO [dbo].[Application]
           ([ProgramMechanismId]
           ,[ParentApplicationId]
           ,[LogNumber]
           ,[ApplicationTitle]
           ,[ResearchArea]
           ,[Keywords]
           ,[ProjectStartDate]
           ,[ProjectEndDate]
           ,[WithdrawnFlag]
           ,[WithdrawnBy]
           ,[WithdrawnDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
    (SELECT   @ProgramMechanismId, NULL, LEFT(LogNumber, 8), ApplicationTitle, ResearchArea, [Keywords],[ProjectStartDate] ,[ProjectEndDate] ,[WithdrawnFlag] ,[WithdrawnBy]
			,[WithdrawnDate],@UserID ,@CurrentDate ,@UserID ,@CurrentDate
    FROM         dbo.[ViewApplication]
     WHERE     (LogNumber = @LogNo))
	 SELECT @NewApplicationId = SCOPE_IDENTITY();
	 --Update parent reference of pre-eval app
	 UPDATE [Application] SET ParentApplicationId = @NewApplicationId
	 WHERE LogNumber = @LogNo AND DeletedFlag = 0;
      ---ApplicationCompliance
	  INSERT INTO [dbo].[ApplicationCompliance]
           ([ApplicationId]
           ,[ComplianceStatusId]
           ,[Comment]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT @NewApplicationId
      ,[ComplianceStatusId]
      ,[ApplicationCompliance].[Comment]
      ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	FROM [dbo].[ApplicationCompliance]
	INNER JOIN [ViewApplication] ON [ApplicationCompliance].ApplicationId = ViewApplication.ApplicationId
	WHERE ViewApplication.LogNumber = @LogNo AND ApplicationCompliance.DeletedFlag = 0;

      ---ApplicationPersonnel
     INSERT INTO [dbo].[ApplicationPersonnel]
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
           ,[StateAbbreviation]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	  SELECT @NewApplicationId
      ,[ClientApplicationPersonnelTypeId]
      ,[FirstName]
      ,[LastName]
      ,[MiddleInitial]
      ,[OrganizationName]
      ,[PhoneNumber]
      ,[EmailAddress]
      ,[PrimaryFlag]
      ,[Source]
      ,[StateAbbreviation]
      ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	  FROM ViewApplicationPersonnel INNER JOIN
		ViewApplication ON ViewApplicationPersonnel.ApplicationId = ViewApplication.ApplicationId
	  WHERE ViewApplication.LogNumber = @LogNo;
 

      ---ApplicationText
		INSERT INTO [dbo].[ApplicationText]
           ([ApplicationId]
           ,[ClientApplicationTextTypeId]
           ,[BodyText]
           ,[AbstractFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT @NewApplicationId
			  ,[ClientApplicationTextTypeId]
			  ,[BodyText]
			  ,[AbstractFlag]
			  ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
		  FROM [dbo].[ViewApplicationText]
		  INNER JOIN ViewApplication ON [ViewApplicationText].ApplicationId = ViewApplication.ApplicationId
		  WHERE ViewApplication.LogNumber = @LogNo;

      ----ApplicationBudget
	INSERT INTO [dbo].[ApplicationBudget]
           ([ApplicationId]
           ,[DirectCosts]
           ,[IndirectCosts]
           ,[TotalFunding]
           ,[Comments]
           ,[CommentModifiedBy]
           ,[CommentModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT @NewApplicationId
      ,[DirectCosts]
      ,[IndirectCosts]
      ,[TotalFunding]
      ,[Comments]
      ,[CommentModifiedBy]
      ,[CommentModifiedDate]
      ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
  FROM [dbo].[ApplicationBudget]
  INNER JOIN ViewApplication ON [ApplicationBudget].ApplicationId = ViewApplication.ApplicationId
  WHERE ViewApplication.LogNumber = @LogNo  AND ApplicationBudget.DeletedFlag = 0;
     
---PanelApplication
   INSERT INTO [dbo].[PanelApplication]
           ([SessionPanelId]
           ,[ApplicationId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT @NewPanelID
      ,@NewApplicationId
      ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
      ---ApplicationReviewStatus
	  INSERT INTO [dbo].[ApplicationReviewStatus]
           ([PanelApplicationId]
           ,[ReviewStatusId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT PanelApplicationId,
		2, @UserID ,@CurrentDate ,@UserID ,@CurrentDate
		FROM ViewPanelApplication
		WHERE ViewPanelApplication.ApplicationId = @NewApplicationId AND ViewPanelApplication.SessionPanelId = @NewPanelID;
	  ---ApplicationStage    
	   INSERT INTO [dbo].[ApplicationStage]
           ([PanelApplicationId]
           ,[ReviewStageId]
           ,[StageOrder]
           ,[ActiveFlag]
           ,[AssignmentVisibilityFlag]
           ,[AssignmentReleaseDate]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		SELECT PanelApplicationId,ReviewStageId,ReviewStageId,1,0,NULL,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
		FROM ViewPanelApplication CROSS JOIN
		ReviewStage 
		WHERE ViewPanelApplication.ApplicationId = @NewApplicationId AND ViewPanelApplication.SessionPanelId = @NewPanelID AND ReviewStage.ReviewStageId IN (1,2);
	  ---ApplicationStageStep
	  INSERT INTO [dbo].[ApplicationStageStep]
           ([ApplicationStageId]
           ,[PanelStageStepId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ViewApplicationStage.ApplicationStageId, ViewPanelStageStep.PanelStageStepId,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	FROM ViewApplicationStage 
	INNER JOIN ViewPanelApplication ON ViewApplicationStage.PanelApplicationId = ViewPanelApplication.PanelApplicationId
	INNER JOIN ViewPanelStage ON ViewPanelApplication.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewApplicationStage.ReviewStageId = ViewPanelStage.ReviewStageId
	INNER JOIN ViewPanelStageStep ON ViewPanelStage.PanelStageId = ViewPanelStageStep.PanelStageId
	WHERE ViewPanelApplication.ApplicationId = @NewApplicationId AND ViewPanelApplication.SessionPanelId = @NewPanelID;
    ---PanelApplicationReviewerExpertise   
	INSERT INTO [dbo].[PanelApplicationReviewerExpertise]
           ([PanelApplicationId]
           ,[PanelUserAssignmentId]
           ,[ClientExpertiseRatingId]
           ,[ExpertiseComments]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT NewPanelApplication.PanelApplicationId, NewPanelUserAssignment.PanelUserAssignmentId, OldExpertise.ClientExpertiseRatingId, OldExpertise.ExpertiseComments,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	FROM ViewPanelApplication OldPanelApplication
	INNER JOIN ViewApplication OldApplication ON OldPanelApplication.ApplicationId = OldApplication.ApplicationId
	INNER JOIN ViewPanelApplicationReviewerExpertise OldExpertise ON OldPanelApplication.PanelApplicationId = OldExpertise.PanelApplicationId
	INNER JOIN ViewPanelUserAssignment OldPanelUserAssignment ON OldExpertise.PanelUserAssignmentId = OldPanelUserAssignment.PanelUserAssignmentId
	INNER JOIN ViewPanelUserAssignment NewPanelUserAssignment ON OldPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	INNER JOIN ViewPanelApplication NewPanelApplication ON NewPanelUserAssignment.SessionPanelId = NewPanelApplication.SessionPanelId
	WHERE OldApplication.LogNumber = @LogNo AND OldPanelApplication.SessionPanelId = @CurrentPanelID AND NewPanelApplication.ApplicationId = @NewApplicationId AND NewPanelApplication.SessionPanelId = @NewPanelID;
   ---PanelApplicationReviewerCoiDetail
      INSERT INTO [dbo].[PanelApplicationReviewerCoiDetail]
           ([PanelApplicationReviewerExpertiseId]
           ,[ClientCoiTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT NewExpertise.[PanelApplicationReviewerExpertiseId], OldCoi.[ClientCoiTypeId] ,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	FROM ViewPanelApplication OldPanelApplication
	INNER JOIN ViewApplication OldApplication ON OldPanelApplication.ApplicationId = OldApplication.ApplicationId
	INNER JOIN ViewPanelApplicationReviewerExpertise OldExpertise ON OldPanelApplication.PanelApplicationId = OldExpertise.PanelApplicationId
	INNER JOIN ViewPanelApplicationReviewerCoiDetail OldCoi ON OldExpertise.PanelApplicationReviewerExpertiseId = OldCoi.PanelApplicationReviewerExpertiseId
	INNER JOIN ViewPanelUserAssignment OldPanelUserAssignment ON OldExpertise.PanelUserAssignmentId = OldPanelUserAssignment.PanelUserAssignmentId
	INNER JOIN ViewPanelUserAssignment NewPanelUserAssignment ON OldPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	INNER JOIN ViewPanelApplication NewPanelApplication ON NewPanelUserAssignment.SessionPanelId = NewPanelApplication.SessionPanelId
	INNER JOIN ViewPanelApplicationReviewerExpertise NewExpertise ON NewPanelApplication.PanelApplicationId = NewExpertise.PanelApplicationId AND NewPanelUserAssignment.PanelUserAssignmentId = NewExpertise.PanelUserAssignmentId
	WHERE OldApplication.LogNumber = @LogNo AND OldPanelApplication.SessionPanelId = @CurrentPanelID AND NewPanelApplication.ApplicationId = @NewApplicationId AND NewPanelApplication.SessionPanelId = @NewPanelID;
                                                
---PanelApplicationReviewerAssignment
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
	SELECT NewPanelApplication.PanelApplicationId, NewPanelUserAssignment.PanelUserAssignmentId, OldAssignment.ClientAssignmentTypeId, OldAssignment.SortOrder, OldAssignment.LegacyProposalAssignmentId,@UserID ,@CurrentDate ,@UserID ,@CurrentDate
	FROM ViewPanelApplication OldPanelApplication
	INNER JOIN ViewApplication OldApplication ON OldPanelApplication.ApplicationId = OldApplication.ApplicationId
	INNER JOIN ViewPanelApplicationReviewerAssignment OldAssignment ON OldPanelApplication.PanelApplicationId = OldAssignment.PanelApplicationId
	INNER JOIN ViewPanelUserAssignment OldPanelUserAssignment ON OldAssignment.PanelUserAssignmentId = OldPanelUserAssignment.PanelUserAssignmentId
	INNER JOIN ViewPanelUserAssignment NewPanelUserAssignment ON OldPanelUserAssignment.UserId = NewPanelUserAssignment.UserId
	INNER JOIN ViewPanelApplication NewPanelApplication ON NewPanelUserAssignment.SessionPanelId = NewPanelApplication.SessionPanelId
	WHERE OldApplication.LogNumber = @LogNo AND OldPanelApplication.SessionPanelId = @CurrentPanelID AND NewPanelApplication.ApplicationId = @NewApplicationId AND NewPanelApplication.SessionPanelId = @NewPanelID;

  FETCH NEXT FROM Cur_LogNos INTO @LogNo
END
CLOSE cur_LogNos
DEALLOCATE cur_LogNos