CREATE PROCEDURE [dbo].[uspViewPanelDetails]
(
@PanelIdIn int,
@UserId int
)
AS
SET NOCOUNT ON
SET FMTONLY OFF
CREATE TABLE dbo.#Tmp_PanelDetails (
	PanelId int,
	ActiveApplication bit,
    ApplicationId nvarchar(20),
    [Order] int,
    LastName nvarchar(50),
    FirstName nvarchar(50),
    PIInstitution nvarchar(150),
    ApplicationTitle nvarchar(500),
    Disapproved bit,
    Triaged bit,
    AwardShortDescription nvarchar(50),
    ReviewTypeId int,
    PossibleScores int,
    ActualScores int,
    AverageOE decimal(4,1),
    CommentsCount int,
    COIs nvarchar(500),
    AssignmentSlots nvarchar(500),
    AssignmentTypes nvarchar(500),
    AssignmentNames nvarchar(500),
    AssignmentPartIds nvarchar(500),
    AssignmentCritiqueAvailable nvarchar(500),
	PanelApplicationId int,
	ReviewStatusId int,
	ApplicationReviewStatusId int,
	ReviewStatusName varchar(50),
	ApplicationIdentifier int,
	AdminNotesCount int,
	UserCoi bit,
	Adjectival bit
)
DECLARE
	@PanelId int,
	@ActiveApplication nvarchar(20),
    @ApplicationId nvarchar(20),
    @Order int,
    @LastName nvarchar(50),
    @FirstName nvarchar(50),
    @PIInstitution nvarchar(150),
    @ApplicationTitle nvarchar(500),
    @Disapproved bit,
    @Triaged bit,
    @AwardShortDescription nvarchar(50),
    @ReviewTypeId int,
    @PossibleScores int,
    @ActualScores int,
    @AverageOE decimal(4,1),
    @CommentsCount int,
    @PMScoringPhase nvarchar(50),
    @PMDeadline datetime2(0),
    @COIs nvarchar(500),
    @AssignmentSlots nvarchar(500),
    @AssignmentTypes nvarchar(500),
    @AssignmentNames nvarchar(500),
    @AssignmentPartIds nvarchar(500),
    @AssignmentCritiqueAvailable nvarchar(500),
    @UserSID varbinary(85),
	@PanelApplicationId int,
	@ReviewStatusId int,
	@ApplicationReviewStatusId int,
	@ReviewStatusName varchar(50),
	@ApplicationIdentifier int,
	@AdminNotesCount int,
	@UserCoi bit,
	@Adjectival bit
	
	SELECT @COIs = NULL
    SELECT @AssignmentSlots = NULL
    SELECT @AssignmentTypes = NULL
    SELECT @AssignmentNames = NULL
    SELECT @AssignmentPartIds = NULL
    SELECT @AssignmentCritiqueAvailable = NULL
    SET @UserSID = CAST(@UserId as varbinary(85))
	SELECT @CommentsCount = 0
	

