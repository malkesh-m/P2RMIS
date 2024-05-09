UPDATE UserAddress
SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE AddressTypeId IS NULL