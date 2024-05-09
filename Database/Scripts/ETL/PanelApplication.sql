
INSERT INTO [dbo].[PanelApplication]
( --[PanelApplicationId]
      [SessionPanelId]
      ,[ApplicationId]
      ,[ReviewOrder]
     -- ,[CreatedBy]
      --,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
SELECT sp.[sessionPanelId] SessionPanelID
	  --,pp.[panel_id]
      ,app.ApplicationId
      ,pp.[Order_of_Review] ReviewOrder
      ,v.[userid] ModifiedBy
      ,pp.[LAST_UPDATE_Date] ModifiedDate
FROM [$(P2RMIS)].[dbo].[PRG_Panel_Proposals] pp
  inner join [dbo].[ViewApplication] app on pp.Log_No = app.LogNumber
  inner join [dbo].[ViewSessionPanel] sp on pp.[panel_id] = sp.[LegacyPanelId]
  left join [dbo].[ViewLegacyUserNameToUserId] v on pp.[LAST_UPDATED_BY] = v.[username]
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplication WHERE SessionPanelId = sp.SessionPanelId AND ApplicationId = app.ApplicationId)
order by pp.[panel_id]