/* 
	Declare cursor to get consumer person information for program and FYs
 */
	DECLARE cur_PanelInfo CURSOR LOCAL SCROLL READ_ONLY FOR
	SELECT     ViewPanelApplication.SessionPanelId AS PanelID, CAST(CASE WHEN ReviewStatus.ReviewStatusId = 8 THEN 1 ELSE 0 END AS bit) AS ActiveApplication, ViewApplication.LogNumber AS ApplicationID, ViewPanelApplication.ReviewOrder AS OrderofReview, ViewApplicationPersonnel.LastName AS PILastName, ViewApplicationPersonnel.FirstName AS PIFirstName,  
                      ViewApplicationPersonnel.OrganizationName AS PIOrgName, ViewApplication.ApplicationTitle, CAST(CASE WHEN ReviewStatus.ReviewStatusId = 5 THEN 1 ELSE 0 END AS bit) AS Disapproved, CAST(CASE WHEN ReviewStatus.ReviewStatusId = 1 THEN 1 ELSE 0 END AS bit) AS Triaged, ViewClientAwardType.AwardAbbreviation AS AwardShortDesc, NULL AS ReviewTypeID, --Whether it is blinded should go here 
                      (SELECT     COUNT(PanelUserAssignmentId) 
						FROM         ViewPanelUserAssignment INNER JOIN
						ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
						WHERE ClientParticipantType.ReviewerFlag = 1 AND PanelUserAssignmentId NOT IN 
							(SELECT PanelUserAssignmentId 
							From ViewPanelApplicationReviewerAssignment INNER JOIN
							ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId
							Where AssignmentTypeId = 8 And PanelApplicationId = ViewPanelApplication.PanelApplicationId)
						GROUP BY SessionPanelId
						HAVING      (SessionPanelId = @PanelIdIn)) AS PossibleScores,  
                      SUM(CASE WHEN OverallScores.Score IS NOT NULL OR OverallScores.Abstain = 1 THEN 1 ELSE 0 END) AS ActualScores, ROUND(AVG(OverallScores.Score), 1) AS AverageOE,
                       PanelStageLastStep.StepName AS ScoringPhase, PanelStageLastStep.EndDate AS PhaseEnd, ViewPanelApplication.PanelApplicationId, ReviewStatus.ReviewStatusId, ViewApplicationReviewStatus.ApplicationReviewStatusId, ReviewStatus.ReviewStatusLabel,
					   ViewPanelApplication.ApplicationId, CASE WHEN ViewApplicationBudget.Comments IS NOT NULL THEN 1 ELSE 0 END,
					   CASE ClientAssignmentType.AssignmentTypeId WHEN 8 THEN 1 ELSE 0 END,CASE WHEN OverallScores.ScoreType = 'Adjectival' THEN 1 ELSE 0 END as Adjectival
	FROM         ViewPanelApplication INNER JOIN
				ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
				ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId AND ReviewStatus.ReviewStatusTypeId = 1 INNER JOIN
				ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId LEFT OUTER JOIN
				ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId AND ViewApplicationPersonnel.PrimaryFlag = 1 INNER JOIN
				ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
				ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
				ViewApplicationStage ON ViewPanelApplication.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 2 LEFT OUTER JOIN
				ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId LEFT OUTER JOIN
				ViewApplicationWorkflowStep ON ViewApplicationWorkflow.ApplicationWorkflowId = ViewApplicationWorkflowStep.ApplicationWorkflowId LEFT OUTER JOIN
                (Select ViewApplicationWorkflowStepElementContent.Score, ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId, ViewApplicationWorkflowStepElementContent.Abstain, ClientScoringScale.ScoreType
				 FROM ViewApplicationWorkflowStepElement LEFT OUTER JOIN
                 ViewApplicationWorkflowStepElementContent ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepElementId = ViewApplicationWorkflowStepElementContent.ApplicationWorkflowStepElementId INNER JOIN
				 ViewApplicationWorkflowStep ON ViewApplicationWorkflowStepElement.ApplicationWorkflowStepId = ViewApplicationWorkflowStep.ApplicationWorkflowStepId INNER JOIN
				 ViewApplicationTemplateElement ON ViewApplicationWorkflowStepElement.ApplicationTemplateElementId = ViewApplicationTemplateElement.ApplicationTemplateElementId INNER JOIN
				 ViewMechanismTemplateElement ON ViewApplicationTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElement.MechanismTemplateElementId LEFT OUTER JOIN
				 ViewMechanismTemplateElementScoring ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElementScoring.MechanismTemplateElementId AND ViewApplicationWorkflowStep.StepTypeId = ViewMechanismTemplateElementScoring.StepTypeId LEFT OUTER JOIN
				 ClientScoringScale ON ViewMechanismTemplateElementScoring.ClientScoringId = ClientScoringScale.ClientScoringId
				  WHERE ViewMechanismTemplateElement.OverallFlag = 1)  OverallScores ON ViewApplicationWorkflowStep.ApplicationWorkflowStepId = OverallScores.ApplicationWorkflowStepId INNER JOIN
				ViewPanelStage ON ViewPanelApplication.SessionPanelId = ViewPanelStage.SessionPanelId AND ViewPanelStage.ReviewStageId = 1 OUTER APPLY --Pre-meeting critique deadline
				udfPanelStageLastStep(ViewPanelStage.PanelStageId) AS PanelStageLastStep LEFT OUTER JOIN
				ViewApplicationBudget ON ViewPanelApplication.ApplicationId = ViewApplicationBudget.ApplicationId LEFT OUTER JOIN
				ViewPanelUserAssignment ON ViewPanelApplication.SessionPanelId = ViewPanelUserAssignment.SessionPanelId AND ViewPanelUserAssignment.UserId = @UserId LEFT OUTER JOIN
				ViewPanelApplicationReviewerAssignment ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationReviewerAssignment.PanelApplicationId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId LEFT OUTER JOIN
				ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId
	WHERE ViewPanelApplication.SessionPanelId = @PanelIdIn
	GROUP BY ViewPanelApplication.SessionPanelId, ReviewStatus.ReviewStatusId, ViewApplication.LogNumber, ViewPanelApplication.ReviewOrder, ViewApplicationPersonnel.LastName, ViewApplicationPersonnel.FirstName,  
                      ViewApplicationPersonnel.OrganizationName, ViewApplication.ApplicationTitle, ViewClientAwardType.AwardAbbreviation,  
                      ViewPanelApplication.PanelApplicationId, PanelStageLastStep.StepName, PanelStageLastStep.EndDate, ReviewStatus.ReviewStatusId, ViewApplicationReviewStatus.ApplicationReviewStatusId, ReviewStatus.ReviewStatusLabel,
					  ViewPanelApplication.ApplicationId, ViewApplicationBudget.Comments, ClientAssignmentType.AssignmentTypeId, OverallScores.ScoreType
	
	OPEN cur_PanelInfo 
		FETCH  FIRST FROM cur_PanelInfo INTO @PanelId, @ActiveApplication, @ApplicationId, @Order, @LastName, @FirstName, @PIInstitution, @ApplicationTitle, @Disapproved, @Triaged, @AwardShortDescription, @ReviewTypeId, @PossibleScores, @ActualScores, @AverageOE, @PMScoringPhase, @PMDeadline, @PanelApplicationId, @ReviewStatusId, @ApplicationReviewStatusId, @ReviewStatusName, @ApplicationIdentifier, @AdminNotesCount, @UserCoi, @Adjectival
		WHILE @@FETCH_STATUS = 0
		
		BEGIN
			SELECT     @COIs=COALESCE(@COIs + '<br />','')+LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.Lastname
			FROM         ViewPanelApplicationReviewerAssignment INNER JOIN
								  ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
								  ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
								  ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
								  ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID
			WHERE     (ClientAssignmentType.AssignmentTypeID = 8) AND (ViewPanelUserAssignment.SessionPanelId = @PanelId) AND (ViewPanelApplicationReviewerAssignment.PanelApplicationId = @PanelApplicationId)
			--Query to return comma delimited list of reviewer assignments.  Critiques are considered available only after the critique deadline has passed and the reviewer has submitted their critique.
			SELECT     @AssignmentSlots=COALESCE(@AssignmentSlots + ';','') + CAST(ViewPanelApplicationReviewerAssignment.SortOrder as nvarchar(4)), @AssignmentTypes=COALESCE(@AssignmentTypes + ';','') + '(' + ClientParticipantType.ParticipantTypeAbbreviation + ')',
						@AssignmentNames=COALESCE(@AssignmentNames + ';','')+LEFT(ViewUserInfo.FirstName, 1) + '. ' + ViewUserInfo.Lastname, @AssignmentPartIds=COALESCE(@AssignmentPartIds + ';','') + CAST(ViewPanelUserAssignment.PanelUserAssignmentId as nvarchar(10)), 
						@AssignmentCritiqueAvailable = Coalesce(@AssignmentCritiqueAvailable + ';','') + CASE WHEN Critique.Resolution = 0 OR @PMDeadline > dbo.GetP2rmisDateTime() THEN 'false' ELSE 'true' END 
			FROM         ViewPanelApplicationReviewerAssignment INNER JOIN
								  ClientAssignmentType ON ViewPanelApplicationReviewerAssignment.ClientAssignmentTypeId = ClientAssignmentType.ClientAssignmentTypeId INNER JOIN
								  ViewPanelUserAssignment ON ViewPanelApplicationReviewerAssignment.PanelUserAssignmentId = ViewPanelUserAssignment.PanelUserAssignmentId INNER JOIN
								  ClientParticipantType ON ViewPanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId INNER JOIN
								  ViewUser ON ViewPanelUserAssignment.UserId = ViewUser.UserID INNER JOIN
								  ViewUserInfo ON ViewUser.UserID = ViewUserInfo.UserID LEFT OUTER JOIN
								  ViewApplicationStage ON ViewPanelApplicationReviewerAssignment.PanelApplicationId = ViewApplicationStage.PanelApplicationId AND ViewApplicationStage.ReviewStageId = 1 LEFT OUTER JOIN
								  ViewApplicationWorkflow ON ViewApplicationStage.ApplicationStageId = ViewApplicationWorkflow.ApplicationStageId AND ViewPanelUserAssignment.PanelUserAssignmentId = ViewApplicationWorkflow.PanelUserAssignmentId OUTER APPLY
								  udfApplicationWorkflowLastStep(ViewApplicationWorkflow.ApplicationWorkflowId) AS Critique
			WHERE     (ClientAssignmentType.AssignmentTypeID <> 8) AND (ViewPanelApplicationReviewerAssignment.PanelApplicationId = @PanelApplicationId)
			ORDER BY ViewPanelApplicationReviewerAssignment.SortOrder;
			

			
			SELECT @CommentsCount = COUNT(UserApplicationComment.UserApplicationCommentID)
			FROM      UserApplicationComment INNER JOIN
								  CommentType ON UserApplicationComment.CommentTypeID = CommentType.CommentTypeID
			WHERE  ('Access ' + CommentType.CommentTypeName IN
									  (SELECT SystemOperation.OperationName
										 FROM      UserSystemRole INNER JOIN
										 RoleTask ON UserSystemRole.SystemRoleId = RoleTask.SystemRoleId INNER JOIN
										 TaskOperation ON RoleTask.SystemTaskId = TaskOperation.SystemTaskId INNER JOIN
										 SystemOperation ON TaskOperation.SystemOperationId = SystemOperation.SystemOperationId
										 WHERE UserSystemRole.UserID = @UserId)) AND UserApplicationComment.DeletedFlag = 0
			GROUP BY UserApplicationComment.PanelApplicationId
			HAVING (UserApplicationComment.PanelApplicationId = @PanelApplicationId)
			
		
			INSERT INTO dbo.#Tmp_PanelDetails (PanelId, ActiveApplication, ApplicationId, [Order], LastName, FirstName, PIInstitution, ApplicationTitle, Disapproved, Triaged, AwardShortDescription, ReviewTypeId, PossibleScores, ActualScores, AverageOE, CommentsCount, COIs, AssignmentSlots, AssignmentTypes, AssignmentNames, AssignmentPartIds, AssignmentCritiqueAvailable, PanelApplicationId, ReviewStatusId, ApplicationReviewStatusId, ReviewStatusName, ApplicationIdentifier, AdminNotesCount, UserCoi, Adjectival)
			VALUES (@PanelId, @ActiveApplication, @ApplicationId, @Order, @LastName, @FirstName, @PIInstitution, @ApplicationTitle, @Disapproved, @Triaged, @AwardShortDescription, @ReviewTypeId, @PossibleScores, @ActualScores, @AverageOE, @CommentsCount, @COIs, @AssignmentSlots, @AssignmentTypes, @AssignmentNames, @AssignmentPartIds, @AssignmentCritiqueAvailable, @PanelApplicationId, @ReviewStatusId, @ApplicationReviewStatusId, @ReviewStatusName, @ApplicationIdentifier, @AdminNotesCount, @UserCoi, @Adjectival)
			FETCH  NEXT FROM cur_PanelInfo INTO @PanelId, @ActiveApplication, @ApplicationId, @Order, @LastName, @FirstName, @PIInstitution, @ApplicationTitle, @Disapproved, @Triaged, @AwardShortDescription, @ReviewTypeId, @PossibleScores, @ActualScores, @AverageOE, @PMScoringPhase, @PMDeadline, @PanelApplicationId, @ReviewStatusId, @ApplicationReviewStatusId, @ReviewStatusName, @ApplicationIdentifier, @AdminNotesCount, @UserCoi, @Adjectival
			SELECT @COIs = NULL
			SELECT @AssignmentSlots = NULL
			SELECT @AssignmentTypes = NULL
			SELECT @AssignmentNames = NULL
			SELECT @AssignmentPartIds = NULL
			SELECT @AssignmentCritiqueAvailable = NULL
			SELECT @CommentsCount = 0
		END
		CLOSE cur_PanelInfo
		DEALLOCATE cur_PanelInfo
		
		Select PanelId, ActiveApplication, ApplicationId, [Order], LastName, FirstName, PIInstitution, ApplicationTitle, Disapproved, Triaged, AwardShortDescription, ReviewTypeId, PossibleScores, ActualScores, AverageOE, CommentsCount, COIs, AssignmentSlots, AssignmentTypes, AssignmentNames, AssignmentPartIds, AssignmentCritiqueAvailable, PanelApplicationId, ReviewStatusId, ApplicationReviewStatusId, ReviewStatusName, ApplicationIdentifier, AdminNotesCount, UserCoi, Adjectival
		FROM dbo.#Tmp_PanelDetails
		
		DROP TABLE dbo.#Tmp_PanelDetails

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspViewPanelDetails] TO [NetSqlAzMan_Users]
    AS [dbo];

