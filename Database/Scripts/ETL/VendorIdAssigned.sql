INSERT INTO dbo.VendorIdAssigned (VendorId, AssignedFlag, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT VENDOR_ID, 0, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM [$(P2RMIS)].dbo.PRG_Vendors