INSERT INTO dbo.Carrier (Name, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT Carrier, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM [$(P2RMIS)].dbo.MTG_Carriers_LU