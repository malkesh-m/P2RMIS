UPDATE UserInfo
SET VendorId = gi.Vendor_ID
FROM UserInfo
	INNER JOIN [User] ON UserInfo.UserID = [User].UserID
	INNER JOIN [$(P2RMIS)].dbo.PPL_General_Info gi ON [User].PersonID = gi.Person_ID