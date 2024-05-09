-- =============================================
-- Author: Pushpa Unnithan
-- Create date: 5/18/2016
-- Description: Storeprocedure to Create Client Report Final Scores
-- ===========================================
CREATE PROCEDURE [dbo].[uspReportFinalScores] 
-- Add the parameters for the stored procedure here
@ProgramList varchar(4000),
@FiscalYearList varchar(4000),
@CycleList varchar(4000),	
@PanelList varchar(4000)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
DECLARE
@NumberTable TABLE (num int)
DECLARE
@MaxCnt int,
@Cnt int = 1;
WITH ProgramParams(ClientProgramId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
CycleParams(Cycle) 
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@CycleList)),
PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT TOP(1) @MaxCnt =  COUNT(ViewMechanismTemplateElement.MechanismTemplateElementId)
FROM ClientProgram INNER JOIN
                      ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
					  ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId INNER JOIN
                      ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
                      ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
                      ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
					  FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
					  CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
					  PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
					  ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
                      ViewMechanismTemplate ON ViewProgramMechanism.ProgramMechanismId = ViewMechanismTemplate.ProgramMechanismId INNER JOIN
                      ViewMechanismTemplateElement ON ViewMechanismTemplate.MechanismTemplateId = ViewMechanismTemplateElement.MechanismTemplateId
WHERE ViewMechanismTemplate.ReviewStageId = 1 AND ViewMechanismTemplateElement.ScoreFlag = 1
GROUP BY ViewPanelApplication.PanelApplicationId, ViewMechanismTemplate.MechanismTemplateId
ORDER BY COUNT(ViewMechanismTemplateElement.MechanismTemplateElementId) DESC;				  

WHILE (@Cnt <= @MaxCnt)
BEGIN
	INSERT INTO @NumberTable (num) VALUES (@Cnt);
	SET @Cnt = @Cnt + 1;
END;

-- Insert statements for procedure here
WITH ProgramParams(ClientProgramId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
FiscalYearParams(FY)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
CycleParams(Cycle) 
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList)),
PanelParams(PanelId)
AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
SELECT     Client.ClientAbrv, ViewProgramYear.Year, ClientProgram.ProgramDescription, ClientProgram.ProgramAbbreviation, ViewProgramMechanism.ReceiptCycle, 
                      ViewSessionPanel.PanelAbbreviation, ViewApplication.LogNumber, dbo.ApplicationTopicCodes.TopicCode,ViewClientAwardType.AwardAbbreviation, ViewClientAwardType.AwardDescription, 
                      ViewApplicationPersonnel.LastName + ', ' +SUBSTRING(ViewApplicationPersonnel.FirstName, 1, 1)+ '. ' AS Name, ViewApplicationPersonnel.LastName,ViewApplicationPersonnel.FirstName,
                      ViewApplicationPersonnel.OrganizationName, ViewApplication.ApplicationTitle, ViewApplicationBudget.TotalFunding, ViewMechanismTemplateElement.SortOrder, ViewClientElement.ElementAbbreviation, 
                      ViewClientElement.ElementDescription, ViewMechanismTemplateElement.ScoreFlag AS Flag, ViewMechanismTemplateElement.OverallFlag, 
                      ViewApplication.ApplicationId, ViewApplicationReviewStatus.ReviewStatusId, ViewMechanismTemplateElementScoring.StepTypeId, 
                      ViewMechanismTemplateElementScoring.ClientScoringId, ClientScoringScale.ScoreType,
                      ROW_NUMBER() OVER (Partition By ViewApplication.LogNumber  Order By ISNULL(ViewMechanismTemplateElement.OverallFlag, 0), ISNULL(ViewMechanismTemplateElement.DenseRank, 99)) as DenseRank,
                      Cast((ViewApplicationRevStdev.AvgScore) as decimal (18,1)) as avg, Cast((ViewApplicationRevStdev.StDev)as decimal (18,1)) As Stdev,
					  Cast((ViewApplicationRevStdev1.AvgScore) as decimal (18,1)) as avgwoCRs, Cast((ViewApplicationRevStdev1.StDev)as decimal (18,1)) As StdevwoCRs
