--Insert an asynch stage for all panels in legacy P2RMIS
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT     SessionPanelId, 1 AS Expr1, 1 AS Expr2, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
FROM         ViewSessionPanel
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 1)

--Insert a synch stage for all panels in legacy P2RMIS with at least one score
INSERT INTO PanelStage
                      (SessionPanelId, ReviewStageId, StageOrder, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT     SessionPanelId, 2 AS Expr1, 2 AS Expr2, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
FROM         ViewSessionPanel 
Where SessionPanelId NOT IN
(Select SessionPanelId FROM PanelStage Where ReviewStageId = 2)


