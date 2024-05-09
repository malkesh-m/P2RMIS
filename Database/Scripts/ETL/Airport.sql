INSERT INTO dbo.Airport (Code, [Description], CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT Airport_Code, Airport_Description, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM [$(P2RMIS)].dbo.MTG_Airports_LU