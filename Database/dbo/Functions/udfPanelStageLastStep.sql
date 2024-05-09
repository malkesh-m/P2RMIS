CREATE FUNCTION [dbo].[udfPanelStageLastStep]
(
	@PanelStageId int
)
RETURNS TABLE
AS
RETURN
SELECT PanelStageStepId, PanelStageId, StepTypeId, StepName, StepOrder, StartDate, EndDate, ReOpenDate, ReCloseDate
FROM
(
SELECT     PanelStageStepId, PanelStageId, StepTypeId, StepName, StepOrder, StartDate, EndDate, ReOpenDate, ReCloseDate, DENSE_RANK() OVER (PARTITION BY PanelStageId ORDER BY StepOrder DESC) AS Ranking
FROM         PanelStageStep
WHERE     (PanelStageId = @PanelStageId AND DeletedFlag = 0)
) Sub
Where Ranking = 1
