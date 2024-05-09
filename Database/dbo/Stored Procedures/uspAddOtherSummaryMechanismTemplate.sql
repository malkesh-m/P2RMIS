-- =============================================
-- Author:		Joe Gao
-- Create date: 3/24/2015
-- Description:	This stored procedure creates summary mechanism templates and their elements for other workflows
-- =============================================
CREATE PROCEDURE [dbo].[uspAddOtherSummaryMechanismTemplate] 
	@ProgramMechanismId int,
	@ReviewStatusId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE
		@SummaryStageId int = 3,
		@CreatedBy int = 10,
		@TriagedStatusId int = 1,
		@IgnoredClientElementId int = 369,
		@SourceStageId int,	
		@SourceMechanismTemplateId int,	
		@SummaryDocumentId int = 2,
		@TargetMechanismTemplateElementId int,
		@TargetMechanismTemplateId int,
		@ClientIdToIncludeUnassignedNotesAndSummary int = 9,
		@UnassignedCommentElementTypeId int = 4,
		@MaxSortOrder INT,
		@OverviewElementTypeId INT = 2

	-- Get Previous Stage record
	SELECT TOP 1 @SourceStageId = ReviewStageId, @SourceMechanismTemplateId = MechanismTemplateId 
			FROM MechanismTemplate WHERE ProgramMechanismId = @ProgramMechanismId AND
			ReviewStageId = 1 ORDER BY ReviewStageId DESC
	
	-- Get and copy records if the previous stage is available
	IF @SourceStageId IS NOT NULL
		BEGIN
			-- Configure transaction settings
			SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
			SET XACT_ABORT ON;
			BEGIN TRANSACTION;	
			
			-- Create MechanismTemplate		
			INSERT INTO MechanismTemplate 
				(ProgramMechanismId, ReviewStatusId, ReviewStageId, SummaryDocumentId, CreatedBy, CreatedDate)
				SELECT ProgramMechanismId, @ReviewStatusId, @SummaryStageId, @SummaryDocumentId, @CreatedBy, dbo.GetP2rmisDateTime()
				FROM ViewMechanismTemplate WHERE ProgramMechanismId = @ProgramMechanismId AND
				ReviewStageId = @SourceStageId AND DeletedFlag = 0;
			SELECT @TargetMechanismTemplateId = SCOPE_IDENTITY();

			-- Create MechanismTemplateElement
			INSERT INTO MechanismTemplateElement
			(MechanismTemplateId, LegacyEcmId, ClientElementId, InstructionText, SortOrder, RecommendedWordCount,
			ScoreFlag, TextFlag, OverallFlag, CreatedBy, CreatedDate, SummarySortOrder)		
			SELECT @TargetMechanismTemplateId, LegacyEcmId, ClientElementId, InstructionText, SortOrder, RecommendedWordCount,
			ScoreFlag, TextFlag, OverallFlag, @CreatedBy, dbo.GetP2rmisDateTime(), SummarySortOrder
 			FROM ViewMechanismTemplateElement WHERE MechanismTemplateId = @SourceMechanismTemplateId AND
			ClientElementId <> @IgnoredClientElementId AND DeletedFlag = 0 ORDER BY SortOrder
			
			--Set up MechanismTemplateElementScoring
			--Current logic is Full review gets last stage last step, Triaged gets stage 1  last step
			INSERT INTO [MechanismTemplateElementScoring]
						([MechanismTemplateElementId]
						,[ClientScoringId]
						,[StepTypeId]
						,[CreatedBy]
						,[CreatedDate]
						,[ModifiedBy]
						,[ModifiedDate])
					SELECT MteSummary.MechanismTemplateElementId, MtesSource.ClientScoringId, StepType.StepTypeId, @CreatedBy, dbo.GetP2rmisDateTime(), @CreatedBy, dbo.GetP2rmisDateTime()
					FROM ViewMechanismTemplateElement MteSummary INNER JOIN
					ViewMechanismTemplate MtSummary ON MteSummary.MechanismTemplateId = MtSummary.MechanismTemplateId INNER JOIN
					ViewMechanismTemplate MtSource ON MtSummary.ProgramMechanismId = MtSource.ProgramMechanismId AND
						MtSource.ReviewStageId = @SourceStageId INNER JOIN
					ViewMechanismTemplateElement  MteSource ON MteSummary.ClientElementId = MteSource.ClientElementId AND MtSource.MechanismTemplateId = MteSource.MechanismTemplateId INNER JOIN
					ViewMechanismTemplateElementScoring MtesSource ON MteSource.MechanismTemplateElementId = MtesSource.MechanismTemplateElementId AND MtesSource.StepTypeId = (Select MAX(StepTypeId) FROM ViewMechanismTemplateElementScoring MtesSub WHERE MtesSub.MechanismTemplateElementId = MtesSource.MechanismTemplateElementId AND StepTypeId <> 7) CROSS JOIN --StepTypeId <> 7 is a hack until we move away from 1.0
					StepType
					WHERE StepType.StepTypeId IN (1,2,3) AND MtSummary.MechanismTemplateId = @TargetMechanismTemplateId

				-- Create Overview MechanismTemplateElement
				INSERT INTO [MechanismTemplateElement]
			   ([MechanismTemplateId]
			   ,[ClientElementId]
			   ,[SortOrder]
			   ,[ScoreFlag]
			   ,[TextFlag]
			   ,[OverallFlag]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
			   SELECT TOP(1)	@TargetMechanismTemplateId, ClientElement.ClientElementId, 0, 0, 1, 0, @CreatedBy, dbo.GetP2rmisDateTime(), @CreatedBy, dbo.GetP2rmisDateTime()
			   FROM ProgramMechanism INNER JOIN
			   ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
			   ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
			   ClientElement ON ClientProgram.ClientId = ClientElement.ClientId
			   WHERE ClientElement.ElementTypeId = @OverviewElementTypeId AND ProgramMechanism.ProgramMechanismId = @ProgramMechanismId

				--Get the max sort order
				SELECT @MaxSortOrder = MAX(SortOrder)
				FROM ViewMechanismTemplateElement MechanismTemplateElement INNER JOIN
				ViewMechanismTemplate MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplate.MechanismTemplateId
				WHERE MechanismTemplate.ProgramMechanismId = @ProgramMechanismId AND MechanismTemplate.ReviewStageId = @SummaryStageId

				--Add mechanism level element for unassigned comments 
				INSERT INTO [MechanismTemplateElement]
				   ([MechanismTemplateId]
				   ,[ClientElementId]
				   ,[SortOrder]
				   ,[ScoreFlag]
				   ,[TextFlag]
				   ,[OverallFlag]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   ,[ModifiedBy]
				   ,[ModifiedDate])
				   SELECT TOP(1)	@TargetMechanismTemplateId, ClientElement.ClientElementId, ISNULL(@MaxSortOrder, 0) + 1, 0, 1, 0, @CreatedBy, dbo.GetP2rmisDateTime(), @CreatedBy, dbo.GetP2rmisDateTime()
				   FROM ProgramMechanism INNER JOIN
				   ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
				   ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
				   ClientElement ON ClientProgram.ClientId = ClientElement.ClientId
				   WHERE ClientElement.ElementTypeId = @UnassignedCommentElementTypeId AND ProgramMechanism.ProgramMechanismId = @ProgramMechanismId
			
			-- Commit
			COMMIT TRANSACTION;		
		END
		
	RETURN @TargetMechanismTemplateId;
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspAddOtherSummaryMechanismTemplate] TO [NetSqlAzMan_Users]
    AS [dbo];