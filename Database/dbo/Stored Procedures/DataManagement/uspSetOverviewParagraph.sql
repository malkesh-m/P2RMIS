/*
This stored procedure can be used to set up boilerplate overview paragraphs for summary statements
It can be run at any time after applications have been assigned to the panel
Text should be HTML encoded
Only inserts if it doesn't already exist
*/

CREATE PROCEDURE [dbo].[uspSetOverviewParagraph]
	@ProgramMechanismId int,
	@BoilerplateHtmlText varchar(4000)
AS
BEGIN
INSERT INTO PanelApplicationSummary
([PanelApplicationId]
           ,[SummaryText]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT ViewPanelApplication.PanelApplicationId, @BoilerplateHtmlText, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM ViewProgramMechanism
	INNER JOIN [ViewApplication] ON ViewProgramMechanism.ProgramMechanismId = [ViewApplication].ProgramMechanismId
	INNER JOIN [ViewPanelApplication] ON ViewApplication.ApplicationId = ViewPanelApplication.ApplicationId
	LEFT OUTER JOIN [ViewPanelApplicationSummary] ON ViewPanelApplication.PanelApplicationId = ViewPanelApplicationSummary.PanelApplicationId
WHERE ViewPanelApplicationSummary.PanelApplicationSummaryId IS NULL AND ViewProgramMechanism.ProgramMechanismId = @ProgramMechanismId
END
