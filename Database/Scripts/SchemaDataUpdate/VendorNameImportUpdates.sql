UPDATE UserInfo SET VendorName = PPL_Addresses.Vendor_Name
FROM UserInfo INNER JOIN
[User] ON UserInfo.UserID = [User].UserID INNER JOIN
[$(P2RMIS)].[dbo].[PPL_Addresses] ON [User].PersonID = PPL_Addresses.Person_ID
WHERE PPL_Addresses.Address_Type = 'W9 Address'