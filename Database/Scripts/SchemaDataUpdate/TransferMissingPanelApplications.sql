--First part is re-associating some assignments to the correct application ID (likely log number changed)
UPDATE PanelApplication
Set ApplicationId = Good.ApplicationId
FROM PanelApplication INNER JOIN
ViewApplication ON PanelApplication.ApplicationId = ViewApplication.ApplicationId INNER JOIN
ViewApplication Good ON REPLACE(REPLACE(ViewApplication.LogNumber, '-A', 'R'), 'A', 'R') = Good.LogNumber
WHERE ViewApplication.LogNumber LIKE 'BC15%A';
--Transfer missing
INSERT INTO [dbo].[PanelApplication]
( 
      [SessionPanelId]
      ,[ApplicationId]
      ,[ReviewOrder]
      ,[ModifiedBy]
      ,[ModifiedDate]
 )
SELECT sp.[sessionPanelId] SessionPanelID
      ,app.ApplicationId
      ,pp.[Order_of_Review] ReviewOrder
      ,v.[userid] ModifiedBy
      ,pp.[LAST_UPDATE_Date] ModifiedDate
FROM [$(P2RMIS)].[dbo].[PRG_Panel_Proposals] pp
  inner join [dbo].[ViewApplication] app on pp.Log_No = app.LogNumber
  inner join [dbo].[ViewSessionPanel] sp on pp.[panel_id] = sp.[LegacyPanelId]
  left join [dbo].[ViewLegacyUserNameToUserId] v on pp.[LAST_UPDATED_BY] = v.[username]
WHERE NOT EXISTS (Select 'X' FROM ViewPanelApplication WHERE ApplicationId = app.ApplicationId)
order by pp.[panel_id];