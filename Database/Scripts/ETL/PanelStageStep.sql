INSERT INTO .[dbo].[PanelStageStep]
           ([PanelStageId]
           ,[StepTypeId]
           ,[StepName]
           ,[StepOrder]
           ,[StartDate]
           ,[EndDate]
           ,[ReOpenDate]
           ,[ReCloseDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT      PanelStage.PanelStageId, CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 5 WHEN 2 THEN 6 ELSE 7 END, CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 'Preliminary' WHEN 2 THEN 'Revised' ELSE 'Online Discussion' END AS StepName, Phase_Order AS StepOrder, Phase_Start AS StartDate, Phase_End AS EndDate, Phase_ReOpen AS ReOpenDate, Phase_ReClose AS ReCloseDate, VUN.UserID AS ModifiedBy, MTG_Phase_Member.LAST_UPDATE_DATE AS ModifiedDate
FROM         [$(P2RMIS)].[dbo].MTG_Phase_Member INNER JOIN
                      [$(P2RMIS)].[dbo].PAN_Master ON MTG_Phase_Member.Session_ID = PAN_Master.Session_ID INNER JOIN
                     ViewSessionPanel SessionPanel ON Pan_Master.Panel_ID = SessionPanel.LegacyPanelId INNER JOIN
                     ViewPanelStage PanelStage ON SessionPanel.SessionPanelId = PanelStage.SessionPanelId AND PanelStage.ReviewStageId = 1 LEFT OUTER JOIN
                     ViewLegacyUserNameToUserId VUN ON MTG_Phase_Member.LAST_UPDATED_BY = VUN.UserName 
WHERE NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = CASE MTG_Phase_Member.Phase_ID WHEN 1 THEN 5 WHEN 2 THEN 6 ELSE 7 END)
UNION ALL
SELECT		PanelStage.PanelStageId, 8, 'Meeting', 1, SessionPanel.StartDate, SessionPanel.EndDate, NULL, NULL, SessionPanel.ModifiedBy, SessionPanel.ModifiedDate
FROM		ViewPanelStage PanelStage INNER JOIN
			ViewSessionPanel SessionPanel ON PanelStage.SessionPanelId = SessionPanel.SessionPanelId
WHERE PanelStage.ReviewStageId = 2 AND NOT EXISTS (Select 'X' FROM ViewPanelStageStep WHERE PanelStageId = PanelStage.PanelStageId AND StepTypeId = 8)