FROM         ClientProgram INNER JOIN
                      ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
                      ViewProgramPanel ON ViewProgramYear.ProgramYearId = ViewProgramPanel.ProgramYearId INNER JOIN
                      ViewSessionPanel ON ViewProgramPanel.SessionPanelId = ViewSessionPanel.SessionPanelId INNER JOIN
                      ViewPanelApplication ON ViewSessionPanel.SessionPanelId = ViewPanelApplication.SessionPanelId 					  
					  INNER JOIN
                      ViewApplication ON ViewPanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
                      Client ON ClientProgram.ClientId = Client.ClientID INNER JOIN
                      ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId INNER JOIN
					  ProgramParams ON ClientProgram.ClientProgramId = ProgramParams.ClientProgramId INNER JOIN
						FiscalYearParams ON ViewProgramYear.Year = FiscalYearParams.FY INNER JOIN
						CycleParams ON CycleParams.Cycle =0 or ViewProgramMechanism.ReceiptCycle = CycleParams.Cycle INNER JOIN
						PanelParams ON PanelParams.PanelId = 0 OR ViewSessionPanel.SessionPanelId = PanelParams.PanelId INNER JOIN
                      ViewClientAwardType ON ViewProgramMechanism.ClientAwardTypeId = ViewClientAwardType.ClientAwardTypeId INNER JOIN
                      ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId LEFT OUTER JOIN
					  dbo.ApplicationTopicCodes ON ViewApplication.ApplicationId=dbo.ApplicationTopicCodes.ApplicationID INNER JOIN
                      ViewMechanismTemplate ON ViewProgramMechanism.ProgramMechanismId = ViewMechanismTemplate.ProgramMechanismId CROSS JOIN
					  --We cross join the Numbers table which includes the total count of scored criteria and outer join off this to ensure placeholders exist for report matrix
					  @NumberTable NumbersTable LEFT OUTER JOIN
                      (Select SortOrder, OverallFlag, ScoreFlag, ClientElementId, MechanismTemplateId, MechanismTemplateElementId, ROW_NUMBER() OVER (Partition By MechanismTemplateId Order By SortOrder) AS DenseRank FROM ViewMechanismTemplateElement TemplateElements WHERE ScoreFlag = 1) ViewMechanismTemplateElement ON ViewMechanismTemplate.MechanismTemplateId = ViewMechanismTemplateElement.MechanismTemplateId AND NumbersTable.num = ViewMechanismTemplateElement.DenseRank LEFT OUTER JOIN
                      ViewClientElement ON Client.ClientID = ViewClientElement.ClientId AND 
                      ViewMechanismTemplateElement.ClientElementId = ViewClientElement.ClientElementId INNER JOIN
                      ClientApplicationPersonnelType ON ViewClientAwardType.ClientId = ClientApplicationPersonnelType.ClientId AND 
                      ViewApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId INNER JOIN
                      ViewApplicationReviewStatus ON ViewPanelApplication.PanelApplicationId = ViewApplicationReviewStatus.PanelApplicationId AND 
                      dbo.ViewPanelApplication.PanelApplicationId = dbo.ViewApplicationReviewStatus.PanelApplicationId INNER JOIN
                      ReviewStatus ON ViewApplicationReviewStatus.ReviewStatusId = ReviewStatus.ReviewStatusId LEFT OUTER JOIN
                      ViewMechanismTemplateElementScoring ON ViewMechanismTemplateElement.MechanismTemplateElementId = ViewMechanismTemplateElementScoring.MechanismTemplateElementId AND (ViewMechanismTemplateElementScoring.StepTypeId = 7 OR
                      ViewMechanismTemplateElementScoring.StepTypeId = 8) LEFT OUTER JOIN
                      ClientScoringScale ON ViewMechanismTemplateElementScoring.ClientScoringId = ClientScoringScale.ClientScoringId  LEFT OUTER JOIN
                      dbo.ViewApplicationBudget ON dbo.ViewApplication.ApplicationId = dbo.ViewApplicationBudget.ApplicationId               
                       OUTER APPLY
						udfLastUpdatedCritiquePhaseAverage(ViewPanelApplication.PanelApplicationId, ViewClientElement.ClientElementId) AS ViewApplicationRevStdev
						OUTER APPLY
						udfLastUpdatedCritiquePhaseAverageWoCRs(ViewPanelApplication.PanelApplicationId, ViewClientElement.ClientElementId) AS ViewApplicationRevStdev1
WHERE     (ViewApplicationPersonnel.PrimaryFlag = 1) AND (ViewMechanismTemplate.ReviewStageId = 1) AND (NOT (ViewApplicationReviewStatus.ReviewStatusId IN (3, 4)))    		     
END
 GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportFinalScores] TO [NetSqlAzMan_Users]
    AS [dbo];                   
                    
                   