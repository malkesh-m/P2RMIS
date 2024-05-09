CREATE PROCEDURE [dbo].[uspAddOverviewBoilerplateByMechanism]
	@ProgramMechanismId int,
	@BoilerplateText varchar(8000)
AS
/*
	This stored procedure adds the supplied boilerplate text for all applications in the specified 
	mechanism which do not have overview text
*/	
	DECLARE @DevUserId int = 10,
	@CurrentDateTime datetime2(0) = dbo.GetP2rmisDateTime();
	INSERT INTO [dbo].[PanelApplicationSummary]
           ([PanelApplicationId]
           ,[SummaryText]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT ViewPanelApplication.PanelApplicationId, @BoilerplateText, @DevUserId, @CurrentDateTime, @DevUserId, @CurrentDateTime
	FROM ViewPanelApplication INNER JOIN
	ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId
	WHERE ViewApplication.ProgramMechanismId = @ProgramMechanismId 
		AND NOT EXISTS (SELECT 'X' FROM PanelApplicationSummary WHERE DeletedFlag = 0 AND ViewPanelApplication.PanelApplicationId = PanelApplicationSummary.PanelApplicationId);
