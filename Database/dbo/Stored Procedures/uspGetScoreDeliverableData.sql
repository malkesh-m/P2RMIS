CREATE PROCEDURE [dbo].[uspGetScoreDeliverableData]
	@ProgramYearId int,
	@ReceiptCycle int
AS
SELECT LogNumber, Min(Case OverallFlag WHEN 1 THEN AvgScore END) GlobalScore, Min(Case OverallFlag WHEN 1 THEN [StDev] END) StandDeviation,
	Min(Case WHEN LegacyRanking = 1 AND OverallFlag = 0 THEN AvgScore END) Eval1,
	Min(Case WHEN LegacyRanking = 2 AND OverallFlag = 0 THEN AvgScore END) Eval2,
	Min(Case WHEN LegacyRanking = 3 AND OverallFlag = 0 THEN AvgScore END) Eval3,
	Min(Case WHEN LegacyRanking = 4 AND OverallFlag = 0 THEN AvgScore END) Eval4,
	Min(Case WHEN LegacyRanking = 5 AND OverallFlag = 0 THEN AvgScore END) Eval5,
	Min(Case WHEN LegacyRanking = 6 AND OverallFlag = 0 THEN AvgScore END) Eval6,
	Min(Case WHEN LegacyRanking = 7 AND OverallFlag = 0 THEN AvgScore END) Eval7,
	Min(Case WHEN LegacyRanking = 8 AND OverallFlag = 0 THEN AvgScore END) Eval8,
	Min(Case WHEN LegacyRanking = 9 AND OverallFlag = 0 THEN AvgScore END) Eval9,
	Min(Case WHEN LegacyRanking = 10 AND OverallFlag = 0 THEN AvgScore END) Eval10, Disapproved, PanelAbbreviation, PanelName, Triaged
FROM
(
	SELECT App.LogNumber, CASE WHEN AppRevStatus.ReviewStatusId = 5 THEN 1 ELSE 0 END as Disapproved, Panel.PanelAbbreviation, Panel.PanelName, 
	CASE WHEN AppRevStatus.ReviewStatusId = 1 THEN 1 ELSE 0 END Triaged, CASE WHEN AppRevStatus.ReviewStatusId = 1 THEN NULL ELSE Round(ScoreTable.AvgScore,1) END AvgScore, CASE WHEN AppRevStatus.ReviewStatusId = 1 THEN NULL ELSE Round(Round(ScoreTable.[StDev], 2), 1) END [StDev], 
						  DENSE_RANK() OVER (Partition By MechTemplate.MechanismTemplateId Order By MechTemplateElement.OverallFlag, MechTemplateElement.SortOrder) AS LegacyRanking,
						  MechTemplateElement.OverallFlag, AppRevStatus.ReviewStatusId
	FROM				dbo.[ViewApplication] [App] 
	INNER JOIN dbo.[ViewPanelApplication] [PanApp] ON App.ApplicationId = PanApp.ApplicationId
	INNER JOIN  dbo.[ViewSessionPanel] [Panel] ON PanApp.SessionPanelId = Panel.SessionPanelId
	INNER JOIN dbo.[ViewProgramMechanism] [ProgMech] ON App.ProgramMechanismId = ProgMech.ProgramMechanismId
	INNER JOIN dbo.[ViewApplicationReviewStatus] [AppRevStatus] ON [PanApp].PanelApplicationId = [AppRevStatus].PanelApplicationId
	INNER JOIN dbo.[ReviewStatus] [RevStatus] ON [AppRevStatus].ReviewStatusId = [RevStatus].ReviewStatusId
	LEFT OUTER JOIN dbo.[ViewMechanismTemplate] [MechTemplate] ON ProgMech.ProgramMechanismId = MechTemplate.ProgramMechanismId AND MechTemplate.ReviewStageId = 1
	LEFT OUTER JOIN dbo.[ViewMechanismTemplateElement] MechTemplateElement ON MechTemplate.MechanismTemplateId = MechTemplateElement.MechanismTemplateId
	OUTER APPLY dbo.[udfLastUpdatedCritiquePhaseAverage](PanApp.PanelApplicationId, MechTemplateElement.ClientElementId) ScoreTable
	WHERE				ProgMech.ProgramYearId = @ProgramYearId  AND ProgMech.ReceiptCycle = @ReceiptCycle AND MechTemplateElement.ScoreFlag = 1
						AND [RevStatus].ReviewStatusTypeId = 1 AND App.ParentApplicationId IS NULL
) MainQuery
GROUP BY LogNumber, Disapproved, PanelAbbreviation, PanelName, Triaged
ORDER BY			LogNumber
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[uspGetScoreDeliverableData] TO [NetSqlAzMan_Users]
    AS [dbo];

